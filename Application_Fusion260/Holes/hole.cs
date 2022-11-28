/* 
 * For management hole
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SldWorks;
using SwConst;

namespace hole_namespace
{
    abstract internal class Hole
    {
        /*
         * Attributes
         */
        protected double depth;
        protected double diameter;
        protected int id; // Id of the hole 
        protected string functionHoleCreation; //Name of the function which created the hole
        protected List<Face2> holeFaces; //Main cylindrical faces of the hole

        /*
         * Assessors
         */
        public double getDepth() { return depth; }
        public double getDiameter() { return diameter; }
        public int getId() { return id; }
        public string getFunctionHoleCreation() { return functionHoleCreation; }

        /*
         * Methods
         */

        /*
         * Method for color hole
         * Input : color
         * 
         * Inspired by the method colorCycle on Moodle
         */
        public void colorHole(ModelDoc2 swDoc, string color)
        {
            double[] descriptionColor = new double[3];

            //Take the right color
            switch (color)
            {
                case "orange":
                    //rgb(240, 94, 0)
                    descriptionColor[0] = 240.0f / 255.0f;
                    descriptionColor[1] = 94.0f / 255.0f;
                    descriptionColor[2] = 0.0f / 255.0f;
                    break;
                case "red":
                    //rgb(198, 26, 15)
                    descriptionColor[0] = 198.0f / 255.0f;
                    descriptionColor[1] = 26.0f / 255.0f;
                    descriptionColor[2] = 15.0f / 255.0f;
                    break;
                case "green":
                    //rgb(75, 152, 28)
                    descriptionColor[0] = 75.0f / 255.0f;
                    descriptionColor[1] = 152.0f / 255.0f;
                    descriptionColor[2] = 28.0f / 255.0f;
                    break;
                default:
                    //Penser à récupérer
                    throw new ArgumentException("Parameter is not a usable color", nameof(color));
            }

            foreach (Face2 swFace in this.holeFaces)
            {
                double[] colorInfo = swFace.MaterialPropertyValues;
                if (colorInfo == null)
                {
                    colorInfo = swDoc.MaterialPropertyValues;
                }
                for (int cpt = 0; cpt < 3; cpt++)
                {
                    colorInfo[cpt] = descriptionColor[cpt];
                }
                swFace.MaterialPropertyValues = colorInfo;
            }
        }

        /*
         * Abstract Method for extract the features of the hole in 
         * order to put them inside a CSV file
         */
        public abstract List<string> extractCharacteristicHole();

        /*
         * Method which known if the dimension are respected
         * Ref to our advance report to know the formulas
         */
        public bool sizeRespected(float min_depth, float max_depth, float min_radius, float max_radius)
        {
            double lu = this.depth;
            double radius = this.diameter/2.0f;
            double dc = 2*radius;

            //Depth test
            if (lu < min_depth) { return false;}
            if (lu > max_depth) { return false;}

            //Radius test
            if (radius < min_radius) { return false;}
            if (radius > max_radius) { return false;}

            //Match test
            if (lu > 3*dc) { return false;}
                        
            return true;
        }
    }
}
