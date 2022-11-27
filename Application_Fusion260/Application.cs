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
using System.Linq.Expressions;

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
 * Methode intéressante :
 * SelectByID2
 * 
 * 
 */

namespace Application_Fusion260
{
    internal class Application
    {
        static void Main(string[] args)
        {
            /* 
             * FOR CONNECTION
             */
            ModelDoc2 swDoc;
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
            /*
             * END FOR CONNECTION
             */

            /*
             * INITIALISATION 
             */
            List<TreeControlItem> featureList;
            Object[] f_list;
            //List<WizardHoleFeatureData2> list_HoleWizard = new List<WizardHoleFeatureData2>();
            List<Hole> list_Hole = new List<Hole>();
            f_list = (Object[])myFeatureExplorer.getListFeature();
            /*
             * END INITIALISATION
             */

            //Browse inside FeatureManager
            foreach (Feature feature in f_list){
                //Console.Write("GetDefinition() :" + f.GetDefinition() + "\n");
                //Console.Write("GetTypeName2 :" + f.GetTypeName2() + "\n");
                Console.Write("GetTypeName :" + feature.GetTypeName2() + "\n");
               
                switch (feature.GetTypeName2())
                {
                    case "HoleWzd":

                        //We collect data
                        int id = feature.GetID();
                        Console.Write("Vérification id =" + id + "\n");

                        string functionHoleCreation = feature.GetTypeName();
                        Console.Write("Vérification name function =" + functionHoleCreation + "\n");

                        List<Face2> holeFaces = new List<Face2>();
                        Object[] tab_Faces;
                        tab_Faces = feature.GetFaces();
                        foreach (Face2 face in tab_Faces)
                        {
                            holeFaces.Add(face);
                        }

                        //We create a new object WizardHole
                        WizardHole holewizard = new WizardHole(id, functionHoleCreation, holeFaces);

                        //We save the object inside the list 
                        list_Hole.Add(holewizard);
                        
                        
                            f.GetDefinition()
                        break;
                    case "AdvHoleWzd":
                        //We collect data
                        break;
                    default:
                        // code block
                        break;
                }
                foreach (Hole hole in list_Hole)
                {
                    hole.colorHole(swDoc, "green");
                }
                /*
                if( f.GetTypeName2() == "HoleWzd")
                {
                    //list_HoleWizard.Add((WizardHoleFeatureData2)f.GetDefinition());
                  
                    list_Face = f.GetFaces();
                    foreach (Face2 faces in list_Face)
                    {
                        color(swDoc, "red", faces);
                    }
                    
                  */

                /*
                foreach (WizardHoleFeatureData2 hole in list_HoleWizard)
                {
                    Console.Write("hole  test:" + hole + "\n");
                    Console.Write("holetype :" + hole.GetType() + "\n");

                    if (hole.GetType().Name == "CounterboreElementData")
                    {
                        CounterboreElementData count_hole = (CounterboreElementData)hole;

                    }
                    double b;
                    b = hole.HoleDepth;
                    Console.Write("b :" + b + "\n");
                }
                */
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

        public static void color(ModelDoc2 swDoc, string color, Face2 swFace)
        {
            double[] descriptionColor = new double[3];

            //Take the right color
            switch (color)
            {
                case "orange":
                    //rgb(240, 94, 0)
                    descriptionColor[0] = 240.0f / 255.0f;
                    descriptionColor[1] = 94.0f / 255.0f;
                    descriptionColor[2] = 0.0f / 255.0f;
                    break;
                case "red":
                    //rgb(198, 26, 15)
                    descriptionColor[0] = 198.0f / 255.0f;
                    descriptionColor[1] = 26.0f / 255.0f;
                    descriptionColor[2] = 15.0f / 255.0f;
                    break;
                case "green":
                    //rgb(75, 152, 28)
                    descriptionColor[0] = 75.0f / 255.0f;
                    descriptionColor[1] = 152.0f / 255.0f;
                    descriptionColor[2] = 28.0f / 255.0f;
                    break;
                default:
                    //Penser à récupérer
                    throw new ArgumentException("Parameter is not a usable color", nameof(color));
            }
            double[] colorInfo = swFace.MaterialPropertyValues;
            if (colorInfo == null)
            {
                colorInfo = swDoc.MaterialPropertyValues;
            }
            for (int cpt = 0; cpt < 3; cpt++)
            {
                colorInfo[cpt] = descriptionColor[cpt];
            }
            swFace.MaterialPropertyValues = colorInfo;
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
                WizardHole testHole = new WizardHole(10, 10, cpt, "coco", listhole);
                testHole.colorHole(swDoc, "orange");
            }
        }
    }
}
