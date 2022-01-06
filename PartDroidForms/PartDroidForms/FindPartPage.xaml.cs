using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
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
    public partial class FindPartPage : ContentPage, INotifyPropertyChanged
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
            await GetPart();
        }

        private async Task GetPart()
        {
            var term = this.FindPartNumber.Text;
            var stuff = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"http://192.168.10.37");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var result = await client.GetAsync(string.Format("/api/part/{0}", term));
                if (result != null)
                {
                    selectedPart = JsonConvert.DeserializeObject<Part>(await result.Content.ReadAsStringAsync());
                    this.PartID.Text = "PARTID: " + selectedPart.PartID.ToString();
                    this.DesriptionText.Text = "DESCRIP: " + selectedPart.description;
                    this.partNumber.Text = "PART NUM: " + selectedPart.partNumber;
                    this.location.Text = "LOCATION: " + selectedPart.Location;
                    this.Unit.Text = $"UNIT: {selectedPart.Unit}";
                    this.Stocklevel.Text = "STOCK LEVEL: " + selectedPart.StockOnHand;
                }
            }
        }



        private async void PullEntry(object sender, EventArgs e)
        {
            if (selectedPart.PartID > 0)
            {
                string result = await DisplayPromptAsync(
                    "Quantity Pulled", "Enter Amount", maxLength: 6, keyboard: Keyboard.Numeric);
                if (result != null)
                {
                    StockForCreationDTO dto = new StockForCreationDTO();

                    dto.PartID = selectedPart.PartID;

                    dto.Quantity = decimal.Parse(result) * -1.0m;

                    dto.TransType = 1;
                    dto.User = "Moto";
                    dto.DateStamp = DateTime.Now;


                    var strJson = System.Text.Json.JsonSerializer.Serialize(dto);
                    Console.WriteLine(strJson);
                    var payload = new StringContent(strJson, Encoding.UTF8, "application/json");

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(@"http://192.168.10.37");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var res = await client.PostAsync(string.Format("/api/Stock/"), payload);
                    }
                    await GetPart();
                }
            }
        }

        private async void SetStock(object sender, EventArgs e)
        {
            if (selectedPart.PartID > 0)
            {
                string result = await DisplayPromptAsync(
                    "Set Stock Amount", "Enter Amount", maxLength: 6, keyboard: Keyboard.Numeric);
                if (result != null)
                {
                    StockForCreationDTO dto = new StockForCreationDTO();
                    decimal desiredStockLevel = Decimal.Parse(result);
                    decimal existing = selectedPart.StockOnHand.GetValueOrDefault();
                    if (existing != decimal.Zero)
                    { dto.Quantity = desiredStockLevel - existing;} 
                    else 
                    {dto.Quantity = desiredStockLevel; }
                        


                    dto.PartID = selectedPart.PartID;
                    dto.TransType = 4;
                    dto.User = "Moto";
                    dto.EmployeeID = 8;

                    dto.DateStamp = DateTime.Now;


                    var strJson = System.Text.Json.JsonSerializer.Serialize(dto);
                    Console.WriteLine(strJson);
                    var payload = new StringContent(strJson, Encoding.UTF8, "application/json");

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(@"http://192.168.10.37/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var res = await client.PostAsync(string.Format("/api/Stock/"), payload);
                    }
                    await GetPart();

                }

            }
        }

        
    }
}