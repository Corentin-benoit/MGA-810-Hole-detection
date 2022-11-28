using hole_namespace;
using SldWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hole_namespace
{
    internal class WizardHole : Hole
    {
        /*
         * Attributes
         */
        protected string norme;

        /*
         * Constructor
         */
        public WizardHole(double depth, double diameter, int id, string functionHoleCreation, List<Face2> holeFaces, string norme) 
        { 
            this.depth = depth;
            this.diameter = diameter;
            this.id = id;
            this.functionHoleCreation = functionHoleCreation;
            this.holeFaces = holeFaces;
            this.norme = norme;
        }

        public WizardHole(int id, string functionHoleCreation, List<Face2> holeFaces)
        {
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
