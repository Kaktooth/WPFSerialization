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

namespace WPFSerialization
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Locality l = (Locality)((ListBox)Application.Current.Windows[0].FindName("listBox1")).SelectedItem;
            Window3 f3 = new Window3();
            f3.ShowDialog();
            if (l.plots != null)
            {
                listBox1.Items.Clear();
                foreach (var plot in l.plots)
                {
                    listBox1.Items.Add(plot);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            Locality l = (Locality)((ListBox)Application.Current.Windows[0].FindName("listBox1")).SelectedItem;

            if (listBox1.SelectedItem != null)
            {
                string p = (listBox1.SelectedItem).ToString();
           
                string[] str = p.Trim().Split(' ');
                MessageBox.Show(p);
                Window3 f3 = new Window3();

                (f3.comboBox1).Text = str[1];
                (f3.textBox1).Text = str[3];
                (f3.textBox2).Text = str[5];
                int year, month, day, hour, minute, seconds;
                string time = str[7] + " " + str[8];
                string[] s = time.Split('.', ':', ' ');
                int i = 0;
                foreach (string st in s)
                {
                    if (st[0] == '0')
                    {
                        s[i] = st.Remove(0, 1);
                    }
                    i++;
                }
                year = Convert.ToInt32(s[2]);
                month = Convert.ToInt32(s[1]);
                day = Convert.ToInt32(s[0].Replace(" ", ""));


                f3.DatePicker.SelectedDate = new DateTime(year, month, day);
                (f3.textBox4).Text = str[14];
                (f3.comboBox2).Text = str[17];


                for (int j = 18; j < str.Length; j++)
                {
                    string[] st = str[j].Split(';');
                    st[0] = st[0].Replace("{X=", "");
                    st[1] = st[1].Replace("Y=", "");
                    st[1] = st[1].Replace("}", "");
                    ((ListBox)f3.listBox1).Items.Add(new Point(Convert.ToInt32(st[0]), Convert.ToInt32(st[1])));
                }
            (f3.textBox5).Text = str[10];
                
                f3.ShowDialog();
                if (l.plots != null)
                {
                    listBox1.Items.Clear();
                    foreach (var plot in l.plots)
                    {
                        listBox1.Items.Add(plot);
                    }
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //var larray = ((ListBox)Application.Current.Windows[0].FindName("listBox1")).Items;
            //List<Locality> listL = new List<Locality>();
            //foreach (var item in larray)
            //{
            //    if()
            //    listL.Add((Locality)item);
            //}
           
            Locality l =(Locality)((ListBox)Application.Current.Windows[0].FindName("listBox1")).SelectedItem;
            ListBox list = ((ListBox)Application.Current.Windows[0].FindName("listBox1"));
            l.RemoveProperty((Property)listBox1.SelectedItem);
            listBox1.Items.Remove((Property)listBox1.SelectedItem);
            //list.Items.Remove((Property)listBox1.SelectedItem);
            //list.Items.Clear();
       
            //list.Items.Add(l);
            //foreach (var item in listL)
            //{
            //    list.Items.Add(item);
            //}
            if (l.plots != null)
            {
                listBox1.Items.Clear();
                foreach (var plot in l.plots)
                {
                    listBox1.Items.Add(plot);
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Locality l = (Locality)((ListBox)Application.Current.Windows[0].FindName("listBox1")).SelectedItem;
            ListBox list = (ListBox)Application.Current.Windows[0].FindName("listBox1");
            //list.Items.Clear();
            l.ClearProperty();
            listBox1.Items.Clear();
            //list.Items.Add(l);
            
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Locality l = (Locality)((ListBox)Application.Current.Windows[0].FindName("listBox1")).SelectedItem;
            if (l.plots != null)
            {
                listBox1.Items.Clear();
                foreach (var plot in l.plots)
                {
                    listBox1.Items.Add(plot);
                }
            }
        }
    }
}
