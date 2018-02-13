using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MNUCHANTIER
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //ApplicationView currentView;
        public MainPage()
        {
            this.InitializeComponent();
            GetProducts((App.Current as App).ConnectionString);
        }

        //private void Btn_DbConnection_Click(object sender, RoutedEventArgs e) => 
        //    FoxProTest.ItemsSource = GetProducts((App.Current as App).ConnectionString);

        public ObservableCollection<FoxProCode> GetProducts(string connectionString)
        {
            const string GetProductsQuery = "select CodeMetier, Titre from dbo.fp_metier";

            var products = new ObservableCollection<FoxProCode>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State != ConnectionState.Closed)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetProductsQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var product = new FoxProCode();
                                    product.CodeMetier = reader.GetString(0);
                                    product.Titre = reader.GetString(1);
                                    products.Add(product);
                                }
                            }
                        }
                    }
                }
                return products;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }
    }

    public class FoxProCode : INotifyPropertyChanged
    {
        public string CodeMetier { get; set; }
        public string Titre { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));      
    }


}
