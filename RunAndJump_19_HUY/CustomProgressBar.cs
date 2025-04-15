using System;
using System.Drawing;
using System.Windows.Forms;

public class CustomProgressBar : ProgressBar
{
    public CustomProgressBar()
    {
        // Loại bỏ hiệu ứng mặc định để vẽ thủ công
        SetStyle(ControlStyles.UserPaint, true);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Rectangle rect = ClientRectangle;
        e.Graphics.FillRectangle(Brushes.Gray, rect); // Màu nền thanh ProgressBar

        rect.Inflate(-3, -3); // Giảm kích thước của hình chữ nhật để tạo viền
        double percent = (double)Value / Maximum; // Tính phần trăm tiến trình

        // Màu chính của thanh tiến trình
        rect.Width = (int)(rect.Width * percent);
        e.Graphics.FillRectangle(Brushes.Red, rect); // Thay "Green" bằng màu bạn muốn
    }
}
