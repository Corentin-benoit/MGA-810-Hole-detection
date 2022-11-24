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

/*
 * Utile types
 * WizardHoleFeatureData2
 * SimpleHoleFeatureData2
 * AdvancedHole2
 * MirrorPartFeatureData
 * 
 * 
 * 
 * ExtrudeFeatureData2
 * FeatureCut3
 * 
 * 
 * 
 */

namespace Application_Fusion260
{
    internal class Application
    {
        static void Main(string[] args)
        {
            ModelDoc2 swDoc;
            List<TreeControlItem> featureList;
            Object[] f_list;
            List<WizardHoleFeatureData2> list_HoleWizard = new List<WizardHoleFeatureData2>();
            // SolidWorks application connection
            SolidWorksWrapper swAppCalls = new SolidWorksWrapper();
            swDoc = (ModelDoc2)swAppCalls.GetPart();
            Feature_Explorer myFeatureExplorer = new Feature_Explorer(swDoc);
            int feature_count = 0;

            if (intitialisation(ref swAppCalls) == true) // solidworks' connection 
            {
                Thread.Sleep(2000);
                return;
            }

            f_list = (Object[])myFeatureExplorer.getListFeature();
            foreach (Feature f in f_list){
                //Console.Write("GetDefinition() :" + f.GetDefinition() + "\n");
                Console.Write("GetTypeName2 :" + f.GetTypeName2() + "\n");
                /*Console.Write("GetSpecificFeature2:" + f.GetSpecificFeature2() + "\n");
                Console.Write("GetEditStatus() :" + f.GetEditStatus() + "\n");
                Console.Write("GetFirstSubFeature() :" + f.GetFirstSubFeature() + "\n");
                Console.Write("GetID() :" + f.GetID() + "\n");
                Console.Write("\n");
                Console.Write("\n");
                Console.Write("\n");*/
                if( f.GetTypeName2() == "HoleWzd")
                {

                    list_HoleWizard.Add((WizardHoleFeatureData2)f.GetDefinition());
                    
                    
                }
                foreach (WizardHoleFeatureData2 hole in list_HoleWizard)
                {
                  
                    Console.Write("holetype : :" + hole.GetType().Name + "\n");
                    if(hole.GetType().Name == 'CounterboreElementData')
                    {
                        CounterboreElementData count_hole = (CounterboreElementData)hole;
                        count_hole.ge
                    }
                    double b;
                    b = hole.HoleDepth;
                    Console.Write("b :" + b + "\n");
                }
                    
                
            }

            // Retreive number of features
            feature_count = myFeatureExplorer.getNumberFeatures();
            // retrive List of every feature in the Feature Manager
            featureList = myFeatureExplorer.TraverseFeatureManager();
            foreach (TreeControlItem feature in featureList){ // traverse the Feature List and print the name
                //Console.Write(feature.Text + "\n");
            }
            Thread.Sleep(1000000);

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

        public static void testColor(SolidWorksWrapper swAppCalls, ModelDoc2 swDoc)
        {
            //Test Method
            object[] bodies;
            object[] faces;
            Surface swSurface;
            bool boolResult;
            int cpt = 0;
            List<Face2> listhole = new List<Face2>();
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
                        listhole.Add(swFace);
                        cpt++;
                    }
                }
                wizardHole testHole = new wizardHole(10, 10, cpt, "coco", listhole);
                testHole.colorHole(swDoc, "orange");
            }
        }
    }
}
