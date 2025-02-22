using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace XML_Files
{
    public partial class Form1 : Form
    {
        private DataSet dataSet;

        public Form1()
        {
            InitializeComponent();
            dataSet = new DataSet();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files|*.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dataSet.ReadXml(openFileDialog.FileName);
                    dataGridView1.DataSource = dataSet.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading the XML file: " + ex.Message);

                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files|*.xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dataSet.WriteXml(saveFileDialog.FileName);
                    MessageBox.Show("File saved successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving the XML file: " + ex.Message);
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string valorBuscar = txtSearch.Text.Trim();

            if (dataSet.Tables.Count > 0)
            {
                if (string.IsNullOrEmpty(valorBuscar))
                {
                    dataGridView1.DataSource = dataSet.Tables[0];
                }
                else
                {
                    var resultados = dataSet.Tables[0].AsEnumerable()
                        .Where(row => row.ItemArray.Any(field => field.ToString().Contains(valorBuscar)))
                        .ToList();

                    if (resultados.Any())
                    {
                        DataTable dtResultados = resultados.CopyToDataTable();
                        dataGridView1.DataSource = dtResultados;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please load a file first.");
            }
        }
    }
}