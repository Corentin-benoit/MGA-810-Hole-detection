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

        /*
         * Attributes
         */
        List<Hole> list_Hole;

        /*
        * Constructor
        */
        public Export(List<Hole> list_Hole)
        {
            this.list_Hole = list_Hole;
        }

        /*
         * Assessors
         */


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

        public void write_csv(string csv_name)
        {
            using (var writer = new StreamWriter(csv_name+".csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(this.list_Hole);
            }
        }

        public void write_csv(List<Hole> list_Hole_specific, string csv_name)
        {
            using (var writer = new StreamWriter(csv_name + ".csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(list_Hole_specific);
            }
        }



        /*
        public static CsvConfiguration GetwizardHoleConfiguration<T>(IEnumerable<T> records) where T : WizardHole
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);

            var classMap = new DefaultClassMap<T>();

            classMap.AutoMap(CultureInfo.InvariantCulture);
            classMap.Map(m => m.norme).Name("Norm");

            //config.RegisterClassMap(classMap);

            return config;
        }
        */
    }
}
