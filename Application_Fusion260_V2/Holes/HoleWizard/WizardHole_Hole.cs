using hole_namespace;
using SldWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hole_namespace
{
    internal class WizardHole_Hole : WizardHole
    {
        protected string fastenerSize;
        protected new int fastenerType;
        public WizardHole_Hole(double depth, double diameter, int id,
                                        string functionHoleCreation,
                                        List<Face2> holeFaces,
                                        int norm, int fastenerType, string fastenerSize)
                                        : base(depth, diameter, id, functionHoleCreation, holeFaces, norm, fastenerType)
        {
            this.depth = depth;
            this.diameter = diameter;
            this.norm = norm;
            this.fastenerType = fastenerType;
            this.fastenerSize = fastenerSize;

        }
        /*
        * Assessors
        */

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
            list_properties.Add((this.fastenerSize).ToString());// 6
            return list_properties;
        }
    }
}
