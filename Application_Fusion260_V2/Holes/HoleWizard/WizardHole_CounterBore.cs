using hole_namespace;
using SldWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hole_namespace
{
    internal class WizardHole_CounterBore : WizardHole
    {


        /*
         Attributes
        */

        protected double counterBoreDepth;
        protected double counterBoreDiameter;
        protected string fastenerSize;
        protected new int fastenerType; //Rajout d'un new à vérifier
      




        /*
         * Constructor
         */
        public WizardHole_CounterBore(  int id, 
                                        string functionHoleCreation, 
                                        List<Face2> holeFaces,
                                        int norm,
                                        double counterBoreDepth, 
                                        double counterBoreDiameter, 
                                        string fastenerSize, 
                                        double depth, int fastenerType) 
                                        : base(id, functionHoleCreation, holeFaces)
        {
            this.counterBoreDepth = counterBoreDepth;
            this.counterBoreDiameter = counterBoreDiameter;
            this.fastenerSize = fastenerSize;
            this.depth = depth;
            this.fastenerType = fastenerType;
           

        }
        /*
         * Assessors
         */
        public double ass_counterBoreDepth
        {
            get { return this.counterBoreDepth; }
            set { this.counterBoreDepth = value; }
        }
        public double ass_counterBoreDiameter
        {
            get { return this.counterBoreDiameter; }
            set { this.counterBoreDiameter = value; }
        }
        public string ass_fastenerSize
        {
            get { return this.fastenerSize; }
            set { this.fastenerSize = value; }
        }
        public int ass_fastenerType
        {
            get { return this.fastenerType; }
            set { this.fastenerType = value; }
        }

        public override List<string> extractCharacteristicHole()
        {
            List<string> list_properties = new List<string>();
            list_properties.Add((this.fastenerType).ToString()); // 0
            list_properties.Add((this.id).ToString()); // 1
            list_properties.Add(this.functionHoleCreation); // 2
            list_properties.Add((this.diameter).ToString()); // 3
            list_properties.Add((this.depth).ToString()); // 4
            list_properties.Add((this.norm).ToString());// 5
            list_properties.Add((this.counterBoreDepth).ToString()); // 6
            list_properties.Add((this.counterBoreDiameter).ToString()); // 7
            list_properties.Add((this.fastenerSize).ToString()); // 8
            return list_properties;
        }

    }
}
