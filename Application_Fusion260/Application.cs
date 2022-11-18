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
using System.Security.Cryptography;

namespace Application_Fusion260
{
    internal class Application
    {
        static void Main(string[] args)
        {
            ModelDoc2 swDoc;
            List<TreeControlItem> featureList;

            // SolidWorks application connection
            SolidWorksWrapper swAppCalls = new SolidWorksWrapper();
            swDoc = (ModelDoc2)swAppCalls.GetPart();
            Feature_Explorer myFeatureExplorer = new Feature_Explorer(swDoc);
            int feature_count = 0;

            if (intitialisation(ref swAppCalls) == true)
            {
                Thread.Sleep(2000);
                return;
            }
            
            // Retreive number of features
            feature_count = myFeatureExplorer.getNumberFeatures();
            // retrive List of every feature in the Feature Manager
            featureList = myFeatureExplorer.TraverseFeatureManager();
            foreach (TreeControlItem feature in featureList){ // traverse the Feature List and print the name
                Console.Write(feature.Text + "\n");
               
               
            }

            //Test Method
            object[] bodies;
            object[] faces;
            Surface swSurface;
            bool boolResult;
            int cpt = 0;
            Face2[] listhole = new Face2[999];
            PartDoc swPart = (PartDoc)swAppCalls.GetPart();
            bodies = swPart.GetBodies2((int)swBodyType_e.swSolidBody, false);

            foreach (Body2 swBody in bodies)
            {
                faces = swBody.GetFaces();
                foreach (Face2 swFace in faces)
                {
                    swSurface = swFace.GetSurface();
                    boolResult = swSurface.IsCylinder();

                    if (boolResult == true)
                    {
                        listhole[cpt] = swFace;
                        cpt++;  
                    }
                }
                wizardHole testHole = new wizardHole(10, 10, cpt, "coco", listhole);
                testHole.colorHole(swDoc, "red");
            }
            Thread.Sleep(2000);
            Thread.Sleep(10000);
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
