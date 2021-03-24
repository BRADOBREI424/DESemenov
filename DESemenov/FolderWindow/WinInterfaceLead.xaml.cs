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
    /// Interaction logic for WinInterfaceLead.xaml
    /// </summary>
    public partial class WinInterfaceLead : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(App.ConnectionString());
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        FolderClass.ClassDG classDG;
        FolderClass.ClassCB classCB;
        FolderClass.ClassEdit classEdit;
        public WinInterfaceLead()
        {
            InitializeComponent();
            classDG = new FolderClass.ClassDG(DgListCall);
            classCB = new FolderClass.ClassCB();
            classEdit = new FolderClass.ClassEdit();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                classCB.LoadCBLead(CbStatus);

                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * from dbo.ViewListLead " +
                $"Where [IdLead]=" +
                $"'{FolderClass.ClassLead.IdLead}'", sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                DpDateTimeLeadCreated.Text = dataReader[4].ToString();
                TbNumberPhone.Text = dataReader[2].ToString();
                TbComment.Text = dataReader[7].ToString();
                TbMasteringSalesSkills.Text = dataReader[3].ToString();
                TbKnowledgeOfCompanysProducts.Text = dataReader[8].ToString();
                TbWorkingWithObjections.Text = dataReader[9].ToString();
                TbFIO.Text = dataReader[1].ToString();

                classDG.LoadDB("Select * From dbo.ViewListCall " +
                $"Where NumberPhoneLead='{TbNumberPhone.Text}'");
            }
            catch (Exception ex)
            {
                FolderClass.ClassMB.MBError(ex);
            }
            finally
            {
                sqlConnection.Close();
            }

            if (CbStatus.Text == "Неактивный")
            {
                BtnSave.IsEnabled = false;
            }
            
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            FolderClass.ClassStatusLead.IdStatus = Int32.Parse(CbStatus.SelectedValue.ToString());

            classEdit.Edit("Update dbo.Lead " +
               $"set NumberPhoneLead='{TbNumberPhone.Text}', " +
               $"WorkingWithObjections='{TbWorkingWithObjections.Text}', " +
               $"KnowledgeOfCompanysProducts='{TbKnowledgeOfCompanysProducts.Text}', " +
               $"MasteringSalesSkills='{TbMasteringSalesSkills.Text}', " +
               $"DateTimeLeadCreated='{DpDateTimeLeadCreated.Text}', " +
               $"Comment='{TbComment.Text}', " +
               $"IdStatus='{FolderClass.ClassStatusLead.IdStatus}' " +
               $"Where IdLead='{FolderClass.ClassLead.IdLead}'");
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (DpDateTimeLeadCreated.SelectedDate.ToString() == "")
            {
               FolderClass.ClassMB.MBError("Введите дату создания лида");
                DpDateTimeLeadCreated.Focus();
            }
            else if (string.IsNullOrEmpty(TbNumberPhone.Text))
            {
                FolderClass.ClassMB.MBError("Введите контактные данные лида");
                TbNumberPhone.Focus();
            }
            else if (string.IsNullOrEmpty(TbMasteringSalesSkills.Text))
            {
               FolderClass.ClassMB.MBError("Введите овладение навыками продаж");
                TbMasteringSalesSkills.Focus();
            }
            else if (string.IsNullOrEmpty(TbWorkingWithObjections.Text))
            {
               FolderClass.ClassMB.MBError("Введите работу с возражениями");
                TbWorkingWithObjections.Focus();
            }
            else if (string.IsNullOrEmpty(TbKnowledgeOfCompanysProducts.Text))
            {
                FolderClass.ClassMB.MBError("Введите знания продуктов компании");
                TbKnowledgeOfCompanysProducts.Focus();
            }
            else if (CbStatus.SelectedValue.ToString() == "")
            {
                FolderClass.ClassMB.MBError("Введите статус");
                CbStatus.Focus();
            }

            else
            {
                FolderClass.ClassStatusLead.IdStatus = Int32.Parse(CbStatus.SelectedValue.ToString());
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert into dbo.Lead " +
                    "(DateTimeLeadCreated, NumberPhoneLead, " +
                    "Comment, MasteringSalesSkills, WorkingWithObjections, " +
                    "KnowledgeOfCompanysProducts, IdStatus) " +
                    "Values " +
                    "(@DateTimeLeadCreated, " +
                    "@NumberPhoneLead, " +
                    "@Comment, " +
                    "@MasteringSalesSkills, " +
                    "@WorkingWithObjections, " +
                    "@KnowledgeOfCompanysProducts, " +
                    "@IdStatus)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("DateTimeLeadCreated", DpDateTimeLeadCreated.SelectedDate.GetValueOrDefault());
                    sqlCommand.Parameters.AddWithValue("NumberPhoneLead", TbNumberPhone.Text);
                    sqlCommand.Parameters.AddWithValue("Comment", TbComment.Text);
                    sqlCommand.Parameters.AddWithValue("MasteringSalesSkills", TbMasteringSalesSkills.Text);
                    sqlCommand.Parameters.AddWithValue("WorkingWithObjections", TbWorkingWithObjections.Text);
                    sqlCommand.Parameters.AddWithValue("KnowledgeOfCompanysProducts", TbKnowledgeOfCompanysProducts.Text);
                    sqlCommand.Parameters.AddWithValue("IdStatus", FolderClass.ClassStatusLead.IdStatus);
                    sqlCommand.ExecuteNonQuery();

                    FolderClass.ClassMB.MBInformation("Добавление лида прошло успешно");
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
}
