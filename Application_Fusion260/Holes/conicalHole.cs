using hole_namespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hole_namespace
{
    internal class ConicalHole : Hole
    {
        public override List<string> extractCharacteristicHole()
        {
            throw new NotImplementedException();
        }

        public bool sizeRespected(float Chanfrein_size_min, float Chanfrein_size_max, float min_depth, float max_depth, float min_radius, float max_radius)
        {
            double lu = this.depth;
            double radius = this.diameter;
            double dc = 2 * radius;

            //Depth test
            if (lu < min_depth) { return false; }
            if (lu > max_depth) { return false; }

            //Radius test
            if (radius < min_radius) { return false; }
            if (radius < max_radius) { return false; }

            //Match test
            if (lu > 3 * dc) { return false; }

            return true;
        }
    }
}
