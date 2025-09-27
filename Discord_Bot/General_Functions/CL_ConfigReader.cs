//standard system refernces


//own refernces

//project refernces


//Class namespase (refernce)
namespace CL_ConfigReader.Class
{
    //Class
    internal class CL_ConfigReader
    {
        //Class Variables
        //Field
        string sPrefix;

        //Properties
        public string __Get
        {
            get { return sPrefix; }
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




        //-------------------------------------------------------------------
        //Constructor 
        CL_ConfigReader(
        //Input
        string sInFilePath,
        string sInFileName

        )
        {
            if (!this.M_Validate(sInFilePath, sInFileName))
            {
                return;
            }




        }
    }
}
