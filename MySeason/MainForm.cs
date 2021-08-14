using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Threading;
using MySeason.SeasonEnum;
using MySeason.MessageBox;
using MySeason.InputBox;

namespace MySeason
{
    /// <summary>
    /// Часть класса для объявления и инициализации переменных
    /// </summary>
    partial class MainForm : MyForm
    {
        #region Variable
        public readonly GroupBox firstGroup;
        public readonly GroupBox secondGroup;
        public readonly Button leftSideBtn;
        public readonly Button rightSideBtn;
        public readonly Button startCalculationBtn;
        public readonly Button shockedStopBtn;
        public readonly Label resultLabel;
        public readonly Label nothingLabel;
        public readonly Label functionLabel;
        public readonly Label leftLabel;
        public readonly Label rightLabel;
        public readonly Label resultValueLable;
        public readonly ProgressBar progressBar;
        private readonly BackgroundWorker backgroundWorker;
        private readonly Panel headerPanel;

        private double x_start;
        private double x_end;
        private double integral;
        private readonly int N = 100;
        #endregion

        #region Ctor
        public MainForm(string season) : base(season)
        {
            this.SuspendLayout();

            /////Form
            style.ResizeForm(new Size(360, 404));
            this.Size = style.FormSize;
            this.Region = style.FormRegion;
            this.AutoSize = false;
            this.KeyDown += MainForm_KeyDown;

            /////Panel
            headerPanel = style.PanelHeader;
            this.Controls.Add(headerPanel);

            /////BackgroundWorker
            backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            backgroundWorker.DoWork += BackgroundWorker_DoWork;

            /////GroupBox
            firstGroup = new GroupBox
            {
                Margin = new Padding(4, 4, 4, 4),
                Padding = new Padding(4, 4, 4, 4),
                Location = new Point(16, 31),
                Size = new Size(325, 176),
                Text = "Working Zone",
                ForeColor = style.ColorText,
            };
            firstGroup.Controls.Add(leftSideBtn);
            firstGroup.Controls.Add(rightSideBtn);
            firstGroup.Controls.Add(shockedStopBtn);
            firstGroup.Controls.Add(progressBar);

            secondGroup = new GroupBox
            {
                Margin = new Padding(4, 4, 4, 4),
                Padding = new Padding(4, 4, 4, 4),
                Location = new Point(16, 214),
                Size = new Size(325, 172),
                Text = "Result Zone",
                ForeColor = style.ColorText
            };
            secondGroup.Controls.Add(resultLabel);
            secondGroup.Controls.Add(nothingLabel);
            secondGroup.Controls.Add(functionLabel);
            secondGroup.Controls.Add(leftLabel);
            secondGroup.Controls.Add(rightLabel);
            secondGroup.Controls.Add(resultValueLable);

            /////Button
            rightSideBtn = new Button
            {
                FlatStyle = FlatStyle.Popup,
                Location = new Point(168, 23),
                Size = new Size(148, 48),
                Text = "Задать правую границу (Double)",
                Margin = new Padding(4, 4, 4, 4),
                UseVisualStyleBackColor = true,
                Parent = firstGroup,
                BackColor = Color.Transparent
            };
            rightSideBtn.Click += RightSideBtn_Click;

            leftSideBtn = new Button
            {
                FlatStyle = FlatStyle.Popup,
                Location = new Point(8, 23),
                Size = new Size(148, 48),
                Text = "Задать левую границу (Double)",
                Margin = new Padding(4, 4, 4, 4),
                UseVisualStyleBackColor = true,
                Parent = firstGroup,
                BackColor = Color.Transparent
            };
            leftSideBtn.Click += LeftSideBtn_Click;

            shockedStopBtn = new Button
            {
                FlatStyle = FlatStyle.Popup,
                Location = new Point(168, 79),
                Size = new Size(148, 48),
                Text = "Внезапная остановка, бдыщ",
                Margin = new Padding(4, 4, 4, 4),
                UseVisualStyleBackColor = true,
                Parent = firstGroup,
                BackColor = Color.Transparent
            };
            shockedStopBtn.Click += ShockedStopBtn_Click;

            startCalculationBtn = new Button
            {
                FlatStyle = FlatStyle.Popup,
                Location = new Point(8, 79),
                Size = new Size(148, 48),
                Text = "Начать вычислять",
                Margin = new Padding(4, 4, 4, 4),
                UseVisualStyleBackColor = true,
                Parent = firstGroup,
                BackColor = Color.Transparent
            };
            startCalculationBtn.Click += StartCalculationBtn_Click1;

            /////Label
            resultLabel = new Label
            {
                Location = new Point(8, 20),
                AutoSize = true,
                Text = "Результат:",
                Margin = new Padding(4, 0, 4, 0),
                Parent = secondGroup,
                BackColor = Color.Transparent
            };

            functionLabel = new Label
            {
                Location = new Point(8, 138),
                AutoSize = true,
                Text = "Функция: y = x * x",
                Margin = new Padding(4, 0, 4, 0),
                Parent = secondGroup,
                BackColor = Color.Transparent
            };

            leftLabel = new Label
            {
                Location = new Point(164, 113),
                AutoSize = true,
                Text = "Left:",
                Margin = new Padding(4, 0, 4, 0),
                Parent = secondGroup,
                BackColor = Color.Transparent
            };

            rightLabel = new Label
            {
                Location = new Point(164, 79),
                AutoSize = true,
                Text = "Right:",
                Margin = new Padding(4, 0, 4, 0),
                Parent = secondGroup,
                BackColor = Color.Transparent
            };

            nothingLabel = new Label
            {
                Location = new Point(8, 87),
                AutoSize = true,
                Text = "Что то еще...",
                Margin = new Padding(4, 0, 4, 0),
                Parent = secondGroup,
                BackColor = Color.Transparent
            };

            resultValueLable = new Label
            {
                Location = new Point(99, 20),
                AutoSize = true,
                Text = "",
                Margin = new Padding(4, 0, 4, 0),
                Parent = secondGroup,
                BackColor = Color.Transparent
            };

            /////ProgressBar
            progressBar = new ProgressBar
            {
                Location = new Point(8, 134),
                Size = new Size(308, 28),
                Margin = new Padding(4, 4, 4, 4),
                Parent = firstGroup
            };

            this.Controls.Add(firstGroup);
            this.Controls.Add(secondGroup);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }

