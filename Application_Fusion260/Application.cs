using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SldWorks;
using SwConst;
using System.Windows;
using Wrapper;
using System.Threading;

//Need to be verify
using hole_namespace;


namespace Application_Fusion260
{
    internal class Application
    {
        static void Main(string[] args)
        {
            // Solid works application connection
            SolidWorksWrapper swAppCalls = new SolidWorksWrapper();
            
            if(intitialisation(ref swAppCalls) == true)
            {
                Thread.Sleep(2000);
                return;
            }
            ModelDoc2 swDoc = swAppCalls.GetPart(); // return the active document
            Feature_Explorer feature_Explorer = new Feature_Explorer(); // Feature manager explorer declaration
        }

        public static bool intitialisation(ref SolidWorksWrapper swAppCalls)
        {
            if (swAppCalls.AppReference == null)
            {
                Console.WriteLine("La connexion au serveur Solidworks a échoué! \n");
                return true;
            }

            object docActif = swAppCalls.ActiveDocument;
            if (docActif == null)
            {
                Console.Write("Il n'y a pas de document actif! \n");
                return true;
            }

            if (swAppCalls.isActiveDocAPart() == false)
            {
                Console.Write("Il n'y a pas de Part actif!");
                return true;
            }
            return false;
        }
    }
}
