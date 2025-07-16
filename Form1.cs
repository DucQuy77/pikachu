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

        private int timeLeft = 600;
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

            panelBoard.BackColor = Color.Transparent;
            panelOverlay.BringToFront();
            panelOverlay.Paint += panelOverlay_Paint;
            panelOverlay.Invalidate();

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
            for (int i = 0; i < (rows * cols) / 2; i++)
            {
                int randomIndexes = i % pokemonImages.Length;
                pokemonIndexes.Add(randomIndexes);
                pokemonIndexes.Add(randomIndexes);
            }

            Shuffer(pokemonIndexes);

            int forcedIndex = pokemonIndexes[0];
            boardMap[1, 1] = forcedIndex;
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
                    if (!IsEmpty(new Point(x, a.Y)))
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
            if (p.X < 0 || p.X > rows + 1 || p.Y < 0 || p.Y > cols + 1)
                return false;
            if (p.X == 0 || p.X == rows + 1 || p.Y == 0 || p.Y == cols + 1)
                return true;
            return boardMap[p.X, p.Y] == -1;
        }

        private bool IsBoardCleared()
        {
            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= cols; j++)
                {
                    if (boardMap[i, j] != -1)
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
            for (int i = 0; i <= rows + 1; i++)
            {
                for (int j = 0; j <= cols + 1; j++)
                {
                    Point mid = new Point(i, j);
                    if (!IsEmpty(mid) || mid == a || mid == b) continue;

                    var path1 = CanConnectStraight(a, mid);
                    var path2 = CanConnectLShape(mid, b);

                    if (path1 != null && path2 != null)
                    {
                        List<Point> fullPath = new List<Point>(path1);
                        fullPath.AddRange(path2.Skip(1));
                        return fullPath;
                    }

                    var path3 = CanConnectLShape(a, mid);
                    var path4 = CanConnectLShape(mid, b);

                    if (path3 != null && path4 != null)
                    {
                        List<Point> fullPath = new List<Point>(path3);
                        fullPath.AddRange(path4.Skip(1));
                        return fullPath;
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

        private void ResetGame(bool isContinue = false)
        {
            //Xóa tất cả PictureBox
            panelBoard.Controls.Clear();

            //Reset lại
            boardMap = new int[rows + 2, cols + 2];
            board = new PictureBox[rows + 2, cols + 2];
            selected1 = null;
            selected2 = null;
            connectionPoints.Clear();

            if (!isContinue)
            {
                //Reset điểm
                score = 0;
                lblScore.Text = "Điểm: 0";

                //Reset timer
                timeLeft = 600;
            }
            else
            {
                timeLeft = 550;
            }

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
            timeLeft = 600;
            lblTime.Text = "Thời gian: " + timeLeft + "s";
            countdownTimer.Start();
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (IsBoardCleared())
            {
                countdownTimer.Stop();
                SaveOrUpdatePlayerScore(playerName, score);

                var result = MessageBox.Show("Bạn đã thắng cuộc!\nBạn có muốn tiếp tục chơi không?", "Chúc mừng bạn !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    ResetGame(true);
                }
                else
                {
                    FormBXH bxhForm = new FormBXH();
                    bxhForm.ShowDialog();
                    this.Close();
                }

                return;
            }

            timeLeft--;
            lblTime.Text = "Thời gian: " + timeLeft + "s";

            if (timeLeft <= 0)
            {
                DialogResult result = MessageBox.Show("Bạn đã thua cuộc! Hãy chơi lại! ", "Hết giờ", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    ResetGame();
                }
                else
                {
                    this.Close();
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

        private void panelBoard_Paint(object sender, PaintEventArgs e)
        {
            if (connectionPoints.Count > 1)
            {
                using (Pen redPen = new Pen(Color.Red, 2))
                {
                    for (int i = 0; i < connectionPoints.Count - 1; i++)
                    {
                        e.Graphics.DrawLine(redPen, connectionPoints[i], connectionPoints[i + 1]);
                    }
                }
            }
        }

        private void panelOverlay_Paint(object sender, PaintEventArgs e)
        {
            if (connectionPoints.Count > 1)
            {
                using (Pen redPen = new Pen(Color.Red, 2))
                {
                    for (int i = 0; i < connectionPoints.Count - 1; i++)
                    {
                        e.Graphics.DrawLine(redPen, connectionPoints[i], connectionPoints[i + 1]);
                    }
                }
            }
        }
    }
}
