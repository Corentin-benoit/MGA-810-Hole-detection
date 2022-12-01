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
        Object[] nearSideElements;
        Object[] farSideElements;
        /*
        * Constructor
        */
        public AdvanceHole(int id, string functionHoleCreation, List<Face2> holeFaces, Object[] farSideElements, Object[] nearSideElements)
        {
            
            this.id = id;
            this.functionHoleCreation = functionHoleCreation;
            this.holeFaces = holeFaces;
            this.nearSideElements = nearSideElements;
            this.farSideElements = farSideElements;
        }   
        public override List<string> extractCharacteristicHole()
        {
            throw new NotImplementedException();
        }
    }
}
