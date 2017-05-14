using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Reflection;

namespace raspTest
{

    public class raspClass
    {
        public raspClass(object[] deviceIO, object[] devLayout, object[] stuff, int devAutoId, string devCode, string devType, string devApi, string devApiKey, string devLocation, string devIP, string devAdmin, string devPass)
        {
            DeviceIO = deviceIO;
            DevLayout = devLayout;
            Stuff = stuff;
            this.devAutoId = devAutoId;
            this.devCode = devCode;
            this.devType = devType;
            this.devApi = devApi;
            this.devApiKey = devApiKey;
            this.devLocation = devLocation;
            this.devIP = devIP;
            this.devAdmin = devAdmin;
            this.devPass = devPass;
        }


        public raspClass()
        {
            DeviceIO = new string[0];
            DevLayout = new string[0];
            Stuff = new string[0];
            this.devAutoId = 0;
            this.devCode = "";
            this.devType = "";
            this.devApi = "";
            this.devApiKey = "";
            this.devLocation = "";
            this.devIP = "";
            this.devAdmin = "";
            this.devPass = "";
        }
        public object[] DeviceIO { get; set; }
        public object[] DevLayout { get; set; }
        public object[] Stuff { get; set; }
        public int devAutoId { get; set; }
        public string devCode { get; set; }
        public string devType { get; set; }
        public string devApi { get; set; }
        public string devApiKey { get; set; }
        public string devLocation { get; set; }
        public string devIP { get; set; }
        public string devAdmin { get; set; }
        public string devPass { get; set; }


        public async static Task<raspClass> GetMyData(string uri)
        {
            var http = new HttpClient();
            var url = String.Format(uri);
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(raspClass));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (raspClass)serializer.ReadObject(ms);

            return data;

        }

        public void sendProperties()
        {
            Type _type = this.GetType();
            PropertyInfo[] properties = _type.GetProperties();

            myModule.MakeTextBoxes(properties, this);
        }
    }

}

