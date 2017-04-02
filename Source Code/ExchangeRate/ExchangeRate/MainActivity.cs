using Android.App;
using Android.Widget;
using Android.OS;
using ExchangeRate.Resources;
using System.Json;
using System.Threading.Tasks;

namespace ExchangeRate
{
    [Activity(Label = "ExchangeRate", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button btnChange;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);           
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            btnChange = FindViewById<Button>(Resource.Id.btnChange);
            TextView usd = FindViewById<TextView>(Resource.Id.usd);
            TextView eur = FindViewById<TextView>(Resource.Id.eur);
            TextView aud = FindViewById<TextView>(Resource.Id.aud);
            TextView jpy = FindViewById<TextView>(Resource.Id.jpy);
            TextView rate = FindViewById<TextView>(Resource.Id.rate);

            btnChange.Click += async (sender, e) =>  {
                api a = new api();
                JsonValue val = await a.RefreshDataAsync();
                EditText money = FindViewById<EditText>(Resource.Id.numFrom);
                double num = double.Parse(money.Text);
                double vnd = double.Parse(val["quotes"]["USDVND"].ToString());
                double eurex = double.Parse(val["quotes"]["USDEUR"].ToString());
                double audex = double.Parse(val["quotes"]["USDAUD"].ToString());
                double jpyex = double.Parse(val["quotes"]["USDJPY"].ToString());

                usd.Text = (num / vnd).ToString("0.00") + "       USD";
                eur.Text = (num * eurex/ vnd).ToString("0.00") + "       EUR";
                aud.Text = (num * audex / vnd).ToString("0.00") + "       AUD";
                jpy.Text = (num * jpyex / vnd).ToString("0.00") + "     JPY";

                string original = val["quotes"].ToString();
                original = " " + original.Substring(1);
                original = original.Substring(0, original.Length - 1);
                for (int i= 0;i < original.Length;i++ )
                {
                    if(original[i]==',')
                    {
                        original = original.Substring(0,i-1) + "\n" + original.Substring(i+1);
                    }
                }

                rate.Text = "Some exchange rate from USD to other:\n" + original;
            };
        }
    }
}

