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

        public List<UserModule> ReadFromJson(string JsonFileName)
        {
            List<UserModule> items;
            using (StreamReader r = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\src\datafiles\" + JsonFileName))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<UserModule>>(json);
                r.Close();
            }
            
            return items;
        }
    }
}