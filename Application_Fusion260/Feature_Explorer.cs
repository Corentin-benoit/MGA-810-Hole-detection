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
        /*
         * method that return the tree control item of the feature manager
         */
        public TreeControlItem GetTreeControlItem()
        {
            
            TreeControlItem rootNode = swFeatureManager.GetFeatureTreeRootItem2(1); 
            return rootNode;
            
        }
        /*
         * recursive method inspired from this code : https://www.codestack.net/solidworks-api/document/features-manager/traverse-feature-nodes/
         *  to traverse go throuth next node
         */
        public void TraverseFeatureNodes (TreeControlItem featNode, String offset, List<TreeControlItem> fList)
        {
            
            String offsetSymbol = " ";
            //Console.WriteLine(offset + featNode.Text);
            fList.Add(featNode);
            TreeControlItem swChildFeatNode = featNode.GetFirstChild();
            while(swChildFeatNode != null)
            {
                TraverseFeatureNodes(swChildFeatNode, offset + offsetSymbol, fList);
                swChildFeatNode = swChildFeatNode.GetNext();
            }
        }
        public List<TreeControlItem> TraverseFeatureManager()
        {
            List<TreeControlItem> featureList = new List<TreeControlItem>();
            TreeControlItem rootNode = GetTreeControlItem();
            featureList.Add(rootNode);
            TraverseFeatureNodes(rootNode, "", featureList);
            return featureList;
        }

        /* Method which go through the Feature Manager
         * and put all the Feature in a list
         */

        public FeatureManager GetFeatureManager()
        {

            
            return swFeatureManager;
        }

    }
}
