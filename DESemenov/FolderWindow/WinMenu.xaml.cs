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
    /// Логика взаимодействия для WinMenu.xaml
    /// </summary>
    public partial class WinMenu : Window
    {
        public WinMenu()
        {
            InitializeComponent();
        }

        private void BtnListCall_Click(object sender, RoutedEventArgs e)
        {
            FolderWindow.WinListCall winListCall =
                new WinListCall();
            winListCall.ShowDialog();
        }

        private void BtnListLead_Click(object sender, RoutedEventArgs e)
        {
            FolderWindow.WinListLead winListLead =
                new WinListLead();
            winListLead.ShowDialog();
        }

        private void BtnListUser_Click(object sender, RoutedEventArgs e)
        {
            FolderWindow.WinUserList winUserList =
                new WinUserList();
            winUserList.ShowDialog();
        }
    }
}
