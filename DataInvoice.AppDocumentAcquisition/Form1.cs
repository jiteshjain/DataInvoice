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
    public partial class Form1 : Form
    {
        DataInvoice.GLOBAL.DataInvoiceEnv env = null;
        public DataInvoice.SOLUTIONS.MANAGEMENT.ORGANIZEDREPOSITORY.OrganizedRepositoryManager   DocRepositoryManager = null;
        public DataInvoice.SOLUTIONS.MANAGEMENT.ORGANIZEDREPOSITORY.OrganizedRepositoryProvider repositoryProvider = null;
        public DataInvoice.SOLUTIONS.MANAGEMENT.ORGANIZEDREPOSITORY.OrganisedRepositoryResults temp_drepos = null;


        public TreeNode ConvertDRepository(OrganizedRepository drepository)
        {
            TreeNode retpur = new TreeNode();
            retpur.Name = drepository.IDRepository.ToString();
            retpur.Text = drepository.LabelRepository;
            return retpur;
        }



        public void SetNodes()
        {

            temp_drepos = repositoryProvider.GetRepositorys(1);
            foreach (var item in temp_drepos)
            {
                TreeNode treenode = ConvertDRepository(item);
                this.treeView1.Nodes.Add(treenode);
            }
            
        }

        public Form1()
        {
            InitializeComponent();
            env = new GLOBAL.DataInvoiceEnv();
            DocRepositoryManager = new SOLUTIONS.MANAGEMENT.ORGANIZEDREPOSITORY.OrganizedRepositoryManager();
            repositoryProvider = new SOLUTIONS.MANAGEMENT.ORGANIZEDREPOSITORY.OrganizedRepositoryProvider(this.env.Connector);
            SetNodes();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.treeView1.SelectedNode == null) { MessageBox.Show("Vous devez d'abord choisir le type de fichier"); return; }
           
                ACQUIRE.AcquireManager scanManag = new ACQUIRE.AcquireManager();
                scanManag.PrepareScan();
                string filepath = scanManag.Scan();
                System.IO.FileInfo fi = new System.IO.FileInfo(filepath);
                GoCopieFile(fi);
            }
            catch (Exception)
            {
                
                throw;
            }


        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode == null) { MessageBox.Show("Vous devez d'abord choisir le type de fichier"); return; }
           
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

            OpenFileDialog filedial = (OpenFileDialog)sender;



           foreach (var item in filedial.FileNames)
           {
               Console.WriteLine("FILE " + item);
               System.IO.FileInfo fi = new System.IO.FileInfo(item);
               this.GoCopieFile(fi);

           }
               

        }



        public void GoCopieFile(System.IO.FileInfo fi) //params
        {


            OrganizedRepository drepository = GetSelectedDRepository();
            if (drepository == null) throw new Exception("repository null");

            try
            {
                FileImport importdialg = new FileImport(drepository, fi);
                //importdialg.inputFile = fi;
                importdialg.ShowDialog();
                Dictionary<string, string> CustomFieldsFile = importdialg.CustomFormResults;
                string userfilerename = importdialg.userfilerename;
                importdialg.Close();
                importdialg.Dispose();
                Console.WriteLine("user eselct");


                this.DocRepositoryManager.CopyFile(drepository, fi, CustomFieldsFile, userfilerename);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR "+ex.Message);
            }

        }



        public OrganizedRepository GetSelectedDRepository()
        {
            if(this.treeView1.SelectedNode==null) return null;
            string nodselect = this.treeView1.SelectedNode.Name;
            if(string.IsNullOrWhiteSpace(nodselect)) return null;
            int IDrepository = Convert.ToInt32(nodselect);
            OrganizedRepository retour = this.temp_drepos.GetRepository(IDrepository);
            return retour;
        }





        private void panel_loaddocument_DragEnter(object sender, DragEventArgs e)
        {
            
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            if (this.treeView1.SelectedNode == null) { MessageBox.Show("Vous devez d'abord choisir le type de fichier"); return; }
           
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var item in files)
            {
                Console.WriteLine("FILE " + item);
                System.IO.FileInfo fi = new System.IO.FileInfo(item);
                this.GoCopieFile(fi);

            }
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }




    }
}
