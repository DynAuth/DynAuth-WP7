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
        private static IsolatedStorageSettings Settings = IsolatedStorageSettings.ApplicationSettings;

        public GeoAppStorage()
        {
            mainServer = "http://cs5221.oko.io:8000";
            DeviceId = "b082253c85cd4ae7ab059523ad7191a0";
            StoreSetting("apiKey", "570915b1b5cb4db6981f463b48d09ad8");
        }

        /// <summary>
        /// Gets or sets the main server to communicate to.
        /// </summary>
        /// <value>
        /// The main server uri
        /// </value>
        public string mainServer
        {
            get
            {
                string value = null;
                if (TryGetSetting<string>("mainServer", out value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                StoreSetting("mainServer", value);
            }
        }

        /// <summary>
        /// Gets the API key.
        /// </summary>
        public string apiKey
        {
            get
            {
                string value = null;
                if (TryGetSetting<string>("apiKey", out value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the device id.
        /// </summary>
        /// <value>
        /// The device id.
        /// </value>
        public string DeviceId
        {
            get
            {
                string value = null;
                if (TryGetSetting<string>("deviceId", out value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                StoreSetting("deviceId", value);
            }
        }

        /// <summary>
        /// Gets or sets the device key.
        /// </summary>
        /// <value>
        /// The device key.
        /// </value>
        public string deviceKey
        {
            get
            {
                string value = null;
                if (TryGetSetting<string>("deviceKey", out value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                StoreSetting("deviceKey", value);
            }
        }

        /// <summary>
        /// Stores the setting.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="value">The value.</param>
        public static void StoreSetting(string settingName, string value)
        {
            StoreSetting<string>(settingName, value);
        }

        /// <summary>
        /// Stores the setting, with specified type.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="value">The value.</param>
        public static void StoreSetting<TValue>(string settingName, TValue value)
        {
            if (!Settings.Contains(settingName))
                Settings.Add(settingName, value);
            else
                Settings[settingName] = value;

            // EDIT: if you don't call Save  thenWP7 will corrupt your memory!
            Settings.Save();
        }

        public static void RemoveSetting(string settingName, string value)
        {
            if (Settings.Contains(settingName))
            {
                Settings.Remove(settingName);
            }
            Settings.Save();
        }

        /// <summary>
        /// Tries to retrieve a setting with specified type.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool TryGetSetting<TValue>(string settingName, out TValue value)
        {
            if (Settings.Contains(settingName))
            {
                value = (TValue)Settings[settingName];
                return true;
            }

            value = default(TValue);
            return false;
        }

    }
}
