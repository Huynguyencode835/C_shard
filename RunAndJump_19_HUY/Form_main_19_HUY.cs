using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using WMPLib;
using MediaPlayer;

namespace RunAndJump_19_HUY
{
    public partial class FormMain_19_HUY : Form
    {
        System.Windows.Forms.Timer flyBotTimer_19_HUY;

        bool IsJump_19_HUY,
            moveLeft_19_HUY,
            moveRight_19_HUY,
            checkJump_19_HUY,
            checktail_19_HUY,
            checkhead_19_HUY,
            checkBullet_19_HUY,
            checkThGate_19_HUY,
            isMusic_19_Huy,
            isPause_19_Huy,
            isSoundAttack_19_Huy;

        int playerBlood_19_HUY,
            botBlood_19_HUY,
            birdBlood_19_HUY,
            force_19_HUY,
            speed_19_HUY,
            jumSpeed_19_HUY,
            backroundSpeed_19_HUY,
            forceBullet_19_HUY,
            cntHeart_19_HUY,
            cntBotUnder_19_HUY,
            cntBotDie_19_Huy,
            reloadTime_19_HUY,
            bulletDelay_19_HUY,
            maxBullets_19_HUY,
            bossBlood_19_HUY;


        List<Point> checkIndexBot_19_HUY = new List<Point>();
        DateTime lastBulletTime_19_HUY = DateTime.MinValue;
        List<PictureBox> heartPlayer_19_HUY = new List<PictureBox>();
        Random random_19_HUY = new Random();
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn_19_Huy
            (
                int nLeft_19_Huy,
                int nTop_19_Huy,
                int nRight_19_Huy,
                int nBottom_19_Huy,
                int nWidthEllipse_19_Huy,
                int nHeightEllipse_19_Huy
            );

        CreateObject EnemyAndItems_19_HUY;
        EnemyBird EnemyFly_19_HUY;
        EnemyBoss EnemyBoss_19_HUY;
        public FormMain_19_HUY()
        {
            InitializeComponent();
            ResetGame_19_HUY();
        }

        WindowsMediaPlayer musicBackground;
        SoundPlayer soundAttack;
        private void MusicBackkground_19_HUY()
        {
            musicBackground= new WindowsMediaPlayer();
            musicBackground.URL = "backgroundMusic_19_Huy.wav";
            musicBackground.settings.setMode("loop", true);
            musicBackground.settings.volume = 25;
            musicBackground.controls.play();
        }
        private void ResetGame_19_HUY()
        {
            MusicBackkground_19_HUY();
            AddValue();
            CreateHeartABullet_19_Huy();
            CreatEnemyAndItems();
            GamerTime_19_HUY.Start();
        }
        private void AddValue()
        {
            soundAttack = new SoundPlayer("soundAttack_19_Huy.wav");
            IsJump_19_HUY = moveLeft_19_HUY =
            moveRight_19_HUY = checktail_19_HUY =
            checkhead_19_HUY = checkThGate_19_HUY = 
            checkBullet_19_HUY = isMusic_19_Huy=
            isPause_19_Huy = isSoundAttack_19_Huy = false;
            checkJump_19_HUY = true;
            speed_19_HUY = 4;
            force_19_HUY = 5;
            playerBlood_19_HUY = 4;
            botBlood_19_HUY = 3;
            birdBlood_19_HUY = 2;
            backroundSpeed_19_HUY = 5;
            jumSpeed_19_HUY = 0;
            forceBullet_19_HUY = 5;
            cntBotUnder_19_HUY = 5;
            cntBotDie_19_Huy = 2;
            reloadTime_19_HUY = 0;
            bulletDelay_19_HUY = 200;
            maxBullets_19_HUY = 10;
            bossBlood_19_HUY = 5;
        }
        private void CreateHeartABullet_19_Huy()
        {
            cntHeart_19_HUY = 4;
            lblHeart_19_HUY.Text = "X " + (cntHeart_19_HUY).ToString();
            lblBullet_19_HUY.Text = "X " + (forceBullet_19_HUY).ToString();

            for (int i = cntHeart_19_HUY + 1; i >  0; i--)
            {
                PictureBox heart_19_Huy = new PictureBox();
                string resourceName = $"heartPlayer_{i}_19_HUY";
                object img_19_Huy = Properties.Resources.ResourceManager.GetObject(resourceName);
                heart_19_Huy.Image = (Image)img_19_Huy;
                heartPlayer_19_HUY.Add(heart_19_Huy);
            }
        }
        private void TimeGameEvent_19_HUY(object sender, EventArgs e)
        {
            //PlayerCollision_19_HUY();
            MovePlayer_19_HUY();
            MoveBullet_19_HUY();
            IncreaseView_19_HUY();
            MoveBackground_19_HUY();
            PlayerDie_19_HUY();
            ENEMYCollision_19_HUY();
            FillBullet_19_Huy();
            FillHeart_19_Huy();
            MoveEnemy_19_HUY();
            MoveEnemyFly_19_Huy();
            BloodBarBoss();
        }
        private void RemoveControl_19_HUY(Control bullet_19_HUY)
        {
            this.Controls.Remove(bullet_19_HUY);
            bullet_19_HUY.Dispose();
        }

