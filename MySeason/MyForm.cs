using System;
using System.Windows.Forms;
using MySeason.Styles;

namespace MySeason
{
    class MyForm : Form
    {
        protected readonly FormStyle style;

        protected MyForm(string seasonName)
        {
            this.SuspendLayout();

            switch (seasonName.ToLower())
            {
                case "autumn":
                    style = new AutumnFormStyle();
                    break;
                case "winter":
                    style = new WinterFormStyle();
                    break;
                case "spring":
                    style = new SpringFormStyle();
                    break;
                case "summer":
                    style = new SummerFormStyle();
                    break;
                default:
                    throw new Exception("Season name is not valid!");
            }

            InitLoadPage();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void InitLoadPage()
        {
            ////Form
            this.BackColor = style.BackColorForm;
            this.Size = style.FormSize;
            this.FormBorderStyle = style.FormBorderStyle;
            this.StartPosition = style.FormStartPosition;
            this.AutoSize = style.FormAutoSize;
            this.AutoSizeMode = style.FormAutoSizeMode;
            this.KeyPreview = true;
        }
    }
}
