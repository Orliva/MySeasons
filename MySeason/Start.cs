using System.Windows.Forms;
using MySeason.Configuration;
using MySeason.LoadPage;

namespace MySeason
{
    //Форма для показа всех сезонов
    class Start : Form
    {
        /// <summary>
        /// Для закрытия MainForm нажать "Esc"
        /// </summary>
        public Start()
        {
            this.Load += Start_Load;
        }

        private void Start_Load(object sender, System.EventArgs e)
        {
            ShowMySeason(Config.AutumnStr);
            ShowMySeason(Config.WinterStr);
            ShowMySeason(Config.SpringStr);
            ShowMySeason(Config.SummerStr);
            this.Close();
        }

        private void ShowMySeason(string season)
        {
            LoadingPage loadPage;

            switch (season.ToLower())
            {
                case "autumn":
                    loadPage = new AutumnLoadPage();
                    break;
                case "winter":
                    loadPage = new WinterLoadPage();
                    break;
                case "spring":
                    loadPage = new SpringLoadPage();
                    break;
                case "summer":
                    loadPage = new SummerLoadPage();
                    break;
                default:
                    throw new System.Exception();
            }
            loadPage.LoadShowDialog();
            new MainForm(season).ShowDialog();
        }
    }
}
