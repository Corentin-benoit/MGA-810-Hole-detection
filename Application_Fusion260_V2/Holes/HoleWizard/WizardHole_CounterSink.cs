using hole_namespace;
using SldWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hole_namespace
{
    internal class WizardHole_CounterSink : WizardHole
    {
        /*
         * Attributes
         */
        protected double counterSinkDiameter;
        protected double counterSinkAngle;
        protected string fastenerSize;
        protected int fastenerType;
        

        /*
         * Constructor
         */

        public WizardHole_CounterSink(int id,
                                        string functionHoleCreation,
                                        List<Face2> holeFaces,
                                        int norm,
                                        double counterSinkDiameter,
                                        double counterSinkAngle,
                                        string fastenerSize,
                                        double depth, int fastenerType)
                                        : base(id, functionHoleCreation, holeFaces)
        {
            this.counterSinkDiameter = counterSinkDiameter;
            this.counterSinkAngle = counterSinkAngle;
            this.depth = depth;
            this.fastenerSize = fastenerSize;
            this.fastenerType = fastenerType;
           
        }

        /*
         * Assessors
         */

        public double ass_counterSinkDiameter
        {
            get { return this.counterSinkDiameter; }
            set { this.counterSinkDiameter = value; }
        }
        public double ass_counterSinkAngle
        {
            get { return this.counterSinkAngle; }
            set { this.counterSinkAngle = value; }
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
            list_properties.Add((this.id).ToString());// 1
            list_properties.Add(this.functionHoleCreation);// 2
            list_properties.Add((this.diameter).ToString());// 3
            list_properties.Add((this.depth).ToString());// 4
            list_properties.Add((this.norm).ToString());// 5
            list_properties.Add((this.counterSinkDiameter).ToString());// 6
            list_properties.Add((this.counterSinkAngle).ToString());// 7
            list_properties.Add((this.fastenerSize).ToString());// 8
            return list_properties;
        }
    }
}
