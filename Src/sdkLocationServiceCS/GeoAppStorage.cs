using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;

namespace GeoAuthApp
{
    /// <summary>
    /// Manages the storage of application settings and information
    /// </summary>
    /// <author>Andrew From (fromx010)</author>
    public class GeoAppStorage
    {
        private IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;

        public string DeviceId
        {
            get
            {
                try
                {
                    return (string)storage["deviceId"];
                }
                catch (System.Collections.Generic.KeyNotFoundException)
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    storage.Add("deviceId", value);
                }
                catch (ArgumentException ex)
                {
                    // TODO how should we handle this?
                }
            }
        }

        public GeoAppStorage()
        { 
        }


    }
}
