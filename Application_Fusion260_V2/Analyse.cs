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
using System.Windows.Forms;

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
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;

namespace Application_Fusion260_V2
{
    internal class Analyse
    {

/*
* -----------------------------------------------------------------------------------------------------------------
* -------------------------------------------GLOBAL VARIABLES------------------------------------------------------ 
* -----------------------------------------------------------------------------------------------------------------
*/
        //Global variable to be modified according to user input
       /* public static float MIN_DEPTH = 0.0f;
        public static float MAX_DEPTH = 1000.0f;
        public static float MIN_RADIUS = 0.5f;
        public static float MAX_RADIUS = 500.0f;*/

/*
* -----------------------------------------------------------------------------------------------------------------
* ---------------------------------------------------MAIN---------------------------------------------------------- 
* -----------------------------------------------------------------------------------------------------------------
*/
        //static void Main(string[] args)
        public void run(float MIN_DEPTH, float MAX_DEPTH, float MIN_RADIUS, float MAX_RADIUS)
        {
            
            /*
            * -----------------------------------------------------------------------------------------------------------------
            * -------------------------------------------INITIALISATION-------------------------------------------------------- 
            * -----------------------------------------------------------------------------------------------------------------
            */

            // Interface Utilisateur 
            //Application.Run(new InterfaceUtilisateur());
           
            
            

            // Model Solidworks
            ModelDoc2 swDoc;
            SolidWorksWrapper swAppCalls = new SolidWorksWrapper();

            //Part SolidWorks
            swDoc = (ModelDoc2)swAppCalls.GetPart();
            Feature_Explorer myFeatureExplorer = new Feature_Explorer(swDoc);

            Object[] f_list;
            //List<WizardHoleFeatureData2> list_HoleWizard = new List<WizardHoleFeatureData2>();
            List<Hole> list_Hole = new List<Hole>();

            f_list = (Object[])myFeatureExplorer.getListFeature();

            int id = 0;
            string functionHoleCreation = "";
            List<Face2> holeFaces;



/*
* -----------------------------------------------------------------------------------------------------------------
* -----------------------------------------------CONNECTION-------------------------------------------------------- 
* -----------------------------------------------------------------------------------------------------------------
*/

            if (intitialisation(ref swAppCalls) == true) // solidworks' connection 
            {
                Thread.Sleep(5000);
                Console.WriteLine("Connection problems");
                
            }

/*
* -----------------------------------------------------------------------------------------------------------------
* ----------------------------------------DISPLAY FEATURES NAMES--------------------------------------------------- 
* -----------------------------------------------------------------------------------------------------------------
*/
            /*
            List<TreeControlItem> featureList;
            displayFeaturesNames(List < TreeControlItem > featureList, Feature_Explorer myFeatureExplorer)
            */


/*
* -----------------------------------------------------------------------------------------------------------------
* --------------------------------------------GET HOLES FEATURES--------------------------------------------------- 
* -----------------------------------------------------------------------------------------------------------------
*/
            //Browse inside FeatureManager
            foreach (Feature feature in f_list){
                //We search inside the tree with the Solidworks name of each function
                switch (feature.GetTypeName2())
                {
                    /*
                     * ---------------------------------------
                     * ----------HOLE WIZARDS-----------------
                     * ---------------------------------------
                     */
                    case "HoleWzd":
                        if (feature != null)
                        {//We collect data
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

                                double diameter = 0;

                                if (hw.HoleDiameter != 0){ diameter = hw.HoleDiameter; }
                                else{diameter = hw.ThruHoleDiameter;}
                                switch (hw.FastenerType2)
                                {

                                    case 135: // cas counterbore
                                                                              
                                        WizardHole_CounterBore myCounterBore = new WizardHole_CounterBore(hw.HoleDepth, diameter, id, functionHoleCreation, holeFaces, hw.Standard2, hw.FastenerType2, hw.CounterBoreDepth, hw.CounterBoreDiameter, hw.FastenerSize);
                                        list_Hole.Add(myCounterBore);
                                        break;
                                    case 141: // cas counterbore
                                        WizardHole_CounterSink myCounterSink = new WizardHole_CounterSink(hw.HoleDepth, diameter, id, functionHoleCreation, holeFaces, hw.Standard2, hw.FastenerType2, hw.CounterSinkDiameter, hw.FarCounterSinkAngle, hw.FastenerSize);
                                        list_Hole.Add(myCounterSink);
                                        break;
                                    default:
                                        break;

                                }


                            }
                        }
                        break;
                    /*
                    * ---------------------------------------
                    * ----------ADVANCES HOLES---------------
                    * ---------------------------------------
                    */
                    case "AdvHoleWzd":
                        if (feature != null)
                        {
                            holeFaces = new List<Face2>();
                            Object[] tab_Faces_ADV_Hole;
                            tab_Faces_ADV_Hole = feature.GetFaces();
                            if (tab_Faces_ADV_Hole != null)
                            {
                                foreach (Face2 face in tab_Faces_ADV_Hole)
                                {
                                    holeFaces.Add(face);
                                }
                            }

                            List<AdvancedHoleElementData> list_farSideEl = new List<AdvancedHoleElementData>();
                            List<AdvancedHoleElementData> list_nearSideEl = new List<AdvancedHoleElementData>();

                            AdvancedHoleFeatureData featdata = feature as AdvancedHoleFeatureData;
                            object[] nearSide = null; // tableaux qui récupèrent les near et far éléments qui composent le perçage 
                            object[] farSide = null;

                            featdata = (AdvancedHoleFeatureData)feature.GetDefinition(); // on précise que nous avons affaire a un AdvancedHoleFeatureData
                            featdata.AccessSelections(swDoc, null); // on accède à l'endroit de l'arbre où on crée le perçage
                            Console.Write("Number of near side hole elements: " + featdata.NearSideElementsCount + "\n"); // on affiche le nombre de near et far side éléments qui composent le perçage
                            Console.Write("Number of far side hole elements: " + featdata.FarSideElementsCount + "\n");

                            if (featdata.FarSideElementsCount != 0)
                            {
                                farSide = (object[])featdata.GetFarSideElements();
                                nearSide = (object[])featdata.GetNearSideElements(); // on les stock dans les tableaux
                                foreach (object o in farSide)
                                {
                                    list_farSideEl.Add((AdvancedHoleElementData)o);
                                }
                                foreach (object o in nearSide)
                                {
                                    list_nearSideEl.Add((AdvancedHoleElementData)o);
                                }
                                AdvanceHole myAdvanceHole = new AdvanceHole(feature.GetID(), feature.GetTypeName(), holeFaces, list_farSideEl, list_nearSideEl);
                                list_Hole.Add(myAdvanceHole);
                                myAdvanceHole.printData();
                            }
                            else
                            {
                                nearSide = (object[])featdata.GetNearSideElements(); // on les stock dans les tableaux
                                foreach (object o in nearSide)
                                {
                                    list_nearSideEl.Add((AdvancedHoleElementData)o);
                                }
                                AdvanceHole myAdvanceHole = new AdvanceHole(feature.GetID(), feature.GetTypeName(), holeFaces, list_nearSideEl);
                                list_Hole.Add(myAdvanceHole);
                            }
                            featdata.ReleaseSelectionAccess();
                        }
                        break;
                    /*
                     * ---------------------------------------
                     * -----------SIMPLE HOLE-----------------
                     * ---------------------------------------
                     */
                    case "SketchHole":
                        if (feature != null)
                        {
                            SimpleHoleFeatureData2 sh = (SimpleHoleFeatureData2)feature.GetDefinition();
                            id = feature.GetID();
                            functionHoleCreation = feature.GetTypeName2();
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
                    /*
                     * ---------------------------------------
                     * -----------EXTRUSION-------------------
                     * ---------------------------------------
                     */
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
                    /*
                     * ---------------------------------------
                     * --------------OTHERS-------------------
                     * ---------------------------------------
                     */
                    default:
                        // code block
                        break;
                }
            }

/*
* -----------------------------------------------------------------------------------------------------------------
* ---------------------------------------------COLOR HOLES FACES--------------------------------------------------- 
* -----------------------------------------------------------------------------------------------------------------
*/
            colorHoles(swDoc, list_Hole, MIN_DEPTH,  MAX_DEPTH,  MIN_RADIUS,  MAX_RADIUS);

/*
* -----------------------------------------------------------------------------------------------------------------
* -------------------------------------------------CSV EXPORT------------------------------------------------------ 
* -----------------------------------------------------------------------------------------------------------------
*/          
            List<HoleExport> list_holes_for_csv = new List<HoleExport>();
            int cpt_adv_max = 0;
            int cpt_adv = 0;
            foreach (Hole hole in list_Hole)
            {
                cpt_adv = 0;
                HoleExport hole_for_csv = new HoleExport();
                //List<string> listCharacteristics = new List<string>();
                _ = new List<string>();
                List<string> listCharacteristics;
                switch (hole.ass_functionHoleCreation)
                {
                    case "HoleWzd":
                        listCharacteristics = hole.extractCharacteristicHole();
                        if (listCharacteristics[0] == "135") // case counterbore
                        {
                            hole_for_csv.ass_id = listCharacteristics[1];
                            hole_for_csv.ass_functionHoleCreation = "Hole Wizard Counter Bore";
                            hole_for_csv.ass_diameter = listCharacteristics[3];
                            hole_for_csv.ass_depth = listCharacteristics[4];
                            hole_for_csv.ass_norm = listCharacteristics[5];
                            hole_for_csv.ass_counterBoreDepth = listCharacteristics[6];
                            hole_for_csv.ass_counterBoreDiameter = listCharacteristics[7];
                            hole_for_csv.ass_fastenerSize = listCharacteristics[8];


                        }
                        else if (listCharacteristics[0] == "141") // case countersink
                        {
                            hole_for_csv.ass_id = listCharacteristics[1];
                            hole_for_csv.ass_functionHoleCreation = "Hole Wizard Counter Sink";
                            hole_for_csv.ass_diameter = listCharacteristics[3];
                            hole_for_csv.ass_depth = listCharacteristics[4];
                            hole_for_csv.ass_norm = listCharacteristics[5];
                            hole_for_csv.ass_counterSinkDiameter = listCharacteristics[6];
                            hole_for_csv.ass_counterSinkAngle = listCharacteristics[7];
                            hole_for_csv.ass_fastenerSize = listCharacteristics[8];
                        }
                        else
                        {

                        }
                        list_holes_for_csv.Add(hole_for_csv);
                        break;

                    case "AdvHoleWzd":
                        listCharacteristics = hole.extractCharacteristicHole();

                        //Begin after 4 to retreive nearSideElements and farSideElements
                        //Step = 2, fot type and size
                        int i;
                        for (i = 4; i < listCharacteristics.Count; i = i + 2)
                        {
                            hole_for_csv.ass_id = listCharacteristics[0];
                            hole_for_csv.ass_functionHoleCreation = "Advanced Hole";
                            hole_for_csv.ass_diameter = listCharacteristics[2];
                            hole_for_csv.ass_depth = listCharacteristics[3];
                            hole_for_csv.ass_adv_size_element = listCharacteristics[i];
                            hole_for_csv.ass_adv_type_element = listCharacteristics[i + 1];
                            cpt_adv++;

                            list_holes_for_csv.Add(hole_for_csv);
                            hole_for_csv = new HoleExport();
                        }
                        break;

                    case "SketchHole":
                        listCharacteristics = hole.extractCharacteristicHole();

                        hole_for_csv.ass_id = listCharacteristics[0];
                        hole_for_csv.ass_functionHoleCreation = "Simple Hole";
                        hole_for_csv.ass_diameter = listCharacteristics[2];
                        hole_for_csv.ass_depth = listCharacteristics[3];

                        list_holes_for_csv.Add(hole_for_csv);
                        break;

                    default: break;
                }
                if (cpt_adv > cpt_adv_max)
                {
                    cpt_adv_max = cpt_adv;
                }
            }

            Export exporter = new Export();
            exporter.ass_nb_adv_element = cpt_adv_max;
            exporter.write_csv(list_holes_for_csv, "test_holes");
            SnapShot screeen;
         
            Thread.Sleep(100);
          
        }


/*
* -----------------------------------------------------------------------------------------------------------------
* ----------------------------------------------------METHODS------------------------------------------------------ 
* -----------------------------------------------------------------------------------------------------------------
*/

        /*
         * Methods which can initialize the project
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

        /*
         * Method for color a specific faces
         */

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

        /*
         * Method for display all the features names in the tree
         */
        public static void displayFeaturesNames(List<TreeControlItem> featureList, Feature_Explorer myFeatureExplorer)
        {
            // Retreive number of features
            int feature_count = myFeatureExplorer.getNumberFeatures();
            // retrive List of every feature in the Feature Manager
            featureList = myFeatureExplorer.TraverseFeatureManager();
            foreach (TreeControlItem feature in featureList)
            { // traverse the Feature List and print the name
                Console.Write(feature.Text + "\n");
            }
        }

        /*
         * Method for color holes
         */
        public static void colorHoles(ModelDoc2 swDoc, List<Hole> list_Hole, float MIN_DEPTH, float MAX_DEPTH, float MIN_RADIUS, float MAX_RADIUS)
        {
            foreach (Hole hole in list_Hole)
            {
                //Data in mm
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
