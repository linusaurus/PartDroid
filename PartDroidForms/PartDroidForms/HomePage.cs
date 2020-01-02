using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXing.Net.Mobile.Forms;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PartDroidForms
{
    public class HomePage : ContentPage
    {
        
            ZXingScannerPage scanPage;
            Button buttonScanDefaultOverlay;         
            Button buttonGenerateBarcode;
            Button buttonFindPart;

            public HomePage() : base()
            {

            buttonScanDefaultOverlay = new Button
            {
                Text = "Scan Part",
                AutomationId = "scanWithDefaultOverlay",
                
                };
                buttonScanDefaultOverlay.Clicked += async delegate {
                    scanPage = new ZXingScannerPage();
                    scanPage.OnScanResult += (result) => {
                        scanPage.IsScanning = false;

                        Device.BeginInvokeOnMainThread(() => {
                            Navigation.PopAsync();
                            DisplayAlert("Scanned Barcode", result.Text, "OK");
                        });
                    };

                    await Navigation.PushAsync(scanPage);
               

                };


            buttonFindPart = new Button
            {
                Text = "Get Part Info",
                AutomationId = "scanWithFindPart",

            };

            buttonFindPart.Clicked += async delegate {
                scanPage = new ZXingScannerPage();
                scanPage.OnScanResult += (result) => {
                    scanPage.IsScanning = false;

                    Device.BeginInvokeOnMainThread(() => {
                        Navigation.PopAsync();
                        DisplayAlert("Scanned Barcode", result.Text, "OK");
                    });
                };

                await Navigation.PushAsync(scanPage);


            };


            buttonGenerateBarcode = new Button
                {
                    Text = "Barcode Generator",
                    AutomationId = "barcodeGenerator",
                };
                buttonGenerateBarcode.Clicked += async delegate {
                    await Navigation.PushAsync(new BarcodePage());
                };

                var stack = new StackLayout();
                stack.Children.Add(buttonScanDefaultOverlay);
                stack.Children.Add(buttonGenerateBarcode);
                stack.Children.Add(buttonFindPart);
                Content = stack;
            }
        
    }
}