/* 
 * Used http://code.msdn.microsoft.com/wpapps/Location-Service-Sample-6b9ef410
 * as inital code base for location services.
*/
using System;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Device.Location;
using GeoAuthApi;
using System.Windows.Navigation;
using GeoAuthApp.ViewModel;
using GeoAuthApp.Model;



namespace GeoAuthApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        private static string DBConnectionString = "Data Source=isostore:/GeoAuth.sdf";
        private GeoAuthViewModel geoAuthDBView;


        private static GeoAppStorage settings = new GeoAppStorage();

        /// <summary>
        /// This sample receives data from the Location Service and displays the geographic coordinates of the device.
        /// </summary>

        GeoCoordinateWatcher watcher;
        string accuracyText = "";

        #region Constructors

        /// <summary>
        /// Constructor for the PhoneApplicationPage object
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            geoAuthDBView = new GeoAuthViewModel(DBConnectionString);
        }

        #endregion

        #region User Interface

        /// <summary>
        /// Click event handler for the low accuracy button
        /// </summary>
        /// <param name="sender">The control that raised the event</param>
        /// <param name="e">An EventArgs object containing event data.</param>
        private void LowButtonClick(object sender, EventArgs e)
        {
            // Start data acquisition from the Location Service, low accuracy
            accuracyText = "default accuracy";
            StartLocationService(GeoPositionAccuracy.Default);
        }

        /// <summary>
        /// Click event handler for the high accuracy button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HighButtonClick(object sender, EventArgs e)
        {
            // Start data acquisition from the Location Service, high accuracy
            accuracyText = "high accuracy";
            StartLocationService(GeoPositionAccuracy.High);
        }
        private void StopButtonClick(object sender, EventArgs e)
        {
            if (watcher != null)
            {
                watcher.Stop();
            }
            StatusTextBlock.Text = "location service is off";
            LatitudeTextBlock.Text = " ";
            LongitudeTextBlock.Text = " ";
        }

        private void CheckInLocation(object sender, EventArgs e)
        {
            clearErrorMessages();
            //Check to see if we have a deviceId otherwise have them register
            if (settings.DeviceId != null)
            {
                if ((watcher != null) && (GeoPositionStatus.Ready == watcher.Status))
                {
                    string latitude = watcher.Position.Location.Latitude.ToString("0.000");
                    string longitude = watcher.Position.Location.Longitude.ToString("0.000");
                    GeoAuthApi.CheckInRequest checkInCall = new GeoAuthApi.CheckInRequest();
                    checkInCall.CheckIn(latitude, longitude, getCurrentDateTime());

                    //Update the UI on the result
                    checkInCall.CheckInStatus += (send, evt) =>
                    {
                        if (evt.Error == null)
                        {
                            lblErrors.Text = evt.Result;
                        }
                    };
                }
                else
                {
                    lblErrors.Text = "Please select GPS Accuracy";
                }
            }
            else
            {
                NavigationService.Navigate(new Uri("/Pages/RegisterPage.xaml", UriKind.RelativeOrAbsolute)); 
            }
        }

        private void CreateRegion_Click(object sender, RoutedEventArgs e)
        {
            clearErrorMessages();

            //Check to see if we have a deviceId otherwise have them register
            if (settings.DeviceId != null)
            {
                //Make sure we have GPS data
                if ((watcher != null) && (GeoPositionStatus.Ready == watcher.Status))
                {
                    //Hide un-needed buttons
                    CreateRegion.Visibility = System.Windows.Visibility.Collapsed;
                    CheckIn.Visibility = System.Windows.Visibility.Collapsed;
                    //show the input controls for region
                    pnlRegion.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    lblErrors.Text = "GPS not enabled to add region";
                }
            }
            else
            {
                NavigationService.Navigate(new Uri("/Pages/RegisterPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void AddRegion_Click(object sender, RoutedEventArgs e)
        {
            clearErrorMessages();

            //error messages
            string alreadyExistsInDB = "A Location with that name already exists";
            double dRadius;
            string locationName = txtBoxLocationName.Text;
            string sLatitude = watcher.Position.Location.Latitude.ToString("0.000");
            string sLongitude =  watcher.Position.Location.Longitude.ToString("0.000");
            double dLatitude = watcher.Position.Location.Latitude;
            double dLongitude = watcher.Position.Location.Longitude;

            

            if (!String.IsNullOrEmpty(txtBoxLocationName.Text))
            {
                if (String.IsNullOrEmpty(txtBoxLocationRadius.Text))
                {
                    //Create the locationRegion object for the table
                    GeoAuthLocationRegion newLocationRegion = new GeoAuthLocationRegion
                    {
                        RegionName = locationName,
                        Latitude = dLatitude,
                        Longitude = dLongitude
                    };

                    if (geoAuthDBView.AddLocationRegion(newLocationRegion))
                    {
                        //Make the API call
                        GeoAuthApi.AddRegionRequest regionRequest = new AddRegionRequest();
                        regionRequest.AddRegion(sLatitude, sLongitude, locationName, getCurrentDateTime(), null);

                        //Update the UI on the result
                        regionRequest.AddRegionStatus += (send, evt) =>
                        {
                            if (evt.Error == null)
                            {
                                lblErrors.Text = evt.Result;
                            }
                        };
                    }
                    else
                    {
                        lblErrors.Text = alreadyExistsInDB;
                    }

                }
                else if (Double.TryParse(txtBoxLocationRadius.Text, out dRadius))
                {
                    //Create the locationRegion object for the table
                    GeoAuthLocationRegion newLocationRegion = new GeoAuthLocationRegion
                    {
                        RegionName = locationName,
                        Latitude = dLatitude,
                        Longitude = dLongitude,
                        Radius = dRadius
                    };

                    if (geoAuthDBView.AddLocationRegion(newLocationRegion))
                    {

                        //Make the API call
                        GeoAuthApi.AddRegionRequest regionRequest = new AddRegionRequest();
                        regionRequest.AddRegion(sLatitude, sLongitude, locationName, getCurrentDateTime(), dRadius.ToString());

                        //Update the UI on the result
                        regionRequest.AddRegionStatus += (send, evt) =>
                        {
                            if (evt.Error == null)
                            {
                                lblErrors.Text = evt.Result;
                            }
                        };
                    }
                    else
                    {
                        lblErrors.Text = alreadyExistsInDB;
                    }
                }
                else
                {
                    lblErrors.Text = "Could not convert Radius to number";
                }
            }
            else
            {
                lblErrors.Text = "Please enter a name";
            }
        }

        private void CancelRegion_Click(object sender, RoutedEventArgs e)
        {
            // Hide the Region UI Elements and clear input
            txtBoxLocationName.Text = "";
            txtBoxLocationRadius.Text = "";
            pnlRegion.Visibility = System.Windows.Visibility.Collapsed;

            // Show the default buttons
            CreateRegion.Visibility = System.Windows.Visibility.Visible;
            CheckIn.Visibility = System.Windows.Visibility.Visible;

            clearErrorMessages();
        }

        #endregion

        #region Application logic

        /// <summary>
        /// Helper method to start up the location data acquisition
        /// </summary>
        /// <param name="accuracy">The accuracy level </param>
        private void StartLocationService(GeoPositionAccuracy accuracy)
        {
            // Reinitialize the GeoCoordinateWatcher
            StatusTextBlock.Text = "starting, " + accuracyText;
            watcher = new GeoCoordinateWatcher(accuracy);
            watcher.MovementThreshold = 20;

            // Add event handlers for StatusChanged and PositionChanged events
            watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(watcher_StatusChanged);
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);

            // Start data acquisition
            watcher.Start();
        }

        /// <summary>
        /// Handler for the StatusChanged event. This invokes MyStatusChanged on the UI thread and
        /// passes the GeoPositionStatusChangedEventArgs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => MyStatusChanged(e));

        }
        /// <summary>
        /// Custom method called from the StatusChanged event handler
        /// </summary>
        /// <param name="e"></param>
        void MyStatusChanged(GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    // The location service is disabled or unsupported.
                    // Alert the user
                    StatusTextBlock.Text = "location is unsupported on this device";
                    break;
                case GeoPositionStatus.Initializing:
                    // The location service is initializing.
                    // Disable the Start Location button
                    StatusTextBlock.Text = "initializing location service," + accuracyText;
                    break;
                case GeoPositionStatus.NoData:
                    // The location service is working, but it cannot get location data
                    // Alert the user and enable the Stop Location button
                    StatusTextBlock.Text = "data unavailable," + accuracyText;
                    break;
                case GeoPositionStatus.Ready:
                    // The location service is working and is receiving location data
                    // Show the current position and enable the Stop Location button
                    StatusTextBlock.Text = "receiving data, " + accuracyText;
                    break;

            }
        }

        /// <summary>
        /// Handler for the PositionChanged event. This invokes MyStatusChanged on the UI thread and
        /// passes the GeoPositionStatusChangedEventArgs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => MyPositionChanged(e));
        }

        /// <summary>
        /// Custom method called from the PositionChanged event handler
        /// </summary>
        /// <param name="e"></param>
        void MyPositionChanged(GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            // Update the TextBlocks to show the current location
            string latitude =  e.Position.Location.Latitude.ToString("0.000");
            string longitude = e.Position.Location.Longitude.ToString("0.000");
            LatitudeTextBlock.Text = latitude;
            LongitudeTextBlock.Text = longitude;

            // Check to see if we are in a named region

        }

        


        #endregion


        #region small helper functions
        private string getCurrentDateTime()
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm:ss");
            return date + "+" + time;
        }

        private void clearErrorMessages()
        {
            lblErrors.Text = "";
        }
        #endregion

    }
}
