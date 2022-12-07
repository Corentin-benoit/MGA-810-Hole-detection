/*
 * Using for export our holes' features
 */

/*
 * https://www.nuget.org/packages/CsvHelper/
 * To use CSV helper, copy and paste the following line in Package Manager console (Tools -> Nugets Package Manager)
 * NuGet\Install-Package CsvHelper -Version 30.0.1
 * https://www.codingame.com/playgrounds/5197/writing-csv-files-using-c
 * https://dev.to/bristolsamo/c-csv-parser-step-by-step-tutorial-25ok
 */

using System.Globalization;

using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SldWorks;
using hole_namespace;


namespace Application_Fusion260
{
    internal class Export
    {
        public static int nb_adv_element = 0;
        /*
         * Attributes
         */
        private int export_nb_adv_element=0;
        /*
        * Constructor
        */
        public Export(){}

        /*
         * Assessors
         */
        public int ass_nb_adv_element
        {
            get { return this.export_nb_adv_element; }
            set { this.export_nb_adv_element = value; }
        }
        /*
         * Methods
         */

        /*
         * To to read or write in a non-standard format
         * \r or \n
         */
        public void configNewLine()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = System.Environment.NewLine,
            };
        }

        
        public void write_csv(List<Hole> list_Hole_specific, string csv_name)
        {
            using (var writer = new StreamWriter(csv_name + ".csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                nb_adv_element = this.ass_nb_adv_element;
                csv.Context.RegisterClassMap<HoleMap>(); // to display header names and index
                csv.WriteRecords(list_Hole_specific);
            }
        }

        public void write_csv(List<HoleExport> list_Hole_specific, string csv_name)
        {
            using (var writer = new StreamWriter(csv_name + ".csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                nb_adv_element = this.ass_nb_adv_element;
                csv.Context.RegisterClassMap<HoleMap>();// to display header names and index
                csv.WriteRecords(list_Hole_specific);
            }
        }

        // to display header names and index
        public sealed class HoleMap : ClassMap<HoleExport>
        {
            public HoleMap()
            {
                int cpt = 0;
                Map(m => m.ass_id).Index(cpt);
                Map(m => m.ass_id).Name("Hole Id");
                cpt++;
                Map(m => m.ass_functionHoleCreation).Index(cpt);
                Map(m => m.ass_functionHoleCreation).Name("Name tree function");
                cpt++;
                Map(m => m.ass_diameter).Index(cpt);
                Map(m => m.ass_diameter).Name("Hole Diameter");
                cpt++;
                Map(m => m.ass_depth).Index(cpt);
                Map(m => m.ass_depth).Name("Hole Depth");
                cpt++;
                Map(m => m.ass_norm).Index(cpt);
                Map(m => m.ass_norm).Name("Norm");
                cpt++;
                Map(m => m.ass_fastenerSize).Index(cpt);
                Map(m => m.ass_fastenerSize).Name("FastenerSize");
                cpt++;
                Map(m => m.ass_counterSinkDiameter).Index(cpt);
                Map(m => m.ass_counterSinkDiameter).Name("Counter Sink Diameter");
                cpt++;
                Map(m => m.ass_counterSinkAngle).Index(cpt);
                Map(m => m.ass_counterSinkAngle).Name("Counter Sink Angle");
                cpt++;
                Map(m => m.ass_counterBoreDiameter).Index(cpt);
                Map(m => m.ass_counterBoreDiameter).Name("Counter Bode Diameter");
                cpt++;
                Map(m => m.ass_counterBoreDepth).Index(cpt);
                Map(m => m.ass_counterBoreDepth).Name("Counter Bore Depth");
                cpt++;
                if(nb_adv_element != 0)
                {
                    Map(m => m.ass_adv_size_element).Index(cpt);
                    Map(m => m.ass_adv_size_element).Name((string)("Adavanced size element "));
                    cpt++;
                    Map(m => m.ass_adv_type_element).Index(cpt);
                    Map(m => m.ass_adv_type_element).Name((string)("Adavanced type element "));
                    cpt++;
                }
            }
        }
    }
}
