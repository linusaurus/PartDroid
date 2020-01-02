using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Net.Mobile.Forms;
using Xamarin.Forms;

namespace PartDroidForms
{
    public class BarcodePage : ContentPage
    {

        ZXingBarcodeImageView barcode;
        public BarcodePage()
        {
      
                barcode = new ZXingBarcodeImageView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    AutomationId = "zxingBarcodeImageView",
                };
                barcode.BarcodeFormat = ZXing.BarcodeFormat.UPC_A;
                barcode.BarcodeOptions.Width = 300;
                barcode.BarcodeOptions.Height = 40;
                barcode.BarcodeOptions.Margin = 10;
                barcode.BarcodeValue = "72527273070";
           
                Content = barcode;
            
        }
    }
}