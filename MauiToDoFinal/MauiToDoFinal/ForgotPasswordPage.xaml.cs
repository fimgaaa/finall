using Newtonsoft.Json;
using System.Text;

namespace MauiToDoFinal;

public partial class ForgotPasswordPage : ContentPage
{
    HttpClient client = new HttpClient();

    public ForgotPasswordPage()
    {
        InitializeComponent();
    }

    private async void OnSendClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EmailEntry.Text))
        {
            MessageLabel.Text = "Lütfen e-posta girin.";
            return;
        }

        var request = new { Email = EmailEntry.Text };
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("https://localhost:7191/api/users/forgot-password", content);

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Başarılı", "E-posta gönderildi", "Tamam");
            await Navigation.PopAsync();
        }
        else
        {
            MessageLabel.Text = "E-posta gönderilemedi";
        }
    }
}