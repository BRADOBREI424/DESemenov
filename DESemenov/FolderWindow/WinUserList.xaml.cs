using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DESemenov.FolderWindow
{
    /// <summary>
    /// Interaction logic for WinUserList.xaml
    /// </summary>
    public partial class WinUserList : Window
    {
        SqlConnection sqlConnection =
           new SqlConnection(App.ConnectionString());
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        FolderClass.ClassDG classDG;
        public WinUserList()
        {
            InitializeComponent();
            classDG = new FolderClass.ClassDG(dgListUser);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            classDG.LoadDB("Select * from dbo.[User]");
        }
    }
}
