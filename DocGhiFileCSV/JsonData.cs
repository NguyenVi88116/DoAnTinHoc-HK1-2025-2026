using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGhiFileCSV
{
    public class JsonData
    {
        public string Key {  get; set; }
        public string[] Data { get; set; }
        public string LeftKey {  get; set; }
        public string RightKey { get; set; }
        public int Height { get; set; }
    }
}
