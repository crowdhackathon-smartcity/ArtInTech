using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Net.Http;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Networking.BackgroundTransfer;
using System.Threading;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace raspTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DownloadOperation downloadOperation;
        CancellationTokenSource cancellationToken;
        BackgroundDownloader backgroundDownloader = new Windows.Networking.BackgroundTransfer.BackgroundDownloader();
        private string myRpId = DeviceInfo.Instance.Id;
        private static string layoutVersion = "";

        public MainPage()
        {
            this.InitializeComponent();
            first_Json_Call();

        }
        private DispatcherTimer _timer = new DispatcherTimer();

        private void Init()
        {
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += timer_Tick;
            _timer.Start();
        }// init timer

        private async void timer_Tick(object sender, object e)
        {
            Rootobject rtNewObj = await  check_for_changes();
        }// timer tick
        
        private async void first_Json_Call()
        {
            MemoryStream ms = new MemoryStream();
            ms = await myModule.GetMyStream("http://192.168.0.97/WebApp/api/devices?devApi=d855afb&devApiKey=073178a");

            if (ms != null)
            {
                Rootobject rootObj = new Rootobject();
                rootObj.GetMyRtData(ms);
                rootObj.sendProperties();
                string dim = rootObj.DevLayout.LayoutSettings[0].ItSPosition;

                Layoutsetting[] lts = rootObj.DevLayout.LayoutSettings;

                for (int i = 0; i < lts.Length; i++)
                {
                    switch (lts[i].ltSType)
                    {
                        case "image":
                            download_media(lts[i].ltSContent);
                            Image myImage = new Image();
                            BitmapImage bitmp = new BitmapImage();
                            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                            
                            string[] delimiter = new string[] { "." };
                            string[] tmp = lts[i].ltSContent.Split(delimiter, StringSplitOptions.None);

                            string fExt = tmp[tmp.Length - 1];
                            Windows.Storage.StorageFile myFile = await storageFolder.GetFileAsync("sample." + fExt);
                            await Task.Delay(3000);
                            FileRandomAccessStream stream = (FileRandomAccessStream)await myFile.OpenAsync(FileAccessMode.Read);


                           await bitmp.SetSourceAsync(stream);
                            myImage.Source = bitmp;

                            string[] loc = lts[i].ItSPosition.Split(':');
                            elmntPos myPos = new elmntPos(loc[0], loc[1], loc[2], loc[3]);
                            myImage.Margin = new Windows.UI.Xaml.Thickness(myPos.left,myPos.top,0,0);
                            myImage.Width = myPos.widgth;
                            myImage.Height = myPos.height;
                            myImage.HorizontalAlignment = HorizontalAlignment.Left;// myPos.widgth;
                            myImage.VerticalAlignment = VerticalAlignment.Top;// myPos.height;
                            rootGrid.Children.Add(myImage);
                            break;
                        case "text":


                            break;
                        case "browser":
                            break;
                    }//switch
                }//for

                layoutVersion = rootObj.DevLayout.dvLtVersion;
                Init();
            }//if
            

            /*Devlayout devLtObj = new Devlayout();
            devLtObj.GetMyDvLtData(ms);

            Layoutsetting ltStObj = new Layoutsetting();
            ltStObj.GetMyLtStData(ms);*/



            /*List<TextBox> myTxb = new List<TextBox>();

            myTxb = myModule.myTextBoxes;
            double topVal = 38;

            foreach (TextBox txbx in myTxb)
            {
                txbx.Margin = new Thickness(15, topVal, 0, 0);
                rootGrid.Children.Add(txbx);
                topVal += 65;
                
            }*/

        }//btnConnect_Click


        private async Task<Rootobject> check_for_changes()
        {
            Rootobject rootObjnow = new Rootobject();
            MemoryStream ms = new MemoryStream();
            ms = await myModule.GetMyStream("http://192.168.0.97/WebApp/api/devices?devApi=d855afb&devApiKey=073178a");
            Debug.WriteLine("zw");
            if (ms == null)
            {
                rootObjnow = null;
            }//if
            else
            {
                rootObjnow.GetMyRtData(ms);
                rootObjnow.sendProperties();
                string dim = rootObjnow.DevLayout.LayoutSettings[0].ItSPosition;
                layoutVersion = rootObjnow.DevLayout.dvLtVersion;
            }//else            

            return rootObjnow;
            
        }//check_for_changes


        private async void save_layout_to_file()
        {        
         Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
         Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync("checkvr.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
         Windows.Storage.StorageFile myFile = await storageFolder.GetFileAsync("checkvr.txt");
         await Windows.Storage.FileIO.WriteTextAsync(myFile, layoutVersion);
        }// layout version file

      

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async Task<string> read_version()
        {
         Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
         Windows.Storage.StorageFile versionFile = await storageFolder.GetFileAsync("checkvr.txt");
         string text = await Windows.Storage.FileIO.ReadTextAsync(versionFile);
         return text;
        }// read layout version from file

        public async void Download(string myUrl)
        {
            StorageFolder strFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            if (strFolder != null)
            {
                string[] delimiter = new string[] { "." };
                string[] tmp = myUrl.Split(delimiter, StringSplitOptions.None);
               
                string fExt = tmp[tmp.Length-1];
                
                StorageFile file = await strFolder.CreateFileAsync("sample."+fExt, CreationCollisionOption.ReplaceExisting);
                Uri durl = new Uri(myUrl);
                downloadOperation =backgroundDownloader.CreateDownload(durl, file);

                Progress<DownloadOperation> progress = new Progress<DownloadOperation>(progressChanged);
                cancellationToken = new CancellationTokenSource();

                try
                {
                    status.Text = "Initializing...";
                    await downloadOperation.StartAsync().AsTask(cancellationToken.Token, progress);
                }//try
                catch (TaskCanceledException)
                {

                    downloadOperation.ResultFile.DeleteAsync();
                    downloadOperation = null;
                }//catch
            }//if
        }//Download media

        private void progressChanged(DownloadOperation downloadOperation)
        {
            int progress = (int)(100 * ((double)downloadOperation.Progress.BytesReceived / (double)downloadOperation.Progress.TotalBytesToReceive));
            status.Text = String.Format("{0} of {1} kb. downloaded - %{2} complete.", downloadOperation.Progress.BytesReceived / 1024, downloadOperation.Progress.TotalBytesToReceive / 1024, progress);

            switch (downloadOperation.Progress.Status)
            {
                case BackgroundTransferStatus.Running:
                    {
                        break;
                    }
                case BackgroundTransferStatus.PausedByApplication:
                    {

                        break;
                    }
                case BackgroundTransferStatus.PausedCostedNetwork:
                    {

                        break;
                    }
                case BackgroundTransferStatus.PausedNoNetwork:
                    {

                        break;
                    }
                case BackgroundTransferStatus.Error:
                    {
                        status.Text = "An error occured while downloading.";
                        break;
                    }
            }
            if (progress >= 100)
            {
                downloadOperation = null;
            }
        }

        private void download_media(string myUrl)
        {
            Download(myUrl);
        }//download_media

    }//mainPgae
}// namespace
