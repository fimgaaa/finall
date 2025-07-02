using Newtonsoft.Json;
using System.Text;

namespace MauiToDoFinal;

public partial class RegisterPage : ContentPage
{
    HttpClient client = new HttpClient();

    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(UsernameEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            MessageLabel.Text = "Lütfen tüm alanları doldurun.";
            return;
        }

        var user = new { Username = UsernameEntry.Text, Password = PasswordEntry.Text };

        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("https://localhost:7191/api/users/register", content);

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Başarılı", "Kayıt tamamlandı. Giriş sayfasına yönlendiriliyorsunuz.", "Tamam");
            await Navigation.PopAsync(); // LoginPage'e geri dön
        }
        else
        {
            MessageLabel.Text = "Kayıt başarısız. Kullanıcı zaten mevcut olabilir.";
        }
    }
}
