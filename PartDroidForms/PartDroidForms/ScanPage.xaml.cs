using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using ZXing;
using Prism.Commands;
using PartDroidForms.ViewModels;

namespace PartDroidForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        private ZXingScannerPage xPage;
        public ICommand doSomethingCommand;

        public ScanPage()
        {
            InitializeComponent();
            
        }

        public async void ReturnInfo(object sender, EventArgs e)
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


        private   async void SetStockLevel(object sender, EventArgs e)
        {
           
                await Navigation.PushAsync(new BarcodePage());
           

           
        }
    }
        
 
}