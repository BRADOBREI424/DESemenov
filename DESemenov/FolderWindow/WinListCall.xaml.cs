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
    /// Interaction logic for WinListCall.xaml
    /// </summary>
    public partial class WinListCall : Window
    {
        FolderClass.ClassDG classDG;
        FolderClass.ClassCB classCB;
        FolderClass.ClassEdit classEdit;
        public WinListCall()
        {
            InitializeComponent();
            classDG = new FolderClass.ClassDG(DgListCall);
            classCB = new FolderClass.ClassCB();
            classEdit = new FolderClass.ClassEdit();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            classDG.LoadDB("Select * from dbo.ViewListCall " +               
                $"Where Login='{FolderClass.ClassUser.Login}' " +
                $"and NameStatus='Активный' " +
                $"and Status='Активен' " +
                $"Order by LeadCallDateTime DESC ");
            classCB.LoadCB(CbUser);
        }
        
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            classEdit.Edit("Update LeadsAndCalls set Status='Удален' " +
                $"Where IdLeadAndCalls='{classDG.SelectId()}'");
            classDG.LoadDB("Select * from dbo.ViewListCall " +
                $"Where Login='{FolderClass.ClassUser.Login}' " +
                $"and NameStatus='Активный' and Status='Активен' " +
                $"Order by LeadCallDateTime DESC ");
        }

        private void CbUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            classDG.LoadDB("Select * from dbo.ViewListCall " +
               $"Where FIO Like '{CbUser.Text}' " +
               $"and NameStatus='Активный' and Status='Активен' " +
               $"Order by LeadCallDateTime DESC ");
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            FolderWindow.WinEdit winEdit = new WinEdit();
            winEdit.ShowDialog();
        }

        private void DgListCall_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FolderClass.ClassLeadAndCall.IdLeadAndCalls = Int32.Parse(classDG.SelectId());          
            if(FolderClass.ClassLeadAndCall.IdLeadAndCalls <1)
            {
                FolderClass.ClassMB.MBInformation("Вы не выбрали строку");
            }
            else
            {
                WinInterfaceCall winInterfaceCall = new WinInterfaceCall();
                winInterfaceCall.ShowDialog();
            }
        }
    }
}
