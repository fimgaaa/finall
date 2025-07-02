
//////using Newtonsoft.Json;
//////using System.Net.Http.Headers;
//////namespace MauiToDoFinal;

//////public partial class MainPage : ContentPage
//////{
//////    HttpClient client;

//////    public MainPage()
//////    {
//////        InitializeComponent();

//////        var handler = new HttpClientHandler();
//////        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
//////        client = new HttpClient(handler);

//////        LoadToDoItems();
//////    }

//////    private async void LoadToDoItems()
//////    {
//////        var response = await client.GetAsync("https://localhost:7191/api/todo");
//////        if (response.IsSuccessStatusCode)
//////        {
//////            var json = await response.Content.ReadAsStringAsync();
//////            var list = JsonConvert.DeserializeObject<List<ToDoItem>>(json);
//////            ToDoListView.ItemsSource = list;
//////        }
//////        else
//////        {
//////            await DisplayAlert("Hata", "API'den veri alınamadı.", "Tamam");
//////        }
//////    }
//////}

//////public class ToDoItem
//////{
//////    public int Id { get; set; }
//////    public string Title { get; set; }
//////    public bool IsCompleted { get; set; }
//////}
////using Newtonsoft.Json;
////using System.Collections;
////using System.Collections.Generic;
////using System.Net.Http.Headers;
////using System.Text;

////namespace MauiToDoFinal;

////public partial class MainPage : ContentPage
////{
////    HttpClient client = new HttpClient();

////    public MainPage()
////    {
////        InitializeComponent();
////        LoadToDoItems();
////    }

////    //private async void LoadToDoItems()
////    //{
////    //    var response = await client.GetAsync("https://localhost:7191/api/todo");
////    //    if (response.IsSuccessStatusCode)
////    //    {
////    //        var json = await response.Content.ReadAsStringAsync();
////    //        var list = JsonConvert.DeserializeObject<List<ToDoItem>>(json);
////    //        ToDoListView.ItemsSource = list;
////    //    }
////    //}
////    //private async void LoadToDoItems()
////    //{
////    //    var user = App.LoggedInUser;
////    //    var response = await client.GetAsync($"https://localhost:7191/api/todo/user/{user}");
////    //    if (response.IsSuccessStatusCode)
////    //    {
////    //        var json = await response.Content.ReadAsStringAsync();
////    //        var list = JsonConvert.DeserializeObject<List<ToDoItem>>(json);
////    //        ToDoListView.ItemsSource = list;
////    //    }
////    //}
////    private async void LoadToDoItems()
////    {
////        var response = await client.GetAsync($"https://localhost:7191/api/todo/user/{App.LoggedInUser}");

////        if (response.IsSuccessStatusCode)
////        {
////            var json = await response.Content.ReadAsStringAsync();
////            var list = JsonConvert.DeserializeObject<List<ToDoItem>>(json);

////            var categories = list.Select(item => item.Category).Distinct().ToList();
////            CategoryPicker.ItemsSource = categories;

////            ToDoListView.ItemsSource = list;
////        }
////    }



////    private async void OnAddClicked(object sender, EventArgs e)
////    {
////        if (string.IsNullOrWhiteSpace(TitleEntry.Text))
////            return;

////        var newItem = new ToDoItem
////        {
////            Title = TitleEntry.Text,
////            IsCompleted = false,
////            CreatedBy = "admin", // Sabit kullanıcı adı
////            CreatedAt = DateTime.Now,
////            Category = CategoryEntry.Text
////        };

////        var json = JsonConvert.SerializeObject(newItem);
////        var content = new StringContent(json, Encoding.UTF8, "application/json");

////        var response = await client.PostAsync("https://localhost:7191/api/todo", content);
////        if (response.IsSuccessStatusCode)
////        {
////            TitleEntry.Text = string.Empty;
////            LoadToDoItems(); // Listeyi yenile
////        }
////    }
////    private async void OnDeleteClicked(object sender, EventArgs e)
////    {
////        if (sender is Button btn && btn.CommandParameter is int id)
////        {
////            var response = await client.DeleteAsync($"https://localhost:7191/api/todo/{id}");
////            if (response.IsSuccessStatusCode)
////            {
////                LoadToDoItems(); // Listeyi yenile
////            }
////            else
////            {
////                await DisplayAlert("Hata", "Görev silinemedi.", "Tamam");
////            }
////        }
////    }


