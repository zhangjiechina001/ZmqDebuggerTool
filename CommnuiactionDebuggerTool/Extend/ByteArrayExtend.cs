using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommnuiactionDebuggerTool.Extend
{
    public static class ByteArrayExtend
    {
        public static string GetFormatString(this byte[] data,bool isHex)
        {
            var arr = data.Select(t => isHex ? t.ToString("x2") : t.ToString());
            return string.Join(" ", arr);
        }
    }
}