        //MenuStrip
        private void nEWGAMEToolStripMenuItem_19_Huy_Click(object sender, EventArgs e)
        {
            FormMain_19_HUY formNew_19_Huy = new FormMain_19_HUY();
            this.Hide();
            musicBackground.controls.stop();
            formNew_19_Huy.ShowDialog();
            this.Close();
        }
        private void pauseToolStripMenuItem_19_Huy_Click(object sender, EventArgs e)
        {
            if (!isPause_19_Huy)
            {
                GamerTime_19_HUY.Stop();
                isPause_19_Huy = true;
            }
            else
            {
                GamerTime_19_HUY.Start();
                isPause_19_Huy = false;
            }
        }
        private void onToolStripMenuItem_19_Huy_Click(object sender, EventArgs e)
        {
            if (!isMusic_19_Huy)
            {
                musicBackground.controls.play();
                isMusic_19_Huy = true;
            }
        }
        private void exitToolStripMenuItem_19_Huy_Click(object sender, EventArgs e)
        {
            DialogResult res_19_HUY = MessageBox.Show("Bạn có chắc muốn thoát ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res_19_HUY == DialogResult.Yes)
                Close();
        }
        private void offToolStripMenuItem_19_Huy_Click(object sender, EventArgs e)
        {
            if (isMusic_19_Huy)
            {
                musicBackground.controls.stop();

                isMusic_19_Huy = false;
            }
        }
        private void aboutToolStripMenuItem_19_Huy_Click(object sender, EventArgs e)
        {
            // Tạo một instance của form mới
            instruct_19_HUY formNew = new instruct_19_HUY();

            // Hiển thị form mới dưới dạng hộp thoại modal

            DialogResult result = formNew.ShowDialog();

            // Kiểm tra kết quả trả về khi form mới đóng lại
            if (result == DialogResult.Cancel)
                formNew.Close();
            else
                formNew.Show();
        }
        
        //Player
        private void MovePlayer_19_HUY()
        {
            // Apply vertical movement (jumping)
            picPlayer_19_HUY.Top += jumSpeed_19_HUY;

            // Horizontal movement logic

            if (moveLeft_19_HUY && picPlayer_19_HUY.Left > picGround_U1_19_HUY.Left)
            {
                picPlayer_19_HUY.Left -= speed_19_HUY;
                checkJump_19_HUY = false;

            }
            else if (moveRight_19_HUY && picPlayer_19_HUY.Left < this.ClientSize.Width - picPlayer_19_HUY.Width)
            {
                picPlayer_19_HUY.Left += speed_19_HUY;
                checkJump_19_HUY = false;
            }

            // Jump state management
            ManageJumpState_19_Huy();

            // Ground collision detection
            HandleGroundCollision_19_Huy();
        }
        private void ManageJumpState_19_Huy()
        {
            // Cancel jump if at top or force is exhausted
            if (IsJump_19_HUY && (force_19_HUY < 0 || picPlayer_19_HUY.Top < 0))
                IsJump_19_HUY = false;
            // Perform jump
            if (IsJump_19_HUY)
            {
                jumSpeed_19_HUY = -12;
                force_19_HUY -= 1;
                checkJump_19_HUY = false;
            }
            // Reset jump state when not jumping
            if (!IsJump_19_HUY && !checkJump_19_HUY)
            {
                jumSpeed_19_HUY = 12;
                checkJump_19_HUY = true;
            }
        }
        private void HandleGroundCollision_19_Huy()
        {
            foreach (Control ground_19_Huy in this.Controls)
            {
                if (ground_19_Huy is PictureBox && ground_19_Huy.Tag?.ToString() == "ground_19_HUY")
                {
                    if (picPlayer_19_HUY.Bounds.IntersectsWith(ground_19_Huy.Bounds) && !IsJump_19_HUY)
                    {
                        picPlayer_19_HUY.Top = ground_19_Huy.Top - picPlayer_19_HUY.Height;
                        force_19_HUY = 12;
                        jumSpeed_19_HUY = 0;
                    }
                    ground_19_Huy.BringToFront();
                }
            }
        }
        private void PlayerCollision_19_HUY()
        {
            foreach (Control bot_19_HUY in this.Controls)
            {
                if (bot_19_HUY is PictureBox && (string)bot_19_HUY.Tag == "bot_19_HUY" || (string)bot_19_HUY.Tag == "bird_19_HUY" || (string)bot_19_HUY.Tag == "boss_19_HUY")
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
                if (bot_19_HUY is PictureBox && (string)bot_19_HUY.Tag == "key_19_HUY" && bot_19_HUY.Bounds.IntersectsWith(picPlayer_19_HUY.Bounds))
                {
                    RemoveControl_19_HUY(bot_19_HUY);
                    GamerTime_19_HUY.Stop();
                    MessageBox.Show("win game");
                }
            }
        }
        private void FillBullet_19_Huy()
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
        private void FillHeart_19_Huy()
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
        private void PlayerDie_19_HUY()
        {
            if (picPlayer_19_HUY.Top + picPlayer_19_HUY.Height > this.ClientSize.Height - 54 || cntHeart_19_HUY == 0)
            {
                ChangeImgPlayer_19_HUY("smoke-10073_128");
                GamerTime_19_HUY.Stop();
                FormExitReset_19_HUY formNew = new FormExitReset_19_HUY();
                musicBackground.controls.stop();
                formNew.ShowDialog();
                this.Close();
            }
        }
        private void ChangeImgPlayer_19_HUY(string characterName)
        {
            picPlayer_19_HUY.Image = (Image)Properties.Resources.ResourceManager.GetObject(characterName);
            picPlayer_19_HUY.BringToFront();
        }

        //Bullet
        
        private void MoveBullet_19_HUY()
        {
            if (checkBullet_19_HUY)
                MakeBullet_19_HUY();
            foreach (Control x_19_HUY in this.Controls)
            {
                if (x_19_HUY is PictureBox && x_19_HUY.Tag != null && x_19_HUY.Tag.ToString() == "bullet_19_HUY")
                {
                    x_19_HUY.Left += 10;
                    if (x_19_HUY.Left > this.ClientSize.Width - 100)
                        RemoveControl_19_HUY(x_19_HUY);
                }
            }
        }
        private void MakeBullet_19_HUY()
        {
            TimeSpan timeSinceLastBullet_19_HUY = DateTime.Now - lastBulletTime_19_HUY;
            // Chỉ cho phép bắn nếu đã qua đủ thời gian delay
            if (timeSinceLastBullet_19_HUY.TotalMilliseconds >= bulletDelay_19_HUY && forceBullet_19_HUY > 0)
            {
                
                    soundAttack.Play();
                    PictureBox bullet_19 = new PictureBox();
                    bullet_19.Image = Properties.Resources.Fireball_19_HUY;
                    bullet_19.Height = bullet_19.Width = 30;

                    bullet_19.Left = picPlayer_19_HUY.Left + picPlayer_19_HUY.Width;
                    bullet_19.Top = picPlayer_19_HUY.Top + picPlayer_19_HUY.Height / 2 - 7;

                    bullet_19.SizeMode = PictureBoxSizeMode.StretchImage;
                    bullet_19.Tag = "bullet_19_HUY";
                    bullet_19.Region = Region.FromHrgn(CreateRoundRectRgn_19_Huy
                (0, 0, bullet_19.Width, bullet_19.Height, bullet_19.Height, bullet_19.Height));
                    this.Controls.Add(bullet_19);
                    bullet_19.BringToFront();
                    forceBullet_19_HUY--; // Giảm số lượng đạn sau khi tạo
                    lastBulletTime_19_HUY = DateTime.Now; // Cập nhật thời điểm bắn
                    lblBullet_19_HUY.Text = "X " + (forceBullet_19_HUY).ToString();
            }
        }

        //Enemy
        private void CreatEnemyAndItems()
        {
            EnemyAndItems_19_HUY = new CreateObject(this);
            EnemyFly_19_HUY = new EnemyBird(this);
            EnemyBoss_19_HUY = new EnemyBoss(this);

            for (int i = 0; i < 5; i++)
            {
                PictureBox bot = EnemyAndItems_19_HUY.CreateEnemy_19_HUY(RandomEnemy_19_Huy(), 60, 60, "bot_19_HUY", 0);
                RandomLocation_19_Huy(bot, bot.Height, 0);
            }
            PictureBox boss = EnemyBoss_19_HUY.CreateBoss_19_HUY();
            CustomProgressBar bossHealthBar = EnemyBoss_19_HUY.CreateBossHealthBar_19_HUY(bossBlood_19_HUY, boss.Location);
            EnemyFly_19_HUY.CreateFlyingBird_19_HUY();

            for (int i = 0; i < 6; i++)
            {
                PictureBox heart = EnemyAndItems_19_HUY.CreateEnemy_19_HUY("heartPlayer_1_19_HUY", 20, 20, "heart_19_HUY", -30);
                RandomLocation_19_Huy(heart, heart.Height, -30);

                PictureBox bullet = EnemyAndItems_19_HUY.CreateEnemy_19_HUY("Fireball_19_HUY", 20, 20, "fillBullet_19_HUY", -90);
                RandomLocation_19_Huy(bullet, bullet.Height, -90);
            }
        }
        private string RandomEnemy_19_Huy()
        {
            List<string> listEnemy_19_Huy = new List<string>
                {
                    "bot1_19_HUY",
                    "bot2_19_HUY",
                    "bot3_19_HUY"
                };
            return listEnemy_19_Huy[random_19_HUY.Next(0, listEnemy_19_Huy.Count)];
        }
        private void ENEMYCollision_19_HUY()
        {
            foreach (Control bot_19_HUY in this.Controls)
            {
                if (bot_19_HUY is PictureBox && (string)bot_19_HUY.Tag == "bot_19_HUY" || (string)bot_19_HUY.Tag == "boss_19_HUY" || (string)bot_19_HUY.Tag == "bird_19_HUY")
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
                                if ((string)bot_19_HUY.Tag == "boss_19_HUY")
                                {
                                    --bossBlood_19_HUY;
                                    foreach (Control control in this.Controls)
                                    {
                                        if (control is CustomProgressBar progressBar && (string)progressBar.Tag == "bloodBar_19_HUY")
                                        {
                                            progressBar.Value = Math.Max(progressBar.Value - 1, 0); // Tránh xuống dưới 0
                                        }
                                    }
                                }
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
                                    EnemyFly_19_HUY.CreateFlyingBird_19_HUY();
                                }
                                if (bossBlood_19_HUY == 0)
                                {
                                    EnemyAndItems_19_HUY.CreateKey_19_HUY(bot_19_HUY.Location);
                                    RemoveControl_19_HUY(bot_19_HUY);
                                    foreach (Control control in this.Controls)
                                    {
                                        if (control is CustomProgressBar progressBar && (string)progressBar.Tag == "bloodBar_19_HUY")
                                        {
                                            this.Controls.Remove(control);
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void MakeKey_19_HUY(Point location_19_HUY)
        {
            PictureBox Bot1 = new PictureBox();
            Bot1.Image = (Image)Properties.Resources.key_19_HUY;
            Bot1.Width = 50;
            Bot1.Height = 50;
            Bot1.Tag = "key_19_HUY";
            Bot1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(Bot1);
            Bot1.Location = new Point(location_19_HUY.X, location_19_HUY.Y + 50);
            Bot1.BringToFront();
        }
        private void BloodBarBoss()
        {
            PictureBox bossControl = null;
            CustomProgressBar progressBar = null;

            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && (string)control.Tag == "boss_19_HUY")
                {
                    bossControl = (PictureBox)control;
                    break;
                }
            }

            foreach (Control control in this.Controls)
            {
                if (control is CustomProgressBar && (string)control.Tag == "bloodBar_19_HUY")
                {
                    progressBar = (CustomProgressBar)control;
                    break;
                }
            }

            if (bossControl != null && progressBar != null)
            {
                progressBar.Location = new Point(bossControl.Location.X, bossControl.Location.Y + 10);
            }
        }
        private Dictionary<Control, bool> EnemyDirections_19_HUY = new Dictionary<Control, bool>();
        private void MoveEnemy_19_HUY()
        {
            foreach (Control bot_19_HUY in this.Controls)
            {
                if (bot_19_HUY is PictureBox && (string)bot_19_HUY.Tag == "bot_19_HUY" || (string)bot_19_HUY.Tag == "boss_19_HUY")
                {
                    bool isOnGround_19_HUY = false;
                    foreach (Control ground_19_HUY in this.Controls)
                    {
                        if (ground_19_HUY is PictureBox && (string)ground_19_HUY.Tag == "ground_19_HUY")
                        {
                            // Kiểm tra xem bot có đứng trên mặt đất này không
                            if (bot_19_HUY.Location.Y == (ground_19_HUY.Location.Y - bot_19_HUY.Height))
                            {
                                isOnGround_19_HUY = true;

                                // Đảm bảo bot có hướng di chuyển
                                if (!EnemyDirections_19_HUY.ContainsKey(bot_19_HUY))
                                    EnemyDirections_19_HUY[bot_19_HUY] = true; // Mặc định đi sang phải

                                if (EnemyDirections_19_HUY[bot_19_HUY]) // Đang đi sang phải
                                {
                                    bot_19_HUY.Left += 1;
                                    // Nếu đến mép phải, đổi hướng
                                    if (bot_19_HUY.Right >= ground_19_HUY.Right)
                                        EnemyDirections_19_HUY[bot_19_HUY] = false;
                                }
                                else // Đang đi sang trái
                                {
                                    bot_19_HUY.Left -= 1;
                                    // Nếu đến mép trái, đổi hướng
                                    if (bot_19_HUY.Left <= ground_19_HUY.Left)
                                        EnemyDirections_19_HUY[bot_19_HUY] = true;
                                }
                                break; // Thoát khỏi vòng lặp kiểm tra mặt đất khi đã tìm thấy mặt đất phù hợp
                            }
                        }
                    }
                }
            }
        }
        private void MoveEnemyFly_19_Huy()
        {
            foreach (Control control_19_Huy in this.Controls)
            {
                if (control_19_Huy is PictureBox && (string)control_19_Huy.Tag == "bird_19_HUY")
                {
                    control_19_Huy.Left -= 5;
                    if (control_19_Huy.Left < 0)
                    {
                        RemoveControl_19_HUY(control_19_Huy);
                        EnemyFly_19_HUY.CreateFlyingBird_19_HUY();
                    }
                }
            }
        }
        private void RandomLocation_19_Huy(PictureBox pictureBox_19_Huy, int height_19_HUY, int width_19_HUY)
        {

            List<Point> positions_19_Huy = new List<Point>
            {
                new Point(473-width_19_HUY, 259-height_19_HUY),
                new Point(807-width_19_HUY, 376-height_19_HUY),
                new Point(1507, 308-height_19_HUY),
                new Point(1180- width_19_HUY, 77-height_19_HUY),
                new Point(1818 - width_19_HUY, 247-height_19_HUY),
                new Point(2077, 424-height_19_HUY),
                new Point(2359 - width_19_HUY, 283-height_19_HUY),
                new Point(2566 - width_19_HUY, 104-height_19_HUY)
            };
            int randomIndex_19_Huy;
            do
            {
                randomIndex_19_Huy = random_19_HUY.Next(positions_19_Huy.Count);
            }
            while (checkIndexBot_19_HUY.Contains(positions_19_Huy[randomIndex_19_Huy]));
            pictureBox_19_Huy.Location = positions_19_Huy[randomIndex_19_Huy];
            checkIndexBot_19_HUY.Add(positions_19_Huy[randomIndex_19_Huy]);
        }

        //Background_Ground
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
        private void MoveGameElevents_19_HUY(string direction_19_HUY, int speed_19_HUY)
        {
            foreach (Control x_19_HUY in this.Controls)
            {
                if (x_19_HUY is PictureBox && (string)x_19_HUY.Tag == "ground_19_HUY" || (string)x_19_HUY.Tag == "key_19_HUY" || (string)x_19_HUY.Tag == "bot_19_HUY" || (string)x_19_HUY.Tag == "boss_19_HUY" || (string)x_19_HUY.Tag == "Bush_19_HUY"
                    || (string)x_19_HUY.Tag == "heart_19_HUY" || (string)x_19_HUY.Tag == "fillBullet_19_HUY" || (string)x_19_HUY.Tag == "bird_19_HUY" || (string)x_19_HUY.Tag == "background_19_HUY")
                {
                    if (direction_19_HUY == "back_19_HUY")
                        x_19_HUY.Left += speed_19_HUY;
                    if (direction_19_HUY == "forward_19_HUY")
                        x_19_HUY.Left -= speed_19_HUY;
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
                checkhead_19_HUY = checkThGate_19_HUY = true;
                checktail_19_HUY = false;
            }
        }

        //CheckInput
        private void KeyIsDown_19_HUY(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && !IsJump_19_HUY)
                IsJump_19_HUY = true;
            if (e.KeyCode == Keys.Right)
                moveRight_19_HUY = true;
            if (e.KeyCode == Keys.Left)
                moveLeft_19_HUY = true;
            if (e.KeyCode == Keys.Space)
                checkBullet_19_HUY = true;
        }
        private void KeyIsUp_19_HUY(object sender, KeyEventArgs e)
        {
            if (IsJump_19_HUY)
                IsJump_19_HUY = false;
            if (e.KeyCode == Keys.Right)
                moveRight_19_HUY = false;
            if (e.KeyCode == Keys.Left)
                moveLeft_19_HUY = false;
            if (e.KeyCode == Keys.Space)
                checkBullet_19_HUY = false;
        }
    }
}