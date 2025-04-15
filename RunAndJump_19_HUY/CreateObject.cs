using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RunAndJump_19_HUY
{
    // Lớp cơ sở Enemy
    internal class CreateObject
    {
        protected Random random_19_HUY = new Random();
        protected Form parentForm_19_HUY; // Form cha sẽ chứa các enemy

        // Constructor nhận form cha
        public CreateObject(Form parentForm_19_HUY)
        {
            this.parentForm_19_HUY = parentForm_19_HUY;
        }

        // Phương thức tạo enemy thông thường
        public virtual PictureBox CreateEnemy_19_HUY(string imageName_19_Huy, int width_19_Huy, int height_19_Huy, string tag_19_Huy, int left_19_Huy) //List<Point> usedPositions_19_HUY
        {
            PictureBox Bot1_19_Huy = new PictureBox();
            Bot1_19_Huy.Image = (Image)Properties.Resources.ResourceManager.GetObject(imageName_19_Huy);
            Bot1_19_Huy.Width = width_19_Huy;
            Bot1_19_Huy.Height = height_19_Huy;
            Bot1_19_Huy.Tag = tag_19_Huy;
            Bot1_19_Huy.SizeMode = PictureBoxSizeMode.StretchImage;

            // Thêm bot vào controls
            parentForm_19_HUY.Controls.Add(Bot1_19_Huy);
            Bot1_19_Huy.BringToFront();

            return Bot1_19_Huy;
        }

        // Phương thức tạo key (chìa khóa)
        public PictureBox CreateKey_19_HUY(Point location_19_HUY)
        {
            PictureBox key_19_Huy = new PictureBox();
            key_19_Huy.Image = (Image)Properties.Resources.key_19_HUY;
            key_19_Huy.Width = 50;
            key_19_Huy.Height = 50;
            key_19_Huy.Tag = "key_19_HUY";
            key_19_Huy.SizeMode = PictureBoxSizeMode.StretchImage;

            parentForm_19_HUY.Controls.Add(key_19_Huy);
            key_19_Huy.Location = new Point(location_19_HUY.X, location_19_HUY.Y + 50);
            key_19_Huy.BringToFront();

            return key_19_Huy;
        }
    }

    // Lớp EnemyBird kế thừa từ Enemy
    internal class EnemyBird : CreateObject
    {
        public EnemyBird(Form parentForm_19_HUY) : base(parentForm_19_HUY)
        {
        }

        // Phương thức tạo chim bay
        public PictureBox CreateFlyingBird_19_HUY()
        {
            PictureBox bird_19_Huy = new PictureBox
            {
                Image = Properties.Resources.Fly_19_HUY,
                Width = 100,
                Height = 40,
                Tag = "bird_19_HUY",
                BackColor = Color.FromArgb(192, 255, 255),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            // Tìm vị trí player để đặt chim bay
            PictureBox player = null;
            foreach (Control control in parentForm_19_HUY.Controls)
            {
                if (control is PictureBox && (string)control.Tag == "player_19_HUY")
                {
                    player = (PictureBox)control;
                    break;
                }
            }

            // Nếu tìm thấy player, đặt chim ở phía trước player
            if (player != null)
            {
                bird_19_Huy.Left = player.Left + 1000;
            }
            else
            {
                bird_19_Huy.Left = 1000; // Giá trị mặc định nếu không tìm thấy player
            }
            bird_19_Huy.Top = random_19_HUY.Next(10, parentForm_19_HUY.ClientSize.Height - 200);

            parentForm_19_HUY.Controls.Add(bird_19_Huy);
            bird_19_Huy.BringToFront();

            return bird_19_Huy;
        }
    }

    // Lớp EnemyBoss kế thừa từ Enemy
    internal class EnemyBoss : CreateObject
    {
        public EnemyBoss(Form parentForm_19_HUY) : base(parentForm_19_HUY)
        {
        }

        // Phương thức tạo boss
        public PictureBox CreateBoss_19_HUY()
        {
            PictureBox Boss_19_Huy = new PictureBox();
            Boss_19_Huy.Image = (Image)Properties.Resources.Boss_19_HUY;
            Boss_19_Huy.Width = 75;
            Boss_19_Huy.Height = 100;
            Boss_19_Huy.Tag = "boss_19_HUY";
            Boss_19_Huy.SizeMode = PictureBoxSizeMode.StretchImage;

            // Thêm boss vào controls
            parentForm_19_HUY.Controls.Add(Boss_19_Huy);

            // Đặt vị trí cố định cho boss
            Boss_19_Huy.Location = new Point(2108, 104 - Boss_19_Huy.Height);
            Boss_19_Huy.BringToFront();

            return Boss_19_Huy;
        }

        // Phương thức tạo thanh máu cho boss
        public CustomProgressBar CreateBossHealthBar_19_HUY(int maxHealth, Point bossLocation)
        {
            var progressBar = new CustomProgressBar
            {
                Maximum = maxHealth,
                Value = maxHealth,
                Width = 80,
                Height = 15,
                Tag = "bloodBar_19_HUY",
                Location = new Point(bossLocation.X, bossLocation.Y + 10)
            };

            parentForm_19_HUY.Controls.Add(progressBar);
            progressBar.BringToFront();

            return progressBar;
        }
    }
}