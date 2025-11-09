using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocGhiFileCSV
{
    public static class ReadCSV
    {
        private static string[] ParseCSVLine(string line)
        {
            List<string> fields = new List<string>();
            StringBuilder currentField = new StringBuilder();
            bool flag = false;
            foreach (char c in line)
            {
                if (c == '"')
                {
                    flag = !flag;
                }
                else if (c == ',' && !flag)
                {
                    fields.Add(currentField.ToString());
                    currentField.Clear();
                }
                else
                {
                    currentField.Append(c);
                }
            }
                fields.Add(currentField.ToString());
                return fields.ToArray();
        }
        public static List<string[]> ReadCsvFile(string filePath)
        {
            List<string[]> rows = new List<string[]>();
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length > 0)
                {
                    foreach(string line in lines)
                    {
                       
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            string[] values = ParseCSVLine(line);
                            rows.Add(values);
                        }
                    }
                    
                    MessageBox.Show($"Đoc tep '{filePath}' thanh cong!", "THANH CONG");
                }
                else
                {
                    MessageBox.Show($"Tep '{filePath}' bi rong.", "LOI");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Da xay ra loi khi doc tep: {ex.Message}");
            }
            return rows;
        }

    }
}
    

