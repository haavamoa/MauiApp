using System;
using MauiApp.ModalBug;
using Microsoft.Maui.Controls;

namespace MauiApp;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }
    

    private void Button_OnClicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new PageWithButtonToOpenModal());
    }
}