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
        // Setting the baseURL as a readonly string since we don't need to change this part of the URL.
        readonly string BaseURL = "https://weather-csharp.herokuapp.com/";

        // Setting a full string list of all availible States.
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
            // When the form loads, set all states into the ComboBox as usable states.
            cbxState.Items.AddRange(States);
        }

        // Get Weather button, locks the button model and attempts to get the availible weather for the City in the selected state.
        private void btnGetWeather_Click(object sender, EventArgs e)
        {
            // Disabling the button, as to prevent multiple inquiries to the server.
            btnGetWeather.Enabled = false;

            string city = txtCity.Text;
            string state = cbxState.Text;
            
            // Validating if the location is valid.
            if (LocationDataValid(city, state))
            {
                // Getting the weather data for said State and City.
                if (GetWeatherData(city, state, out string weather, out string dataError))
                {
                    // If we have data, post it in the box for the data.
                    lblWeather.Text = weather;
                }
                else
                {
                    // Elsewise, show the error provided by the server.
                    MessageBox.Show(dataError, "Error");
                    return;
                }
                // If the picturebox has an image, we need to get rid of the current image to set a new one.
                if (picWeather.Image != null)
                {
                    picWeather.Image.Dispose();
                }
                // Now we're ready to get a picture that shows the weather in that location. 
                if (GetWeatherImage(city, state, out Image image, out string imageError))
                {
                    picWeather.Image = image;
                }
                else
                {
                    // Else the server will respond with an error.
                    MessageBox.Show(imageError, "Error");
                    return;
                }
            } else
            {
                MessageBox.Show("Please verify both City and State.", "Error");
            }
            // Once everything has completed, allow the button to be clicked again.
            btnGetWeather.Enabled = true;
        }

        // Basic bool validation method to make sure the city and state values are not Null or Empty.
        private bool LocationDataValid(string city, string state)
        {
            if (String.IsNullOrEmpty(city) || String.IsNullOrEmpty(state))
            {
                return false;
            }
            return true;
        }

        // Bool method for getting weather data from the server based on the state and city provided.
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

        // Bool method for getting the weather image from the server based on state and city provided.
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
