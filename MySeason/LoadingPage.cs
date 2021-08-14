using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using MySeason.Styles;

namespace MySeason
{
    abstract class LoadingPage : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        protected static extern IntPtr CreateRoundRectRgn(int left, int top, int right,
                                                           int bottom, int width, int height);

        protected readonly LoadPageStyle loadStyle;
        protected Label labelProcentege;

        protected int initProcentege = 0;
        protected int offsetY = 0; //Cмещаем всю анимацию по y
        protected int offsetX = 15; //Смещаем всю анимацию по х
        protected LoadingPage(string seasonName)
        {
            this.SuspendLayout();

            switch (seasonName.ToLower())
            {
                case "autumn":
                    loadStyle = new AutumnLoadPageStyle();
                    break;
                case "winter":
                    loadStyle = new WinterLoadPageStyle();
                    break;
                case "spring":
                    loadStyle = new SpringLoadPageStyle();
                    break;
                case "summer":
                    loadStyle = new SummerLoadPageStyle();
                    break;
                default:
                    throw new Exception("Season name is not valid!");
            }

            InitLoadPage();

            this.PerformLayout();
        }

        public abstract DialogResult LoadShowDialog();
        protected abstract void LoadAnimationStart();

        private void InitLoadPage()
        {
            ////Form
            this.BackColor = loadStyle.BackColorForm;
            this.Size = loadStyle.FormSize;
            this.FormBorderStyle = loadStyle.FormBorderStyle;
            this.StartPosition = loadStyle.FormStartPosition;
            this.Region = loadStyle.FormRegion;
            this.AutoSize = loadStyle.FormAutoSize;
            this.AutoSizeMode = loadStyle.FormAutoSizeMode;

            ////Label
            labelProcentege = new Label
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Name = "label_procentege",
                Font = loadStyle.Font,
                AutoSize = false,
                Size = new Size(this.Width, 20),
                Location = new Point(0, 335 + offsetY),
                TabIndex = 0,
                ForeColor = loadStyle.ColorText
            };
        }
    }
}
