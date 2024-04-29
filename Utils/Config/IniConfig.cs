using IniFileParser;
using IniFileParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Config
{
    public class IniConfig
    {
        public static IniData ReadInit(string fileName)
        {
            var parser = new IniFileParser.IniFileParser();
            IniData data=parser.ReadFile(fileName);
            return data;
        }
    }
}
