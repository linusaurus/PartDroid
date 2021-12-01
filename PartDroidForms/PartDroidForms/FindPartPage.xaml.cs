using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PartDroidForms.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using Android.Views;
using Android.App;
using Android.Widget;

namespace PartDroidForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindPartPage : ContentPage , INotifyPropertyChanged
    {
        private Part selectedPart;

    

        public FindPartPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public Part SelectedPart { get => selectedPart; set => selectedPart = value; }

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            var term = this.FindPartNumber.Text;
            var stuff = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"http://192.168.10.37/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var result = await client.GetAsync(string.Format("/api/Part/{0}", term));
                if (result != null)
                {
                    selectedPart = JsonConvert.DeserializeObject<Part>(await result.Content.ReadAsStringAsync());
                    this.PartID.Text = "PARTID: " + selectedPart.PartID.ToString();
                    this.DesriptionText.Text = "DESCRIP: " + selectedPart.description;
                    this.partNumber.Text = "PART NUM: " + selectedPart.partNumber;
                    this.location.Text = "LOCATION: " + selectedPart.Location;
                    this.Stocklevel.Text = "STOCK LEVEL: " + selectedPart.StockOnHand;
                }

            }
        }

        async  void ShowPullEntryDialog(object sender, EventArgs e)
        {
            if (selectedPart.PartID > 0)
            {
                string result = await DisplayPromptAsync(
                    "Quantity Pulled", "Enter Amount", maxLength: 6, keyboard: Keyboard.Numeric);
                if (result != null)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(@"http://192.168.10.37/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    }
                }
              
            }
        }

        

    }
}