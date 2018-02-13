using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNUCHANTIER
{
    public class FoxProCode : INotifyPropertyChanged
    {
        public char CodeMetier { get; set; }
        public string Titre { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ObservableCollection<FoxProCode> GetProducts(string connectionString)
        {
            const string GetProductsQuery = "select CodeMetier, Titre" +
               " from dbo.fp_metier";

            var products = new ObservableCollection<FoxProCode>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetProductsQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var product = new FoxProCode();
                                    product.CodeMetier = reader.GetChar(0);
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

}
