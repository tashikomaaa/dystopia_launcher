using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
//Using namespaces  
using System.Data;
using System.Configuration;


namespace The_Dystopia
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //WebClient webClient;               // Our WebClient that will be doing the downloading for us
        Stopwatch sw = new Stopwatch();    // The stopwatch which we will be using to calculate the download speed
        string mods1 = "@Dystopia;";
        public MainWindow()
        {
            InitializeComponent();
        }
        #region MySqlConnection Connection  
        MySqlConnection conn = new
        MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        #endregion
        #region bind data to DataGrid.  
        private void btnloaddata_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("================");
            try
            {
                Console.WriteLine("totot");
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("Select Title, Content, Date FROM news", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                Console.WriteLine(ds);
                adp.Fill(ds, "LoadDataBinding");
                dataGridCustomers.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            finally {
                conn.Close();
            }
        }
        #endregion
        /*        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
                {
                    string sql = "SELECT * FROM news";
                    MySqlConnection conn = DBUtils.GetDBConnection();
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    // Établissez la connexion de la commande.
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                            }
                        }
                    }
                    conn.Close();
                }*/

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }



        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/wCjKV4QN");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("ts3server://ts.the-dystopia.online");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://the-dystopia.online/");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ConfigWindow cw = new ConfigWindow();
            cw.Owner = this;
            cw.Show();
        }
        
        private void Launch(object sender, RoutedEventArgs e)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string a3path = docPath + "\\a3.txt";
            String ArmaCommandLine = String.Join(" ", Environment.GetCommandLineArgs()).Replace(AppDomain.CurrentDomain.FriendlyName.ToString(), "");
            using (StreamReader sr = File.OpenText(a3path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    string a3exe = s + "\\arma3battleye.exe";
                    
                    using (Process compiler = new Process())
                    {
                        compiler.StartInfo.FileName = a3exe;
                        compiler.StartInfo.Arguments = " -connect=51.210.116.139:2337 -password=plopplop123 -mod=!Workshop\\" + mods1;
                        compiler.StartInfo.UseShellExecute = false;
                        compiler.StartInfo.RedirectStandardOutput = true;
                        Process.Start("ts3server://ts.the-dystopia.online");
                        //Process.Start(s + "\\arma3.exe");
                        compiler.Start();

                        // Process.Start(s + "\\BattlEye\\BEService.exe");
                        Console.WriteLine(compiler.StandardOutput.ReadToEnd());

                        compiler.WaitForExit();
                    }
                }
            }
        }

        private void Update(object sender, RoutedEventArgs e)
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


                    if (Directory.Exists(s + "\\" + mods1))
                    {
                        System.Windows.MessageBox.Show("Vous êtes à jour !");
                    }
                    else
                    {
                        string tspath = docPath + "\\a3.txt";
                        string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                        string a3FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                        string inputfilepath = a3FolderPath + "\\@Dystopia.rar";

                        string ftpfullpath = "ftp://165.22.93.32/pub/@Dystopia.rar";

                        // Download(ftpfullpath);
                        string fileName = getFilename(ftpfullpath);
                        downloadFile(ftpfullpath, a3FolderPath + "\\!Workshop");
                            /*Process process = new Process();
                            process.StartInfo.FileName = @"C:\Program Files\WinRAR\WinRAR.exe";
                            process.StartInfo.CreateNoWindow = true;
                            process.EnableRaisingEvents = false;
                            process.StartInfo.Arguments = string.Format("x -o+ \"{0}\" \"{1}\"", inputfilepath, s + "\\@Dystopia");
                            process.Start();*/
                  }
                }
            }
        }


        private void downloadFile(string url, string pathFolder)
        {
            string filename = getFilename(url);

            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileCompleted += wc_DownloadFileCompleted;
                wc.DownloadFileAsync(new Uri(url), pathFolder + "\\" + filename);
            }
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)        {
            progressBar1.Value = e.ProgressPercentage;
            Console.WriteLine(e.ProgressPercentage + "% | " + e.BytesReceived + " byte out of " + e.TotalBytesToReceive + " bytes retriven");
        }

        private void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            progressBar1.Value = 0;

            if(e.Cancelled)
            {
                System.Windows.MessageBox.Show("The Download has been canceled");
                return;
            }

            if(e.Error != null)
            {
                Console.WriteLine(e.Error);
                System.Windows.MessageBox.Show("An error occurred while trying to download file");
                return;
            }

            System.Windows.MessageBox.Show("File succefully downloaded");

        }
        private string getFilename(string hreflink)
        {
            Uri uri = new Uri(hreflink);

            string filename = System.IO.Path.GetFileName(uri.LocalPath);

            return filename;
        }
    }
}