////    private async void OnToggleCompletedClicked(object sender, EventArgs e)
////    {
////        if (sender is Button btn && btn.BindingContext is ToDoItem item)
////        {
////            item.IsCompleted = !item.IsCompleted;
////            item.UpdatedAt = DateTime.Now;
////            item.UpdatedBy = "admin";

////            var json = JsonConvert.SerializeObject(item);
////            var content = new StringContent(json, Encoding.UTF8, "application/json");

////            var response = await client.PutAsync($"https://localhost:7191/api/todo/{item.Id}", content);
////            if (response.IsSuccessStatusCode)
////            {
////                LoadToDoItems(); // Listeyi yenile
////            }
////            else
////            {
////                await DisplayAlert("Hata", "Güncellenemedi.", "Tamam");
////            }
////        }
////    }
////    private async void OnCategorySelected(object sender, EventArgs e)
////    {
////        if (CategoryPicker.SelectedItem is string selectedCategory)
////        {
////            var response = await client.GetAsync($"https://localhost:7191/api/todo/user/{App.LoggedInUser}");
////            if (response.IsSuccessStatusCode)
////            {
////                var json = await response.Content.ReadAsStringAsync();
////                var list = JsonConvert.DeserializeObject<List<ToDoItem>>(json);

////                // Seçili kategoriye göre filtrele
////                var filtered = list.Where(item => item.Category == selectedCategory).ToList();
////                ToDoListView.ItemsSource = filtered;
////            }
////        }
////    }


////}

////// UI tarafında geçici ToDoItem (API tarafındakiyle bire bir uyumlu olmalı)
////public class ToDoItem
////{
////    public int Id { get; set; }
////    public string Title { get; set; }
////    public bool IsCompleted { get; set; }
////    public string Category { get; set; } = string.Empty;
////    public DateTime CreatedAt { get; set; }
////    public string CreatedBy { get; set; }
////    public DateTime? UpdatedAt { get; set; }
////    public string? UpdatedBy { get; set; }
////}
//using Newtonsoft.Json;
//using System.Net.Http.Headers;
//using System.Text;

//namespace MauiToDoFinal;

//public partial class MainPage : ContentPage
//{
//    private readonly HttpClient client = new();
//    private ToDoItem? currentlyEditingItem;
//    public MainPage()
//    {
//        InitializeComponent();
//        LoadToDoItems();
//    }

//    private async void LoadToDoItems()
//    {
//        var response = await client.GetAsync($"https://localhost:7191/api/todo/user/{App.LoggedInUser}");
//        if (response.IsSuccessStatusCode)
//        {
//            var json = await response.Content.ReadAsStringAsync();
//            var list = JsonConvert.DeserializeObject<List<ToDoItem>>(json);

//            ToDoListView.ItemsSource = list;
//            CategoryPicker.ItemsSource = list?.Select(item => item.Category).Distinct().ToList();
//        }
//        else
//        {
//            await DisplayAlert("Hata", "Görevler yüklenemedi.", "Tamam");
//        }
//    }
//    private async void OnEditClicked(object sender, EventArgs e)
//    {
//        if (sender is Button btn && btn.BindingContext is ToDoItem item)
//        {
//            TitleEntry.Text = item.Title;
//            CategoryEntry.Text = item.Category;

//            // Güncelle butonunu görünür yap
//            EditButton.IsVisible = true;
//            AddButton.IsVisible = false;

//            // Güncellenecek görevi sakla
//            currentlyEditingItem = item;
//        }
//    }
//    private async void OnUpdateClicked(object sender, EventArgs e)
//    {
//        if (currentlyEditingItem == null)
//            return;

//        currentlyEditingItem.Title = TitleEntry.Text;
//        currentlyEditingItem.Category = CategoryEntry.Text;
//        currentlyEditingItem.UpdatedAt = DateTime.Now;
//        currentlyEditingItem.UpdatedBy = App.LoggedInUser;

