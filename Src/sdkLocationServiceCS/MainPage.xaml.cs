/* 
 * Used http://code.msdn.microsoft.com/wpapps/Location-Service-Sample-6b9ef410
 * as inital code base for location services.
*/
using System;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Device.Location;

namespace GeoAuthApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        private GeoAuthAPI geoAuthApi = new GeoAuthAPI();
        /// <summary>
        /// This sample receives data from the Location Service and displays the geographic coordinates of the device.
        /// </summary>

        GeoCoordinateWatcher watcher;
        string accuracyText = "";

        #region Initialization

        /// <summary>
        /// Constructor for the PhoneApplicationPage object
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
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
            //Log the new location
            geoAuthApi.Checkin(latitude, longitude, DateTime.Now.ToString("HH:mm:ss tt"));
        }

        #endregion
    }
}
