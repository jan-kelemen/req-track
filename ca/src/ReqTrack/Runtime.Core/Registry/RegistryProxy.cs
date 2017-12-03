using System;
using System.Collections.Generic;
using System.Text;

namespace ReqTrack.Runtime.Core.Registry
{
    public static class RegistryProxy
    {
        public static IRegistry Get { get; internal set; }
    }
}
