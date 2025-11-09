using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace DocGhiFileCSV
{
    public partial class Form1 : Form
    {
        private AVLTree hotelDataTree;
        private string[] csvHeaders;
        public Form1()
        {
            InitializeComponent();
            hotelDataTree = new AVLTree();
        }

        private void btnread_Click(object sender, EventArgs e)
        {
            string csvFilePath = "Hotel Reservations.csv";
            List<string[]> csvData = ReadCSV.ReadCsvFile(csvFilePath);

            if (csvData.Count < 1)
            {
                MessageBox.Show("Khong có du lieu trong tep CSV.");
                return;
            }

            DataTable dataTable = new DataTable();

            string[] headers = csvData[0];
            csvHeaders = headers;

            foreach (string header in headers)
            {
                dataTable.Columns.Add(header);
            }

            for (int i = 1; i < csvData.Count; i++)
            {
                dataTable.Rows.Add(csvData[i]);
            }

            dataGridView2.DataSource = dataTable;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string csvFilePath = "Hotel Reservations.csv";
            LoadDataFromCSV(csvFilePath);
            PopulateTreeView();
        }
        private void LoadDataFromCSV(string csvFilePath)
        {
            hotelDataTree = new AVLTree();
            try
            {
                var lines = File.ReadAllLines(csvFilePath).Skip(1);
                foreach (string line in lines)
                {
                    if (string.IsNullOrEmpty(line)) continue;
                    string[] fields = line.Split(',');
                    try
                    { 
                        string key=fields[0];
                        hotelDataTree.Insert(key, fields);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi đọc dòng: {line}\nLỗi: {ex.Message}");
                    }
                }
                MessageBox.Show("Tải dữ liệu và tạo cây AVL thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể đọc file CSV: {ex.Message}", "Lỗi Đọc File");
            }

        }
        private TreeNode BuildTreeViewNode(Node avlNode)
        {
            if (avlNode == null)
            {
                return null;
            }
            string displayText = $"ID: {avlNode.Data[0]} (Giá: {avlNode.Data[16]})";
            TreeNode treeViewNode = new TreeNode(displayText);//chuyen giao dien 
            TreeNode leftChild = BuildTreeViewNode(avlNode.Left);
            if (leftChild != null)
            {
                leftChild.Text = "[L] " + leftChild.Text;
                treeViewNode.Nodes.Add(leftChild);
            }
            TreeNode rightChild = BuildTreeViewNode(avlNode.Right);
            if (rightChild != null)
            {
                rightChild.Text = "[R] " + rightChild.Text;
                treeViewNode.Nodes.Add(rightChild);
            }
            return treeViewNode;
        }
        private void PopulateTreeView()
        {
            treeView1.Nodes.Clear();

            Node avlRoot = hotelDataTree.Root;
            int nodeCount=hotelDataTree.CountTotalNodes();

            if (avlRoot != null&&nodeCount>0)
            {
                TreeNode treeViewRoot = BuildTreeViewNode(avlRoot);
                if (treeViewRoot != null)
                {
                    treeView1.Nodes.Add(treeViewRoot);
                    //treeView1.ExpandAll(); mo rong cac nut
                    MessageBox.Show($"Cây AVL có {nodeCount} nút. Đã thêm nút gốc vào TreeView.", "Kiểm tra thành công");
                }
                else
                {
                    // Lỗi BuildTreeViewNode
                    MessageBox.Show("LỖI: Hàm BuildTreeViewNode trả về null (Nút gốc không được tạo).", "Kiểm tra Lỗi");
                }
            }
            else
            {
                MessageBox.Show("Cây rỗng, không có gì để hiển thị.", "Thông báo");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchKey = nhapID.Text.Trim(); // Lấy ID cần tìm từ TextBox nhapID

            if (string.IsNullOrEmpty(searchKey))
            {
                MessageBox.Show("Vui lòng nhập ID để tìm kiếm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Node foundNode = hotelDataTree.Search(searchKey);

            if (foundNode != null)
            {
                DisplayNodeDetails(foundNode);
                MessageBox.Show($"Tìm thấy ID: {searchKey}. Kết quả đã được hiển thị trên bảng dữ liệu.", "Tìm kiếm thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Xóa dữ liệu cũ và hiển thị thông báo
                dataGridView2.DataSource = null;
                MessageBox.Show($"Không tìm thấy dữ liệu đặt phòng với ID: {searchKey}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void DisplayNodeDetails(Node node)
        {
            DataTable resultTable = new DataTable("SearchResult");
            string[] headersToUse;
            if (csvHeaders != null && csvHeaders.Length == node.Data.Length)
            {
                headersToUse = csvHeaders;
            }
            else
                headersToUse = Enumerable.Range(0, node.Data.Length).Select(i => $"Col_{i + 1}").ToArray();
            foreach (string header in headersToUse)
            {
                // Kiểm tra trùng tên để tránh lỗi
                if (!resultTable.Columns.Contains(header))
                {
                    resultTable.Columns.Add(header);
                }
                else
                {
                    resultTable.Columns.Add($"{header}_{resultTable.Columns.Count}");
                }
            }

            // 3. Thêm hàng dữ liệu và gán cho DataGridView
            resultTable.Rows.Add(node.Data);
            dataGridView2.DataSource = resultTable;
        }

        private void btnJson_Click(object sender, EventArgs e)
        {
            if (hotelDataTree.Root == null)
            {
                MessageBox.Show("Cây AVL chưa được tải! Vui lòng nhấn nút Load trước.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Chọn đường dẫn và tên file để lưu
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";
            saveFileDialog.DefaultExt = "json";
            saveFileDialog.FileName = "hotel_reservations_avl_export.json";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 3. Gọi hàm xuất JSON từ AVLTree
                    hotelDataTree.ExportToJson(saveFileDialog.FileName);

                    MessageBox.Show($"Xuất Cây AVL thành công ra file: {saveFileDialog.FileName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xuất file JSON: {ex.Message}", "Lỗi Xuất File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
    

