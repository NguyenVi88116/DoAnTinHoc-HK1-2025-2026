using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
namespace DocGhiFileCSV
{
    internal class AVLTree
    {
        public Node Root;

        private int Height(Node n) => n == null ? 0 : n.Height;
        private int Balance(Node n) => n == null ? 0 : Height(n.Left) - Height(n.Right);

        private Node RotateRight(Node y)
        {
            Node x = y.Left;
            Node T2 = x.Right;
            x.Right = y;
            y.Left = T2;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            return x;
        }

        private Node RotateLeft(Node x)
        {
            Node y = x.Right;
            Node T2 = y.Left;
            y.Left = x;
            x.Right = T2;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            return y;
        }

        public void Insert(string key, string[] data)
        {
            Root = InsertNode(Root, key, data);
        }

        private Node InsertNode(Node node,string key, string[] data)
        {
            if (node == null)
                return new Node(key, data);
            int compareResult= string.Compare(key,node.Key,StringComparison.Ordinal);
            if (compareResult < 0)
            {
                node.Left = InsertNode(node.Left, key, data);
            }
            else if(compareResult > 0)
            {
                    node.Right = InsertNode(node.Right, key, data);
            }
            else
                return node;
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;

            int balance = Balance(node);
            //logic xoay
            if (balance > 1 && string.Compare(key, node.Left.Key, StringComparison.Ordinal) < 0)
                return RotateRight(node);
            if (balance < -1 && string.Compare(key, node.Right.Key, StringComparison.Ordinal) > 0)
                return RotateLeft(node);
            if (balance > 1 && string.Compare(key, node.Left.Key, StringComparison.Ordinal) > 0)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
            if (balance < -1 && string.Compare(key, node.Right.Key, StringComparison.Ordinal) < 0)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }
            return node;
        }

        // ======= Thống kê =======
        public int CountTotalNodes() => CountNodes(Root);
        private int CountNodes(Node node)
        {
            if (node == null) return 0;
            return 1 + CountNodes(node.Left) + CountNodes(node.Right);
        }

        public int CountLeafNodes() => CountLeaf(Root);
        private int CountLeaf(Node node)
        {
            if (node == null) return 0;
            if (node.Left == null && node.Right == null) return 1;
            return CountLeaf(node.Left) + CountLeaf(node.Right);
        }

        public int CountNodesInLeftSubtree() => Root?.Left == null ? 0 : CountNodes(Root.Left);
        public int CountNodesInRightSubtree() => Root?.Right == null ? 0 : CountNodes(Root.Right);
        public Node Search(string key)
        {
            return SearchNode(Root, key);
        }

        private Node SearchNode(Node node, string key)
        {
            if (node == null)
                return null;

            int compareResult = string.Compare(key, node.Key, StringComparison.Ordinal);

            if (compareResult < 0)
            {
                return SearchNode(node.Left, key);
            }
            else if (compareResult > 0)
            {
                return SearchNode(node.Right, key);
            }
            else
            {
                return node;
            }
        }
        public void ExportToJson(string filePath)
        {
            if(Root == null)
            {
                throw new InvalidOperationException("Cây AVL rỗng, không thể xuất JSON");
            }
            List<JsonData> treeData = new List<JsonData>();
            PreOrderTraversalForJson(Root,treeData);
            string jsonString = JsonConvert.SerializeObject(treeData, Formatting.Indented);
            File.WriteAllText(filePath, jsonString);
        }
        private void PreOrderTraversalForJson(Node node, List<JsonData> list)
        {
            if (node != null)
            {
                // Ánh xạ Node AVL sang JsonNodeData
                var jsonNode = new JsonData
                {
                    Key = node.Key,
                    Data = node.Data,
                    Height = node.Height,
                    LeftKey = node.Left?.Key,   // Lấy Key của con trái
                    RightKey = node.Right?.Key  // Lấy Key của con phải
                };
                list.Add(jsonNode);

                PreOrderTraversalForJson(node.Left, list);
                PreOrderTraversalForJson(node.Right, list);
            }
        }
    }
}
