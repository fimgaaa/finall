//namespace MauiToDoFinal;

//public partial class LoginPage : ContentPage
//{
//    public LoginPage()
//    {
//        InitializeComponent();
//    }

//    private void OnLoginClicked(object sender, EventArgs e)
//    {
//        var username = usernameEntry.Text?.Trim();
//        var password = passwordEntry.Text;

//        if (username == "admin" && password == "1234")
//        {
//            App.LoggedInUser = username;
//            Application.Current.MainPage = new MainPage();
//        }
//        else
//        {
//            loginStatusLabel.Text = "Kullanıcı adı veya şifre hatalı!";
//            loginStatusLabel.IsVisible = true;
//        }
//    }
//}
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using Microsoft.Maui.Storage;

namespace MauiToDoFinal;

public partial class LoginPage : ContentPage
{
    HttpClient client = new HttpClient();

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var username = usernameEntry.Text?.Trim();
        var password = passwordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            loginStatusLabel.Text = "Lütfen kullanıcı adı ve şifre girin.";
            loginStatusLabel.IsVisible = true;
            return;
        }

        // Giriş verisini JSON'a çevir
        var user = new
        {
            Username = username,
            Password = password
        };

        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // API isteği gönder
        var response = await client.PostAsync("https://localhost:7191/api/users/login", content);

        if (response.IsSuccessStatusCode)
        {
            // ✅ Giriş başarılı, kullanıcı adını kaydet
            App.LoggedInUser = username;
            Preferences.Set("username", username);

            // Ana sayfaya geç
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
        else
        {
            loginStatusLabel.Text = "Kullanıcı adı veya şifre yanlış.";
            loginStatusLabel.IsVisible = true;
        }
    }

    private void OnGoToRegister(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegisterPage());
    }
}
