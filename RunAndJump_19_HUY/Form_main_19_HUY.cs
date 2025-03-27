using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace RunAndJump_19_HUY
{
    public partial class FormMain_19_HUY : Form
    {
        System.Windows.Forms.Timer flyBotTimer_19_HUY;
        int cntBotDie = 2;
        //check move player
        bool moveJump_19_HUY,
            moveLeft_19_HUY,
            moveRight_19_HUY,
            checkAnmPlayer_19_HUY,
            checktail_19_HUY,
            checkhead_19_HUY,
            checkBullet_19_HUY,
            checkThGate_19_HUY, checkAnmPlayer_19_HUY2;

        int bulletDelay_19_HUY = 200;
        int playerBlood_19_HUY, botBlood_19_HUY, birdBlood_19_HUY;
        int force_19_HUY,
            speed_19_HUY,
            jumSpeed_19_HUY,
            backroundSpeed_19_HUY,
            forceBullet_19_HUY,
            cntHeart_19_HUY,
            cntBotUnder_19_HUY;

         bool isReloading_19_HUY = false;
        int reloadTime_19_HUY = 0;
         int maxBullets_19_HUY = 10;
        List<Point> checkIndexBot_19_HUY = new List<Point>();
        DateTime lastBulletTime_19_HUY = DateTime.MinValue;
        List<PictureBox> heartPlayer_19_HUY = new List<PictureBox>();
        Random random_19_HUY = new Random();
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (
                int nLeft,
                int nTop,
                int nRight,
                int nBottom,
                int nWidthEllipse,
                int nHeightEllipse
            );
        public FormMain_19_HUY()
        {
            InitializeComponent();
            ResetGame_19_HUY();
        }
        private void ResetGame_19_HUY()
        {
            checkAnmPlayer_19_HUY2 = moveJump_19_HUY = moveLeft_19_HUY = 
                moveRight_19_HUY = checkAnmPlayer_19_HUY = checktail_19_HUY = 
                checkhead_19_HUY = checkThGate_19_HUY= checkBullet_19_HUY = false;
            speed_19_HUY = 4;
            force_19_HUY = 12;
            playerBlood_19_HUY = 4;
            botBlood_19_HUY = 3;
            birdBlood_19_HUY = 2;
            backroundSpeed_19_HUY = 5;
            jumSpeed_19_HUY = 0;
            forceBullet_19_HUY = 5;
            cntBotUnder_19_HUY = 5;
            CreateHeartABullet();
            for (int i = 0; i < 5; i++)
                RandomObject(RandomBot(), 60, 60, "bot_19_HUY",0);//random bot

            for(int i = 0; i < 6; i++)
            {
                RandomObject("heartPlayer_1_19_HUY", 20, 20, "heart_19_HUY", -30);
                RandomObject("Fireball_19_HUY", 20, 20, "fillBullet_19_HUY", -90);
            }
            MakeBotFly();
            
            GamerTime_19_HUY.Start();
        }
        private void CreateHeartABullet()
        {
            cntHeart_19_HUY = 4;
            lblHeart_19_HUY.Text = "X " + (cntHeart_19_HUY).ToString();
            lblBullet_19_HUY.Text = "X " + (forceBullet_19_HUY).ToString();


            for (int i = cntHeart_19_HUY+1; i >0; i--)
            {
                PictureBox heart = new PictureBox();
                string resourceName = $"heartPlayer_{i}_19_HUY";
                object img = Properties.Resources.ResourceManager.GetObject(resourceName);
                heart.Image = (Image)img;
                heartPlayer_19_HUY.Add(heart);
            }
        }
        private void TimeGameEvent_19_HUY(object sender, EventArgs e)
        {
            PlayerCollision_19_HUY();
            MovePlayer_19_HUY();
            MoveBullet_19_HUY();
            IncreaseView_19_HUY();
            MoveBackground_19_HUY();
            PlayerDie_19_HUY();
            BotCollision_19_HUY();
            FillBullet();
            FillHeart();
            MoveBot_19_HUY();
            MoveBird();
        }
        private void InitializeFlyBotTimer_19_HUY()
        {
            flyBotTimer_19_HUY = new System.Windows.Forms.Timer();
            flyBotTimer_19_HUY.Interval = 5000; // 110 giây = 110000 ms
            flyBotTimer_19_HUY.Tick += (sender, e) => MakeBotFly();
            flyBotTimer_19_HUY.Start();
        }

        private string RandomBot()
        {
            List<string> listBot = new List<string>
                {
                    "bot1_19_HUY",
                    "bot2_19_HUY",
                    "bot3_19_HUY"
                };
            return listBot[random_19_HUY.Next(0, listBot.Count)];
        }
        private void RandomLocation(PictureBox pictureBox,int height_19_HUY, int width_19_HUY)
        {
            
                // Danh sách các tọa độ cố định
                List<Point> positions = new List<Point>
            {
                new Point(473-width_19_HUY, 259-height_19_HUY),
                new Point(771-width_19_HUY, 355-height_19_HUY),
                new Point(1507, 308-height_19_HUY),
                new Point(1180- width_19_HUY, 77-height_19_HUY),
                new Point(1818 - width_19_HUY, 247-height_19_HUY),
                new Point(2077, 424-height_19_HUY),
                new Point(2359 - width_19_HUY, 283-height_19_HUY),
                new Point(2566 - width_19_HUY, 104-height_19_HUY)
            };

                // Chọn ngẫu nhiên một vị trí từ danh sách
                // Gán vị trí mới cho PictureBox
                int randomIndex;
                do
                {
                    randomIndex = random_19_HUY.Next(positions.Count);
                }
                while (checkIndexBot_19_HUY.Contains(positions[randomIndex]));
                pictureBox.Location = positions[randomIndex];
                checkIndexBot_19_HUY.Add(positions[randomIndex]);
        }
        private void MakeBotFly()
        {
            PictureBox bird = new PictureBox
            {
                Image = Properties.Resources.Fly_19_HUY,
                Width = 100,
                Height = 40,
                Tag = "bird_19_HUY",
                BackColor = Color.FromArgb(192, 255, 255),

                SizeMode = PictureBoxSizeMode.StretchImage,
                Left = picPlayer_19_HUY.Left + 1000,
                Top = random_19_HUY.Next(10, this.ClientSize.Height-picGround_U1_19_HUY.Height*2)
            };

            this.Controls.Add(bird);
            bird.BringToFront();
        }
        private void MoveBird()
        {
            foreach(Control control in this.Controls)
            {
                if (control is PictureBox && (string)control.Tag == "bird_19_HUY")
                {
                    control.Left -= 5;
                    if(control.Left < 0)
                    {
                        RemoveControl_19_HUY(control);
                        MakeBotFly();
                    }
                }
            }
        }
        //heart_19_HUY
        private void RandomObject(string imageName, int width,int height,string tag,int left)
        {
            PictureBox Bot1 = new PictureBox();
            Bot1.Image = (Image)Properties.Resources.ResourceManager.GetObject(imageName);
            Bot1.Width = width;
            Bot1.Height = height;
            Bot1.Tag = tag;
            Bot1.SizeMode = PictureBoxSizeMode.StretchImage;
            // Thêm bot vào controls trước
            this.Controls.Add(Bot1);
            // Bây giờ gọi RandomLocation để đặt vị trí ngẫu nhiên
            RandomLocation(Bot1, Bot1.Height,left);
            Bot1.BringToFront();
        }
        private void MoveBackground_19_HUY()
        {
                if (moveLeft_19_HUY && picBrg_1_19_HU.Left < 0)
                {
                    picBrg_1_19_HU.Left += backroundSpeed_19_HUY;
                    MoveGameElevents_19_HUY("back_19_HUY", backroundSpeed_19_HUY);
                }
                if (moveRight_19_HUY && picBrg_1_19_HU.Left > -(picBrg_1_19_HU.Width - this.ClientSize.Width))
                {
                    picBrg_1_19_HU.Left -= backroundSpeed_19_HUY;
                    MoveGameElevents_19_HUY("forward_19_HUY", backroundSpeed_19_HUY);
                }
        }
        private void MovePlayer_19_HUY()
        {
            picPlayer_19_HUY.Top += jumSpeed_19_HUY;

            if (moveLeft_19_HUY && picPlayer_19_HUY.Left > 0)
            {
                picPlayer_19_HUY.Left -= speed_19_HUY;
                checkAnmPlayer_19_HUY = true;

            }

            if (moveRight_19_HUY && picPlayer_19_HUY.Left < this.ClientSize.Width - picPlayer_19_HUY.Width)
            {
                picPlayer_19_HUY.Left += speed_19_HUY;
                checkAnmPlayer_19_HUY = true;
            }

            if (moveJump_19_HUY && force_19_HUY < 0 || picPlayer_19_HUY.Top < 0)
                moveJump_19_HUY = false;
            if (moveJump_19_HUY)
            {
                jumSpeed_19_HUY = -12;
                force_19_HUY -= 1;
                checkAnmPlayer_19_HUY = true;
            }

            if (!moveJump_19_HUY && checkAnmPlayer_19_HUY || checkAnmPlayer_19_HUY2)
            {
                jumSpeed_19_HUY = 12;
                checkAnmPlayer_19_HUY = false;
            }

            foreach (Control x_19_HUY in this.Controls)
            {
                if (x_19_HUY is PictureBox && (string)x_19_HUY.Tag == "ground_19_HUY")
                {
                    if (picPlayer_19_HUY.Bounds.IntersectsWith(x_19_HUY.Bounds) && !moveJump_19_HUY)
                    {
                        picPlayer_19_HUY.Top = x_19_HUY.Top - picPlayer_19_HUY.Height;
                        force_19_HUY = 12;
                        jumSpeed_19_HUY = 0;
                    }
                    x_19_HUY.BringToFront();
                }
            }
        }
        private void MoveBullet_19_HUY()
        {
            if (checkBullet_19_HUY)
            {
                MakeBullet_19_HUY();
            }

            foreach (Control x_19_HUY in this.Controls)
            {
                if (x_19_HUY is PictureBox && x_19_HUY.Tag != null && x_19_HUY.Tag.ToString() == "bullet_19_HUY")
                {

                    x_19_HUY.Left += 10; // Tốc độ di chuyển đạn

                    // Xóa đạn khi ra khỏi màn hình
                    if (x_19_HUY.Left > this.ClientSize.Width - 100)
                        RemoveControl_19_HUY(x_19_HUY);
                }
            }
        }
        private Dictionary<Control, bool> botDirections_19_HUY = new Dictionary<Control, bool>();
        private void MoveBot_19_HUY()
        {
            foreach (Control bot_19_HUY in this.Controls)
            {
                if (bot_19_HUY is PictureBox && (string)bot_19_HUY.Tag == "bot_19_HUY")
                {
                    bool isOnGround_19_HUY = false;

                    foreach (Control ground_19_HUY in this.Controls)
                    {
                        if (ground_19_HUY is PictureBox && (string)ground_19_HUY.Tag == "ground_19_HUY")
                        {
                            // Kiểm tra xem bot có đứng trên mặt đất này không
                            if (bot_19_HUY.Top == (ground_19_HUY.Location.Y - bot_19_HUY.Height))
                            {
                                isOnGround_19_HUY = true;

                                // Đảm bảo bot có hướng di chuyển
                                if (!botDirections_19_HUY.ContainsKey(bot_19_HUY))
                                {
                                    botDirections_19_HUY[bot_19_HUY] = true; // Mặc định đi sang phải
                                }

                                if (botDirections_19_HUY[bot_19_HUY]) // Đang đi sang phải
                                {
                                    bot_19_HUY.Left += 1;

                                    // Nếu đến mép phải, đổi hướng
                                    if (bot_19_HUY.Right >= ground_19_HUY.Right)
                                    {
                                        botDirections_19_HUY[bot_19_HUY] = false;
                                    }
                                }
                                else // Đang đi sang trái
                                {
                                    bot_19_HUY.Left -= 1;

                                    // Nếu đến mép trái, đổi hướng
                                    if (bot_19_HUY.Left <= ground_19_HUY.Left)
                                    {
                                        botDirections_19_HUY[bot_19_HUY] = true;
                                    }
                                }
                                break; // Thoát khỏi vòng lặp kiểm tra mặt đất khi đã tìm thấy mặt đất phù hợp
                            }
                        }
                    }
                }
            }
        }
        private void IncreaseView_19_HUY()
        {
            if (picPlayer_19_HUY.Left > 500 && !checktail_19_HUY)
            {
                picBrg_1_19_HU.Left -= 600;
                MoveGameElevents_19_HUY("forward_19_HUY", 600);
                picPlayer_19_HUY.Left = 5;
                checktail_19_HUY = checkThGate_19_HUY = true;
                checkhead_19_HUY = false;
            }
            if (picPlayer_19_HUY.Left < 0 && !checkhead_19_HUY && checkThGate_19_HUY)
            {
                picBrg_1_19_HU.Left += 400;
                MoveGameElevents_19_HUY("back_19_HUY", 400);
                picPlayer_19_HUY.Left += 400;
                checkhead_19_HUY = checkhead_19_HUY = true;
                checktail_19_HUY = false;
            }
        }
        private void PlayerDie_19_HUY()
        {
            if (picPlayer_19_HUY.Top + picPlayer_19_HUY.Height > this.ClientSize.Height-54 || cntHeart_19_HUY == 0)
            {
                GamerTime_19_HUY.Stop();
                MessageBox.Show("sssssssss");
            }
        }
        private void MakeBullet_19_HUY()
        {
            TimeSpan timeSinceLastBullet_19_HUY = DateTime.Now - lastBulletTime_19_HUY;
            // Chỉ cho phép bắn nếu đã qua đủ thời gian delay
            if (timeSinceLastBullet_19_HUY.TotalMilliseconds >= bulletDelay_19_HUY)
            {
                if (forceBullet_19_HUY > 0) // Chỉ tạo bullet khi còn "số đạn"
                {
                    PictureBox bullet_19 = new PictureBox();
                    bullet_19.Image = Properties.Resources.Fireball_19_HUY;
                    bullet_19.Height = bullet_19.Width = 30;

                    bullet_19.Left = picPlayer_19_HUY.Left + picPlayer_19_HUY.Width;
                    bullet_19.Top = picPlayer_19_HUY.Top + picPlayer_19_HUY.Height / 2 - 7;

                    bullet_19.SizeMode = PictureBoxSizeMode.StretchImage;
                    bullet_19.Tag = "bullet_19_HUY";
                    bullet_19.Region = Region.FromHrgn(CreateRoundRectRgn
                (0, 0, bullet_19.Width, bullet_19.Height, bullet_19.Height, bullet_19.Height));
                    this.Controls.Add(bullet_19);
                    bullet_19.BringToFront();
                    forceBullet_19_HUY--; // Giảm số lượng đạn sau khi tạo
                    lastBulletTime_19_HUY = DateTime.Now; // Cập nhật thời điểm bắn
                    lblBullet_19_HUY.Text = "X " + (forceBullet_19_HUY).ToString();
                }
            }
        } 
        private void RemoveControl_19_HUY(Control bullet_19_HUY)
        {
            this.Controls.Remove(bullet_19_HUY);
            bullet_19_HUY.Dispose();
        }
        private void BotCollision_19_HUY()
        {
            foreach (Control bot_19_HUY in this.Controls)
            {
                if (bot_19_HUY is PictureBox && (string)bot_19_HUY.Tag == "bot_19_HUY" || (string)bot_19_HUY.Tag == "bird_19_HUY")
                {
                    foreach (Control bullet_19_HUY in this.Controls)
                    {
                        if (bullet_19_HUY is PictureBox && (string)bullet_19_HUY.Tag == "bullet_19_HUY" && bullet_19_HUY.Visible)
                        {
                            if (bot_19_HUY.Bounds.IntersectsWith(bullet_19_HUY.Bounds))
                            {
                                if ((string)bot_19_HUY.Tag == "bot_19_HUY")
                                    --botBlood_19_HUY;
                                if ((string)bot_19_HUY.Tag == "bird_19_HUY")
                                    --birdBlood_19_HUY;
                                bullet_19_HUY.Visible = false; // Ẩn đạn khi va chạm
                                bot_19_HUY.Left += 10;
                                if (botBlood_19_HUY == 0)
                                {
                                    botBlood_19_HUY = 3;
                                    bot_19_HUY.Visible = false; // Ẩn bot khi máu = 0
                                    RemoveControl_19_HUY(bot_19_HUY);
                                }
                                if (birdBlood_19_HUY == 0)
                                {
                                    birdBlood_19_HUY = 2;
                                    bot_19_HUY.Visible = false; // Ẩn bot khi máu = 0
                                    RemoveControl_19_HUY(bot_19_HUY);
                                    MakeBotFly();
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void PlayerCollision_19_HUY()
        {
            foreach (Control bot_19_HUY in this.Controls)
            {
                if (bot_19_HUY is PictureBox && (string)bot_19_HUY.Tag == "bot_19_HUY" || (string)bot_19_HUY.Tag == "bird_19_HUY")
                {
                    if (bot_19_HUY.Bounds.IntersectsWith(picPlayer_19_HUY.Bounds))
                    {
                        picPlayer_19_HUY.Left -= 50;

                        if (cntHeart_19_HUY >= 0)
                            picHeartPlayer_19_HUY.Image = heartPlayer_19_HUY[cntHeart_19_HUY - 1].Image;
                        --cntHeart_19_HUY;
                        lblHeart_19_HUY.Text = "X " + (cntHeart_19_HUY).ToString();
                    }
                }
            }
        }
        private void FillBullet()
        {
            foreach (Control bullet in this.Controls)
            {
                if (bullet is PictureBox && (string)bullet.Tag == "fillBullet_19_HUY")
                {
                    if (bullet.Bounds.IntersectsWith(picPlayer_19_HUY.Bounds))
                    {
                        forceBullet_19_HUY += 3;
                        bullet.Visible = false;
                        RemoveControl_19_HUY(bullet);
                        lblBullet_19_HUY.Text = "X " + (forceBullet_19_HUY).ToString();
                    }
                }
            }
        }
        private void FillHeart()
        {
            foreach (Control bullet in this.Controls)
            {
                if (bullet is PictureBox && (string)bullet.Tag == "heart_19_HUY")
                {
                    if (bullet.Bounds.IntersectsWith(picPlayer_19_HUY.Bounds))
                    {
                        if (cntHeart_19_HUY <= 4)
                        {
                            cntHeart_19_HUY += 1;
                            picHeartPlayer_19_HUY.Image = heartPlayer_19_HUY[cntHeart_19_HUY - 1].Image;
                        }
                        bullet.Visible = false;
                        RemoveControl_19_HUY(bullet);
                        lblHeart_19_HUY.Text = "X " + (cntHeart_19_HUY).ToString();

                    }
                }
            }
        }
        private void MoveGameElevents_19_HUY(string direction_19_HUY, int speed_19_HUY)
        {
            foreach (Control x_19_HUY in this.Controls)
            {
                if (x_19_HUY is PictureBox && (string)x_19_HUY.Tag == "ground_19_HUY" || (string)x_19_HUY.Tag == "bot_19_HUY" || (string)x_19_HUY.Tag == "Bush_19_HUY"
                    || (string)x_19_HUY.Tag == "heart_19_HUY" || (string)x_19_HUY.Tag == "fillBullet_19_HUY" || (string)x_19_HUY.Tag == "bird_19_HUY")
                {
                    if (direction_19_HUY == "back_19_HUY")
                        x_19_HUY.Left += speed_19_HUY;
                    if (direction_19_HUY == "forward_19_HUY")
                        x_19_HUY.Left -= speed_19_HUY;
                }
            }
        }
        private void KeyIsDown_19_HUY(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && !moveJump_19_HUY)
                moveJump_19_HUY = true;
            if (e.KeyCode == Keys.Right)
                moveRight_19_HUY = true;
            if (e.KeyCode == Keys.Left)
                moveLeft_19_HUY = true;
            if (e.KeyCode == Keys.R)
            {
                checkBullet_19_HUY = true;
            }
        }
        private void KeyIsUp_19_HUY(object sender, KeyEventArgs e)
        {
            if (moveJump_19_HUY)
                moveJump_19_HUY = false;
            if (e.KeyCode == Keys.Right)
                moveRight_19_HUY = false;
            if (e.KeyCode == Keys.Left)
                moveLeft_19_HUY = false;
            if (e.KeyCode == Keys.R)
            {
                checkBullet_19_HUY = false;
            }
        }

    }
}