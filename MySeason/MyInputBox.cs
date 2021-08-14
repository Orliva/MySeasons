using System;
using System.Windows.Forms;
using System.Drawing;
using MySeason.SeasonEnum;
using MySeason.MessageBox;

namespace MySeason
{
    namespace InputBox
    {
        class MyInputBox : MyForm
        {
            #region Variable
            private static double valueX;
            private readonly Label messageLabel;
            private readonly TextBox valueTextBox;
            private readonly Button buttonOK;
            private readonly Button buttonCancel;
            private readonly Panel headerPanel;
            #endregion

            #region Ctor
            private MyInputBox(string season, string capture, string text) : base(season)
            {
                this.SuspendLayout();

                /////Form
                style.ResizeForm(new Size(270, 123));
                this.Size = style.FormSize;
                this.Region = style.FormRegion;
                this.AcceptButton = this.buttonOK;
                this.CancelButton = this.buttonCancel;
                style.HeaderTextLabel.Text = capture;
                headerPanel = style.PanelHeader;

                /////Label
                messageLabel = new Label
                {
                    AutoSize = true,
                    Location = new Point(9, 30),
                    Text = text,
                    ForeColor = style.ColorText,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                /////TextBox
                valueTextBox = new TextBox
                {
                    Location = new Point(12, 51),
                    Size = new Size(245, 20),
                    WordWrap = false
                };

                ///Button
                buttonOK = new Button
                {
                    DialogResult = DialogResult.OK,
                    Location = new Point(57, 87),
                    Size = new Size(75, 23),
                    Text = DialogResult.OK.ToString(),
                    FlatStyle = FlatStyle.Popup,
                    BackColor = style.BackColorHeader,
                    UseVisualStyleBackColor = true
                };
                buttonOK.Click += ButtonOK_Click;

                buttonCancel = new Button
                {
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(138, 87),
                    Size = new Size(75, 23),
                    Text = DialogResult.Cancel.ToString(),
                    FlatStyle = FlatStyle.Popup,
                    BackColor = style.BackColorHeader,
                    UseVisualStyleBackColor = true
                };
                buttonCancel.Click += ButtonCancel_Click;

                ///Controls
                this.Controls.Add(this.buttonCancel);
                this.Controls.Add(this.buttonOK);
                this.Controls.Add(this.valueTextBox);
                this.Controls.Add(this.messageLabel);
                this.Controls.Add(headerPanel);

                this.ResumeLayout(false);
                this.PerformLayout();
            }
            #endregion

            #region Interface
            /// <summary>
            /// Показываем модальное окно для ввода значения
            /// </summary>
            /// <param name="season">Стиль</param>
            /// <param name="capture">Заголовок</param>
            /// <param name="text">Сообщение</param>
            /// <param name="value">Выходное значение</param>
            /// <returns></returns>
            public static DialogResult ShowInputBox(string season, string capture, string text, out double value)
            {
                value = default;
                valueX = value;
                DialogResult dialRes = new MyInputBox(season, capture, text).ShowDialog();
                value = valueX;

                return dialRes;
            }
            #endregion

            #region Implemention
            private void ButtonCancel_Click(object sender, EventArgs e) => this.Close();

            private void ButtonOK_Click(object sender, EventArgs e)
            {
                if (TryInputValue(valueTextBox.Text))
                    this.Close();
            }

            private bool TryInputValue(string value)
            {
                //Если строка пустая, то считаем парсинг успешным и оставляем старое значение
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    return true;

                //Пробуем парсить значение
                try
                {
                    valueX = double.Parse(value);
                    return true;
                }
                catch (Exception ex)
                {
                    MyMessageBox.ShowMessage(style.SeasonStr, "Месдж бокзс", "Требуется ввести число типа Double!\n" + ex.Message,
                        My_TypeButton.OK, My_TypeIcon.Inform);
                    return false;
                }
            }
            #endregion
        }
    }
}
