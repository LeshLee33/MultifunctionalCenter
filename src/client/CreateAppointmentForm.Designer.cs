using System.Windows.Forms;

namespace client
{
    partial class CreateAppointmentForm
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtServiceTitle;
        private TextBox txtPassportSeries;
        private TextBox txtPassportNumber;
        private TextBox txtSurname;
        private TextBox txtName;
        private TextBox txtPatronymic;
        private TextBox txtNationality;
        private TextBox txtDate;
        private TextBox txtTime;
        private Button btnCreate;
        private Button btnCancel;
        private Label lblServiceTitle;
        private Label lblPassportSeries;
        private Label lblPassportNumber;
        private Label lblSurname;
        private Label lblName;
        private Label lblPatronymic;
        private Label lblNationality;
        private Label lblDate;
        private Label lblTime;

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
            this.txtServiceTitle = new System.Windows.Forms.TextBox();
            this.txtPassportSeries = new System.Windows.Forms.TextBox();
            this.txtPassportNumber = new System.Windows.Forms.TextBox();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPatronymic = new System.Windows.Forms.TextBox();
            this.txtNationality = new System.Windows.Forms.TextBox();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblServiceTitle = new System.Windows.Forms.Label();
            this.lblPassportSeries = new System.Windows.Forms.Label();
            this.lblPassportNumber = new System.Windows.Forms.Label();
            this.lblSurname = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPatronymic = new System.Windows.Forms.Label();
            this.lblNationality = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.lblServiceTitle.AutoSize = true;
            this.lblServiceTitle.Location = new System.Drawing.Point(19, 21);
            this.lblServiceTitle.Name = "lblServiceTitle";
            this.lblServiceTitle.Size = new System.Drawing.Size(122, 13);
            this.lblServiceTitle.TabIndex = 0;
            this.lblServiceTitle.Text = "Наименование услуги:";
            
            this.txtServiceTitle.Location = new System.Drawing.Point(167, 17);
            this.txtServiceTitle.Name = "txtServiceTitle";
            this.txtServiceTitle.Size = new System.Drawing.Size(250, 20);
            this.txtServiceTitle.TabIndex = 0;

            this.lblPassportSeries.AutoSize = true;
            this.lblPassportSeries.Location = new System.Drawing.Point(19, 51);
            this.lblPassportSeries.Name = "lblPassportSeries";
            this.lblPassportSeries.Size = new System.Drawing.Size(91, 13);
            this.lblPassportSeries.TabIndex = 1;
            this.lblPassportSeries.Text = "Серия паспорта:";
            
            this.txtPassportSeries.Location = new System.Drawing.Point(167, 47);
            this.txtPassportSeries.MaxLength = 6;
            this.txtPassportSeries.Name = "txtPassportSeries";
            this.txtPassportSeries.Size = new System.Drawing.Size(100, 20);
            this.txtPassportSeries.TabIndex = 1;
            this.txtPassportSeries.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassportSeries_KeyPress);

            this.lblPassportNumber.AutoSize = true;
            this.lblPassportNumber.Location = new System.Drawing.Point(19, 81);
            this.lblPassportNumber.Name = "lblPassportNumber";
            this.lblPassportNumber.Size = new System.Drawing.Size(94, 13);
            this.lblPassportNumber.TabIndex = 2;
            this.lblPassportNumber.Text = "Номер паспорта:";
            
            this.txtPassportNumber.Location = new System.Drawing.Point(167, 77);
            this.txtPassportNumber.MaxLength = 12;
            this.txtPassportNumber.Name = "txtPassportNumber";
            this.txtPassportNumber.Size = new System.Drawing.Size(100, 20);
            this.txtPassportNumber.TabIndex = 2;
            this.txtPassportNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassportNumber_KeyPress);

            this.lblSurname.AutoSize = true;
            this.lblSurname.Location = new System.Drawing.Point(19, 111);
            this.lblSurname.Name = "lblSurname";
            this.lblSurname.Size = new System.Drawing.Size(103, 13);
            this.lblSurname.TabIndex = 3;
            this.lblSurname.Text = "Фамилия клиента:";
            
            this.txtSurname.Location = new System.Drawing.Point(167, 107);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(250, 20);
            this.txtSurname.TabIndex = 3;

            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(19, 141);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(76, 13);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Имя клиента:";
            
            this.txtName.Location = new System.Drawing.Point(167, 137);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(250, 20);
            this.txtName.TabIndex = 4;

            this.lblPatronymic.AutoSize = true;
            this.lblPatronymic.Location = new System.Drawing.Point(19, 171);
            this.lblPatronymic.Name = "lblPatronymic";
            this.lblPatronymic.Size = new System.Drawing.Size(101, 13);
            this.lblPatronymic.TabIndex = 5;
            this.lblPatronymic.Text = "Отчество клиента:";
            
            this.txtPatronymic.Location = new System.Drawing.Point(167, 167);
            this.txtPatronymic.Name = "txtPatronymic";
            this.txtPatronymic.Size = new System.Drawing.Size(250, 20);
            this.txtPatronymic.TabIndex = 5;

            this.lblNationality.AutoSize = true;
            this.lblNationality.Location = new System.Drawing.Point(19, 201);
            this.lblNationality.Name = "lblNationality";
            this.lblNationality.Size = new System.Drawing.Size(77, 13);
            this.lblNationality.TabIndex = 6;
            this.lblNationality.Text = "Гражданство:";
            
            this.txtNationality.Location = new System.Drawing.Point(167, 197);
            this.txtNationality.Name = "txtNationality";
            this.txtNationality.Size = new System.Drawing.Size(250, 20);
            this.txtNationality.TabIndex = 6;

            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(19, 231);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(75, 13);
            this.lblDate.TabIndex = 7;
            this.lblDate.Text = "Дата записи:";
            
            this.txtDate.Location = new System.Drawing.Point(167, 227);
            this.txtDate.MaxLength = 10;
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(100, 20);
            this.txtDate.TabIndex = 7;
            this.txtDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDate_KeyPress);
            this.txtDate.TextChanged += new System.EventHandler(this.txtDate_TextChanged);

            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(19, 261);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(82, 13);
            this.lblTime.TabIndex = 8;
            this.lblTime.Text = "Время записи:";
            
            this.txtTime.Location = new System.Drawing.Point(167, 257);
            this.txtTime.MaxLength = 5;
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(100, 20);
            this.txtTime.TabIndex = 8;
            this.txtTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTime_KeyPress);
            this.txtTime.TextChanged += new System.EventHandler(this.txtTime_TextChanged);

            this.btnCreate.Location = new System.Drawing.Point(96, 299);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(120, 30);
            this.btnCreate.TabIndex = 9;
            this.btnCreate.Text = "Создать запись";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);

            this.btnCancel.Location = new System.Drawing.Point(236, 299);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 341);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.txtNationality);
            this.Controls.Add(this.txtPatronymic);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtSurname);
            this.Controls.Add(this.txtPassportNumber);
            this.Controls.Add(this.txtPassportSeries);
            this.Controls.Add(this.txtServiceTitle);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblNationality);
            this.Controls.Add(this.lblPatronymic);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblSurname);
            this.Controls.Add(this.lblPassportNumber);
            this.Controls.Add(this.lblPassportSeries);
            this.Controls.Add(this.lblServiceTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateAppointmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Создать запись на услугу";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}