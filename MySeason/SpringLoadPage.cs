using System;
using System.Drawing;
using System.Windows.Forms;

namespace MySeason
{
    namespace LoadPage
    {
        /// <summary>
        /// Загрузочная форма "Весна"
        /// </summary>
        sealed class SpringLoadPage : LoadingPage
        {
            #region Variable
            private readonly PictureBox cloudSpring;
            private readonly PictureBox puddle;
            private readonly PictureBox tree;
            private readonly Panel panelForAnimation;
            private readonly Panel panelForLoadBar;
            private readonly Panel panelForTree;

            private readonly PictureBox[] blobs; //Капли
            private readonly Timer animationTimer;
            private readonly Timer timer_2; //Заглушка

            private readonly int countBlobs;
            private readonly int[] rainSpeed;
            private readonly int loadingSpeedPuddle; //Заглушка для загрузочной полоски
            #endregion

            #region Ctor
            public SpringLoadPage() : base("Spring")
            {
                SuspendLayout();

                countBlobs = 8;
                loadingSpeedPuddle = 3;
                rainSpeed = new[] { 2, 3, 1, 3, 4, 2, 1, 3 };

                //////CloudSpring
                cloudSpring = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(150, 90),
                    Location = new Point(60 + offsetX, 40 + offsetY),
                    Image = Properties.Resources.CloudSpring,
                    BackColor = Color.Transparent
                };

                //////Tree
                tree = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(250, 210),
                    Location = new Point(0, 0),
                    Parent = panelForTree,
                    Image = Properties.Resources.Tree,
                    BackColor = Color.Transparent
                };

                //////Puddle
                puddle = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Region = Region.FromHrgn(CreateRoundRectRgn(0, 30, 200, 62, 0, 0)),
                    Size = new Size(135, 93),
                    Location = new Point(60 + offsetX, 360 + offsetY),
                    Image = Properties.Resources.PuddleSpring,
                    BackColor = Color.Transparent
                };

                /////Panel
                panelForAnimation = new Panel
                {
                    Size = new Size(150, 55),
                    Location = new Point(50 + offsetX, 115 + offsetY),
                    BackColor = Color.Transparent
                };

                panelForLoadBar = new Panel
                {
                    Region = Region.FromHrgn(CreateRoundRectRgn(0, 30, 200, 62, 0, 0)),
                    Size = new Size(135, 93),
                    Location = new Point(60 + offsetX, 360 + offsetY),
                    BackColor = Color.Transparent
                };

                panelForTree = new Panel
                {
                    Size = new Size(250, 210),
                    Location = new Point(5 + offsetX, 170 + offsetY),
                    BackColor = Color.Transparent
                };

                labelProcentege.Location = new Point(0, 417 + offsetY); //Меняем дефолтное положение label'a

                /////Blobs 
                blobs = new PictureBox[countBlobs];
                for (int i = 0; i < countBlobs; i++)
                    blobs[i] = new PictureBox();

                BlobInit(0, new Size(7, 39), new Point(23, 3));
                BlobInit(1, new Size(9, 50), new Point(53, 28));
                BlobInit(2, new Size(5, 27), new Point(40, 27));
                BlobInit(3, new Size(6, 22), new Point(73, 15));
                BlobInit(4, new Size(8, 36), new Point(85, 35));
                BlobInit(5, new Size(4, 20), new Point(97, 14));
                BlobInit(6, new Size(7, 24), new Point(113, 28));
                BlobInit(7, new Size(4, 39), new Point(129, 8));

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
                this.Controls.Add(panelForLoadBar);
                this.Controls.Add(puddle);
                this.Controls.Add(cloudSpring);
                this.Controls.Add(panelForAnimation);
                this.Controls.Add(panelForTree);
                this.Controls.Add(labelProcentege);
                panelForTree.Controls.Add(tree);

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
                blobs[index].Image = Properties.Resources.RainSpring;
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
