//#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;
using System.Text.Json;
using System.Xml.Serialization;


namespace FuncEvent
{
    public enum TriggerMode { None, SoftTrigger, HardTrigger }
    public enum eSerialList { No1, No2, No3, No4 }

    [Serializable]
    public class ClassJson
    {

        [Browsable(false)]
        public string ConfigDir { get; set; }
        static readonly string setupJson = "SetupJ.json";

        //[Category("Date"), Description("날짜 정하기")]
        //public DateTimeOffset saveDate { get; set; }


        // [JsonProperty("@url", Order = 0)]
        public int TemperatureCelsius { get; set; }
        public string Summary { get; set; }

        //public string SummaryField { get; set; }
        public IList<DateTimeOffset> DatesAvailable { get; set; }
        public Dictionary<string, HighLowTemps> TemperatureRanges { get; set; } // = new Dictionary<string, HighLowTemps>();
                                                                                //public string[] SummaryWords { get; set; }

//        [Newtonsoft.Json.JsonIgnore]
        public List<alarmList> aList { get; set; } = new List<alarmList>();
        public TriggerMode TriggerMode { get; set; }
        [Category("Date"), Description("날짜 정하기")]
        public List<HighLowTemps> TestList { get; set; } = new List<HighLowTemps>()
        {

        };
        public HighLowTemps[] TestArr { get; set; } = new HighLowTemps[3]
        {
            new HighLowTemps(){Low=1,High=1}
            ,new HighLowTemps(){ Low=2,High=2}
            ,new HighLowTemps(){ Low=3,High=3}
        };

        [Newtonsoft.Json.JsonIgnore]
        public HighLowTemps Test1 { get; set; } = new HighLowTemps() { Low = 1, High = 2 };
        [Newtonsoft.Json.JsonIgnore]
        public HighLowTemps Test2 { get; set; } = new HighLowTemps() { Low = 6, High = 7 };


        public HighLowTemps GetAxis(eSerialList axisname) => TestArr[(int)axisname];

        [Newtonsoft.Json.JsonIgnore, Browsable(false)]
        public HighLowTemps air => GetAxis(eSerialList.No3);
        [Newtonsoft.Json.JsonIgnore, Browsable(false)]
        public HighLowTemps Vac => GetAxis(eSerialList.No2);


        public ClassJson()
        {
            if (aList.Count == 0)
            {
                //for (int i = 0; i < 5; i++)
                //    aList.Add(new alarmList() { ID = i, Text = i.ToString() });
            }
            TemperatureRanges = new Dictionary<string, HighLowTemps>();
            //Test2.Add(new HighLowTemps() { High = 1, Low = 3 });
            //Test2.Add(new HighLowTemps() { High = 11, Low = 33 });
        }

        public ClassJson LoadJson()
        {
            JObject jo = null;
            ClassJson setup = null;
            try
            {
                FileInfo fi = new FileInfo(Path.Combine(ConfigDir, setupJson));
                //FileInfo.Exists로 파일 존재유무 확인 "
                if (fi.Exists) { Console.WriteLine("파일 존재" + fi.FullName); }
                else { Console.WriteLine("파일 부재"); }

                jo = (JObject)JsonConvert.DeserializeObject(File.ReadAllText(Path.Combine(ConfigDir, setupJson)));
                if (jo != null) setup = jo.ToObject<ClassJson>();
                if (setup == null) throw new Exception(setupJson + " 파일을 찾을 수 없거나 읽을 수 없습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("File Load Err" + ex.Message);
                if (setup == null)
                {

                    setup = Default;
                    Default.ConfigDir = ConfigDir;
                    setup.SaveJson();
                }
            }
            //////setup.ConfigDir = configDir;
            //////값이 설정되지 않은 경우 Default값에서 채움
            //StringBuilder sb = new StringBuilder(1 << 10);
            //if (jo != null)
            //{
            //    SortedSet<string> S = new SortedSet<string>(jo.Properties().Select(p => p.Name));
            //    foreach (var p in pros)
            //    {
            //        object value = p.GetValue(setup);
            //        if (!S.Contains(p.Name) || value == null)
            //        {
            //            value = p.GetValue(Default);
            //            p.SetValue(setup, value);
            //            sb.AppendFormat("{0}의 값을 Default 값인 \"{1}\"로 설정합니다.\r\n", p.Name, value);
            //        }
            //    }
            //}
            //if (sb.Length > 0)
            //    MessageBox.Show(setupJson + sb.ToString());
            return setup;
        }

        public void SaveJson()
        {
            // this.saveDate = DateTimeOffset.Now;
            string dir = Path.GetDirectoryName(Path.Combine(ConfigDir, setupJson));

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                IgnoreNullValues = true
            };
            System.Text.Json.JsonSerializer.Serialize(this, options);

            //File.WriteAllText(Path.Combine(ConfigDir, setupJson), JsonConvert.SerializeObject(this, Formatting.Indented), Encoding.UTF8);
            File.WriteAllText(Path.Combine(ConfigDir, setupJson), JsonConvert.SerializeObject(this), Encoding.UTF8);

            KUtil.SaveJson(Path.Combine(ConfigDir, setupJson), this);

        }
        public void SaveXml()
        {
            string filename = Path.Combine(ConfigDir, setupJson);

            //Directory.CreateDirectory(Path.GetDirectoryName(filename));
            using (StreamWriter sw = new StreamWriter(filename, false, Encoding.Default))
            {
                XmlSerializer xs = new XmlSerializer(typeof(ClassJson));
                xs.Serialize(sw, this);
            }
        }

