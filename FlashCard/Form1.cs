using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel; 

namespace FlashCard
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
            label1.Text = Environment.CurrentDirectory;
            txtFrom.KeyPress += new KeyPressEventHandler(txtFrom_KeyPress);
            txtTo.KeyPress += new KeyPressEventHandler(txtTo_KeyPress);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = openFileDialog1.FileName;
            }
        }
        private void txtFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b') //The  character represents a backspace
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }
        private void txtTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b') //The  character represents a backspace
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void btnGenerateCards_Click(object sender, EventArgs e)
        {
            if (txtFrom.Text == "" || txtTo.Text == "")
            {
                lblMessage.Text = "To and From boxes must be filled out.";
            }
            else if (Convert.ToInt32(txtFrom.Text) > Convert.ToInt32(txtTo.Text))
            {
                lblMessage.Text = "The From box cannot be higher than the To box.";
            }
            else if (txtFile.Text == "")
            {
                lblMessage.Text = "Please select an excel file.";
            }
            else if (Convert.ToInt32(txtFrom.Text) < 1)
            {
                lblMessage.Text = "The From box cannot be lower than 1";
            }
            else
            {
                lblMessage.Text = "";

                //Create COM Objects. Create a COM object for everything that is referenced
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(txtFile.Text);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count;
                int colCount = 2;
                List<String> rawData = new List<string>();
                //iterate over the rows and columns and print to the console as it appears in the file
                //excel is not zero based!!
                for (int i = 1; i <= rowCount; i++)
                {
                    for (int j = 1; j <= colCount; j++)
                    {
                        

                        //write the value to the console
                        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                            rawData.Add(xlRange.Cells[i, j].Value2.ToString());
                    }
                }

                //cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //rule of thumb for releasing com objects:
                //  never use two dots, all COM objects must be referenced and released individually
                //  ex: [somthing].[something].[something] is bad

                //release com objects to fully kill excel process from running in the background
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);

                //close and release
                xlWorkbook.Close();
                Marshal.ReleaseComObject(xlWorkbook);

                //quit and release
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
                int englishCount = 0;
                int greekCount = 1;
                
                int rawNumber = (rawData.Count / 2);
                List<Card> cardList = new List<Card>();
                for (int i = 1; i <= rawNumber; i++ )
                {
                    try
                    {
                        
                        cardList.Add(new Card(i, rawData[englishCount], rawData[greekCount]));
                        
                        englishCount = englishCount + 2;
                        greekCount = greekCount + 2;
                    }
                    catch (Exception ee)
                    {
                    }
                }
                if (Convert.ToInt32(txtTo.Text) > cardList.Count)
                {
                    lblMessage.Text = "The To box cannot be higher than the number of rows in the excel file.";
                }
                else
                {
                    //new form
                    Form2 f = new Form2(cardList, Convert.ToInt32(txtFrom.Text), Convert.ToInt32(txtTo.Text));
                    f.Show();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
