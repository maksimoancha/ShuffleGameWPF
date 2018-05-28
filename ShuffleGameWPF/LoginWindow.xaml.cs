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

namespace ShuffleGameWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginWindow_OnContentRendered(object sender, EventArgs e)
        {
            txtNickName.SelectAll();
            txtNickName.Focus();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (!Db_Controller.ContainsPlayer(txtNickName.Text))
            {
                Db_Controller.InsertPlayer(txtNickName.Text);
            }
            this.Hide();
            var wind = new MainWindow(txtNickName.Text);
            wind.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