        public ClassJson LoadXml()
        {
            ClassJson systemSetup = new ClassJson();

            FileInfo fi = new FileInfo(Path.Combine(ConfigDir, setupJson));
            if (fi.Exists == false)
            {

            }
            else
            {
                using (StreamReader sr = new StreamReader(Path.Combine(ConfigDir, setupJson), Encoding.Default))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(ClassJson));
                    systemSetup = (ClassJson)xs.Deserialize(sr);
                }
            }

            return systemSetup;
        }
        static readonly ClassJson Default = new ClassJson()
        {

            //  saveDate = DateTimeOffset.Now,
            TemperatureCelsius = 10,
            //DatesAvailable = new List<DateTimeOffset>(),
            //SummaryWords = new string[2] { "", "" }
        };

        //[TypeConverter(typeof(ExpandableObjectConverter))]// 선언하면 저장값이 남지 않음
        //[Serializable]
        public class HighLowTemps
        {
            public string name { get; set; }
            public int High { get; set; }

            public int Low { get; set; }
        }
        static readonly PropertyInfo[] pros;

        //static ClassJson()
        //{
        //    pros = typeof(ClassJson).GetProperties(BindingFlags.Instance | BindingFlags.Public)
        //        .Where(p => p.GetCustomAttribute<JsonIgnoreAttribute>() == null).ToArray();
        //}
        public class alarmList
        {
            public int ID { get; set; } = 0;
            public string Text { get; set; } = "melong";

        }
    }
    // https://docs.microsoft.com/ko-kr/dotnet/standard/serialization/system-text-json-polymorphism


    public class ClassJsonVer2
    {
        [Browsable(false)]
        public string ConfigDir { get; set; }
        static readonly string setupJson = "SetupJ.json";

        [Category("Date"), Description("날짜 정하기")]
        public DateTimeOffset saveDate { get; set; }


        [JsonProperty("@url", Order = 0)]
        public int TemperatureCelsius { get; set; }
        public string Summary { get; set; }

        public string SummaryField { get; set; }
        public IList<DateTimeOffset> DatesAvailable { get; set; }
        public Dictionary<string, HighLowTemps> TemperatureRanges { get; set; } = new Dictionary<string, HighLowTemps>();
        public string[] SummaryWords { get; set; }
        public TriggerMode TriggerMode { get; set; }

        public ClassJsonVer2()
        {
        }

        public void Load()
        {
            JObject jo = null;
            ClassJson setup = null;
            object rtnObj = null;
            try
            {
                FileInfo fi = new FileInfo(Path.Combine(ConfigDir, setupJson));
                //FileInfo.Exists로 파일 존재유무 확인 "
                if (fi.Exists) { Console.WriteLine("파일 존재" + fi.FullName); }
                else { Console.WriteLine("파일 부재"); }

                jo = (JObject)JsonConvert.DeserializeObject(File.ReadAllText(Path.Combine(ConfigDir, setupJson)));

                if (jo != null)
                    setup = jo.ToObject<ClassJson>();
                //if (setup == null) throw new Exception(setupJson + " 파일을 찾을 수 없거나 읽을 수 없습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("File Load Err" + ex.Message);
                //if (setup == null)
                //{

                //    setup = Default;
                //    Default.ConfigDir = ConfigDir;
                //    setup.Save();
                //}
            }
            //setup.ConfigDir = configDir;
            //값이 설정되지 않은 경우 Default값에서 채움
            //StringBuilder sb = new StringBuilder(1 << 10);
            //if (jo != null)
            //{
            //    SortedSet<string> S = new SortedSet<string>(jo.Properties().Select(p => p.Name));
            //    foreach (var p in pros)
            //    {
            //        object value = p.GetValue(setup);
            //        if (!S.Contains(p.Name) || value == null)
            //        {
            //            value = p.GetValue(Default);
            //            p.SetValue(setup, value);
            //            sb.AppendFormat("{0}의 값을 Default 값인 \"{1}\"로 설정합니다.\r\n", p.Name, value);
            //        }
            //    }
            //}
            //if (sb.Length > 0)
            //    MessageBox.Show(setupJson + sb.ToString());
            //   return jo;
        }

        public void Save(object obj)
        {

            this.saveDate = DateTimeOffset.Now;
            KUtil.SaveJson(Path.Combine(ConfigDir, setupJson), this);
            string PathFile = Path.Combine(ConfigDir, setupJson);
            using (StreamWriter file = File.CreateText(PathFile))
            {
                var serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.Serialize(file, obj);
            }

        }
        static readonly ClassJson Default = new ClassJson()
        {

            //   saveDate = DateTimeOffset.Now,
            TemperatureCelsius = 10,
            //DatesAvailable = new List<DateTimeOffset>(),
            //SummaryWords = new string[2] { "", "" }
        };

        public class HighLowTemps
        {
            public int High { get; set; }
            public int Low { get; set; }
        }
        static readonly PropertyInfo[] pros;

        //static ClassJsonVer2()
        //{
        //    pros = typeof(ClassJson).GetProperties(BindingFlags.Instance | BindingFlags.Public)
        //        .Where(p => p.GetCustomAttribute<JsonIgnoreAttribute>() == null).ToArray();
        //}
    }

}
