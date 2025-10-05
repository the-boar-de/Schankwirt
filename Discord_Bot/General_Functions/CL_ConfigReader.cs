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
        internal List<string> lConfiguration;
        bool bConfigValid;
       

        //Properties
        public List<string> __Get
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
            string sInFilePath,
            string sInFileName
          )
        {
            Console.WriteLine("Read config");
            string sTemp = File.ReadAllText(Path.Combine(sInFilePath + sInFileName));

            List<string> lConfiguration = JsonConvert.DeserializeObject<List<string>>(sTemp);

            return true;
        }




        //-------------------------------------------------------------------
        //Constructor 
        public CL_ConfigReader(
        //Input
        string sInFilePath,
        string sInFileName

        )
        {
            if (!this.M_Validate(sInFilePath, sInFileName))
            {
                return ;
            }

            if (!this.bConfigValid)
            {
                this.bConfigValid =  M_ReadConfig(sInFilePath, sInFileName);
            }



        }
    }
}
