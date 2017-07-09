namespace DataInvoice.AppDocumentAcquisition
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_loaddocument = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel_loaddocument = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(511, 546);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(258, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Scanner un document (1 page)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DataInvoice.AppDocumentAcquisition.Properties.Resources.datainvoice;
            this.pictureBox1.Location = new System.Drawing.Point(32, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(270, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(690, 21);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(92, 17);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Accès au site";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Liste des répertoires";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(32, 195);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(327, 384);
            this.treeView1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(508, 242);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Déposer un document";
            // 
            // btn_loaddocument
            // 
            this.btn_loaddocument.AllowDrop = true;
            this.btn_loaddocument.Location = new System.Drawing.Point(511, 284);
            this.btn_loaddocument.Name = "btn_loaddocument";
            this.btn_loaddocument.Size = new System.Drawing.Size(258, 53);
            this.btn_loaddocument.TabIndex = 7;
            this.btn_loaddocument.Text = "Charger un document";
            this.btn_loaddocument.UseVisualStyleBackColor = true;
            this.btn_loaddocument.Click += new System.EventHandler(this.button2_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 601);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(794, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // panel_loaddocument
            // 
            this.panel_loaddocument.AllowDrop = true;
            this.panel_loaddocument.BackColor = System.Drawing.Color.PaleTurquoise;
            this.panel_loaddocument.Location = new System.Drawing.Point(511, 343);
            this.panel_loaddocument.Name = "panel_loaddocument";
            this.panel_loaddocument.Size = new System.Drawing.Size(258, 197);
            this.panel_loaddocument.TabIndex = 5;
            this.panel_loaddocument.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel1_DragDrop);
            this.panel_loaddocument.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel1_DragEnter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 623);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btn_loaddocument);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel_loaddocument);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "DataInvoice Document Acquisition";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_loaddocument;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel_loaddocument;
    }
}

