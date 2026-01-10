//standard system refernces
using System;

//own refernces

//project refernces
using Discord;

//Class namespase (refernce)
namespace GeneralFunctions
{
    //Class
    public class Configreader
    {
        //Class Variables
        //Field
        static private string token = Environment.GetEnvironmentVariable("DISCORD_TOKEN") ?? "TOKEN";
        private string ClassName = "Configreader";
        private Func<LogMessage, Task> _logger;
        //Message
        private const string ErrorLogMessage = "No TOKEN was handover";
        private const string ReadConfigMessage = "Reading config..";
        private const string ExceptionMessage = "Exception - No TOKEN";

        //Properties
        public  string __GetString
        {
            get { return token; }
        }

        //Internal
        //-------------------------------------------------------------------
        //Methodes
        //-------------------------------------------------------------------
        private bool Checktoken(
            //Inputs
            string token,
            ref Func<LogMessage, Task> logger)
        {
            if( token == "" || token == null)
            {
                var ErrorLog = new LogMessage(LogSeverity.Error, $"{ClassName}", ErrorLogMessage);
                logger(ErrorLog);
                return false;
                throw new Exception(ExceptionMessage);
            }
            else
            {
                return true;
            }
        }
        //-------------------------------------------------------------------

        private void ReadConfig(
            //Inputs
            ref Func<LogMessage, Task> logger
          )
        {
            if(Checktoken(token, ref logger))
            {
                var ReadConfigLog = new LogMessage(LogSeverity.Info, $"{ClassName}", ReadConfigMessage);
                logger(ReadConfigLog);
            }
        }

        //-------------------------------------------------------------------
        //Constructor 
        //-------------------------------------------------------------------
        public Configreader(
        //Input
          Func<LogMessage, Task> logger
        )
        {
            _logger = logger;
            ReadConfig(ref _logger);
        }
    }
}
