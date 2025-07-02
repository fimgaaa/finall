//namespace MauiToDoFinal
//{
//    public partial class App : Application
//    {
//        public App()
//        {
//            InitializeComponent();

//            MainPage = new LoginPage();
//        }

//        public static string LoggedInUser { get; set; } = string.Empty;
//    }
//}
namespace MauiToDoFinal
{
    public partial class App : Application
    {
        public static string LoggedInUser { get; set; } = string.Empty;

        public App()
        {
            InitializeComponent();

            // Sayfalar arası geçiş yapabilmek için NavigationPage kullan
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
