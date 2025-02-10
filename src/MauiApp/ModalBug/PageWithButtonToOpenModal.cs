using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;
using Page = Microsoft.Maui.Controls.Page;

namespace MauiApp.ModalBug;

internal class PageWithButtonToOpenModal : ContentPage
{
    public PageWithButtonToOpenModal()
    {
        Content = new VerticalStackLayout()
        {
            Children =
            {
                new Button()
                {
                    Text = "2. Open Modal", Command = new Command(() => Shell.Current.Navigation.PushModalAsync(
                        new ModalNavigationPage(new ModalPageWithNavigationButton())
                    )), HorizontalOptions = LayoutOptions.Center
                },
                new Label() {Text = "5. When you return here, try to use navigation and observe that it is broken."}

            }
        };
    }
}

public class ModalNavigationPage : NavigationPage
{
    public ModalNavigationPage(Page root) : base(root)
    {
        On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.PageSheet);
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();

        return; //Remove this to use the work around. It does not fix the problem, but it limits the chances that people will reproduce it in the real world.
        if (Shell.Current.Navigation is NavigationProxy navigationProxy)
        {
            if (navigationProxy.ModalStack.Contains(this)) //This might happen if people swipe down on iOS, MAUI gets out of sync.
            {
                await Navigation.PopModalAsync(false); //Sync MAUI
            }
        }
    }
}

public class ModalPageWithNavigationButton : ContentPage
{
    public ModalPageWithNavigationButton()
    {
        Content = new Button()
        {
            Text = "3. Navigate",
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center,
            Command = new Command(() => Shell.Current.Navigation.PushAsync(new ModalPageToQuicklySwipeDown()))
        };
    }
}

public class ModalPageToQuicklySwipeDown : ContentPage
{
    public ModalPageToQuicklySwipeDown()
    {
        Content = new Label() {Text = "4. Swipe this page down quickly when the page appears"};
    }
}