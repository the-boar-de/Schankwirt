//standard system refernces
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//own refernces

//project refernces
using Discord;
using Discord.WebSocket;

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
        private string filename;

        //Message


        //Properties
        public IReadOnlyList<string> __GetConfigList
        {
            get { return config.AsReadOnly(); }
        }

        //Internal

        //-------------------------------------------------------------------
        //Methodes
        private bool ReadConfig(
            //Inputs
            string filename,
            ref Func<LogMessage, Task> logger
          )
        {
            if (filename == null)
            {
                var ErrorFilePath = new LogMessage(LogSeverity.Error, $"{classname}", "No Path or Filename was handover");
                _logger(ErrorFilePath);

                //exception handler
                // ?????
                return false;
            }
            else
            {
                //Logger
                var InfoReadConfig = new LogMessage(LogSeverity.Info, $"{classname}", "Read Config...");
                _logger(InfoReadConfig);
                //Read Config File
                string projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
                string fullPath = Path.Combine(projectRoot, "Config", filename);
                string sTemp = File.ReadAllText(fullPath);
                config = JsonConvert.DeserializeObject<List<string>>(sTemp);

                return true;
            }

        }

        //-------------------------------------------------------------------
        //Constructor 
        public Configreader(
        //Input
          string filename,
          Func<LogMessage, Task> logger
        )
        {

            _logger = logger;
            this.filename = filename;
        
            ReadConfig(this.filename,ref _logger);


        }
    }
}
