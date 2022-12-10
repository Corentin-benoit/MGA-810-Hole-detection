using SldWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hole_namespace
{
    internal class WizardHole_Straight_Tab : WizardHole
    {
        public WizardHole_Straight_Tab(double depth, double diameter, int id,
                                        string functionHoleCreation,
                                        List<Face2> holeFaces,
                                        int norm, int fastenerType, string fastenerSize)
                                        : base(depth, diameter, id, functionHoleCreation, holeFaces, norm, fastenerType)
        {
            this.depth = depth;
            this.diameter = diameter;
            this.norm = norm;
            
        }
    }
}
