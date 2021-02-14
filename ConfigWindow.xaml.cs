using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace The_Dystopia
{
    /// <summary>
    /// Logique d'interaction pour ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public ConfigWindow()
        {
            InitializeComponent();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Validate(object sender, RoutedEventArgs e)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string A3Text = A3.Text;
            string TSText = TS.Text;
            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(docPath + "\\ts.txt"))
                {
                    if (TSText != "")
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(TSText);
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Vous devez entre un chemin valide vers Teamspeak");
                    }
                }
                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(docPath + "\\ts.txt"))
                {
                    string ts = "";
                    while ((ts = sr.ReadLine()) != null)
                    {
                        this.Close();
                    }
                }
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(docPath + "\\a3.txt"))
                {
                    if(A3Text != "")
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(A3Text);
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Vous devez entre un chemin valide vers Arma III");
                    }
                }
                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(docPath + "\\a3.txt"))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        if(s != "")
                        {
                            this.Close();
                        }
                        Console.WriteLine(s);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            A3.Text = dialog.SelectedPath;

            string folder = dialog.SelectedPath;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            TS.Text = dialog.SelectedPath;

            string folder = dialog.SelectedPath;
        }

        private void InstallTFAR(object sender, RoutedEventArgs e)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string tspath = docPath + "\\ts.txt";
            string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            
                    string TSFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    string inputfilepath = TSFolderPath + "\\task_force_radio.ts3_plugin";
                    string ftphost = "165.22.93.32";
                    string ftpfilepath = "/pub/task_force_radio.ts3_plugin";

                    string ftpfullpath = "ftp://" + ftphost + ftpfilepath;

                    using (WebClient request = new WebClient())
                    {
                        byte[] fileData = request.DownloadData(ftpfullpath);
                        Console.WriteLine(fileData);
                        using (FileStream file = File.Create(inputfilepath))
                        {
                            file.Write(fileData, 0, fileData.Length);
                            Console.WriteLine("dl");
                            file.Close();
                        }
                        System.Windows.MessageBox.Show("Download Complete");
                        ProcessStartInfo psi = new ProcessStartInfo(TSFolderPath + "\\task_force_radio.ts3_plugin");
                        Process.Start(psi);
                    }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string a3path = docPath + "\\a3.txt";
                using (StreamReader sr = File.OpenText(a3path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        if (s == "")
                        {
                            System.Windows.MessageBox.Show("Veuillez renseignez le chemin vers Arma III");
                        }


                        if (Directory.Exists(s + "\\!Workshop\\@Dystopia"))
                        {
                            System.Windows.MessageBox.Show("Vous êtes à jour !");
                        }
                        else
                        {
                            string tspath = docPath + "\\a3.txt";
                            string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                            string a3FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                            string inputfilepath = a3FolderPath + "\\@Dystopia.rar";

                        }
                        // System.Windows.MessageBox.Show("vous devez installer le mod Dystopia avant");
                        // DownloadElement("165.22.93.32/pub/@Dystopia.rar", s + "\\!Workshop\\@Dystopia");
                    }
                    }
                }
        }
    }
}
