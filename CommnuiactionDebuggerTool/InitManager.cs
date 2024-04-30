using IniFileParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Config;

namespace CommnuiactionDebuggerTool
{
    public class InitManager
    {
        IniData _iniData;
        private InitManager()
        {
            _iniData = IniConfig.ReadInit("Configuration.ini");
        }

        public IniData IniData => _iniData;

        public string GetSection(string sectionName,string key)
        {
            return _iniData[sectionName][key];
        }

        public void SaveSection(string sectionName,string key,string value)
        {
            _iniData[sectionName][key] = value;
            IniConfig.SaveInit("Configuration.ini",_iniData);
        }

        private static InitManager _instance = new InitManager();
        public static InitManager GetInstance()
        {
            return _instance;
        }
    }
}
