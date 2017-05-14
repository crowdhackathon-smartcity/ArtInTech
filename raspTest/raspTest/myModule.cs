using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Windows.UI.Xaml.Controls;
using System.Windows;
using Windows.UI.Popups;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.IO;

namespace raspTest
{
    public static class myModule
    {
        public static string json;
        public async static Task<MemoryStream> GetMyStream(string uri)
        {
            var http = new HttpClient();
            var url = String.Format(uri);
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            if (result != json)
            {
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                json = result;
                return ms;
            }//if
            else
            {
                return null;
            }//else           

        }//GetMyStream
        public static List<TextBox> myTextBoxes { get; set; }
        public static void MakeTextBoxes(PropertyInfo[] properties, object obj)
        {
            List<TextBox> myTxbxList = new List<TextBox>();
            foreach (PropertyInfo _property in properties)
            {
                Type p = _property.PropertyType;
                //Debug.WriteLine(p.ToString()+" "+ typeof(String).ToString()+" " + Convert.ToString(_property.GetValue(obj, null)));

                if (p == typeof(String) && Convert.ToString(_property.GetValue(obj, null)) != "")
                {
                    TextBox txbx = new TextBox {
                    Name = _property.Name,
                    Width = 220,
                    Height = 58,
                    Header = _property.Name,
                    VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top,
                    HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left,
                    Text = _property.GetValue(obj, null).ToString()
                   };

                 myTxbxList.Add(txbx);
                }//if


                myTextBoxes = myTxbxList;
                
            }//foreach

        }//makeTextBoxes


       /* public static bool objComp( cmp1, object cmp2)
        {
           
            Type _type = cmp1.GetType();
            PropertyInfo[] properties = _type.GetProperties();

            Type d_type = cmp2.GetType();
            PropertyInfo[] d_properties = d_type.GetProperties();

            foreach (PropertyInfo thisPr in properties)
            {
                foreach (PropertyInfo dPr in d_properties)
                {
                    if (thisPr.Name == dPr.Name)
                    {
                        
                    }//if
                }// foreach
            }//foreach
            return;
        }//GetMyLtStData*/

        private async static void MessageBox(string content)
        {
            var dialog = new MessageDialog(content);
            await dialog.ShowAsync();
        }
    }//myModule

}
