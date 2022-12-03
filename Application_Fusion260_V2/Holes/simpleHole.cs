using SldWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hole_namespace
{
    internal class SimpleHole : Hole
    {
        /*
        * Constructor
        */
        public SimpleHole(double depth, double diameter, int id, string functionHoleCreation, List<Face2> holeFaces)
        {
            this.depth = depth;
            this.diameter = diameter;
            this.id = id;
            this.functionHoleCreation = functionHoleCreation;
            this.holeFaces = holeFaces;
        }
        public override List<string> extractCharacteristicHole()
        {
            List<string> list_properties = new List<string>();

            list_properties.Add((this.id).ToString()); // 0
            list_properties.Add(this.functionHoleCreation); // 1
            list_properties.Add((this.diameter).ToString()); // 2
            list_properties.Add((this.depth).ToString()); // 3

            return list_properties;
        }
    }
}
