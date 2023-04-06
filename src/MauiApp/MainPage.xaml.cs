namespace MauiApp;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new ContentPage(){Title = "Second", Content = new Label() {Text = "Hello world"}});
    }
}