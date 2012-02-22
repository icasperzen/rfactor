using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleApplication
{
    class Program
    {
        private string message;

        public string Message
        {
            get
            {
                return message.ToLower();
            }
            set
            {
                message = value;
            }
        }

        public Program(string message)
        {
            this.message = message;
        }

        public string Rename(string message)
        {
            string Message = message.ToUpper();
            return Message;
        }

        static void Main(string[] args)
        {
            string greeting = "Hello, World!";
            Program prog = new Program(greeting);
            prog.Rename(greeting);
        }
    }
}