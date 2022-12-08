using SldWorks;
using SwConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hole_namespace
{
    internal class AdvanceHole : Hole
    {
        protected List<AdvancedHoleElementData> nearSideElements;
        protected List<AdvancedHoleElementData> farSideElements;
        protected List<String> holesSize = new List<String>();
        protected List<String> holesType = new List<String>();
        /*
        * Constructor
        */
        public AdvanceHole(int id, string functionHoleCreation, List<Face2> holeFaces, List<AdvancedHoleElementData> farSideElements, List<AdvancedHoleElementData> nearSideElements)
        {

            this.id = id;
            this.functionHoleCreation = functionHoleCreation;
            this.holeFaces = holeFaces;
            this.nearSideElements = nearSideElements;
            this.farSideElements = farSideElements;
            this.diameter = nearSideElements[1].Diameter;
            this.depth = nearSideElements[1].BlindDepth;
            Console.WriteLine(nearSideElements[1].BlindDepth);





            foreach (AdvancedHoleElementData nsElements in nearSideElements)
            { // on parcours ces tableaux pour connaitre les types des éléments



                this.holesSize.Add(nsElements.Size);
                this.holesType.Add(Enum.GetName(typeof(swAdvWzdGeneralHoleTypes_e), nsElements.ElementType));

                Enum.GetName(typeof(swAdvWzdGeneralHoleTypes_e), nsElements.ElementType);
            }
            foreach (AdvancedHoleElementData fsElements in farSideElements)
            { // on parcours ces tableaux pour connaitre les tailles des éléments



                this.holesSize.Add(fsElements.Size);
                this.holesType.Add(Enum.GetName(typeof(swAdvWzdGeneralHoleTypes_e), fsElements.ElementType));

                Enum.GetName(typeof(swAdvWzdGeneralHoleTypes_e), fsElements.ElementType);
            }
        }

        public AdvanceHole(int id, string functionHoleCreation, List<Face2> holeFaces, List<AdvancedHoleElementData> nearSideElements)
        {

            this.id = id;
            this.functionHoleCreation = functionHoleCreation;
            this.holeFaces = holeFaces;
            this.nearSideElements = nearSideElements;
            this.farSideElements = null;

            List<String> holesSize = new List<String>();
            List<String> holesType = new List<String>();

            foreach (AdvancedHoleElementData nsElements in nearSideElements)
            { // on parcours ces tableaux pour connaitre les types des éléments

                this.holesSize.Add(nsElements.Size);
                this.holesType.Add(Enum.GetName(typeof(swAdvWzdGeneralHoleTypes_e), nsElements.ElementType));

                Enum.GetName(typeof(swAdvWzdGeneralHoleTypes_e), nsElements.ElementType);
            }

        }


        public void printData()
        {
            foreach (String size in holesSize)
            {
                Console.Write("hole size :" + size + "\n");
            }
            foreach (String type in holesType)
            {
                Console.Write("hole type :" + type + "\n");
            }
        }


        public override List<string> extractCharacteristicHole()
        {
            int cpt = 0;
            List<string> list_properties = new List<string>();

            list_properties.Add((this.id).ToString()); // 0
            list_properties.Add(this.functionHoleCreation); // 1
            list_properties.Add((this.diameter).ToString()); // 2
            list_properties.Add((this.depth).ToString()); // 3

            // 4 and +
            foreach(String size in this.holesSize)
            {
                list_properties.Add((size).ToString()); //even
                list_properties.Add(((this.holesType)[cpt]).ToString()); //odd
                cpt++;
            }
            


            return list_properties;
        }

        
    }
}
