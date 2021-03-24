using System;
using System.Collections.Generic;
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
    /// Interaction logic for WinListLead.xaml
    /// </summary>
    public partial class WinListLead : Window
    {
        FolderClass.ClassDG classDG;
        FolderClass.ClassCB classCB;
        FolderClass.ClassEdit classEdit;
        public WinListLead()
        {
            InitializeComponent();
            classDG = new FolderClass.ClassDG(DgListLead);
            classCB = new FolderClass.ClassCB();
            classEdit = new FolderClass.ClassEdit();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            classDG.LoadDB("Select * from dbo.ViewListLead " +
                $"Where Login='{FolderClass.ClassUser.Login}' " +
                $"and NameStatus='Активный' " +
                $"Order by DateTimeLeadCreated DESC");
            classCB.LoadCB(CbUser);
            classCB.LoadCBLead(CbStatus);
        }

        private void CbUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            classDG.LoadDB("Select * From dbo.ViewListLead " +
                $"Where FIO Like '{CbUser.Text}' " +
                $"and NameStatus='Активный' " +
                $"Order by DateTimeLeadCreated DESC ");
        }

        private void CbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            classDG.LoadDB("Select * From dbo.ViewListLead " +
                $"Where NameStatus Like '{CbStatus.Text}' " +
                $"and FIO Like '{CbUser.Text}' " +
                $"Order by DateTimeLeadCreated DESC ");
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            classEdit.Edit("Update Lead set IdStatus='2' " +
                  $"Where IdLead='{classDG.SelectId()}'");
            classDG.LoadDB("Select * from dbo.ViewListLead " +
                $"Where Login='{FolderClass.ClassUser.Login}' " +
                $"and NameStatus='Активный' " +
                $"Order by DateTimeLeadCreated DESC ");

        }

        private void DgListLead_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FolderClass.ClassLead.IdLead = Int32.Parse(classDG.SelectId());
            if (FolderClass.ClassLead.IdLead < 1)
            {
                FolderClass.ClassMB.MBInformation("Вы не выбрали строку");
            }
            else
            {
                FolderWindow.WinInterfaceLead winInterfaceLead
                    = new WinInterfaceLead();
                winInterfaceLead.ShowDialog();
            }
        }
    }
}
