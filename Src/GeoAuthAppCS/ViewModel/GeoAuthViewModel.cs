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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using GeoAuthApp.Model;
using System.Data.Linq;

namespace GeoAuthApp.ViewModel
{
    /// <summary>
    /// Implements the LINQ to SQL data accesors, settors, and queries
    /// for the local database.
    /// </summary>
    /// <author>Andrew From (fromx010)</author>
    public class GeoAuthViewModel : INotifyPropertyChanged
    {
        // LINQ to SQL data context for the local database.
        private GeoAuthDataContext geoAuthAppDB;

        public GeoAuthViewModel(string geoAuthDBConnectionString)
        {
            geoAuthAppDB = new GeoAuthDataContext(geoAuthDBConnectionString);
        }

        /// <summary>
        /// Locations the exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// <c>true</c>: If a location exists using that name
        /// <c>false</c>: If that nane is not in use
        /// </returns>
        public bool LocationExists(string name)
        {
            bool exists;
            return exists = geoAuthAppDB.LocationRegion.Any(r => r.RegionName == name);
        }

        /// <summary>
        /// Adds a location region.
        /// </summary>
        /// <param name="newLocationRegion">The new location region.</param>
        /// <returns>
        /// <c>true</c>: If the location was added
        /// <c>false</c>: If the location already exists
        /// </returns>
        public bool AddLocationRegion(GeoAuthLocationRegion newLocationRegion)
        {
            if (!LocationExists(newLocationRegion.RegionName))
            {
                //add the item to the table
                geoAuthAppDB.LocationRegion.InsertOnSubmit(newLocationRegion);

                //Save the changes
                geoAuthAppDB.SubmitChanges();

                //name didn't exist so we added it
                return true;
            }
            else
            {
                return false;
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify Silverlight that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
