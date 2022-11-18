/*
 * To use the application 
 * Main class
 */

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
            ModelDoc2 swDoc;
            

            // Solid works application connection
            SolidWorksWrapper swAppCalls = new SolidWorksWrapper();
            swDoc = (ModelDoc2)swAppCalls.GetPart();
            Feature_Explorer swFeatureManager = new Feature_Explorer(swDoc);
            int feature_count = 0;

            if (intitialisation(ref swAppCalls) == true)
            {
                Thread.Sleep(2000);
                return;
            }
            
            // Retreive number of features
            feature_count = swFeatureManager.getNumberFeatures();
            Console.Write(feature_count);
            Thread.Sleep(2000);
        }


        /*
         * Method to initialise our code and connect to SolidWorks
         */

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
