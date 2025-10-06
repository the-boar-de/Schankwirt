//standard system refernces
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//own refernces

//project refernces


//Class namespase (refernce)
namespace CL_ConfigReader
{
    //Class
    public class CL_ConfigReader
    {
        //Class Variables
        //Field
        private List<string> lConfiguration = new();
       

        //Properties
        public List<string> __GetConfigList
        {
            get { return lConfiguration; }
        }

        //Internal

        //-------------------------------------------------------------------
        //Methodes
        private bool M_Validate(
            //Input
             object oInObject1,
             object oInObject2)
        {
            if (oInObject1 == null || oInObject2 == null)
            {
                Console.WriteLine("This call is not valid!"); // --> künftig in einen Logger packen und ich json form speichern
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool M_ReadConfig(
            //Inputs
            ref string sInFilePath,
            ref string sInFileName
          )
        {
            if (sInFilePath == null || sInFileName == null)
            {
                Console.WriteLine("No Path or Filename was handover");

                //exception handler
                // ?????
                return false;
            }
            else
            {
                Console.WriteLine("Read Config...");
                string sTemp = File.ReadAllText(Path.Combine(sInFilePath + sInFileName));
                this.lConfiguration = JsonConvert.DeserializeObject<List<string>>(sTemp);

                foreach (string sTempString in lConfiguration)
                {
                    Console.WriteLine(sTempString);
                }

                return true;



            }

        }




        //-------------------------------------------------------------------
        //Constructor 
        public CL_ConfigReader(
        //Input
         string sInFilePath,
         string sInFileName

        )
        {
            if (!this.M_Validate( sInFilePath, sInFileName))
            {
                return ;
            }

            M_ReadConfig(ref sInFilePath, ref sInFileName);


        }
    }
}
