using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace raspTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage2 : Page
    {
        public BlankPage2()
        {
            this.InitializeComponent();
        }

        private async void button_ClickAsync(object sender, RoutedEventArgs e)
        {

         
         Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
         Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync(nameTxbx.Text+".txt",Windows.Storage.CreationCollisionOption.ReplaceExisting);
         Windows.Storage.StorageFile myFile = await storageFolder.GetFileAsync(nameTxbx.Text + ".txt");
         await Windows.Storage.FileIO.WriteTextAsync(myFile, cntTxbx.Text);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            button_ClickAsync(sender, e);
        }
    }
}
