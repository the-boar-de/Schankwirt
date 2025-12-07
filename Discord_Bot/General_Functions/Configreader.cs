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
        static private string token = Environment.GetEnvironmentVariable("DISCORD_TOKEN") ?? "MTM5MjkxODg3MTQ3NjAxNTMyNw.GbFEA3.4GTqX5D2QqV8yc9h-DW3LjplY6GFrUV71zIKC8";
        private string ClassName = "Configreader";
        private List<string> config = new();
        private Func<LogMessage, Task> _logger;
        //Message
        private string ErrorLogMessage = "No TOKEN was handover";
        private string ReadConfigMessage = "Reading config..";
        private string ExceptionMessage = "Exception - No TOKEN";

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
