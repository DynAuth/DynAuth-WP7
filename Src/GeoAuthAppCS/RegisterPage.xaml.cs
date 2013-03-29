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
        }
        #endregion

        #region User Interface
        private void Register_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}