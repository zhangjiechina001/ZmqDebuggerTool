﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmqDebuggerTool.Config
{
    public class JsonConfig
    {
        private readonly JObject _obj;
        public JsonConfig() 
        {
            string jsonContent = File.ReadAllText("./appsettings.json");
            _obj=JObject.Parse(jsonContent);
        }

        public JObject GetSectionObj(string key)
        {
            var obj=_obj[key];
            return obj.ToObject<JObject>();
        }

        public JArray GetSectionArray(string key)
        {
            var obj = _obj[key];
            return obj.ToObject<JArray>();
        }

        public JToken GetSectionToken(string key)
        {
            var obj = _obj[key];
            return obj;
        }

        public string GetSectionString(string key)
        {
            var obj = _obj[key];
            return (string)obj;
        }
    }
}
