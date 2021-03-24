using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESemenov.FolderClass
{
    class ClassEdit
    {
        SqlConnection sqlConnection =
        new SqlConnection(App.ConnectionString());
        SqlCommand sqlCommand;

        public void Edit(string sqlCommand)
        {
            try
            {
                sqlConnection.Open();
                this.sqlCommand = new SqlCommand(sqlCommand, sqlConnection);
                this.sqlCommand.ExecuteNonQuery();
                ClassMB.MBInformation("Данные отредактированы");

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
