using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DESemenov.FolderClass
{   
    class ClassCB
    {
        SqlConnection sqlConnection =
            new SqlConnection(App.ConnectionString());
        SqlDataAdapter dataAdapter;
        DataSet dataSet;

        public void LoadCB(ComboBox cbUser)
        {
            try
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter("SELECT IdUser, " +
                "FIO FROM dbo.[User] Order by IdUser ASC",
                sqlConnection);
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "[User]");
                cbUser.ItemsSource = dataSet.Tables["[User]"].DefaultView;
                cbUser.DisplayMemberPath = dataSet.Tables["[User]"].Columns["FIO"].ToString();
                cbUser.SelectedValuePath = dataSet.Tables["[User]"].Columns["IdUser"].ToString();
            }
            catch (Exception ex)
            {
                ClassMB.MBError(ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void LoadCBLead(ComboBox cbUser)
        {
            try
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter("SELECT IdStatus," +
                    "NameStatus FROM dbo.[LeadStatus] Order by IdStatus ASC",
                    sqlConnection);
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "[LeadStatus]");
                cbUser.ItemsSource = dataSet.Tables["[LeadStatus]"].DefaultView;
                cbUser.DisplayMemberPath =
                    dataSet.Tables["[LeadStatus]"].Columns["NameStatus"].ToString();
                cbUser.SelectedValuePath =
                    dataSet.Tables["[LeadStatus]"].Columns["IdStatus"].ToString();
            }
            catch (Exception ex)
            {
                ClassMB.MBError(ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
