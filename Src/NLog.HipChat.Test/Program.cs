using NLog;
using System;

namespace NLg.HipChat.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            
            logger.Debug("This is Debug");
            logger.Info("This is Info");
            logger.Warn("This is Warn");
            logger.Error("This is Error");
            logger.Fatal("This is Fatal");

            Console.WriteLine("Finish!");
            Console.Read();
        }
    }
}
