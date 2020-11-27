using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventArgs
{
    public partial class MainForm : Form
    {
        bool isPressed;
        bool PictureMove;

        string KeyCode = "";

        int virtual_x = Screen.PrimaryScreen.WorkingArea.Width;
        int virtual_y = Screen.PrimaryScreen.WorkingArea.Height;

        public MainForm()
        {
            InitializeComponent();
        }

        void Form1_Load(object sender, System.EventArgs e)
        {
            MouseWheel += OpacityScroll;
            MouseWheel += DeltaMove;
            KeyDown += FormMove;
            KeyDown += ChangeFormSize;
            KeyUp += ControllerMoveUp;
            KeyDown += ControllerMoveDown;

            MessageBox.Show(
                "WASD + Scroll     - Переместить картинку\n\n" +
                "Shift + Page      - Переместить форму\n\n" +
                "Ctrl  + Page      - Увеличить форму\n\n" +
                "Alt   + Page      - Уменьшить форму\n\n" +
                "Scroll            - Изменить прозрачность\n\n" +
                "Left Click        - Текущая дата\n\n" +
                "Right Click       - Текущее время\n\n" +
                "Double Left Click - Сменить цвет\n\n" +
                "F1                - Развернуть окно\n\n" +
                "F2                - Восстановить окно\n\n" +
                "F3                - Свернуть форму",
                "Прочтите!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // Меняем прозрачность прокруткой
        void OpacityScroll(object sender, MouseEventArgs e)
        {
            double xOpacity = Math.Round(Opacity, 1);
            OpacityValueTextBox.Text = Convert.ToString(xOpacity);

            if (PictureMove == false)
            {
                if (e.Delta == 120)
                {
                    xOpacity += 0.1;
                    if (xOpacity <= 1)
                    {
                        Opacity += 0.1;
                    }
                }
                else if (e.Delta == -120)
                {
                    xOpacity -= 0.1;
                    if (xOpacity >= 0.2)
                    {
                        Opacity -= 0.1;
                    }
                }
            }
        }

        // Двигаем форму
        void FormMove(object sender, KeyEventArgs e)
        {
            KeyCode = Convert.ToString(e.KeyData);
            KeyCodeTextBox.Text = KeyCode;

            // Двигаем форму вверх
            if (Convert.ToString(KeyCode) == "Up, Shift")
            {
                if (Location.Y > 0)
                {
                    Location = new Point(Left, Top - 4);
                }
            }
            // Двигаем форму вниз
            else if (Convert.ToString(KeyCode) == "Down, Shift")
            {
                if (Location.Y < (virtual_y - Height))
                {
                    Location = new Point(Left, Top + 4);
                }
            }
            // Двигаем форму влево
            else if (Convert.ToString(KeyCode) == "Left, Shift")
            {
                if (Location.X > 0)
                {
                    Location = new Point(Left - 4, Top);
                }
            }
            // Двигаем форму вправо
            else if (Convert.ToString(KeyCode) == "Right, Shift")
            {
                if (Location.X < (virtual_x - Width))
                {
                    Location = new Point(Left + 4, Top);
                }
            }

            else if (Convert.ToString(KeyCode) == "F1")
            {
                WindowState = FormWindowState.Maximized;
            }

            else if (Convert.ToString(KeyCode) == "F2")
            {
                WindowState = FormWindowState.Normal;
            }

            else if (Convert.ToString(KeyCode) == "F3")
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        // Двигаем картинку
        void DeltaMove(object sender, MouseEventArgs wheel)
        {
            if (isPressed == true)
            {
                // Двигаем картнку вверх
                if (KeyCode == "W" && wheel.Delta == 120)
                {
                    if (PictureBox.Location.Y > 0)
                    {
                        PictureBox.Location = new Point(PictureBox.Left, PictureBox.Top - 4);
                    }
                }
                // Двигаем картинку вниз
                else if (KeyCode == "S" && wheel.Delta == -120)
                {
                    if (PictureBox.Location.Y < (WorkSpace.Height - PictureBox.Height))
                    {
                        PictureBox.Location = new Point(PictureBox.Left, PictureBox.Top + 4);
                    }
                }
                // Двигаем картинку влево
                else if (KeyCode == "A" && wheel.Delta == 120)
                {
                    if (PictureBox.Location.X > 0)
                    {
                        PictureBox.Location = new Point(PictureBox.Left - 4, PictureBox.Top);
                    }
                }
                // Двигаем картинку вправо
                else if (KeyCode == "D" && wheel.Delta == -120)
                {
                    if (PictureBox.Location.X < (WorkSpace.Width - PictureBox.Width))
                    {
                        PictureBox.Location = new Point(PictureBox.Left + 4, PictureBox.Top);
                    }
                }
            }
        }

        void ChangeFormSize(object sender, KeyEventArgs e)
        {

            // Увеличиваем
            if (KeyCode == "Up, Control" && Location.Y > 0)
            {
                Size = new Size(Width, Height + 10);
                Location = new Point(Location.X, Location.Y - 10);
            }

            else if (KeyCode == "Down, Control" && Location.Y < (virtual_y - Height))
            {
                Size = new Size(Width, Height + 10);
            }

            else if (KeyCode == "Left, Control" && Location.X > 0)
            {
                Size = new Size(Width + 10, Height);
                Location = new Point(Location.X - 10, Location.Y);
            }

            else if (KeyCode == "Right, Control" && Location.X < (virtual_x - Width))
            {
                Size = new Size(Width + 10, Height);
            }

            // Уменьшаем
            if (KeyCode == "Up, Alt")
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    WindowState = FormWindowState.Normal;
                    Size = new Size(virtual_x - 10, virtual_y - 10);
                    CenterToScreen();
                }
                Size = new Size(Width, Height - 10);
                
            }

            else if (KeyCode == "Down, Alt")
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    WindowState = FormWindowState.Normal;
                    Size = new Size(virtual_x - 10, virtual_y - 10);
                    CenterToScreen();
                }
                Size = new Size(Width, Height - 10);
                if (Size.Width > MinimumSize.Width && 
                    Size.Height > MinimumSize.Height)
                {
                    Location = new Point(Location.X, Location.Y + 10);
                }
            }

            else if (KeyCode == "Left, Alt")
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    WindowState = FormWindowState.Normal;
                    Size = new Size(virtual_x - 10, virtual_y - 10);
                    CenterToScreen();
                }
                Size = new Size(Width - 10, Height);
            }

            else if (KeyCode == "Right, Alt")
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    WindowState = FormWindowState.Normal;
                    Size = new Size(virtual_x - 10, virtual_y - 10);
                    CenterToScreen();
                }
                Size = new Size(Width - 10, Height);
                if (Size.Width > MinimumSize.Width 
                    && Size.Height > MinimumSize.Height)
                {
                    Location = new Point(Location.X + 10, Location.Y);
                }
            }
        }

        // Отпустили клавишу
        void ControllerMoveUp(object sender, KeyEventArgs e)
        {
            // Отключается событие DeltaMove
            isPressed = false;
            // Теперь можно менять прозрачность формы
            PictureMove = false;
        }

        // Нажали клавишу
        void ControllerMoveDown(object sender, KeyEventArgs e)
        {
            // Отключается событие DeltaMove
            isPressed = true;
            // Пока двигаем картинку, прозрачность не меняем
            PictureMove = true;
        }

        void WorkSpaceDoubleClick(object sender, System.EventArgs e)
        {
            Random rand = new Random();
            int r = rand.Next(0, 255);
            int g = rand.Next(0, 255);
            int b = rand.Next(0, 255);
            BackColor = Color.FromArgb(r, g, b);
            RgbCodeTextBox.Text = "RGB: (" + r + ", " + g + ", " + b + ")";
        }

        void WorkSpace_MouseMove(object sender, MouseEventArgs e)
        {
            CursorPositionTextBox.Text = "X: " + e.X + " ,Y: " + e.Y; 
        }

        void WorkSpace_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CurrentDateTextBox.Text = DateTime.Today.ToString("D");
            }
            else if (e.Button == MouseButtons.Right)
            {
                CurrentTimeTextBox.Text = DateTime.Now.ToString("t");
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Вы точно уверены, что хотите выйти?",
                "Внимание!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
