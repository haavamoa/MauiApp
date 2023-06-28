using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace MauiApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OpenBuggyModal(object sender, EventArgs e)
    {
        var modalPage = new ModalPage();
        modalPage.On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.PageSheet);
        Shell.Current.Navigation.PushModalAsync(modalPage);
    }
    
    private void OpenNonBuggyModal(object sender, EventArgs e)
    {
        var modalPage = new ModalPage();
        Shell.Current.Navigation.PushModalAsync(modalPage);
    }
}