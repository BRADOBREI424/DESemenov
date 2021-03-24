using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DESemenov.FolderClass
{
    class ClassAuthorization
    {
        SqlConnection sqlConnection = 
            new SqlConnection(App.ConnectionString());
        SqlCommand sqlCommand;
        SqlDataReader dataReader;

        public void Authorization(TextBox TbLogin, PasswordBox PsbPassword)
        {
            if(string.IsNullOrEmpty(TbLogin.Text))
            {
                ClassMB.MBError("Введите логин");
                TbLogin.Focus();
            }
            else if (string.IsNullOrEmpty(PsbPassword.Password))
            {
                ClassMB.MBError("Введите пароль");
                TbLogin.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Select [Password] from dbo.[User] " +
                        $"Where [Login]='{TbLogin.Text}'",sqlConnection);
                    dataReader = sqlCommand.ExecuteReader();
                    dataReader.Read();

                    if(dataReader[0].ToString() != PsbPassword.Password)
                    {
                        ClassMB.MBError("Неверный пароль");
                        PsbPassword.Focus();
                    }
                    else
                    {
                        ClassUser.Login = TbLogin.Text;
                        ClassUser.Password = PsbPassword.Password;

                        FolderWindow.WinMenu winMenu = 
                            new FolderWindow.WinMenu();
                        winMenu.ShowDialog();              
                    }
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
}
