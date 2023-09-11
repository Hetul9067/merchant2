using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_5
{
    [Serializable]
    public class CustomException : Exception
    {
        public static string msg = "";
        public CustomException() : base() { }
        public CustomException(string message) : base(message)
        {
            msg = message;
            //processError(message, value);
        }

        public CustomException(string message, Exception inner) : base(message, inner)
        {
            //processError(message, inner);
        }


        public static void processError(string msg, int value)
        {
            switch (msg)
            {

                case "error1":
                    Console.WriteLine(HardCodedValue.ERROR1+ value);
                    break;
                case "error2":
                    Console.WriteLine(HardCodedValue.ERROR4+value);
                    break;
                default:
                    //Console.WriteLine(e.ToString());
                    break;

            }
        }
    }

}
