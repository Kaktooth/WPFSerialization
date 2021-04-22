using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace WPFSerialization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window2 f = new Window2();
            Locality l = new Locality(listBox1.Items.Count);
            listBox1.Items.Add(l);

            listBox1.SelectedItem = l;
            f.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (listBox1.Items != null && listBox1.SelectedItem is Locality)
            {
                Locality l = (Locality)listBox1.SelectedItem;
                Window2 f = new Window2();
                f.ShowDialog();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }
        private String fileName = Directory.GetCurrentDirectory() + "\\SerializedData.txt";

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Do you want to save changes?", "Serialisation",
             MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                List<Locality> list = new List<Locality>();
                foreach (var loc in listBox1.Items)
                {
                    list.Add((Locality)loc);
                }
                FileStream fs = new FileStream(fileName, FileMode.Create);

                // Construct a BinaryFormatter and use it to serialize the data to the stream.
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(fs, list);
                }
                catch (SerializationException ex)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + ex.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(File.Exists(fileName)))
                {
                    File.Create(fileName);
                }
                FileStream fileStream2 = new FileStream(fileName, FileMode.Open);
                BinaryFormatter bf2 = new BinaryFormatter();

                List<Locality> l = new List<Locality>();
                try
                {
                    l = bf2.Deserialize(fileStream2) as List<Locality>;
                }
                catch (SerializationException ex)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + ex.Message);
                    throw;
                }
                finally
                {
                    fileStream2.Close();
                }
                foreach (var locality in l)
                {
                    listBox1.Items.Add(locality);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + " " + fileName);

            }
        }
    }
}
