using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.IO;

namespace WeatherPanel
{
    public partial class Form1 : Form
    {
        readonly string BaseURL = "https://weather-csharp.herokuapp.com/";

        string[] States = {"Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado",
                        "Connecticut", "Delaware", "District of Columbia", "Florida", "Georgia", "Hawaii",
                        "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine",
                        "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri",
                        "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York",
                        "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania",
                        "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah",
                        "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming" };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbxState.Items.AddRange(States);
        }

        private void btnGetWeather_Click(object sender, EventArgs e)
        {
            btnGetWeather.Enabled = false;

            string city = txtCity.Text;
            string state = cbxState.Text;
            if (LocationDataValid(city, state))
            {
                if (GetWeatherData(city, state, out string weather, out string dataError))
                {
                    lblWeather.Text = weather;
                }
                else
                {
                    MessageBox.Show(dataError, "Error");
                    return;
                }
                if (picWeather.Image != null)
                {
                    picWeather.Image.Dispose();
                }
                if (GetWeatherImage(city, state, out Image image, out string imageError))
                {
                    picWeather.Image = image;
                }
                else
                {
                    MessageBox.Show(imageError, "Error");
                    return;
                }
            } else
            {
                MessageBox.Show("Please verify both City and State.", "Error");
            }
            btnGetWeather.Enabled = true;
        }

        private bool LocationDataValid(string city, string state)
        {
            if (String.IsNullOrEmpty(city) || String.IsNullOrEmpty(state))
            {
                return false;
            }
            return true;
        }

        private bool GetWeatherData(string city, string state, out string weatherText, out string errorMessage)
        {
            string weatherTextURL = String.Format("{0}text?city={1}&state={2}", BaseURL, city, state);
            errorMessage = null;
            weatherText = null;

            try
            {
                using (WebClient client = new WebClient())
                {
                    weatherText = client.DownloadString(weatherTextURL);
                }
                return true;
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return false;
            }
        }
        private bool GetWeatherImage(string city, string state, out Image weatherImage, out string errorMessage)
        {
            weatherImage = null;
            errorMessage = null;
            
            try
            {
                using (WebClient client = new WebClient())
                {
                    string weatherPhotoURL = String.Format("{0}photo?city={1}&state={2}", BaseURL, city, state);
                    string tempFileDir = Path.GetTempPath().ToString();
                    String weatherFilePath = Path.Combine(tempFileDir, "weather_image.jpeg");
                    client.DownloadFile(weatherPhotoURL, weatherFilePath);
                    weatherImage = Image.FromFile(weatherFilePath);
                }
                return true;
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return false;
            }
        }
    }
}
