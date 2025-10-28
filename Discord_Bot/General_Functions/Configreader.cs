//standard system refernces
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        private string classname = "Configreader";
        private List<string> config = new();
        private Func<LogMessage, Task> _logger;

        //Message


        //Properties
        public IReadOnlyList<string> __GetConfigList
        {
            get { return config.AsReadOnly(); }
        }

        //Internal

        //-------------------------------------------------------------------
        //Methodes
        
       
        private bool Validate(
            //Input
             string oInObject1,
             string oInObject2,
            ref Func<LogMessage, Task> logger)
        {
            

            if (oInObject1 == null || oInObject2 == null)
            {
                var ErrorNotValid = new LogMessage(LogSeverity.Error,$"{classname}","This call is not valid!");
                _logger(ErrorNotValid);
                return false;
            }
            else
            {
                var InfoValid = new LogMessage(LogSeverity.Info, $"{classname}", "This call is valid!");
                _logger(InfoValid);
                return true;
            }
        }
        private bool ReadConfig(
            //Inputs
            string filepath,
            string filename,
            ref Func<LogMessage, Task> logger
          )
        {
            if (filepath == null || filename == null)
            {
                var ErrorFilePath = new LogMessage(LogSeverity.Error, $"{classname}", "No Path or Filename was handover");
                _logger(ErrorFilePath);

                //exception handler
                // ?????
                return false;
            }
            else
            {
                var InfoReadConfig = new LogMessage(LogSeverity.Info, $"{classname}", "Read Config...");
                _logger(InfoReadConfig);
                string sTemp = File.ReadAllText(Path.Combine(filepath + filename));
                config = JsonConvert.DeserializeObject<List<string>>(sTemp);

                foreach (string sTempString in config)
                {
                    Console.WriteLine(sTempString);
                }
                return true;
            }

        }

        //-------------------------------------------------------------------
        //Constructor 
        public Configreader(
        //Input
          string filepath,
          string filename,
          Func<LogMessage, Task> logger
        )
        {
            _logger = logger;

            if (!Validate( filepath, filename, ref _logger))
            {
                return ;
            }
            

            ReadConfig(filepath, filename,ref _logger);


        }
    }
}
