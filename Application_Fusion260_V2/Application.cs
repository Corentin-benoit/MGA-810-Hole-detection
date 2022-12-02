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
using System.Runtime.InteropServices;
using System.Diagnostics;
using CsvHelper;
using System.Globalization;
using System.IO;
using Application_Fusion260_V2;
using namespace_Feature_Explorer;
using Application_Fusion260;


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

namespace Application_Fusion260_V2
{
    internal class Application
    {
        //Variable globales à modifier en fonction de l'entrée utilisateur
        public static float MIN_DEPTH = 0.0f;
        public static float MAX_DEPTH = 1000.0f;
        public static float MIN_RADIUS = 0.5f;
        public static float MAX_RADIUS = 500.0f;

        static public DispatchWrapper[] ObjectArrayToDispatchWrapperArray(object[] Objects)
        {
            int ArraySize = 0;
            ArraySize = Objects.GetUpperBound(0);
            DispatchWrapper[] d = new DispatchWrapper[ArraySize + 1];
            int ArrayIndex = 0;
            for (ArrayIndex = 0; ArrayIndex <= ArraySize; ArrayIndex++)
            {
                ;
                d[ArrayIndex] = new DispatchWrapper(Objects[ArrayIndex]);
            }
            return d;
        }

        static void Main(string[] args)
        {
            /*
             * INITIALISATION 
             */
            ModelDoc2 swDoc;
            // SolidWorks application connection
            SolidWorksWrapper swAppCalls = new SolidWorksWrapper();
            swDoc = (ModelDoc2)swAppCalls.GetPart();
            Feature_Explorer myFeatureExplorer = new Feature_Explorer(swDoc);

            List<TreeControlItem> featureList;
            Object[] f_list;
            //List<WizardHoleFeatureData2> list_HoleWizard = new List<WizardHoleFeatureData2>();
            List<Hole> list_Hole = new List<Hole>();
            List<WizardHole> list_Hole_wizard = new List<WizardHole>();

            f_list = (Object[])myFeatureExplorer.getListFeature();

            int id = 0;
            string functionHoleCreation = "";
            List<Face2> holeFaces;

           
            /*
             * END INITIALISATION
             */
            /* 
             * FOR CONNECTION
             */

            if (intitialisation(ref swAppCalls) == true) // solidworks' connection 
            {
                Thread.Sleep(2000);
                return;
            }
            /*
             * END FOR CONNECTION
             */

            //Browse inside FeatureManager
            foreach (Feature feature in f_list){
                //Console.Write("GetDefinition() :" + f.GetDefinition() + "\n");
                //Console.Write("GetTypeName2 :" + f.GetTypeName2() + "\n");
                /*
                Console.Write("GetTypeName :" + feature.GetTypeName2() + "\n");
                if(feature.GetTypeName2() == "ICE")
                {
                    Console.Write("GetTypeName ICE:" + feature.GetTypeName() + "\n");
                }
                */
                
                switch (feature.GetTypeName2())
                {
                    /*
                     * HOLE WIZARDS
                     */
                    
                    case "HoleWzd":
                        if (feature != null)
                        {
                            //We collect data
                            id = feature.GetID();
                            Console.Write("Vérification id =" + id + "\n");

                            functionHoleCreation = feature.GetTypeName();
                            Console.Write("Vérification name function =" + functionHoleCreation + "\n");

                           
                            holeFaces = new List<Face2>();
                            Object[] tab_Faces_HW;
                            tab_Faces_HW = feature.GetFaces();
                            if (tab_Faces_HW != null)
                            {
                                foreach (Face2 face in tab_Faces_HW)
                                {
                                    holeFaces.Add(face);
                                }
                                WizardHoleFeatureData2 hw = (WizardHoleFeatureData2)feature.GetDefinition();

                                //hw.GetType().GetProperty(
                                Console.Write("Vhole wizard fastenerSize =" + hw.FastenerSize + "\n");
                                Console.Write("Vhole wizard FastenerType2 =" + hw.FastenerType2 + "\n");

                                Console.Write("Vhole wizard CounterBoreDepth =" + hw.CounterBoreDepth + "\n");
                                Console.Write("Vhole wizard CounterBoreDiameter =" + hw.CounterBoreDiameter + "\n");

                                
                                Console.Write("Vhole wizard MajorDiameter =" + hw.MajorDiameter + "\n");
                                Console.Write("Vhole wizard MinorDiameter =" + hw.MinorDiameter + "\n");
                                Console.Write("Vhole wizard MidCounterSinkDiameter =" + hw.MidCounterSinkDiameter + "\n");
                                Console.Write("Vhole wizard FarCounterSinkDiameter =" + hw.FarCounterSinkDiameter + "\n");
                                Console.Write("Vhole wizard NearCounterSinkDiameter =" + hw.NearCounterSinkDiameter + "\n");
                                Console.Write("Vhole wizard TapDrillDiameter =" + hw.TapDrillDiameter + "\n");
                                Console.Write("Vhole wizard ThreadDiameter =" + hw.ThreadDiameter + "\n");
                                Console.Write("Vhole wizard ThruHoleDiameter =" + hw.ThruHoleDiameter + "\n");
                                Console.Write("Vhole wizard ThruTapDrillDiameter =" + hw.ThruTapDrillDiameter + "\n");

                                Console.Write("Vhole wizard ThruTapDrillDiameter =" + hw.Standard2 + "\n");

                                Console.Write( "\n");
                                

                                    
                                Console.Write("Hole Wizard's Standard: " + Enum.GetName(typeof(swWzdHoleStandards_e),hw.Standard2) + "\n");
                                Console.Write("Hole Wizard's FastenerType2: " + Enum.GetName(typeof(swWzdHoleStandardFastenerTypes_e), hw.FastenerType2) + "\n");




                                Console.Write("Vhole wizard CounterDrillDepth =" + hw.CounterDrillDepth + "\n");
                                Console.Write("Vhole wizard CounterDrillDiameter =" + hw.CounterDrillDiameter + "\n");

                                Console.Write("Vhole wizard CounterSinkDiameter =" + hw.CounterSinkDiameter + "\n");
                                Console.Write("Vhole wizard CounterSinkDiameter =" + hw.CounterSinkDiameter + "\n");

                                Console.Write("Vhole wizard Depth =" + hw.HoleDepth + "\n"); // if equals to 0, it means that the hole is through hole

                                Console.Write("Vhole wizard HeadClearance =" + hw.HeadClearance + "\n");
                                Console.Write("Vhole wizard HeadClearanceType =" + hw.HeadClearanceType + "\n");

                                
                                

                                WizardHole holewizard = new WizardHole(id, functionHoleCreation, holeFaces);

                                //We save the object inside the list 
                                list_Hole.Add(holewizard);
                                list_Hole_wizard.Add(holewizard);

                                /*
                                 * Advanced Holes 
                                 */
                            }
                        }
                    
                        break;
                    case "AdvHoleWzd":
                        if (feature != null)
                        {
                            
                            AdvancedHoleFeatureData featdata = feature as AdvancedHoleFeatureData;
                            object[] nearSide = null; // tableaux qui récupèrent les near et far éléments qui composent le perçage 
                            object[] farSide = null;

                            CountersinkElementData swCounterSinkNear = default(CountersinkElementData);
                            CounterboreElementData swCounterBoreFar = default(CounterboreElementData);
                            StraightElementData swStraightHoleFar = default(StraightElementData);
                            
                            featdata = (AdvancedHoleFeatureData)feature.GetDefinition(); // on précise que nous avons affaire a un AdvancedHoleFeatureData
                            featdata.AccessSelections(swDoc, null); // on accède à l'endroit de l'arbre où on crée le perçage
                            Console.Write("Number of near side hole elements: " + featdata.NearSideElementsCount + "\n"); // on affiche le nombre de near et far side éléments qui composent le perçage
                            Console.Write("Number of far side hole elements: " + featdata.FarSideElementsCount + "\n");
                            
                            nearSide = (object[])featdata.GetNearSideElements(); // on les stock dans les tableaux
                     
                            swCounterSinkNear = (CountersinkElementData)nearSide[0];
                            farSide = (object[])featdata.GetFarSideElements();

                            foreach (AdvancedHoleElementData farSideElements in farSide) { // on parcours ces tableaux pour connaitre les types des éléments

                                
                                Console.Write("farSideElements Standard: " + Enum.GetName(typeof(swAdvWzdGeneralHoleTypes_e), farSideElements.ElementType) + "\n");
                                Console.Write("farSideElements size:"+ farSideElements.Size  + "\n");
                            }
                            swCounterBoreFar = (CounterboreElementData)farSide[0];
                            swStraightHoleFar = (StraightElementData)farSide[1];


                            Console.Write("Near side countersink: \n" );

                            Console.Write("   Hole element type as defined in swAdvWzdGeneralHoleTypes_e: " + ((AdvancedHoleElementData)swCounterSinkNear).ElementType + "\n");
                            Console.Write("   Size as defined on the Advanced Hole PropertyManager page: " + ((AdvancedHoleElementData)swCounterSinkNear).Size + "\n");
                            Console.Write("   Standard as defined in swWzdHoleStandards_e: " + ((AdvancedHoleElementData)swCounterSinkNear).Standard + "\n");
                           


                        }
                        break;
                    
                    case "SketchHole":
                        if (feature != null)
                        {
                            SimpleHoleFeatureData2 sh = (SimpleHoleFeatureData2)feature.GetDefinition();
                            id = feature.GetID();
                            functionHoleCreation = feature.GetTypeName();
                            holeFaces = new List<Face2>();
                            Object[] tab_Faces_SH;
                            tab_Faces_SH = feature.GetFaces();
                            if (tab_Faces_SH != null)
                            {
                                foreach (Face2 face in tab_Faces_SH)
                                {
                                    holeFaces.Add(face);
                                }
                                SimpleHole simpleHole = new SimpleHole(sh.Depth, sh.Diameter, id, functionHoleCreation, holeFaces);

                                //We save the object inside the list 
                                list_Hole.Add(simpleHole);
                            }
                        }
                        break;
                    //To get the underlying type of feature of an Instant3D feature (i.e., "ICE"), call IFeature::GetTypeName; otherwise, call this GetTypeName. 
                    case "ICE":
                        switch (feature.GetTypeName())
                        {
                            case "Cut":
                                Feature subFeat = (Feature)feature.GetFirstSubFeature();
                                Sketch sketchSubFeature = (Sketch)subFeat.GetDefinition();
                                //Console.Write("Test valeur cut =" + subFeat.GetDefinition() + "\n");
                                
                                if (true)
                                {
                                    ExtrudeFeatureData2 cutHole = (ExtrudeFeatureData2)feature.GetDefinition();
                                }
                                


                                //Console.Write("Vérification id adv =" + cutHole. + "\n");
                                break;
                            default:
                                // code block
                            break;

                        }
                        break;
                    default:
                        // code block
                        break;
                }
            }
            colorHoles(swDoc, list_Hole);

            /*
             * CSV EXPORT
             */
            /*
            using (var writer = new StreamWriter("Hole.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(list_Hole);
            }
            */
            Export exporter = new Export(list_Hole);
            exporter.write_csv("test_holes");

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

        public static void displayFeaturesNames(List<TreeControlItem> featureList, Feature_Explorer myFeatureExplorer)
        {
            // Retreive number of features
            int feature_count = myFeatureExplorer.getNumberFeatures();
            // retrive List of every feature in the Feature Manager
            featureList = myFeatureExplorer.TraverseFeatureManager();
            foreach (TreeControlItem feature in featureList)
            { // traverse the Feature List and print the name
                //Console.Write(feature.Text + "\n");
            }
        }

        public static void colorHoles(ModelDoc2 swDoc, List<Hole> list_Hole)
        {
            foreach (Hole hole in list_Hole)
            {
                if(hole.sizeRespected(mmTOm(MIN_DEPTH), mmTOm(MAX_DEPTH), mmTOm(MIN_RADIUS), mmTOm(MAX_RADIUS)) == true)
                {
                    hole.colorHole(swDoc, "green");
                }
                else
                {
                    hole.colorHole(swDoc, "red");
                }
            }
        }

        //Meters to millimeters 
        public static int mTOmm(int value){ return (value * 1000); }
        public static float mTOmm(float value) { return (value * 1000.0f); }
        public static double mTOmm(double value) { return (value * 1000.0d); }
        public static uint mTOmm(uint value) { return (value * 1000); }
        public static long mTOmm(long value) { return (value * 1000L); }
        public static ulong mTOmm(ulong value) { return (value * 1000ul); }

        //Millimeters to meters
        public static int mmTOm(int value) { return (value / 1000); }
        public static float mmTOm(float value) { return (value / 1000.0f); }
        public static double mmTOm(double value) { return (value / 1000.0d); }
        public static uint mmTOm(uint value) { return (value / 1000); }
        public static long mmTOm(long value) { return (value / 1000L); }
        public static ulong mmTOm(ulong value) { return (value / 1000ul); }
    }
}
