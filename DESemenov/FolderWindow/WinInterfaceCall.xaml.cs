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
    /// Interaction logic for WinInterfaceCall.xaml
    /// </summary>
    public partial class WinInterfaceCall : Window
    {
        SqlConnection sqlConnection =
           new SqlConnection(App.ConnectionString());
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        FolderClass.ClassEdit classEdit;
        public WinInterfaceCall()
        {
            InitializeComponent();
            classEdit = new FolderClass.ClassEdit();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            classEdit.Edit("Update LeadsAndCalls " +
               $"set LeadCallDateTime='{DpDateCall.Text}', " +
               $"CallDuration='{TbDurationCall.Text}', " +
               $"Comment='{TbCommentCall.Text}' " +
               $"Where IdLeadAndCalls='" +
               $"{FolderClass.ClassLeadAndCall.IdLeadAndCalls}'");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * from dbo.ViewListCall " +
                $"Where [IdLeadAndCalls]=" +
                $"'{FolderClass.ClassLeadAndCall.IdLeadAndCalls}'", sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                DpDateCall.Text = dataReader[1].ToString();
                TbDurationCall.Text = dataReader[4].ToString();
                TbLeadCall.Text = dataReader[3].ToString();
                TbUserCall.Text = dataReader[2].ToString();
                TbCommentCall.Text = dataReader[8].ToString();
            }
            catch (Exception ex)
            {
                FolderClass.ClassMB.MBError(ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
