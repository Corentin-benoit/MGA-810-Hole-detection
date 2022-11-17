using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Fusion260
{
    internal class Feature_Explorer
    {
        /*
         * Attributes
         */


        /*
         * Constructor
         */


        /*
         * Assessors
         */


        /*
         * Methods
         */
        public object[] GetFeature()
        {
            Object[] features = swFeatureManager.GetFeatures(true);
        }
    }
}
