using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Pikachu
{
    public partial class Form1 : Form
    {
        Connect db = new Connect();
        int rows = 10, cols = 18;
        int tileSize = 48;
        PictureBox[,] board;
        Image[] pokemonImages;
        int[,] boardMap;
        Random rand = new Random();

        PictureBox? selected1 = null;
        PictureBox? selected2 = null;
        private List<Point> connectionPoints;

        private int score = 0;
        private string playerName;

        private int timeLeft = 500;
        private System.Windows.Forms.Timer countdownTimer;

        public Form1(string tenNguoiChoi)
        {
            InitializeComponent();
            playerName = tenNguoiChoi;
            LoadPokemonImages();
            InitializeBoard();
            connectionPoints = new List<Point>();
            this.FormClosing += Form1_FormClosing;
            btnChoiLai.Click += btnChoiLai_Click;

            int highScore = GetHightScore(playerName);
            lblDiemCao.Text = "Điểm kỷ lục: " + highScore;

            countdownTimer = new System.Windows.Forms.Timer();
            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;
            StartCountDown();
        }

        private void LoadPokemonImages()
        {
            pokemonImages = new Image[]
            {
                Properties.Resources.pokemon1,
                Properties.Resources.pokemon2,
                Properties.Resources.pokemon3,
                Properties.Resources.pokemon4,
                Properties.Resources.pokemon5,
                Properties.Resources.pokemon6,
                Properties.Resources.pokemon7,
                Properties.Resources.pokemon8,
                Properties.Resources.pokemon9,
                Properties.Resources.pokemon10,
                Properties.Resources.pokemon11,
                Properties.Resources.pokemon12,
                Properties.Resources.pokemon13,
                Properties.Resources.pokemon14,
                Properties.Resources.pokemon15,
                Properties.Resources.pokemon16,
                Properties.Resources.pokemon17,
                Properties.Resources.pokemon18,
                Properties.Resources.pokemon19,
                Properties.Resources.pokemon20,
                Properties.Resources.pokemon21,
                Properties.Resources.pokemon22,
            };
        }

        private void InitializeBoard()
        {
            board = new PictureBox[rows + 2, cols + 2];
            boardMap = new int[rows + 2, cols + 2];

            List<int> pokemonIndexes = new List<int>();
            for (int i = 0; i < (rows * cols)/2; i++)
            {
                int randomIndexes = i % pokemonImages.Length;
                pokemonIndexes.Add(randomIndexes);
                pokemonIndexes.Add(randomIndexes);
            }

            Shuffer(pokemonIndexes);

            int forcedIndex = pokemonIndexes[0];
            boardMap[1,1] = forcedIndex;
            boardMap[1, cols] = forcedIndex;
            pokemonIndexes.Remove(forcedIndex);
            pokemonIndexes.Remove(forcedIndex);

            int k = 0;
            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= cols; j++)
                {
                    if ((i == 1 && (j == 1 || j == cols))) continue;

                    boardMap[i, j] = pokemonIndexes[k++];
                }
            }

            //pokemonIndexes = pokemonIndexes.OrderBy(x => rand.Next()).ToList();
            //int imageIndex = 0;

            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= cols; j++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Width = tileSize;
                    pb.Height = tileSize;
                    pb.Left = (j - 1) * tileSize;
                    pb.Top = (i - 1) * tileSize;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.BorderStyle = BorderStyle.FixedSingle;

                    //int pokemonIndex = pokemonIndexes[imageIndex++];
                    pb.Image = pokemonImages[boardMap[i, j]];
                    

                    pb.Click += PictureBox_Click;
                    panelBoard.Controls.Add(pb);
                    board[i, j] = pb;
                }
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox? clicked = sender as PictureBox;
            if (clicked == null || clicked.Image == null) return;

            if (selected1 == null)
            {
                selected1 = clicked;
                selected1.BorderStyle = BorderStyle.Fixed3D;
                return;
            }

            if (clicked == selected1) return;

            selected2 = clicked;
            selected2.BorderStyle = BorderStyle.Fixed3D;

            Point pos1 = GetPictureBoxPosition(selected1);
            Point pos2 = GetPictureBoxPosition(selected2);


            if (boardMap[pos1.X, pos1.Y] == boardMap[pos2.X, pos2.Y])
            {
                var path = CanConnectStraight(pos1, pos2) ?? CanConnectLShape(pos1, pos2) ?? CanConnect3Lines(pos1, pos2);
                if (path != null)
                {
                    connectionPoints = path.Select(p => new Point((p.Y - 1) * tileSize + tileSize / 2, (p.X - 1) * tileSize + tileSize / 2)).ToList();
                    panelBoard.Invalidate();

                    boardMap[pos1.X, pos1.Y] = -1;
                    boardMap[pos2.X, pos2.Y] = -1;

                    selected1.Image = null;
                    selected2.Image = null;

                    score += 10;
                    lblScore.Text = "Điểm: " + score;

                    var timer = new System.Windows.Forms.Timer { Interval = 300 };
                    timer.Tick += (s, ev) =>
                    {
                        connectionPoints.Clear();
                        panelBoard.Invalidate();
                        timer.Stop();

                        if (IsBoardCleared())
                        {
                            countdownTimer.Stop();
                            SaveOrUpdatePlayerScore(playerName, score);
                            MessageBox.Show("Chúc mừng! Bạn đã hoàn thành trò chơi!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    };
                    timer.Start();
                }
            }

            ResetSelections();
        }

        private List<Point>? CanConnectStraight(Point a, Point b)
        {

            if (a.X == b.X)
            {
                int minY = Math.Min(a.Y, b.Y);
                int maxY = Math.Max(a.Y, b.Y);
                for (int y = minY + 1; y < maxY; y++)
                {
                    if (boardMap[a.X, y] != -1)
                    {
                        return null;
                    }
                }
                return new List<Point> { a, b };
            }
            else if (a.Y == b.Y)
            {
                int minX = Math.Min(a.X, b.X);
                int maxX = Math.Max(a.X, b.X);
                for (int x = minX + 1; x < maxX; x++)
                {
                    if (boardMap[x, a.Y] != -1)
                    {
                        return null;
                    }
                }
                return new List<Point> { a, b };
            }
            return null;
        }

        private Point GetPictureBoxPosition(PictureBox pb)
        {
            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= cols; j++)
                {
                    if (board[i, j] == pb)
                    {
                        return new Point(i, j);
                    }
                }
            }
            return Point.Empty;
        }

        private void ResetSelections()
        {
            selected1.BorderStyle = BorderStyle.FixedSingle;
            selected2.BorderStyle = BorderStyle.FixedSingle;
            selected1 = null;
            selected2 = null;
        }

        private List<Point>? CanConnectLShape(Point a, Point b)
        {
            Point c1 = new Point(a.X, b.Y);
            if (IsEmpty(c1) && CanConnectStraight(a, c1) != null && CanConnectStraight(c1, b) != null)
            {
                return new List<Point> { a, c1, b };
            }

            Point c2 = new Point(b.X, a.Y);
            if (IsEmpty(c2) && CanConnectStraight(a, c2) != null && CanConnectStraight(c2, b) != null)
            {
                return new List<Point> { a, c2, b };
            }
            return null;
        }

        private bool IsEmpty(Point p)
        {
            return boardMap[p.X, p.Y] == -1;
        }

        private bool IsBoardCleared()
        {
            for (int i = 1; i <= rows; i++) {
                for (int j = 1; j <= cols; j++) {
                    if (boardMap[i,j] != -1)
                    {
                        return false;
                    }
                }
            } 
            return true;
        }
        
        private void Shuffer(List<int> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private List<Point>? CanConnect3Lines(Point a, Point b)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Point mid1 = new Point(i, j);
                    if (!IsEmpty(mid1) || mid1 == a || mid1 == b) continue;

                    for (int x = 0; x < rows; x++)
                    {
                        for (int y = 0; y < cols; y++)
                        {
                            Point mid2 = new Point(x, y);
                            if (!IsEmpty(mid2) || mid2 == a || mid2 == b || mid2 == mid1) continue;

                            // A → mid1 → mid2 → B
                            if (CanConnectStraight(a, mid1) != null &&
                                CanConnectStraight(mid1, mid2) != null &&
                                CanConnectStraight(mid2, b) != null)
                            {
                                return new List<Point> { a, mid1, mid2, b };
                            }
                        }
                    }
                }
            }
            return null;
        }

        private void SaveOrUpdatePlayerScore(string playerName, int score)
        {
            using (SqlConnection conn = Connect.GetConnection())
            {
                conn.Open();

                // Kiểm tra xem người chơi đã có điểm chưa
                string checkQuery = "SELECT Score FROM Information WHERE PlayerName = @PlayerName";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@PlayerName", playerName);
                    object result = checkCmd.ExecuteScalar();

                    if (result == null)
                    {
                        // Nếu chưa có người chơi, thêm mới
                        string insertQuery = "INSERT INTO Information (PlayerName, Score) VALUES (@PlayerName, @Score)";
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@PlayerName", playerName);
                            insertCmd.Parameters.AddWithValue("@Score", score);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        int oldScore = Convert.ToInt32(result);

                        if (score > oldScore)
                        {
                            // Nếu điểm mới cao hơn, cập nhật
                            string updateQuery = "UPDATE Information SET Score = @Score WHERE PlayerName = @PlayerName";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@PlayerName", playerName);
                                updateCmd.Parameters.AddWithValue("@Score", score);
                                updateCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        private int GetHightScore(string playerName)
        {
            using (SqlConnection conn = Connect.GetConnection())
            {
                conn.Open();
                string query = "SELECT Score FROM Information WHERE PlayerName = @PlayerName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlayerName", playerName);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        private void ResetGame()
        {
            //Xóa tất cả PictureBox
            panelBoard.Controls.Clear();

            //Reset lại
            boardMap = new int[rows + 2, cols + 2];
            board = new PictureBox[rows + 2, cols + 2];
            selected1 = null;
            selected2 = null;
            connectionPoints.Clear();

            //Reset điểm
            score = 0;
            lblScore.Text = "Điểm: 0";

            //Reset timer
            timeLeft = 500;
            lblTime.Text = "Thời gian: " + timeLeft + "s";

            if (countdownTimer != null)
            {
                countdownTimer.Stop();
                countdownTimer.Start();
            }

            //Khởi tạo lại board
            InitializeBoard();
        }

        private void StartCountDown()
        {
            timeLeft = 500;
            lblTime.Text = "Thời gian: " + timeLeft + "s";
            countdownTimer.Start();
        }
        
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            lblTime.Text = "Thời gian: " + timeLeft + "s";

            if (timeLeft <= 0)
            {
                countdownTimer.Stop();
                if (IsBoardCleared())
                {
                    MessageBox.Show("Bạn đã thắng cuộc!", "Kết thúc");
                }
                else
                {
                    DialogResult result = MessageBox.Show("Bạn đã thua cuộc! Hãy chơi lại! ", "Hết giờ", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes) { 
                        ResetGame();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveOrUpdatePlayerScore(playerName, score);
        }

        private void btnBangXH_Click(object sender, EventArgs e)
        {
            FormBXH formBXH = new FormBXH();
            formBXH.ShowDialog();
        }

        private void btnChoiLai_Click(object sender, EventArgs e)
        {
            ResetGame();
        }
    }
}
