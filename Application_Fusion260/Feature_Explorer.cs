/*
 * Using for navigate inside the FeatureManager Design Tree
 */

using SldWorks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Application_Fusion260
{
    internal class Feature_Explorer
    {
        /*
         * Attributes
         */

        protected FeatureManager swFeatureManager;


        /*
         * Constructor
         */

        public Feature_Explorer(ModelDoc2 swDoc) /* Il le faut ici */
        {
            swFeatureManager = swDoc.FeatureManager;
        }

        /*
         * Assessors
         */


        /*
         * Methods
         */

        /*
         * Method which return the number of features for a body
         */
        public int getNumberFeatures()
        {
            return swFeatureManager.GetFeatureCount(true);
        }

        /* Method which go through the Feature Manager
         * and put all the Feature in a list
         */
        
        public object getListFeature()
        {
   
            Object[] features = swFeatureManager.GetFeatures(true); 
            return features;
        }

        public void browseFeatures()
        {

        } 
        
        /*
            FeatureManager swFeatureManager = swDoc.FeatureManager;
            object[] features = swFeatureManager.GetFeatures(true);
            Feature lol;
            Feature sub;
            foreach (Object feature in features)
            {
                Debug.Print("name features:" + feature);
                lol = (Feature)feature;
               

                sub = lol.GetNameForSelection();
                
            }
        */
    }
}
