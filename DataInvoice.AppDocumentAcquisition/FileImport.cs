using DataInvoice.SOLUTIONS.MANAGEMENT.ORGANIZEDREPOSITORY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataInvoice.AppDocumentAcquisition
{
    public partial class FileImport : Form
    {
        OrganizedRepository drepository = null;
        public System.IO.FileInfo inputFile = null;
        public string userfilerename = null; // si renommer par l'utilisateur
        public Dictionary<string, string> CustomFormResults = null;

        private Dictionary<string, TextBox> TextBoxfields = new Dictionary<string, TextBox>();


        public FileImport(OrganizedRepository drepository, System.IO.FileInfo fi)
        {
            this.drepository = drepository;
            this.inputFile = fi;
            if(this.drepository==null) throw new Exception("Repository Empty");
            InitializeComponent();

            List<NGLib.DATA.DATAVALUES.DataValues_data> fields = this.drepository.GetCustomFileFields();
            if(fields!=null)
                foreach (NGLib.DATA.DATAVALUES.DataValues_data itemfield in fields)
                {
                    this.GenerateFieldArea(itemfield.NameMinimal,itemfield.NameMinimal,itemfield.ToString(true));
                }


            if (inputFile != null)
                this.textBox_filerename.Text = inputFile.Name;
            if(!string.IsNullOrWhiteSpace(drepository.FormatedFileName))
                this.textBox_filerename.ReadOnly =true;



        }



        public void GenerateFieldArea(string name, string description,string defaultvalue="")
        {

            System.Windows.Forms.Panel panel1 = new Panel();

            panel1.Location = new System.Drawing.Point(3, 3);
            //panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(465, 50);
            panel1.TabIndex = 0;
            

            System.Windows.Forms.Label label2 = new Label();
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(3, 9);
            //label2.Name = "label2";
            label2.Size = new System.Drawing.Size(46, 17);
            label2.TabIndex = 0;
            label2.Text = description;
            // 
            // textBox1
            // 
            System.Windows.Forms.TextBox textBox1 = new TextBox();
            textBox1.Location = new System.Drawing.Point(283, 6);
            textBox1.Name = "textBox" + name;
            textBox1.Size = new System.Drawing.Size(300, 40);
            textBox1.TabIndex = 1;
            textBox1.Text = defaultvalue;

            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label2);
            //panel1.ResumeLayout(false);
            //panel1.PerformLayout();

            textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.b_KeyDown);

            this.flowLayoutPanel1.Controls.Add(panel1);
            TextBoxfields.Add(name, textBox1);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            ValideForm();
        }


        private void b_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
                ValideForm();
        }



        private void ValideForm()
        {
            this.CustomFormResults = new Dictionary<string, string>();
            foreach (var itemkey in TextBoxfields.Keys)
            {
                string val = TextBoxfields[itemkey].Text;
                if (string.IsNullOrEmpty(val)) { MessageBox.Show("EmptyValues"); return; }
                this.CustomFormResults.Add(itemkey, val);
            }

            if (!this.textBox_filerename.ReadOnly)//; && this.textBox_filerename.Text!= )
                this.userfilerename = this.textBox_filerename.Text;

            this.Close();
        }










    }
}
