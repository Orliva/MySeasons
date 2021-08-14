using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MySeason
{
    namespace Styles
    {
        abstract class CustomStyleForm : IColorStyleForm
        {
            [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
            protected static extern IntPtr CreateRoundRectRgn(int left, int top, int right,
                                                       int bottom, int width, int height);
            public Font Font { get; protected set; }
            public Color ColorText { get; protected set; }
            public Color BackColorForm { get; protected set; }
            public Color BackColorHeader { get; protected set; }

            private Size formSize;
            public Size FormSize
            {
                get
                {
                    return formSize;
                }
                set
                {
                    formSize = value;
                    FormRegion = Region.FromHrgn(CreateRoundRectRgn(0, 0, formSize.Width, formSize.Height, 6, 6));
                }
            }

            public FormBorderStyle FormBorderStyle { get; protected set; }
            public FormStartPosition FormStartPosition { get; protected set; }
            public Region FormRegion { get; protected set; }
            public AutoSizeMode FormAutoSizeMode { get; protected set; }
            public bool FormAutoSize { get; protected set; }
            public string SeasonStr { get; protected set; }

            protected CustomStyleForm()
            {
                Font = new Font("Segoe MDL2 Assets", 10, FontStyle.Regular);
                FormBorderStyle = FormBorderStyle.None;
                FormStartPosition = FormStartPosition.CenterScreen;
                FormAutoSize = true;
                FormAutoSizeMode = AutoSizeMode.GrowAndShrink;
            }
            protected abstract void SetSeasonStyle();
            public abstract void ResizeForm(Size size);
        }
    }
}
