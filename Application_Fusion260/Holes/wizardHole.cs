using hole_namespace;
using SldWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hole_namespace
{
    internal class wizardHole : Hole
    {
        /*
         * Constructor
         */
        public wizardHole(double depth, double radius, int id, string functionHoleCreation, Face2[] holeFaces) 
        { 
            this.depth = depth;
            this.radius = radius;
            this.id = id;
            this.functionHoleCreation = functionHoleCreation;
            this.holeFaces = new Face2[holeFaces.Length];
            Array.Copy(holeFaces, this.holeFaces, holeFaces.Length);
        }
        public override List<string> extractCharacteristicHole()
        {
            throw new NotImplementedException();
        }
    }
}
