using System.Drawing;

namespace MySeason
{
    namespace Styles
    {
        abstract class LoadPageStyle : CustomStyleForm
        {
            public LoadPageStyle() : base()
            {
                BackColorHeader = Color.Transparent;
                FormSize = new Size(290, 500);
                SetSeasonStyle();
            }
            public override void ResizeForm(Size size) { }
        }

        sealed class WinterLoadPageStyle : LoadPageStyle
        {
            public WinterLoadPageStyle() : base() { }

            protected override void SetSeasonStyle()
            {
                ColorText = Color.Black;
                BackColorForm = Color.FromArgb(202, 240, 248);
            }
        }

        sealed class AutumnLoadPageStyle : LoadPageStyle
        {
            public AutumnLoadPageStyle() : base() { }

            protected override void SetSeasonStyle()
            {
                ColorText = Color.White;
                BackColorForm = Color.FromArgb(25, 25, 25);
            }
        }

        sealed class SummerLoadPageStyle : LoadPageStyle
        {
            public SummerLoadPageStyle() : base() { }

            protected override void SetSeasonStyle()
            {
                ColorText = Color.Black;
                BackColorForm = Color.FromArgb(42, 157, 143);
            }
        }

        sealed class SpringLoadPageStyle : LoadPageStyle
        {
            public SpringLoadPageStyle() : base() { }

            protected override void SetSeasonStyle()
            {
                ColorText = Color.Black;
                BackColorForm = Color.FromArgb(148, 210, 189);
            }
        }
    }
}
