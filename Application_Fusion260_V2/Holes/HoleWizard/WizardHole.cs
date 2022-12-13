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

        protected int norm;
        protected int fastenerType;
        private List<Face2> holeFaces1;
        private List<Face2> holeFaces2;

        /*
         * Assessors
         */
        public int ass_norme
        {
            get { return this.norm; }
            set { this.norm = value; }
        }

        /*
         * Constructor
         */
        public WizardHole(double depth, double diameter, int id, string functionHoleCreation, List<Face2> holeFaces, int norm, int fastenerType) 
        { 
            this.depth = depth;
            this.diameter = diameter;
            this.id = id;
            this.functionHoleCreation = functionHoleCreation;
            this.holeFaces = holeFaces;
            this.norm = norm;
            this.fastenerType = fastenerType;
        }

        public WizardHole(int id, string functionHoleCreation, List<Face2> holeFaces)
        {
            this.id = id;
            this.functionHoleCreation = functionHoleCreation;
            this.holeFaces = holeFaces;
        }

        public WizardHole(int id, string functionHoleCreation, List<Face2> holeFaces, int norm) : this(id, functionHoleCreation, holeFaces)
        {
            this.norm = norm;
        }

       

        public override List<string> extractCharacteristicHole()
        {
            throw new NotImplementedException();
        }

    }
}
