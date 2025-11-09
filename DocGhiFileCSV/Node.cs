using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocGhiFileCSV
{
    public class Node
    {
        public string Key;
        public string[] Data;
        public Node Left, Right;
        public int Height;
        public Node(string key, string[] data)
        {
            Key = key;
            Data = data;
            Height = 1;
        }
    }
}
