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
    /// Interaction logic for TableRecordsWindow.xaml
    /// </summary>
    public partial class TableRecordsWindow : Window
    {
        public TableRecordsWindow()
        {
            InitializeComponent();
        }

        public TableRecordsWindow(List<Player> listPlayers)
        {
            InitializeComponent();
            foreach (var player in listPlayers)
            {
                NickNameListBox.Items.Add(player.NickName);
                ResultListBox.Items.Add(player.Result);
            }
        }
    }
}
