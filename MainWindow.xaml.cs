using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Xml.Serialization;

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

            comboBox1.Items.Add(Serialization.Binary);
            comboBox1.Items.Add(Serialization.XML);
            comboBox1.SelectedItem = comboBox1.Items[0];
        }
        public enum Serialization { Binary, XML }
        

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
        private String fileNameBinary = Directory.GetCurrentDirectory() + "\\SerializedData.txt";
        private String fileNameXML = Directory.GetCurrentDirectory() + "\\SerializedData.xml";

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Do you want to save changes?", "Serialisation",
             MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                //MessageBox.Show(fileNameBinary);
                List<Locality> list = new List<Locality>();
                foreach (var loc in listBox1.Items)
                {
                    list.Add((Locality)loc);
                }
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(List<Locality>), new Type[] { typeof(Owner), typeof(Property), typeof(Description), typeof(Owner), typeof(Locality), typeof(Appointment) });

                FileStream fsBinary = new FileStream(fileNameBinary, FileMode.Create);
                FileStream fsXML = new FileStream(fileNameXML, FileMode.Create);
                // Construct a BinaryFormatter and use it to serialize the data to the stream.
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    
                    xmlFormatter.Serialize(fsXML, list);
                    formatter.Serialize(fsBinary, list);
                }
                catch (SerializationException ex)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + ex.Message);
                    throw;
                }
                finally
                {
                    fsBinary.Close();
                    fsXML.Close();
                }
            }
        }

        private void Deserialize(object sender, RoutedEventArgs e)
        {
            try
            {     
                switch ((Serialization)comboBox1.SelectedItem)
                {
                    case Serialization.Binary:
                        if (!(File.Exists(fileNameBinary)))
                        {
                            File.Create(fileNameBinary);
                        }
                        FileStream fileStream2 = new FileStream(fileNameBinary, FileMode.Open);
                        BinaryFormatter bf2 = new BinaryFormatter();

                        List<Locality> l = new List<Locality>();
                        try
                        {
                            l = bf2.Deserialize(fileStream2) as List<Locality>;
                        }
                        catch (SerializationException ex)
                        {
                            Console.WriteLine("Failed to open serializable file. File not created yet? ");
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
                        break;
                    case Serialization.XML:
                        if (!(File.Exists(fileNameXML)))
                        {
                            File.Create(fileNameXML);
                        }
                        FileStream fileStream = new FileStream(fileNameXML, FileMode.Open);
                        XmlSerializer xmlFormatter = new XmlSerializer(typeof(List<Locality>), new Type[] { typeof(Owner), typeof(Property), typeof(Description), typeof(Owner), typeof(Locality), typeof(Appointment) });
                        List<Locality> loc = new List<Locality>();
                        try
                        {
                            loc = xmlFormatter.Deserialize(fileStream) as List<Locality>;
                        }
                        catch (SerializationException ex)
                        {
                            Console.WriteLine("Failed to open serializable file. File not created yet? ");
                            throw;
                        }
                        finally
                        {
                            fileStream.Close();
                        }
                        foreach (var locality in loc)
                        {
                            listBox1.Items.Add(locality);
                        }
                        break;
                    default:
                        Console.WriteLine("Please change method");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + " " + fileNameBinary+fileNameXML);

            }
        }
    }
}
