using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Utils.Config
{
    public class JsonConfig
    {
        private readonly JsonObject _obj;
        public JsonConfig() 
        {
            string jsonContent = File.ReadAllText("./appsettings.json");
            _obj = JsonObject.Parse(jsonContent).AsObject() ;
        }

        public JsonObject GetSectionObj(string key)
        {
            var obj=_obj[key];
            return obj.AsObject() ;
        }

        public JsonNode GetSectionToken(string key)
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
