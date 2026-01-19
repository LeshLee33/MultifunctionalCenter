using System.Windows.Forms;

namespace client
{
    partial class AddServiceForm
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtType;
        private TextBox txtTitle;
        private TextBox txtGovStructure;
        private TextBox txtDuration;
        private TextBox txtDocuments;
        private Button btnConfirm;
        private Button btnCancel;
        private Label lblType;
        private Label lblTitle;
        private Label lblGovStructure;
        private Label lblDuration;
        private Label lblDocuments;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtGovStructure = new System.Windows.Forms.TextBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.txtDocuments = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblType = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblGovStructure = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblDocuments = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.txtType.Location = new System.Drawing.Point(167, 17);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(200, 20);
            this.txtType.TabIndex = 0;
            
            this.txtTitle.Location = new System.Drawing.Point(167, 47);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(200, 20);
            this.txtTitle.TabIndex = 1;
           
            this.txtGovStructure.Location = new System.Drawing.Point(167, 77);
            this.txtGovStructure.Name = "txtGovStructure";
            this.txtGovStructure.Size = new System.Drawing.Size(200, 20);
            this.txtGovStructure.TabIndex = 2;
            
            this.txtDuration.Location = new System.Drawing.Point(167, 107);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(200, 20);
            this.txtDuration.TabIndex = 3;
            
            this.txtDocuments.Location = new System.Drawing.Point(167, 137);
            this.txtDocuments.Multiline = true;
            this.txtDocuments.Name = "txtDocuments";
            this.txtDocuments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDocuments.Size = new System.Drawing.Size(200, 60);
            this.txtDocuments.TabIndex = 4;
           
            this.btnConfirm.Location = new System.Drawing.Point(96, 219);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(90, 30);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "Подтвердить";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.ButtonConfirm_Click);
            
            this.btnCancel.Location = new System.Drawing.Point(196, 219);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Отменить";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(19, 21);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(65, 13);
            this.lblType.TabIndex = 11;
            this.lblType.Text = "Тип услуги:";
            
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(19, 51);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(122, 13);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Наименование услуги:";
           
            this.lblGovStructure.AutoSize = true;
            this.lblGovStructure.Location = new System.Drawing.Point(19, 81);
            this.lblGovStructure.Name = "lblGovStructure";
            this.lblGovStructure.Size = new System.Drawing.Size(145, 13);
            this.lblGovStructure.TabIndex = 9;
            this.lblGovStructure.Text = "Ответственное ведомство:";
            
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(19, 111);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(98, 13);
            this.lblDuration.TabIndex = 8;
            this.lblDuration.Text = "Срок исполнения:";
           
            this.lblDocuments.AutoSize = true;
            this.lblDocuments.Location = new System.Drawing.Point(19, 141);
            this.lblDocuments.Name = "lblDocuments";
            this.lblDocuments.Size = new System.Drawing.Size(122, 26);
            this.lblDocuments.TabIndex = 7;
            this.lblDocuments.Text = "Перечень документов:\r\n(через запятую)";
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 261);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtDocuments);
            this.Controls.Add(this.txtDuration);
            this.Controls.Add(this.txtGovStructure);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.lblDocuments);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.lblGovStructure);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddServiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавить услугу";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}