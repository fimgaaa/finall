namespace MauiToDoFinal;

public partial class DetailPage : ContentPage
{
    public DetailPage(ToDoItem item)
    {
        InitializeComponent();

        TitleLabel.Text = item.Title;
        CategoryLabel.Text = item.Category;
        StatusLabel.Text = item.IsCompleted ? "Tamamlandý" : "Tamamlanmadý";
        CreatedAtLabel.Text = item.CreatedAt.ToString("g");
        CreatedByLabel.Text = item.CreatedBy;
        UpdatedAtLabel.Text = item.UpdatedAt?.ToString("g") ?? "-";
        UpdatedByLabel.Text = item.UpdatedBy ?? "-";
        DueDateLabel.Text = item.DueDate.ToString("dd.MM.yyyy");
    }
}
