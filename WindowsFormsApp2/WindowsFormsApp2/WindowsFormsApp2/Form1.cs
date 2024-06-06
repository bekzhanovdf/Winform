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

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("asda");
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            var folderResult = folderBrowserDialog.ShowDialog();
            if (folderResult == DialogResult.OK)
            {
                string selectedFolder = folderBrowserDialog.SelectedPath;
                listBox1.Items.Clear();
                DirectoryInfo directoryInfo = new DirectoryInfo(selectedFolder);    
                foreach(DirectoryInfo directory in directoryInfo.GetDirectories())
                {
                    listBox1.Items.Add(directory.Name);
                }
                textBox1.Text = selectedFolder; // Показываем путь к выбранной папке
                PopulateDataGridView(selectedFolder); // Обновляем DataGridView содержимым выбранной папки
            }

        }

        private void PopulateDataGridView(string folderPath)
        {
            dataGridView1.Rows.Clear(); // Очищаем существующие строки в DataGridView

            // Получаем все файлы и папки в выбранной папке
            string[] allEntries = Directory.GetFileSystemEntries(folderPath);

            

            // Добавляем каждый файл или папку в DataGridView
            foreach (string entry in allEntries)
            {
                if (File.Exists(entry))
                {
                    string name = Path.GetFileName(entry);
                    FileInfo fileInfo = new FileInfo(entry);
                    long fileSizeInBytes = fileInfo.Length;
                    DateTime modification = fileInfo.LastWriteTime;

                    dataGridView1.Rows.Add(name, fileSizeInBytes, modification); // Добавляем данные в DataGridView
                }

                
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Проверяем, что нажата ячейка содержимого (Content)
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Получаем значение ячейки
                object cellValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                // Проверяем, что значение ячейки не пустое
                if (cellValue != null)
                {
                    // Получаем полный путь к файлу или папке
                    string fullPath = cellValue.ToString();

                    // Проверяем, является ли выбранный элемент папкой
                    if (Directory.Exists(fullPath))
                    {
                        // Если это папка, обновляем DataGridView содержимым папки
                        PopulateDataGridView(fullPath);
                    }
                    else if (File.Exists(fullPath))
                    {
                        // Если это файл, открываем его в стандартном приложении
                        System.Diagnostics.Process.Start(fullPath);
                    }
                }
            }
        }

        private async void button3_Click(object sender, EventArgs e)

        {
            Random random = new Random();
            Task[] na = new Task[dataGridView1.Rows.Count - 1];
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                int randomNumber = random.Next(1, dataGridView1.Rows.Count + 1);
                na[i] = Delay(randomNumber,i);
                
            }
            await Task.WhenAll(na);
        }
        private async Task Delay(int num,int i)
        {
            await Task.Delay(num*1000);
            dataGridView1.Rows[i].Cells["calc"].Value = num.ToString();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            DialogResult PAP = MessageBox.Show("Продублировать файл?", caption: "Dublicate?", buttons: MessageBoxButtons.YesNoCancel);
            if (PAP == DialogResult.Yes)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                DataGridViewRow newRow = (DataGridViewRow)selectedRow.Clone();

                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    newRow.Cells[cell.ColumnIndex].Value = cell.Value;
                }

                dataGridView1.Rows.Insert(selectedRow.Index + 1, newRow);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string path = listBox1.SelectedItem as string;

            Form2 nana = new Form2(Path.Combine(textBox1.Text,path));
            nana.ShowDialog();

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kabaev Timur SEST 1-22");
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            var folderResult = folderBrowserDialog.ShowDialog();
            if (folderResult == DialogResult.OK)
            {
                string selectedFolder = folderBrowserDialog.SelectedPath;
                listBox1.Items.Clear();
                DirectoryInfo directoryInfo = new DirectoryInfo(selectedFolder);
                foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                {
                    listBox1.Items.Add(directory.Name);
                }
                textBox1.Text = selectedFolder; // Показываем путь к выбранной папке
                PopulateDataGridView(selectedFolder); // Обновляем DataGridView содержимым выбранной папки
            }
        }
    }
}
