using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace GeoAuthApp
{
    public partial class RegisterPage : PhoneApplicationPage
    {
        private GeoAppStorage settings = new GeoAppStorage();

        #region Constructors
        public RegisterPage()
        {
            InitializeComponent();
            if (settings.deviceKey == null)
            {
                Register.Visibility = System.Windows.Visibility.Collapsed;

                GeoAuthApi.RequestDeviceKey deviceKeyReq = new GeoAuthApi.RequestDeviceKey();

                //get the device key
                deviceKeyReq.RequestDevKey();

                //Update the UI on the result
                deviceKeyReq.DeviceKeyStatus += (send, evt) =>
                {
                    if (evt.Error != null)
                    {
                        lblErrors.Text = "Error Retrieving Device key";
                    }
                    //enable the register button
                    else
                    {
                        Register.Visibility = System.Windows.Visibility.Visible;
                    }
                };
            }
        }
        #endregion

        #region User Interface
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string deviceName = txtBoxDevName.Text;
            string username = txtBoxUsername.Text;
            string password = txtBoxPassword.Text;

            if (settings.apiKey == null)
            {
                lblErrors.Text = "Api Key is Missing";
                return;
            }
            //check to see if we need to get a device key
            if (settings.deviceKey == null)
            {
                lblErrors.Text = "Device Key is Missing";
                return;
            }

            GeoAuthApi.RegisterRequest registerRequest = new GeoAuthApi.RegisterRequest();

            registerRequest.RegisterDevice(deviceName, username, password);

            //Update the UI on the result
            registerRequest.RegisterStatus += (send, evt) =>
            {
                if (evt.Error != null)
                {
                    lblErrors.Text = "Error registering";
                }
                //go back to the main page
                else
                {
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute)); 
                }
            };

        }
        #endregion
    }
}