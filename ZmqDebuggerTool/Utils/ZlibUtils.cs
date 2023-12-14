using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ZmqDebuggerTool.Utils
{
    public class ZlibUtils
    {
        // 声明 zlib 的 uncompress 函数
        [DllImport("zlib1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int uncompress(byte[] dest, ref uint destLen, byte[] source, uint sourceLen);

        // 声明 zlib 的 compress 函数
        [DllImport("zlib1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int compress(byte[] dest, ref uint destLen, byte[] source, uint sourceLen);

        public static byte[] UnCompressWrap(byte[] source)
        {
            uint leng = 1024 * 1024;
            byte[] dest = new byte[leng];
            uncompress(dest, ref leng, source, (uint)source.Length);
            return dest.Take((int)leng).ToArray();
        }

        public static byte[] CompressWrap(byte[] source)
        {
            uint leng = 1024 * 1024;
            byte[] dest = new byte[leng];
            compress(dest, ref leng, source, (uint)source.Length);
            return dest;
        }
    }
}
