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
    /// Логика взаимодействия для WinAuthorization.xaml
    /// </summary>
    public partial class WinAuthorization : Window
    {
        public WinAuthorization()
        {
            InitializeComponent();
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            FolderClass.ClassAuthorization classAuthorization = 
                new FolderClass.ClassAuthorization();
            classAuthorization.Authorization(TbLogin,PsbPassword);
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            FolderClass.ClassMB.MessageQuestionExit();
        }
    }
}
