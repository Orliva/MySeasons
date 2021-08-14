using System.Windows.Forms;
using System.Drawing;
using MySeason.SeasonEnum;

namespace MySeason
{
    namespace MessageBox
    {
        class MyMessageBox : MyForm
        {
            #region Variable
            private readonly Label labelText;
            private readonly Panel headerPanel;
            private PictureBox messageIcon; ///Иконка MessageBox'a

            private Button buttonOK = null;
            private Button buttonCancel = null;
            private Button buttonRetry = null;
            private Button buttonAbort = null;
            private Button buttonIgnore = null;
            private Button buttonYes = null;
            private Button buttonNo = null;
            #endregion

            #region Ctor
            private MyMessageBox(string season, string capture, string text, My_TypeButton typeButton, My_TypeIcon typeIcon)
                : base(season)
            {
                SuspendLayout();

                /////Form
                style.ResizeForm(new Size(270, 123));
                this.Size = style.FormSize;
                AcceptButton = buttonOK;
                CancelButton = buttonCancel;
                style.HeaderTextLabel.Text = capture;
                headerPanel = style.PanelHeader;

                /////Label
                labelText = new Label
                {
                    AutoSize = true,
                    Location = new Point(9, 30),
                    Text = text,
                    AutoEllipsis = true,
                    ForeColor = style.ColorText,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                /////Button
                SwitchButtonSet(typeButton, new Point(57, 87), new Size(75, 23), style.BackColorHeader);

                /////PictureBox
                SwitchIconSet(typeIcon, new Point(4, 60), new Size(28, 28), style.BackColorHeader);

                /////Controls
                this.Controls.Add(buttonCancel);
                this.Controls.Add(buttonOK);
                this.Controls.Add(labelText);
                this.Controls.Add(headerPanel);

                ResumeLayout(false);
                PerformLayout();
            }
            #endregion

            #region Interface
            /// <summary>
            /// Вызов модального окна для вывода сообщения
            /// </summary>
            /// <param name="season">Стиль</param>
            /// <param name="capture">Заголовок</param>
            /// <param name="text">Сообщение</param>
            /// <param name="typeButton">Тип кнопки</param>
            /// <param name="typeIcon">Тип иконки</param>
            /// <returns></returns>
            public static DialogResult ShowMessage(string season, string capture, string text, My_TypeButton typeButton, My_TypeIcon typeIcon)
            {
                return new MyMessageBox(season, capture, text, typeButton, typeIcon).ShowDialog();
            }
            #endregion

            #region Implementation

            /// <summary>
            /// Выбираем какой вид иконки нужно создать
            /// </summary>
            /// <param name="typeIcon"></param>
            /// <param name="location"></param>
            /// <param name="size"></param>
            /// <param name="clr"></param>
            private void SwitchIconSet(My_TypeIcon typeIcon, Point location, Size size, Color clr)
            {
                switch (typeIcon)
                {
                    case My_TypeIcon.Critycal:
                        {
                            messageIcon = CreateIcon(location, size, Properties.Resources.my_crytical, clr);
                            this.Controls.Add(messageIcon);
                            break;
                        }
                    case My_TypeIcon.Inform:
                        {
                            messageIcon = CreateIcon(location, size, Properties.Resources.my_info, clr);
                            this.Controls.Add(messageIcon);
                            break;
                        }
                    case My_TypeIcon.None:
                    default:
                        break;
                }
            }

            /// <summary>
            /// Создаем PictureBox для нужной иконки
            /// </summary>
            /// <param name="location"></param>
            /// <param name="size"></param>
            /// <param name="img"></param>
            /// <param name="clr"></param>
            /// <returns></returns>
            private PictureBox CreateIcon(Point location, Size size, Image img, Color clr)
            {
                return new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = size,
                    Location = location,
                    Image = img,
                    BackColor = clr
                };
            }

            /// <summary>
            /// Выбираем какой набор кнопок создать
            /// </summary>
            /// <param name="typeButton"></param>
            /// <param name="location"></param>
            /// <param name="size"></param>
            /// <param name="clr"></param>
            private void SwitchButtonSet(My_TypeButton typeButton, Point location, Size size, Color clr)
            {
                switch (typeButton)
                {
                    case My_TypeButton.Ok_Cancel:
                        {
                            buttonOK = CreateButton(DialogResult.OK, location, size, clr);
                            buttonCancel = CreateButton(DialogResult.Cancel, new Point(location.X + 81, 87), size, clr);
                            this.Controls.Add(buttonOK);
                            this.Controls.Add(buttonCancel);
                            break;
                        }
                    case My_TypeButton.Abort_Retry:
                        {
                            buttonAbort = CreateButton(DialogResult.Abort, location, size, clr);
                            buttonRetry = CreateButton(DialogResult.Retry, new Point(location.X + 81, 87), size, clr);
                            this.Controls.Add(buttonAbort);
                            this.Controls.Add(buttonRetry);
                            break;
                        }
                    case My_TypeButton.Abort_Ignore:
                        {
                            buttonAbort = CreateButton(DialogResult.Abort, location, size, clr);
                            buttonIgnore = CreateButton(DialogResult.Ignore, new Point(location.X + 81, 87), size, clr);
                            this.Controls.Add(buttonAbort);
                            this.Controls.Add(buttonIgnore);
                            break;
                        }
                    case My_TypeButton.Ok_Cancel_Abort:
                        {
                            buttonOK = CreateButton(DialogResult.OK, new Point(7, 87), size, clr);
                            buttonCancel = CreateButton(DialogResult.Cancel, new Point(98, 87), size, clr);
                            buttonAbort = CreateButton(DialogResult.Abort, new Point(188, 87), size, clr);
                            this.Controls.Add(buttonOK);
                            this.Controls.Add(buttonCancel);
                            this.Controls.Add(buttonAbort);
                            break;
                        }
                    case My_TypeButton.Yes_No:
                        {
                            buttonYes = CreateButton(DialogResult.Yes, location, size, clr);
                            buttonNo = CreateButton(DialogResult.No, new Point(location.X + 81, 87), size, clr);
                            this.Controls.Add(buttonYes);
                            this.Controls.Add(buttonNo);
                            break;
                        }
                    case My_TypeButton.OK:
                        {
                            buttonOK = CreateButton(DialogResult.OK, new Point((this.Size.Width / 2) - (size.Width / 2), 87), size, clr);
                            this.Controls.Add(buttonOK);
                            break;
                        }
                    case My_TypeButton.None:
                    default:
                        break;
                }
            }

            /// <summary>
            /// Создаем кнопку
            /// </summary>
            /// <param name="dialogRes"></param>
            /// <param name="location"></param>
            /// <param name="size"></param>
            /// <param name="clr"></param>
            /// <returns></returns>
            private Button CreateButton(DialogResult dialogRes, Point location, Size size, Color clr)
            {
                return new Button
                {
                    DialogResult = dialogRes,
                    Location = location,
                    Size = size,
                    Text = dialogRes.ToString(),
                    FlatStyle = FlatStyle.Popup,
                    BackColor = clr,
                    UseVisualStyleBackColor = true
                };
            }
            #endregion
        }
    }
}
