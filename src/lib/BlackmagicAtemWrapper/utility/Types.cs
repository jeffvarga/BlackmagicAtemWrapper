using System;
using System.Runtime.InteropServices;

namespace BlackmagicAtemWrapper.utility
{
    public class FailedException : Exception
    {
        public FailedException(Exception e) : base("E_FAIL", e) { }

        public static bool IsFailedException(int hr)
        {
            return (uint)hr == 0x80004005;
        }
    }

}