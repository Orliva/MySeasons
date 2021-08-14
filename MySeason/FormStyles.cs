using System.Drawing;
using MySeason.Configuration;
using System.Windows.Forms;

namespace MySeason
{
    namespace Styles
    {
        abstract class FormStyle : CustomStyleForm
        {
            public Panel PanelHeader { get; private set; }
            public Label HeaderTextLabel { get; private set; }
            public PictureBox IconPB { get; private set; }
            public PictureBox ExitPB { get; private set; }

            public FormStyle() : base()
            {
                BackColorHeader = Color.Transparent;
                CreateHeader();
                SetSeasonStyle();
            }
            private Panel CreateHeader()
            {
                PanelHeader = new Panel
                {
                    Size = new Size(FormSize.Width, 25),
                    Location = new Point(0, 0),
                    BackColor = BackColorHeader
                };

                ExitPB = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(25, 20),
                    Location = new Point(PanelHeader.Width - 30, 3),
                    Parent = PanelHeader,
                    BackColor = Color.Transparent
                };

                IconPB = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(25, 20),
                    Location = new Point(5, 2),
                    Parent = PanelHeader,
                    BackColor = Color.Transparent
                };

                var width = FormSize.Width - (IconPB.Width + IconPB.Location.X + ExitPB.Width +
                    (FormSize.Width - ExitPB.Location.X - ExitPB.Width));
                HeaderTextLabel = new Label
                {
                    Location = new Point(0 + IconPB.Width + IconPB.Location.X, 0),
                    Size = new Size(width, 20),
                    AutoSize = false,
                    Text = "MySeasons",
                    Margin = new Padding(4, 0, 4, 0),
                    Parent = PanelHeader,
                    BackColor = Color.Transparent,
                    ForeColor = ColorText,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                PanelHeader.Controls.Add(HeaderTextLabel);
                PanelHeader.Controls.Add(ExitPB);
                PanelHeader.Controls.Add(IconPB);

                return PanelHeader;
            }
            public override void ResizeForm(Size size)
            {
                FormSize = size;
                CreateHeader();
                SetSeasonStyle();
            }
            protected override void SetSeasonStyle() { }
        }

        sealed class WinterFormStyle : FormStyle
        {
            public WinterFormStyle() : base() { }

            protected override void SetSeasonStyle()
            {
                IconPB.Image = Properties.Resources.Seadog2;
                ExitPB.Image = Properties.Resources.Closes;
                SeasonStr = Config.WinterStr;
                ColorText = Color.Black;
                BackColorForm = Color.FromArgb(202, 240, 248);
                BackColorHeader = Color.FromArgb(190, 18, 78, 120);
            }
        }

        sealed class AutumnFormStyle : FormStyle
        {
            public AutumnFormStyle() : base() { }

            protected override void SetSeasonStyle()
            {
                IconPB.Image = Properties.Resources.CloudAutumn;
                ExitPB.Image = Properties.Resources.Closes;
                SeasonStr = Config.AutumnStr;
                ColorText = Color.White;
                BackColorForm = Color.FromArgb(25, 25, 25);
                BackColorHeader = Color.FromArgb(110, 110, 110);
            }
        }

        sealed class SummerFormStyle : FormStyle
        {
            public SummerFormStyle() : base() { }

            protected override void SetSeasonStyle()
            {
                IconPB.Image = Properties.Resources.Sun;
                ExitPB.Image = Properties.Resources.Closes;
                SeasonStr = Config.SummerStr;
                ColorText = Color.Black;
                BackColorForm = Color.FromArgb(42, 157, 143);
                BackColorHeader = Color.FromArgb(170, 110, 110, 110);
            }
        }

        sealed class SpringFormStyle : FormStyle
        {
            public SpringFormStyle() : base() { }

            protected override void SetSeasonStyle()
            {
                IconPB.Image = Properties.Resources.Tree;
                ExitPB.Image = Properties.Resources.Closes;
                SeasonStr = Config.SpringStr;
                ColorText = Color.Black;
                BackColorForm = Color.FromArgb(148, 210, 189);
                BackColorHeader = Color.FromArgb(124, 174, 122);
            }
        }
    }
}
