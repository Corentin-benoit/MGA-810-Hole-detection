using SldWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hole_namespace
{ 
    internal class AdvanceHole : Hole
    {
        /*
        * Constructor
        */
        public AdvanceHole(double depth, double radius, int id, string functionHoleCreation, List<Face2> holeFaces)
        {
            this.depth = depth;
            this.radius = radius;
            this.id = id;
            this.functionHoleCreation = functionHoleCreation;
            this.holeFaces = holeFaces;
        }
        public override List<string> extractCharacteristicHole()
        {
            throw new NotImplementedException();
        }
    }
}
