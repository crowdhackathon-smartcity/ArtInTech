using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace raspTest
{

    public class Rootobject
    {
        public Rootobject(Deviceio[] devIO, Devlayout devLayout, object[] stuff, int devAutoId, string devCode, string devType, string devApi, string devApiKey, string devLocation, string devIP, string devAdmin, string devPass, int devLayoutId, int devLaytIOId)
        {
            DeviceIO = devIO;
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
            this.devLayoutId = devLayoutId;
            this.devLayoutIOId = devLaytIOId;
        }// Rootobject Constructor Valued

        public Rootobject()
        {
            DeviceIO = new Deviceio[0];
            DevLayout = new Devlayout();
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
            this.devLayoutId = 0;
            this.devLayoutIOId = 0;
        }//Rootobject Constructor

        public Deviceio[] DeviceIO { get; set; }
        public Devlayout DevLayout { get; set; }
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
        public int devLayoutId { get; set; }
        public int? devLayoutIOId { get; set; }

        public void GetMyRtData(MemoryStream ms)
        {
            var serializer = new DataContractJsonSerializer(typeof(Rootobject));
            //var ms = new MemoryStream(Encoding.UTF8.GetBytes(rs));
            var data = (Rootobject)serializer.ReadObject(ms);

            Type _type = this.GetType();
            PropertyInfo[] properties = _type.GetProperties();

            Type d_type = data.GetType();
            PropertyInfo[] d_properties = d_type.GetProperties();
                        
            foreach (PropertyInfo thisPr in properties)
            {
             foreach (PropertyInfo dPr in d_properties)
             {
              if (thisPr.Name == dPr.Name)
              {
               thisPr.SetValue(this, dPr.GetValue(data, null));
              }//if
             }// foreach
            }//foreach
        }//GetMyRtData

        public void sendProperties()
        {
         Type _type = this.GetType();
         PropertyInfo[] properties = _type.GetProperties();

         myModule.MakeTextBoxes(properties, this);
        }//sendProperties

    }//Rootobject Class

    public class Devlayout
    {
        public Devlayout(object[] devices, Layoutsetting[] layoutSettings, int dvLtAutoId, string dvLtName, string dvLtVersion, string dvLtDescription, string dvLtLastUpdate, int dvLtTypes)
        {
            Devices = devices;
            LayoutSettings = layoutSettings;
            this.dvLtAutoId = dvLtAutoId;
            this.dvLtName = dvLtName;
            this.dvLtVersion = dvLtVersion;
            this.dvLtDescription = dvLtDescription;
            this.dvLtLastUpdate = dvLtLastUpdate;
            this.dvLtType = dvLtType; 
        }//Devlayout Constructor Valued

        public Devlayout()
        {
            Devices = new string[0]; 
            LayoutSettings = new Layoutsetting[0];
            this.dvLtAutoId = 0;
            this.dvLtName = "";
            this.dvLtVersion = "";
            this.dvLtDescription = "";
            this.dvLtLastUpdate = "";
            this.dvLtType = 0;
        }// Devlayout Constructor

        public object[] Devices { get; set; }
        public Layoutsetting[] LayoutSettings { get; set; }
        public int dvLtAutoId { get; set; }
        public string dvLtName { get; set; }
        public string dvLtVersion { get; set; }
        public string dvLtDescription { get; set; }
        public string dvLtLastUpdate { get; set; }
        public int dvLtType { get; set; }

        public void GetMyDvLtData(MemoryStream ms)
        {
            var serializer = new DataContractJsonSerializer(typeof(Devlayout));
            var data = (Devlayout)serializer.ReadObject(ms);

            Type _type = this.GetType();
            PropertyInfo[] properties = _type.GetProperties();

            Type d_type = data.GetType();
            PropertyInfo[] d_properties = d_type.GetProperties();

            foreach (PropertyInfo thisPr in properties)
            {
                foreach (PropertyInfo dPr in d_properties)
                {
                    if (thisPr.Name == dPr.Name)
                    {
                        thisPr.SetValue(this, dPr.GetValue(data, null));
                    }//if
                }// foreach
            }//foreach
        }//GetMyDvLtData


        public void sendProperties()
        {
         Type _type = this.GetType();
         PropertyInfo[] properties = _type.GetProperties();

         myModule.MakeTextBoxes(properties, this);
        }//sendProperties

    }// Devlayout Class

    public class Layoutsetting
    {
        private string pos = "";
        public Layoutsetting(int ltSId, int ltSDevLayoutId, string ltSType, string ltSContent, string ltSValue, string itSPosition)
        {
            this.ltSId = ltSId;
            this.ltSDevLayoutId = ltSDevLayoutId;
            this.ltSType = ltSType;
            this.ltSContent = ltSContent;
            this.ltSValue = ltSValue;
            this.pos = itSPosition;
        }//Layoutsetting Constructor Valued

        public Layoutsetting()
        {
            this.ltSId = 0;
            this.ltSDevLayoutId = 0;
            this.ltSType = "";
            this.ltSContent = "";
            this.ltSValue = "";
            this.pos = "";
        }//Layoutsetting Constructor

        public int ltSId { get; set; }
        public int ltSDevLayoutId { get; set; }
        public string ltSType { get; set; }
        public string ltSContent { get; set; }
        public string ltSValue { get; set; }
        public string ItSPosition {
            get
            {
                string[] delimiter = new string[] { ";" };
                string[] tmp;                
                tmp = this.pos.Split(delimiter,StringSplitOptions.None);
                string[] resulttmp = new string[tmp.Length - 1];
                for (int i=0; i < tmp.Length-1; i++)
                {
                    string temp = tmp[i];
                    int start = (temp.IndexOf(':') + 1);
                    int stop = (temp.Length - start);
                    resulttmp[i] = temp.Substring(start,stop);

                    if (resulttmp[i].IndexOf('.') > -1)
                    {
                        string[] mm = resulttmp[i].Split('.');
                        resulttmp[i] = mm[0];
                    }//if
                }//foreach

                this.pos = string.Join(":", resulttmp);
             return this.pos;
            }//get

            set { this.pos = value; }
        }// public string ItSPosition

        public void GetMyLtStData(MemoryStream ms)
        {
            var serializer = new DataContractJsonSerializer(typeof(Layoutsetting));
            var data = (Layoutsetting)serializer.ReadObject(ms);

            Type _type = this.GetType();
            PropertyInfo[] properties = _type.GetProperties();

            Type d_type = data.GetType();
            PropertyInfo[] d_properties = d_type.GetProperties();

            foreach (PropertyInfo thisPr in properties)
            {
                foreach (PropertyInfo dPr in d_properties)
                {
                    if (thisPr.Name == dPr.Name)
                    {
                        thisPr.SetValue(this, dPr.GetValue(data, null));
                    }//if
                }// foreach
            }//foreach
        }//GetMyLtStData

        public void sendProperties()
        {
         Type _type = this.GetType();
         PropertyInfo[] properties = _type.GetProperties();

         myModule.MakeTextBoxes(properties, this);
        }//sendProperties

    }//Layoutsetting Class


    public class Deviceio
    {
        public Deviceio(int ioId, string ioName, string ioPortName, string ioType, string ioValType, string ioValue, int ioDeviceId)
        {
            this.ioId = ioId;
            this.ioName = ioName;
            this.ioPortName = ioPortName;
            this.ioType = ioType;
            this.ioValType = ioValType;
            this.ioValue = ioValue;
            this.ioDeviceId = ioDeviceId;
        }//Deviceio Constructor Valued

        public Deviceio()
        {
            this.ioId = 0;
            this.ioName = "";
            this.ioPortName = "";
            this.ioType = "";
            this.ioValType = "";
            this.ioValue = "";
            this.ioDeviceId = 0;
        }// Deviceio Constructor

        public int ioId { get; set; }
        public string ioName { get; set; }
        public string ioPortName { get; set; }
        public string ioType { get; set; }
        public string ioValType { get; set; }
        public string ioValue { get; set; }
        public int ioDeviceId { get; set; }


    }//Deviceio
}
