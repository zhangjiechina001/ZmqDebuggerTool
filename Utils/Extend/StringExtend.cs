using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Extend
{
    public static class StringExtend
    {
        public static byte[] GetBytesFromLine(this string data,bool isHex)
        {
            return data.Split(" ").Select(t => isHex?Convert.ToByte(t, 16): Convert.ToByte(t, 10)).ToArray();
        }
    }
}
