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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ExAdoNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                List<Currency> сurrencies = new List<Currency>();
                XDocument xdoc = XDocument.Load("http://www.nationalbank.kz/rss/rates_all.xml");

                foreach (XElement item in xdoc.Element("rss").Element("channel").Elements("item"))
                {
                    Currency cur = new Currency();
                    cur.title = item.Element("title").Value;
                    cur.PubDate = Convert.ToDateTime(item.Element("pubDate").Value);
                    cur.Description = Convert.ToDouble(item.Element("description").Value.Replace('.', ','));

                    сurrencies.Add(cur);
                }

                cbxCur1.ItemsSource = сurrencies;
                cbxCur1.SelectedIndex = 0;
                cbxCur2.ItemsSource = сurrencies;
                cbxCur2.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblMessage.Content = ex.Message;
            }
        }

        private void TbxSum1_KeyUP(object sender, RoutedEventArgs e)
        {
            double sum = 1;
            double.TryParse(tbxSum1.Text, out sum);
            Currency cur = (Currency)cbxCur2.SelectedItem;
            tbxSum2.Text = (sum / cur.Description).ToString();
        }

        private void TbxSum2_KeyUP(object sender, RoutedEventArgs e)
        {
            double sum = 1;
            double.TryParse(tbxSum2.Text, out sum);
            Currency cur = (Currency)cbxCur2.SelectedItem;
            tbxSum1.Text = (sum / cur.Description).ToString();
        }

        private void CbxCur1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CbxCur2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