    /// <summary>
    /// Часть класса для логики
    /// </summary>
    partial class MainForm
    {
        #region Methods

        /// <summary>
        /// Считаем интеграл в фоновой операции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DialogResult dlg_res;
            double res;
            double delta_x;
            double x;
            int i;

            x = x_start;
            integral = 0;
            delta_x = Math.Abs((x_start - x_end)) / N;
            res = 0;
            i = 0;
            try
            {
                while (i < N)
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        backgroundWorker.ReportProgress(0);
                        return;
                    }
                    res += F(x);
                    x += delta_x;
                    if (double.IsInfinity(res) || double.IsInfinity(x))
                        throw new Exception("Ошибка переполнение типа");
                    backgroundWorker.ReportProgress((int)((double)i / (double)N * 100));
                    Thread.Sleep(100); ////Тормозим процесс, для долгих процессов можно убирать
                    i++;
                }
                res *= delta_x;
                if (double.IsInfinity(res))
                    throw new Exception("Ошибка переполнение типа");
            }
            catch (Exception ex)
            {
                dlg_res = MyMessageBox.ShowMessage(style.SeasonStr, "Message_error", ex.Message,
                           My_TypeButton.Abort_Ignore, My_TypeIcon.Critycal);
                if (dlg_res == DialogResult.Abort)
                    backgroundWorker.CancelAsync();
            }
            integral = res;
        }

        /// <summary>
        /// Отрисовываем проценты выполнения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            startCalculationBtn.Text = e.ProgressPercentage.ToString() + "%";
        }

        /// <summary>
        /// Действие по завершению фоновой операции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                shockedStopBtn.Text = "Oxx... Here we go again!";
            startCalculationBtn.Text = "Начать вычислять";
            progressBar.Value = 0;
            resultValueLable.Text = integral.ToString();
        }

        /// <summary>
        /// Пробуем запустить фоновую операцию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartCalculationBtn_Click1(object sender, EventArgs e)
        {
            if (!backgroundWorker.IsBusy)
            {
                shockedStopBtn.Text = "Внезапная остановка, бдыщ";
                if (x_start < x_end)
                    backgroundWorker.RunWorkerAsync();
                else
                    MyMessageBox.ShowMessage(style.SeasonStr, "Месдж бокзс", "Неверно задан предел итегрирования!\n",
                        My_TypeButton.OK, My_TypeIcon.Inform);
            }
        }

        /// <summary>
        /// Отменяем фоновую операцию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShockedStopBtn_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
                backgroundWorker.CancelAsync();
        }

        /// <summary>
        /// Задаем значение левой границе через MyInputBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftSideBtn_Click(object sender, EventArgs e)
        {
            if (MyInputBox.ShowInputBox(style.SeasonStr, "Инпьють бокз", "Введите число типа Double", out x_start) == DialogResult.OK)
                leftLabel.Text = "Left: " + x_start.ToString();
        }

        /// <summary>
        /// Задаем значение правой границе через MyInputBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightSideBtn_Click(object sender, EventArgs e)
        {
            if (MyInputBox.ShowInputBox(style.SeasonStr, "Инпьють бокз", "Введите число типа Double", out x_end) == DialogResult.OK)
                rightLabel.Text = "Right: " + x_end.ToString();
        }

        /// <summary>
        /// Закрываем форму на клавишу (Esc)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keys)e.KeyCode == Keys.Escape)
                this.Close();
        }

        /// <summary>
        /// Функция для интегрирования
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double F(double x) => x * x;
        #endregion
    }
}
