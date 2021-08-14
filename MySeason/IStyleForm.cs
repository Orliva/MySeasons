using System.Drawing;

namespace MySeason
{
    namespace Styles
    {
        /// <summary>
        /// Минимальные цветовые части формы
        /// </summary>
        interface IColorStyleForm
        {
            Color BackColorForm { get; }
            Color BackColorHeader { get; }
            Color ColorText { get; }
        }
    }
}
