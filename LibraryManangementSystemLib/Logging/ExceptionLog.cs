using System;
using System.IO;
namespace HotelManangementSystemLibrary.Logging
{
    public class ExceptionLog
    {
        private readonly string file = Path.Combine(Directory.GetCurrentDirectory(), "Log", "Error.log");
        public event delUserExceptionEvent UserExceptionEvent;
        public readonly static ExceptionLog _logger = new ExceptionLog();
        private ExceptionLog() { }//ctor default
        public static ExceptionLog GetLogger() => _logger;
        public bool LogActivity(Exception ex, ErrorServerity ser, TypeOfError type, string title = "System Error")
        {
            if (type == TypeOfError.UserError || type == TypeOfError.BusinessRuleBridge)
            {
                //Alert the user about what they are doing wrong
                AlertUserEvent args = new AlertUserEvent(ex.Message,title);
                UserExceptionEvent?.Invoke(args);
                return args.Handled;
            }//end if user error
            string error_lines_of_code = ExtactLinesCode(ex.StackTrace);

            string header = $"Date : {DateTime.Now}\t Type : {type.ToString()}\t Serverity : {ser.ToString()}";
            int len = header.Length;
            header += "\n".PadRight(16 + len,'=');
            using(StreamWriter wr = new StreamWriter(file,true))
            {
                wr.WriteLine("".PadRight(16 + len, '='));
                wr.WriteLine(header);
                wr.WriteLine("Error message     :\n{0}\n", ex.Message);
                wr.WriteLine("Importance lines  :\n{0}\n", error_lines_of_code);
                wr.WriteLine("Stacktrace        :\n{0}\n", ex.StackTrace);
                wr.WriteLine("----------------------------------End logg---------------------------------------\n\n");
            }//write to a log file
            return true;
        }//LogActivity
        private string ExtactLinesCode(string stacktrace)
        {
            string[] lines = stacktrace.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            int len = lines.Length;

            return lines[len - 2] + "\n" + lines[len - 1];
        }//GetLastwoMessages
        public static void Exception(string message, string title)
        {
            //It does not matter where this is throw since the user's exceptions won't be logged.
            var ex = new ArgumentException(message);
            bool handled = GetLogger().LogActivity(ex, ErrorServerity.Warning, TypeOfError.UserError, title);
            if (!handled)
                throw ex;
        }//Exception
    }//class
}//namespace
