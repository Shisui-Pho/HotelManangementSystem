using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManangementSystemLibrary.Logging
{
    //https://docs.oracle.com/cd/E19225-01/820-5823/ahyip/index.html
    public enum ErrorServerity
    {
        /// <summary>
        /// An informative message, usually describing server activity. No action is necessary.
        /// </summary>
        Info = 0,
        /// <summary>
        ///  A severe error that might cause the loss or corruption of unsaved data. Immediate action must be taken to prevent losing data.
        /// </summary>
        Error = 1,
        /// <summary>
        /// Action must be taken at some stage to prevent a severe error from occurring in the future.
        /// </summary>
        Warning = 2,
        /// <summary>
        /// A severe error that causes your system to crash, resulting in the loss or corruption of unsaved data.
        /// </summary>
        Fetal = 4
    }//ErrorServerity
    public enum TypeOfError
    {
        UserError,
        SystemError,
        DatabaseError,
        BusinessRuleBridge
    }
}//namespace