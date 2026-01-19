using System.Windows.Forms;

namespace client
{
    partial class EditServiceForm
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtType;
        private TextBox txtTitle;
        private TextBox txtGovStructure;
        private TextBox txtDuration;
        private TextBox txtDocuments;
        private Button btnConfirm;
        private Button btnCancel;
        private Button btnReset;
        private Label lblType;
        private Label lblTitle;
        private Label lblGovStructure;
        private Label lblDuration;
        private Label lblDocuments;
        private ToolTip toolTip1;

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
            this.components = new System.ComponentModel.Container();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtGovStructure = new System.Windows.Forms.TextBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.txtDocuments = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblType = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblGovStructure = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblDocuments = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();

            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(19, 21);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(65, 13);
            this.lblType.TabIndex = 11;
            this.lblType.Text = "Тип услуги:";
            
            this.txtType.Location = new System.Drawing.Point(167, 17);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(200, 20);
            this.txtType.TabIndex = 0;

            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(19, 51);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(122, 13);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Наименование услуги:";
            
            this.txtTitle.Location = new System.Drawing.Point(167, 47);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(200, 20);
            this.txtTitle.TabIndex = 1;

            this.lblGovStructure.AutoSize = true;
            this.lblGovStructure.Location = new System.Drawing.Point(19, 81);
            this.lblGovStructure.Name = "lblGovStructure";
            this.lblGovStructure.Size = new System.Drawing.Size(145, 13);
            this.lblGovStructure.TabIndex = 9;
            this.lblGovStructure.Text = "Ответственное ведомство:";
            
            this.txtGovStructure.Location = new System.Drawing.Point(167, 77);
            this.txtGovStructure.Name = "txtGovStructure";
            this.txtGovStructure.Size = new System.Drawing.Size(200, 20);
            this.txtGovStructure.TabIndex = 2;

            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(19, 111);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(98, 13);
            this.lblDuration.TabIndex = 8;
            this.lblDuration.Text = "Срок исполнения:";
            
            this.txtDuration.Location = new System.Drawing.Point(167, 107);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(200, 20);
            this.txtDuration.TabIndex = 3;
            this.txtDuration.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDuration_KeyPress);

            this.lblDocuments.AutoSize = true;
            this.lblDocuments.Location = new System.Drawing.Point(19, 141);
            this.lblDocuments.Name = "lblDocuments";
            this.lblDocuments.Size = new System.Drawing.Size(122, 26);
            this.lblDocuments.TabIndex = 7;
            this.lblDocuments.Text = "Перечень документов:\r\n(через запятую)";
            
            this.txtDocuments.Location = new System.Drawing.Point(167, 137);
            this.txtDocuments.Multiline = true;
            this.txtDocuments.Name = "txtDocuments";
            this.txtDocuments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDocuments.Size = new System.Drawing.Size(200, 60);
            this.txtDocuments.TabIndex = 4;
            this.txtDocuments.MouseEnter += new System.EventHandler(this.txtDocuments_MouseEnter);

            this.btnConfirm.Location = new System.Drawing.Point(76, 219);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(90, 30);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "Сохранить";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.ButtonConfirm_Click);

            this.btnCancel.Location = new System.Drawing.Point(176, 219);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.ButtonCancel_Click);

            this.btnReset.Location = new System.Drawing.Point(276, 219);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(90, 30);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "Сбросить";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.ButtonReset_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 261);
            this.Controls.Add(this.btnReset);
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
            this.Name = "EditServiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактировать услугу";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}