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
    public partial class Form2 : Form
    {
        public Form2(string Path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Path);

            InitializeComponent();
            label1.Text = directoryInfo.Name;
            label2.Text = directoryInfo.LastWriteTime.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
