using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.Extensions
{
    public static class ByteArrayExtension
    {
        public static string ToStr(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
