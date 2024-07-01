using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmqDebuggerTool.Config
{

    
    public class GlobalVar
    {
        // 私有构造函数，防止外部实例化
        private readonly Ini _ini;
        private GlobalVar()
        {
            _ini = new Ini("./ConfigFile/Config.ini");
        }

        public string GetValue(string section,string key)
        {
            return _ini.GetValue(key, section);
        }

        public void SetValue(string section, string key,string val)
        {
            _ini.WriteValue( key,section,val);
            _ini.Save();
        }


        private static readonly GlobalVar instance = new GlobalVar();
        // 公共静态属性，提供全局访问点
        public static GlobalVar GetInstance()
        {
            return instance;
        }
    }
}
