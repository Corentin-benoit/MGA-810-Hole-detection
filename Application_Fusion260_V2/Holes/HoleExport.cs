using CsvHelper.Configuration.Attributes;
using SldWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hole_namespace
{
    internal class HoleExport
    {
        /*
        * Attributes
        */
        protected string depth = "NaN";
        protected string diameter = "NaN" ;
        protected string id = "NaN"; // Id of the hole 
        protected string functionHoleCreation = "NaN"; //Name of the function which created the hole
        protected string norm = "NaN";
        protected string counterSinkDiameter = "NaN";
        protected string counterSinkAngle = "NaN";
        protected string fastenerSize = "NaN";
        protected string counterBoreDiameter = "NaN";
        protected string counterBoreDepth = "NaN";

        /*
         * Assessors
         */

        [Name("Hole Id")]
        [Index(0)]
        public string ass_id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        [Name("Name tree function")]
        [Index(1)]
        public string ass_functionHoleCreation
        {
            get { return this.functionHoleCreation; }
            set { this.functionHoleCreation = value; }
        }
        [Name("Hole Diameter")]
        [Index(2)]
        public string ass_diameter
        {
            get { return this.diameter; }
            set { this.diameter = value; }
        }
        [Name("Hole Depth")]
        [Index(3)]
        public string ass_depth
        {
            get { return this.depth; }
            set { this.depth = value; }
        }
        [Name("Norm")]
        [Index(4)]
        public string ass_norm
        {
            get { return this.norm; }
            set { this.norm = value; }
        }
        [Name("FastenerSize")]
        [Index(5)]
        public string ass_fastenerSize
        {
            get { return this.fastenerSize; }
            set { this.fastenerSize = value; }
        }
        [Name("Counter Sink Diameter")]
        [Index(6)]
        public string ass_counterSinkDiameter
        {
            get { return this.counterSinkDiameter; }
            set { this.counterSinkDiameter = value; }
        }
        [Name("Counter Sink Angle")]
        [Index(7)]
        public string ass_counterSinkAngle
        {
            get { return this.counterSinkAngle; }
            set { this.counterSinkAngle = value; }
        }
        [Name("Counter Bode Diameter")]
        [Index(8)]
        public string ass_counterBoreDiameter
        {
            get { return this.counterBoreDiameter; }
            set { this.counterBoreDiameter = value; }
        }
        [Name("Counter Bore Depth")]
        [Index(9)]
        public string ass_counterBoreDepth
        {
            get { return this.counterBoreDepth; }
            set { this.counterBoreDepth= value; }
        }
        /*
         * Methods
         */

    }
}
