using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Framework.Src.Modules;
using Newtonsoft.Json;

namespace Framework.Src.Tools
{
    public class DataReaderMethods
    {
        public DataTable readCSVfile(string CSVFileName)
        {
            DataTable table = new DataTable();
            {

                var reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\src\datafiles\" + CSVFileName);
                int counter = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var entries = line.Split(';');
                    if (counter == 0)
                    {
                        foreach (string header in entries)
                        {
                            table.Columns.Add(header);
                        }
                    }
                    else
                    {
                        DataRow row = table.NewRow();
                        for (int i = 0; i < entries.Length; i++)
                        {
                            row[i] = entries[i];
                        }
                        table.Rows.Add(row);
                    }
                    counter++;
                }
                reader.Close();
                return table;
            }
        }

        public string RetrieveDataFromDataTable(DataTable table, string columnName, int rowNumber)
        {
            return (table.Rows[rowNumber])[columnName].ToString();
        }

        public T ReadFromJson<T>(string JsonFileName, bool CSVFile = false)
        {
            T items;
            if (!CSVFile)
            {
                using (StreamReader r = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\src\datafiles\" + JsonFileName))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<T>(json);
                    r.Close();
                }
            }
            else
            {
                items = JsonConvert.DeserializeObject<T>(JsonFileName);
            }
            return items;
        }

        public string ConvertCsvFileToJsonObject(string CSVFile)
        {
            var csv = new List<string[]>();
            var lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\src\datafiles\" + CSVFile);

            foreach (string line in lines)
                csv.Add(line.Split(';'));

            var properties = lines[0].Split(';');

            var listObjResult = new List<Dictionary<string, string>>();

            for (int i = 1; i < lines.Length; i++)
            {
                var objResult = new Dictionary<string, string>();
                for (int j = 0; j < properties.Length; j++)
                    objResult.Add(properties[j], csv[i][j]);

                listObjResult.Add(objResult);
            }

            return JsonConvert.SerializeObject(listObjResult);
        }
    }
}