//        var json = JsonConvert.SerializeObject(currentlyEditingItem);
//        var content = new StringContent(json, Encoding.UTF8, "application/json");

//        var response = await client.PutAsync($"https://localhost:7191/api/todo/{currentlyEditingItem.Id}", content);
//        if (response.IsSuccessStatusCode)
//        {
//            TitleEntry.Text = "";
//            CategoryEntry.Text = "";
//            EditButton.IsVisible = false;
//            AddButton.IsVisible = true;
//            currentlyEditingItem = null;

//            LoadToDoItems(); // Listeyi yenile
//        }
//        else
//        {
//            await DisplayAlert("Hata", "Güncelleme başarısız oldu", "Tamam");
//        }
//    }

//    private async void OnAddClicked(object sender, EventArgs e)
//    {
//        if (string.IsNullOrWhiteSpace(TitleEntry.Text))
//            return;

//        var newItem = new ToDoItem
//        {
//            Title = TitleEntry.Text,
//            IsCompleted = false,
//            CreatedBy = App.LoggedInUser ?? "admin",
//            CreatedAt = DateTime.Now,
//            Category = CategoryEntry.Text
//        };

//        var json = JsonConvert.SerializeObject(newItem);
//        var content = new StringContent(json, Encoding.UTF8, "application/json");

//        var response = await client.PostAsync("https://localhost:7191/api/todo", content);
//        if (response.IsSuccessStatusCode)
//        {
//            TitleEntry.Text = string.Empty;
//            CategoryEntry.Text = string.Empty;
//            LoadToDoItems();
//        }
//    }

//    private async void OnDeleteClicked(object sender, EventArgs e)
//    {
//        if (sender is Button btn && btn.CommandParameter is int id)
//        {
//            var response = await client.DeleteAsync($"https://localhost:7191/api/todo/{id}");
//            if (response.IsSuccessStatusCode)
//            {
//                LoadToDoItems();
//            }
//        }
//    }

//    private async void OnToggleCompletedClicked(object sender, EventArgs e)
//    {
//        if (sender is Button btn && btn.BindingContext is ToDoItem item)
//        {
//            item.IsCompleted = !item.IsCompleted;
//            item.UpdatedAt = DateTime.Now;
//            item.UpdatedBy = App.LoggedInUser ?? "admin";

//            var json = JsonConvert.SerializeObject(item);
//            var content = new StringContent(json, Encoding.UTF8, "application/json");

//            var response = await client.PutAsync($"https://localhost:7191/api/todo/{item.Id}", content);
//            if (response.IsSuccessStatusCode)
//            {
//                LoadToDoItems();
//            }
//        }
//    }

//    private async void OnCategorySelected(object sender, EventArgs e)
//    {
//        if (CategoryPicker.SelectedItem is string selectedCategory)
//        {
//            var response = await client.GetAsync($"https://localhost:7191/api/todo/user/{App.LoggedInUser}");
//            if (response.IsSuccessStatusCode)
//            {
//                var json = await response.Content.ReadAsStringAsync();
//                var list = JsonConvert.DeserializeObject<List<ToDoItem>>(json);
//                var filtered = list?.Where(item => item.Category == selectedCategory).ToList();
//                ToDoListView.ItemsSource = filtered;
//            }
//        }
//    }

//    private async void OnItemSelected(object sender, SelectionChangedEventArgs e)
//    {
//        if (e.CurrentSelection.FirstOrDefault() is ToDoItem selectedItem)
//        {
//            await Navigation.PushAsync(new DetailPage(selectedItem));
//            ToDoListView.SelectedItem = null; // Seçimi sıfırla
//        }
//    }

//}

//public class ToDoItem
//{
//    public int Id { get; set; }
//    public string Title { get; set; } = "";
//    public bool IsCompleted { get; set; }
//    public string Category { get; set; } = "";
//    public DateTime CreatedAt { get; set; }
//    public string CreatedBy { get; set; } = "";
//    public DateTime? UpdatedAt { get; set; }
//    public string? UpdatedBy { get; set; }
//}
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;



