﻿using System;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace PartDroidForms
{
    public partial class MainPage : ContentPage
    {
        ZXingScannerPage xPage;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void ShowInfo(object sender, EventArgs e)
        {
            xPage = new ZXingScannerPage();
            xPage.OnScanResult += (result) => {
                xPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() => {
                    Navigation.PopAsync();
                    DisplayAlert("Scanned Barcode", result.Text, "OK");
                });
            };

            await Navigation.PushAsync(xPage);
        }

        private async void FindPart(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FindPartPage());
        }

        private async void EnterJobDialog(object sender, EventArgs e)
        {
           var response = await DisplayAlert("Enter a Job Number","Are you sure","Yes","NO");
            if (response)
                await DisplayAlert("Done", "OK", "OK");
        }

        private void Handle_Activated(object sender, EventArgs e)
        {
           

           
        }
    }
}
