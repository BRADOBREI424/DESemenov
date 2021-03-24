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
    class ClassDG
    {
        SqlConnection sqlConnection =
            new SqlConnection(App.ConnectionString());
        SqlDataAdapter dataAdapter;
        DataGrid dataGrid;
        DataTable dataTable;

        public ClassDG(DataGrid dataGrid)
        {
            this.dataGrid = dataGrid;
        }

        public void LoadDB(string sqlCommand)
        {
            try
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(sqlCommand, sqlConnection);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                ClassMB.MBError(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public string SelectId()
        {
            object[] mass = null;
            string id = "";
            try
            {
                if (dataGrid != null)
                {
                    DataRowView dataRowView = dataGrid.SelectedItem
                        as DataRowView;
                    if (dataRowView != null)
                    {
                        DataRow dataRow = (DataRow)dataRowView.Row;
                        mass = dataRow.ItemArray;
                    }
                }
                id = mass[0].ToString();
            }
            catch
            {
                FolderClass.ClassMB.MBError("Вы не выбрали строку!");
            }
            return id;
        }
    }
}
