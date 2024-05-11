using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManangementSystemLibrary

{
    public class ServiceLog
    {
        public string Activity { get; private set; }
        public DateTime Timestamp { get; private set; }
        public ServiceLog(string activity,DateTime timestamp)
        {
            this.Activity = activity;
            this.Timestamp = timestamp;
        }
    }//namespace
}//class
