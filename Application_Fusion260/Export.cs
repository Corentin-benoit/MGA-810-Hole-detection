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

        public void WriterCSV()
        {

            /*
            var holes = new List<User>
            {
                new ("John", "Doe", "gardener"),
                new ("Roger", "Roe", "driver"),
                new ("Lucy", "Smith", "teacher"),
            };
            */

            using var mem = new MemoryStream();
            using var writer = new StreamWriter(mem);
            using var csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture);

            csvWriter.WriteField("FirstName");
            csvWriter.WriteField("LastName");
            csvWriter.WriteField("Occupation");
            csvWriter.NextRecord();

            foreach (var user in users)
            {
                csvWriter.WriteField(user.FirstName);
                csvWriter.WriteField(user.LastName);
                csvWriter.WriteField(user.Occupation);
                csvWriter.NextRecord();
            }

            writer.Flush();
            var res = Encoding.UTF8.GetString(mem.ToArray());
            Console.WriteLine(res);

            record User(string FirstName, string LastName, string Occupation);
        }


    }
}
