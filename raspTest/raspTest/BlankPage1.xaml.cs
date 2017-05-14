using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace raspTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        MediaElement demoMedia = new MediaElement();
        public BlankPage1()
        {
            this.InitializeComponent();
            
            Grid itm1Grid = new Grid {
                Name = "itm1Grd",
                Margin = new Thickness(10,5,10,5)
                            
            };

            PlayFile();
            itm1Grid.Children.Add(demoMedia);

            Pivot myPvt = new Pivot {
                Name = "myPivotContent",
                Title = "Dynamic Pivot",
                Margin = new Thickness(15,5,15,50),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            pg1RootGrid.Children.Add(myPvt);

            PivotItem pvItm1 = new PivotItem {
                Name = "itm1",
                Header = "Dynam 1",
                Content = itm1Grid

            };
            myPvt.Items.Add(pvItm1);

            PivotItem pvItm2 = new PivotItem
            {
                Name = "itm2",
                Header = "Dynam 2",
                Content = new CalendarView()
            };
            
            myPvt.Items.Add(pvItm2);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        public async void PlayFile()
        {

            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile myFile = await storageFolder.GetFileAsync("sample.avi");
            demoMedia.AutoPlay = true;
            demoMedia.SetPlaybackSource(MediaSource.CreateFromStorageFile(myFile));
            demoMedia.Play();
        }
    }
}