namespace MauiToDoFinal;

public partial class MainPage : ContentPage
{
    private readonly HttpClient client = new();
    private ToDoItem? currentlyEditingItem;

    public MainPage()
    {
        InitializeComponent();

        // Kullanıcı bilgisini al (login sonrası kayıt edilmiş olmalı)
        App.LoggedInUser = Preferences.Get("username", "");

        LoadToDoItems();
    }

    private async void LoadToDoItems()
    {
        var response = await client.GetAsync($"https://localhost:7191/api/todo/user/{App.LoggedInUser}");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<ToDoItem>>(json);

            ToDoListView.ItemsSource = list;
            CategoryPicker.ItemsSource = list?.Select(item => item.Category).Distinct().ToList();
        }
        else
        {
            await DisplayAlert("Hata", "Görevler yüklenemedi.", "Tamam");
        }
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text))
            return;

        var newItem = new ToDoItem
        {
            Title = TitleEntry.Text,
            IsCompleted = false,
            CreatedBy = App.LoggedInUser ?? "admin",
            CreatedAt = DateTime.Now,
            Category = CategoryEntry.Text,
            DueDate = DueDatePicker.Date
        };

        var json = JsonConvert.SerializeObject(newItem);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("https://localhost:7191/api/todo", content);
        if (response.IsSuccessStatusCode)
        {
            TitleEntry.Text = "";
            CategoryEntry.Text = "";
            LoadToDoItems();
        }

    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is ToDoItem item)
        {
            TitleEntry.Text = item.Title;
            CategoryEntry.Text = item.Category;

            EditButton.IsVisible = true;
            AddButton.IsVisible = false;

            currentlyEditingItem = item;
        }
    }

    private async void OnUpdateClicked(object sender, EventArgs e)
    {
        if (currentlyEditingItem == null)
            return;

        currentlyEditingItem.Title = TitleEntry.Text;
        currentlyEditingItem.Category = CategoryEntry.Text;
        currentlyEditingItem.UpdatedAt = DateTime.Now;
        currentlyEditingItem.UpdatedBy = App.LoggedInUser ?? "admin";

        var json = JsonConvert.SerializeObject(currentlyEditingItem);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PutAsync($"https://localhost:7191/api/todo/{currentlyEditingItem.Id}", content);
        if (response.IsSuccessStatusCode)
        {
            TitleEntry.Text = "";
            CategoryEntry.Text = "";
            EditButton.IsVisible = false;
            AddButton.IsVisible = true;
            currentlyEditingItem = null;

            LoadToDoItems();
        }
        else
        {
            await DisplayAlert("Hata", "Güncelleme başarısız oldu", "Tamam");
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is int id)
        {
            var response = await client.DeleteAsync($"https://localhost:7191/api/todo/{id}");
            if (response.IsSuccessStatusCode)
            {
                LoadToDoItems();
            }
        }
    }

    private async void OnToggleCompletedClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is ToDoItem item)
        {
            item.IsCompleted = !item.IsCompleted;
            item.UpdatedAt = DateTime.Now;
            item.UpdatedBy = App.LoggedInUser ?? "admin";

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"https://localhost:7191/api/todo/{item.Id}", content);
            if (response.IsSuccessStatusCode)
            {
                LoadToDoItems();
            }
        }
    }

    private async void OnCategorySelected(object sender, EventArgs e)
    {
        if (CategoryPicker.SelectedItem is string selectedCategory)
        {
            var response = await client.GetAsync($"https://localhost:7191/api/todo/user/{App.LoggedInUser}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<ToDoItem>>(json);
                var filtered = list?.Where(item => item.Category == selectedCategory).ToList();
                ToDoListView.ItemsSource = filtered;
            }
        }
    }

    private async void OnItemSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ToDoItem selectedItem)
        {
            await Navigation.PushAsync(new DetailPage(selectedItem));
            ToDoListView.SelectedItem = null;
        }
    }
}

public class ToDoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public bool IsCompleted { get; set; }
    public string Category { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = "";
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime DueDate { get; set; }
    public string CompletionText => IsCompleted ? "Yapıldı" : "Yapılmadı";
}
