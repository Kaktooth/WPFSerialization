using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFSerialization
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
            comboBox1.Items.Add(Appointment.AppointmentValue.Agricultural);
            comboBox1.Items.Add(Appointment.AppointmentValue.Reserved);
            comboBox1.Items.Add(Appointment.AppointmentValue.UnderBuilding);

            comboBox2.Items.Add(Description.SoilType.Chalk);
            comboBox2.Items.Add(Description.SoilType.Clay);
            comboBox2.Items.Add(Description.SoilType.Loam);
            comboBox2.Items.Add(Description.SoilType.Peat);
            comboBox2.Items.Add(Description.SoilType.Sandy);
            comboBox2.Items.Add(Description.SoilType.Silt);

            try
            {
                Locality l = (Locality)((ListBox)Application.Current.Windows[0].FindName("listBox1")).SelectedItem;

                ListBox plist = (ListBox)Application.Current.Windows[0].FindName("listBox1");
                ListBox pslist = (ListBox)Application.Current.Windows[2].FindName("listBox1");
                
                if (pslist != null && pslist.Items.Count != 0)
                {
                    Property p = (Property)pslist.SelectedItem;
                    if (p != null)
                    {
                        pslist.Items.Remove(p);
                        l.RemoveProperty(p);
                        plist.Items.Remove(p);
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        readonly Regex NameReg = new Regex(@"^[a-zA-Z]+\s?$");
        readonly Regex NumReg = new Regex(@"^[\d]+$");
        readonly Regex PriceReg = new Regex(@"^[\d]+(((,|\.)[1-9]+))?$");
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var control in grid.Children)
            {
                if (control is TextBox)
                {
                    if (((TextBox)control).Background == Brushes.Red)
                    {
                        MessageBox.Show("You have red text fields!");
                        return;
                    }
                }
            }
            Locality l = (Locality)((ListBox)Application.Current.Windows[0].FindName("listBox1")).SelectedItem;
            try
            {
                ListBox plist = (ListBox)Application.Current.Windows[0].FindName("listBox1");
                Appointment a = new Appointment((Appointment.AppointmentValue)comboBox1.SelectedItem);
                Owner o = new Owner(textBox1.Text, textBox2.Text, (DateTime)DatePicker.SelectedDate);
                List<Point> geo = new List<Point>();
                foreach (var point in listBox1.Items)
                {
                    geo.Add((Point)point);
                }
                Description d = new Description(Convert.ToInt32(textBox4.Text), (Description.SoilType)comboBox2.SelectedItem, geo);
                
                textBox5.Text.Replace(".", ",");
                double price = Convert.ToDouble(textBox5.Text);
                Property p = new Property(a, o, d, price);

              
                l.AddProperty(p);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check your form, you have error");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameReg.IsMatch(textBox1.Text))
            {
                textBox1.Background = Brushes.LightGreen;
            }
            else
            {
                textBox1.Background = Brushes.Red;
            }
        }

      

        private void textBox4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NumReg.IsMatch(textBox4.Text))
            {
                textBox4.Background = Brushes.LightGreen;
            }
            else
            {
                textBox4.Background = Brushes.Red;
            }
        }

        private void textBox5_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PriceReg.IsMatch(textBox5.Text))
            {
                textBox5.Background = Brushes.LightGreen;
            }
            else
            {
                textBox5.Background = Brushes.Red;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(textBox3.Text);
                int y = Convert.ToInt32(textBox6.Text);
                listBox1.Items.Add(new Point(x, y));
            }
            catch (System.FormatException ex)
            {
                return;
            }
            
            
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void TextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameReg.IsMatch(textBox2.Text))
            {
                textBox2.Background = Brushes.LightGreen;
            }
            else
            {
                textBox2.Background = Brushes.Red;
            }
        }

        private void TextBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NumReg.IsMatch(textBox3.Text))
            {
                textBox3.Background = Brushes.LightGreen;
            }
            else
            {
                textBox3.Background = Brushes.Red;
            }
            if(textBox3.Text == "")
            {
                textBox3.Background = Brushes.White;
            }
        }

        private void TextBox6_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NumReg.IsMatch(textBox6.Text))
            {
                textBox6.Background = Brushes.LightGreen;
            }
            else
            {
                textBox6.Background = Brushes.Red;
            }
            if (textBox6.Text == "")
            {
                textBox6.Background = Brushes.White;
            }
        }
    }
}
