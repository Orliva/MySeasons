using System;
using System.Drawing;
using System.Windows.Forms;

namespace MySeason
{
    namespace LoadPage
    {
        /// <summary>
        /// Загрузочная форма "Осень"
        /// </summary>
        sealed class AutumnLoadPage : LoadingPage
        {
            #region Variable
            private readonly PictureBox puddle;
            private readonly PictureBox cloud;
            private readonly Panel panelForAnimation;
            private readonly Panel panelForLoadBar;

            private readonly PictureBox[] blobs; //Капли
            private readonly Timer animationTimer;
            private readonly Timer timer_2; //Заглушка

            private readonly int countBlobs;
            private readonly int[] rainSpeed;
            private readonly int loadingSpeedPuddle; //Заглушка для загрузочной полоски
            #endregion

            #region Ctor
            public AutumnLoadPage() : base("Autumn")
            {
                SuspendLayout();

                countBlobs = 8;
                loadingSpeedPuddle = 3;
                rainSpeed = new[] { 4, 5, 2, 4, 6, 3, 2, 5 };

                //////Cloud
                cloud = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(180, 90),
                    Location = new Point(40 + offsetX, 110 + offsetY),
                    Image = Properties.Resources.CloudAutumn,
                    BackColor = Color.Transparent
                };

                //////Puddle
                puddle = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Region = Region.FromHrgn(CreateRoundRectRgn(0, 30, 200, 62, 0, 0)),
                    Size = new Size(150, 90),
                    Location = new Point(40 + offsetX, 270 + offsetY),
                    Image = Properties.Resources.PuddleAutumn,
                    BackColor = Color.Transparent
                };

                /////Panel
                panelForAnimation = new Panel
                {
                    Size = new Size(150, 100),
                    Location = new Point(40 + offsetX, 199 + offsetY),
                    BackColor = Color.Transparent
                };

                panelForLoadBar = new Panel
                {
                    Region = Region.FromHrgn(CreateRoundRectRgn(0, 30, 200, 62, 0, 0)),
                    Size = new Size(150, 90),
                    Location = new Point(40 + offsetX, 270 + offsetY),
                    BackColor = Color.Transparent
                };

                /////Blobs 
                blobs = new PictureBox[countBlobs];
                for (int i = 0; i < countBlobs; i++)
                    blobs[i] = new PictureBox();

                BlobInit(0, new Size(15, 39), new Point(23, 3));
                BlobInit(1, new Size(15, 50), new Point(53, 28));
                BlobInit(2, new Size(10, 27), new Point(40, 27));
                BlobInit(3, new Size(10, 22), new Point(73, 15));
                BlobInit(4, new Size(10, 36), new Point(85, 35));
                BlobInit(5, new Size(15, 20), new Point(97, 14));
                BlobInit(6, new Size(10, 24), new Point(113, 28));
                BlobInit(7, new Size(15, 39), new Point(129, 8));

                ////Timer
                animationTimer = new Timer
                {
                    Interval = 30
                };
                animationTimer.Tick += AnimationTimerTick;

                //Заглушка для имитации загрузки
                timer_2 = new Timer
                {
                    Interval = 300
                };
                timer_2.Tick += Timer_Tick_2;

                ////Controls
                this.Controls.Add(cloud);
                this.Controls.Add(panelForLoadBar);
                this.Controls.Add(puddle);
                this.Controls.Add(panelForAnimation);
                this.Controls.Add(labelProcentege);

                this.PerformLayout();
            }
            #endregion

            #region ClassInterface
            /// <summary>
            /// Запуск загрузочной страницы
            /// Не использовать ShowDialog()!
            /// </summary>
            /// <returns></returns>
            public override DialogResult LoadShowDialog()
            {
                LoadAnimationStart();
                timer_2.Start();
                return ShowDialog();
            }

            /// <summary>
            /// Запускает таймер, который отвечает за анимацию дождика
            /// </summary>
            protected override void LoadAnimationStart()
            {
                animationTimer.Start();
            }
            #endregion

            #region Implementation
            /// <summary>
            /// Инициализируем капли дождя
            /// </summary>
            /// <param name="index">Индекс капли в массиве</param>
            /// <param name="size">Размер капли</param>
            /// <param name="point">Расположение капли</param>
            private void BlobInit(int index, Size size, Point point)
            {
                blobs[index].SizeMode = PictureBoxSizeMode.Zoom;
                blobs[index].Size = size;
                blobs[index].Location = point;
                blobs[index].Image = Properties.Resources.RainAutumn;
                blobs[index].Parent = panelForAnimation;
                blobs[index].BackColor = Color.Transparent;
            }

            /// <summary>
            /// Отрисовка падения дождика
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void AnimationTimerTick(object sender, EventArgs e)
            {
                for (int i = 0; i < 8; i++)
                {
                    blobs[i].Location = new Point(blobs[i].Location.X, blobs[i].Location.Y + rainSpeed[i]);
                    if (blobs[i].Location.Y > blobs[i].Size.Height + panelForAnimation.Height)
                        blobs[i].Location = new Point(blobs[i].Location.X, 0 - blobs[i].Location.Y);
                }
            }

            /// <summary>
            /// Заглушка для отрисовка лужи и label'a с процентами
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void Timer_Tick_2(object sender, EventArgs e)
            {
                double proc;

                initProcentege += loadingSpeedPuddle;
                panelForLoadBar.Location = new Point(panelForLoadBar.Location.X + loadingSpeedPuddle, panelForLoadBar.Location.Y);
                proc = (double)initProcentege / (double)puddle.Width * 100;
                labelProcentege.Text = ((int)proc).ToString() + "%";
                if (panelForLoadBar.Location.X > puddle.Width + puddle.Location.X)
                {
                    timer_2.Stop();
                    labelProcentege.Text = "100%";
                    System.Threading.Thread.Sleep(500);//// delete
                    this.Close();
                }
            }
            #endregion
        }
    }
}