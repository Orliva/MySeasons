using System;
using System.Windows.Forms;
using System.Drawing;

namespace MySeason
{
    namespace LoadPage
    {
        /// <summary>
        /// Загрузочная форма "Лето"
        /// </summary>
        sealed class SummerLoadPage : LoadingPage
        {
            #region Variable
            ///Статические переменные для оптимизации по памяти
            static private readonly Bitmap sunTmpBitmap = new Bitmap(Properties.Resources.Sun);
            static private Bitmap emptyTmpBitmap;

            private readonly PictureBox grass;
            private readonly PictureBox sun;
            private readonly Panel panelForAnimation;
            private readonly Panel panelForLoadBar;

            private readonly Timer animationTimer;
            private readonly Timer timer_2; //Заглушка

            private float rotateAngle;
            private readonly int loadingSpeedPuddle; //Заглушка для загрузочной полоски
            #endregion

            #region Ctor
            public SummerLoadPage() : base("Summer")
            {
                SuspendLayout();

                rotateAngle = 0;
                loadingSpeedPuddle = 3;

                //////Sun
                sun = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(150, 150),
                    Location = new Point(0, 0),
                    Image = Properties.Resources.Sun,
                    Parent = panelForAnimation,
                    BackColor = Color.Transparent
                };

                //////Grass
                grass = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Region = Region.FromHrgn(CreateRoundRectRgn(0, 30, 200, 62, 0, 0)),
                    Size = new Size(150, 90),
                    Location = new Point(60 + offsetX, 270 + offsetY),
                    Image = Properties.Resources.Grass,
                    BackColor = Color.Transparent
                };

                /////Panel
                panelForAnimation = new Panel
                {
                    Size = new Size(200, 200),
                    Location = new Point(60 + offsetX, 120 + offsetY),
                    BackColor = Color.Transparent
                };

                panelForLoadBar = new Panel
                {
                    Region = Region.FromHrgn(CreateRoundRectRgn(0, 30, 200, 62, 0, 0)),
                    Size = new Size(150, 90),
                    Location = new Point(60 + offsetX, 270 + offsetY),
                    BackColor = Color.Transparent
                };

                ////Timer
                animationTimer = new Timer
                {
                    Interval = 30
                };
                animationTimer.Tick += AnimationTimerTick;

                timer_2 = new Timer
                {
                    Interval = 300
                };
                timer_2.Tick += Timer_Tick_2;

                ////Controls
                this.Controls.Add(panelForLoadBar);
                this.Controls.Add(grass);
                this.Controls.Add(panelForAnimation);
                this.Controls.Add(labelProcentege);
                panelForAnimation.Controls.Add(sun);

                emptyTmpBitmap = new Bitmap(sun.Image.Width, sun.Image.Height);//Инициализация пустого bitmap'a

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
                animationTimer.Start();
                timer_2.Start();
                return ShowDialog();
            }

            /// <summary>
            /// Запускает таймер, который отвечает за анимацию поворота солнца
            /// </summary>
            protected override void LoadAnimationStart()
            {
                animationTimer.Start();
            }
            #endregion

            #region Implementation
            /// <summary>
            /// Отрисовываем вращение солнышка
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void AnimationTimerTick(object sender, EventArgs e)
            {
                sun.Image = RotateImage(sunTmpBitmap, rotateAngle++);
                if (rotateAngle == 360)
                    rotateAngle = 0;
            }

            /// <summary>
            /// Создаем Graphics по пустому битмапу нужного размера
            /// Смещаем начало координац в центр, поворачиваем bitmap, возвращаем центр координат назад
            /// Рисуем солнце, не поворачивая его на повернутый bitmap
            /// </summary>
            /// <param name="img">Изначальная картинка солнца</param>
            /// <param name="rotationAngle">На какой угол поворачиваем</param>
            /// <returns>Повернутая картинка солнца</returns>
            private static Image RotateImage(Image img, float rotationAngle)
            {
                using (Graphics gfx = Graphics.FromImage(emptyTmpBitmap))
                {
                    gfx.Clear(Color.Transparent);

                    gfx.TranslateTransform((float)emptyTmpBitmap.Width / 2, (float)emptyTmpBitmap.Height / 2, System.Drawing.Drawing2D.MatrixOrder.Prepend);
                    gfx.RotateTransform(rotationAngle);
                    gfx.TranslateTransform(-(float)emptyTmpBitmap.Width / 2, -(float)emptyTmpBitmap.Height / 2, System.Drawing.Drawing2D.MatrixOrder.Prepend);
                    gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor; //Для оптимизации по скорости
                    gfx.DrawImage(img, new Point(0, 0));
                }

                return emptyTmpBitmap;
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
                proc = (double)initProcentege / (double)grass.Width * 100;
                labelProcentege.Text = ((int)proc).ToString() + "%";
                if (panelForLoadBar.Location.X > grass.Width + grass.Location.X)
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
