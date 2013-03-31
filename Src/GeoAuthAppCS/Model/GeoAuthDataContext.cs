﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace GeoAuthApp.Model
{
    public class GeoAuthDataContext : DataContext
    {
        // Pass the connection string to the base class.
        public GeoAuthDataContext(string connectionString)
            : base(connectionString)
        { }

        // Specify a table for location region
        public Table<LocationRegion> LocationRegion;
    }

    [Table]
    public class LocationRegion : INotifyPropertyChanged, INotifyPropertyChanging
    {

        #region Columns

        private int _regionId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int regionId
        {
            get
            {
                return _regionId;
            }
            set
            {
                if (_regionId != value)
                {
                    NotifyPropertyChanging("regionId");
                    _regionId = value;
                    NotifyPropertyChanged("regionId");
                }
            }
        }


        private string _regionName;

        [Column(IsPrimaryKey = true, IsDbGenerated = false, DbType = "NVARCHAR(255) NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnUpdate)]
        public string regionName
        {
            get
            {
                return _regionName;
            }
            set
            {
                if (_regionName != value)
                {
                    NotifyPropertyChanging("regionName");
                    _regionName = value;
                    NotifyPropertyChanged("regionName");
                }
            }
        }

        private double _latitude;
        [Column(IsPrimaryKey = false, IsDbGenerated = false, DbType = "float NOT NULL", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public double latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                if (_latitude != value)
                {
                    NotifyPropertyChanging("latitude");
                    _latitude = value;
                    NotifyPropertyChanged("latitude");
                }
            }
        }

        private double _longitude;
        [Column(IsPrimaryKey = false, IsDbGenerated = false, DbType = "float NOT NULL", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public double longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                if (_longitude != value)
                {
                    NotifyPropertyChanging("longitude");
                    _longitude = value;
                    NotifyPropertyChanged("longitude");
                }
            }
        }

        private double _radius;
        [Column(IsPrimaryKey = false, IsDbGenerated = false, DbType = "float NOT NULL", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public double radius
        {
            get
            {
                return _radius;
            }
            set
            {
                if (_radius != value)
                {
                    NotifyPropertyChanging("radius");
                    _longitude = value;
                    NotifyPropertyChanged("radius");
                }
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
}