namespace client
{
    partial class MainForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MainFormTabControl = new System.Windows.Forms.TabControl();
            this.ServicesPage = new System.Windows.Forms.TabPage();
            this.RefreshServicesButton = new System.Windows.Forms.Button();
            this.ServicePropertiesPanel = new System.Windows.Forms.Panel();
            this.DocumentsListLabel = new System.Windows.Forms.TextBox();
            this.ServiceTimeDurationLabel = new System.Windows.Forms.Label();
            this.GovernmentStructureLabel = new System.Windows.Forms.Label();
            this.ServiceTitleLabel = new System.Windows.Forms.Label();
            this.ServiceTypeLabel = new System.Windows.Forms.Label();
            this.ServiceTimeDurationLiner = new System.Windows.Forms.Label();
            this.GovernmentStructureLiner = new System.Windows.Forms.Label();
            this.DocumentsListLiner = new System.Windows.Forms.Label();
            this.ServiceTitleLiner = new System.Windows.Forms.Label();
            this.ServiceTypeLiner = new System.Windows.Forms.Label();
            this.EditServiceButton = new System.Windows.Forms.Button();
            this.CreateAppointmentButton = new System.Windows.Forms.Button();
            this.SearchServicesTextBox = new System.Windows.Forms.TextBox();
            this.AddServiceButton = new System.Windows.Forms.Button();
            this.SearchServicesPanel = new System.Windows.Forms.Panel();
            this.AppointmentsPage = new System.Windows.Forms.TabPage();
            this.RefreshAppointmentsButton = new System.Windows.Forms.Button();
            this.AppointmentPropertiesPanel = new System.Windows.Forms.Panel();
            this.AppointmentDaytimeLabel = new System.Windows.Forms.Label();
            this.AppointmentDaytimeLiner = new System.Windows.Forms.Label();
            this.AppointmentDateLabel = new System.Windows.Forms.Label();
            this.AppointmentNationalityLabel = new System.Windows.Forms.Label();
            this.AppointmentClientNationalityLiner = new System.Windows.Forms.Label();
            this.AppointmentDateLiner = new System.Windows.Forms.Label();
            this.AppointmentClientPatronymicLabel = new System.Windows.Forms.Label();
            this.AppointmentClientPatronymicLiner = new System.Windows.Forms.Label();
            this.AppointmentClientNameLabel = new System.Windows.Forms.Label();
            this.AppointmentClientSurnameLabel = new System.Windows.Forms.Label();
            this.PassportNumberLabel = new System.Windows.Forms.Label();
            this.PassportSeriesLabel = new System.Windows.Forms.Label();
            this.AppointmentServiceTitleLabel = new System.Windows.Forms.Label();
            this.AppointmentClientSurnameLiner = new System.Windows.Forms.Label();
            this.PassportNumberLiner = new System.Windows.Forms.Label();
            this.AppointmentClientNameLiner = new System.Windows.Forms.Label();
            this.PassportSeriesLiner = new System.Windows.Forms.Label();
            this.AppointmentServiceTitleLiner = new System.Windows.Forms.Label();
            this.EditAppointmentButton = new System.Windows.Forms.Button();
            this.DeleteAppointmentButton = new System.Windows.Forms.Button();
            this.SearchAppointmentsTextBox = new System.Windows.Forms.TextBox();
            this.SearchAppointmentsPanel = new System.Windows.Forms.Panel();
            this.ServedAppointmentsPage = new System.Windows.Forms.TabPage();
            this.RefreshAppointmentsHistoryButton = new System.Windows.Forms.Button();
            this.SearchAppointmentsHistoryPanel = new System.Windows.Forms.Panel();
            this.SearchAppointmentsHistoryTextBox = new System.Windows.Forms.TextBox();
            this.UpdateAppointmentsHistoryTimer = new System.Windows.Forms.Timer(this.components);
            this.MainFormTabControl.SuspendLayout();
            this.ServicesPage.SuspendLayout();
            this.ServicePropertiesPanel.SuspendLayout();
            this.AppointmentsPage.SuspendLayout();
            this.AppointmentPropertiesPanel.SuspendLayout();
            this.ServedAppointmentsPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainFormTabControl
            // 
            this.MainFormTabControl.Controls.Add(this.ServicesPage);
            this.MainFormTabControl.Controls.Add(this.AppointmentsPage);
            this.MainFormTabControl.Controls.Add(this.ServedAppointmentsPage);
            this.MainFormTabControl.Location = new System.Drawing.Point(13, 13);
            this.MainFormTabControl.Name = "MainFormTabControl";
            this.MainFormTabControl.SelectedIndex = 0;
            this.MainFormTabControl.Size = new System.Drawing.Size(1159, 536);
            this.MainFormTabControl.TabIndex = 0;
            // 
            // ServicesPage
            // 
            this.ServicesPage.Controls.Add(this.RefreshServicesButton);
            this.ServicesPage.Controls.Add(this.ServicePropertiesPanel);
            this.ServicesPage.Controls.Add(this.SearchServicesTextBox);
            this.ServicesPage.Controls.Add(this.AddServiceButton);
            this.ServicesPage.Controls.Add(this.SearchServicesPanel);
            this.ServicesPage.Location = new System.Drawing.Point(4, 22);
            this.ServicesPage.Name = "ServicesPage";
            this.ServicesPage.Padding = new System.Windows.Forms.Padding(3);
            this.ServicesPage.Size = new System.Drawing.Size(1151, 510);
            this.ServicesPage.TabIndex = 0;
            this.ServicesPage.Text = "Услуги";
            this.ServicesPage.UseVisualStyleBackColor = true;
            // 
            // RefreshServicesButton
            // 
            this.RefreshServicesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.RefreshServicesButton.Location = new System.Drawing.Point(280, 6);
            this.RefreshServicesButton.Name = "RefreshServicesButton";
            this.RefreshServicesButton.Size = new System.Drawing.Size(66, 23);
            this.RefreshServicesButton.TabIndex = 4;
            this.RefreshServicesButton.Text = "Обновить";
            this.RefreshServicesButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.RefreshServicesButton.UseVisualStyleBackColor = true;
            this.RefreshServicesButton.Click += new System.EventHandler(this.RefreshServices);
            // 
            // ServicePropertiesPanel
            // 
            this.ServicePropertiesPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ServicePropertiesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ServicePropertiesPanel.Controls.Add(this.DocumentsListLabel);
            this.ServicePropertiesPanel.Controls.Add(this.ServiceTimeDurationLabel);
            this.ServicePropertiesPanel.Controls.Add(this.GovernmentStructureLabel);
            this.ServicePropertiesPanel.Controls.Add(this.ServiceTitleLabel);
            this.ServicePropertiesPanel.Controls.Add(this.ServiceTypeLabel);
            this.ServicePropertiesPanel.Controls.Add(this.ServiceTimeDurationLiner);
            this.ServicePropertiesPanel.Controls.Add(this.GovernmentStructureLiner);
            this.ServicePropertiesPanel.Controls.Add(this.DocumentsListLiner);
            this.ServicePropertiesPanel.Controls.Add(this.ServiceTitleLiner);
            this.ServicePropertiesPanel.Controls.Add(this.ServiceTypeLiner);
            this.ServicePropertiesPanel.Controls.Add(this.EditServiceButton);
            this.ServicePropertiesPanel.Controls.Add(this.CreateAppointmentButton);
            this.ServicePropertiesPanel.Location = new System.Drawing.Point(502, 7);
            this.ServicePropertiesPanel.Name = "ServicePropertiesPanel";
            this.ServicePropertiesPanel.Size = new System.Drawing.Size(643, 497);
            this.ServicePropertiesPanel.TabIndex = 3;
            // 
            // DocumentsListLabel
            // 
            this.DocumentsListLabel.Location = new System.Drawing.Point(185, 137);
            this.DocumentsListLabel.MinimumSize = new System.Drawing.Size(300, 100);
            this.DocumentsListLabel.Multiline = true;
            this.DocumentsListLabel.Name = "DocumentsListLabel";
            this.DocumentsListLabel.ReadOnly = true;
            this.DocumentsListLabel.Size = new System.Drawing.Size(453, 100);
            this.DocumentsListLabel.TabIndex = 12;
            // 
            // ServiceTimeDurationLabel
            // 
            this.ServiceTimeDurationLabel.AutoSize = true;
            this.ServiceTimeDurationLabel.Location = new System.Drawing.Point(185, 114);
            this.ServiceTimeDurationLabel.Name = "ServiceTimeDurationLabel";
            this.ServiceTimeDurationLabel.Size = new System.Drawing.Size(16, 13);
            this.ServiceTimeDurationLabel.TabIndex = 10;
            this.ServiceTimeDurationLabel.Text = "...";
            // 
            // GovernmentStructureLabel
            // 
            this.GovernmentStructureLabel.AutoSize = true;
            this.GovernmentStructureLabel.Location = new System.Drawing.Point(185, 91);
            this.GovernmentStructureLabel.Name = "GovernmentStructureLabel";
            this.GovernmentStructureLabel.Size = new System.Drawing.Size(16, 13);
            this.GovernmentStructureLabel.TabIndex = 9;
            this.GovernmentStructureLabel.Text = "...";
            // 
            // ServiceTitleLabel
            // 
            this.ServiceTitleLabel.AutoSize = true;
            this.ServiceTitleLabel.Location = new System.Drawing.Point(185, 68);
            this.ServiceTitleLabel.Name = "ServiceTitleLabel";
            this.ServiceTitleLabel.Size = new System.Drawing.Size(16, 13);
            this.ServiceTitleLabel.TabIndex = 8;
            this.ServiceTitleLabel.Text = "...";
            // 
            // ServiceTypeLabel
            // 
            this.ServiceTypeLabel.AutoSize = true;
            this.ServiceTypeLabel.Location = new System.Drawing.Point(185, 45);
            this.ServiceTypeLabel.Name = "ServiceTypeLabel";
            this.ServiceTypeLabel.Size = new System.Drawing.Size(16, 13);
            this.ServiceTypeLabel.TabIndex = 7;
            this.ServiceTypeLabel.Text = "...";
            // 
            // ServiceTimeDurationLiner
            // 
            this.ServiceTimeDurationLiner.AutoSize = true;
            this.ServiceTimeDurationLiner.Location = new System.Drawing.Point(5, 114);
            this.ServiceTimeDurationLiner.Name = "ServiceTimeDurationLiner";
            this.ServiceTimeDurationLiner.Size = new System.Drawing.Size(149, 13);
            this.ServiceTimeDurationLiner.TabIndex = 6;
            this.ServiceTimeDurationLiner.Text = "Срок оказания услуги (дни):";
            // 
            // GovernmentStructureLiner
            // 
            this.GovernmentStructureLiner.AutoSize = true;
            this.GovernmentStructureLiner.Location = new System.Drawing.Point(5, 91);
            this.GovernmentStructureLiner.Name = "GovernmentStructureLiner";
            this.GovernmentStructureLiner.Size = new System.Drawing.Size(145, 13);
            this.GovernmentStructureLiner.TabIndex = 5;
            this.GovernmentStructureLiner.Text = "Ответственное ведомство:";
            // 
            // DocumentsListLiner
            // 
            this.DocumentsListLiner.AutoSize = true;
            this.DocumentsListLiner.Location = new System.Drawing.Point(5, 137);
            this.DocumentsListLiner.Name = "DocumentsListLiner";
            this.DocumentsListLiner.Size = new System.Drawing.Size(122, 13);
            this.DocumentsListLiner.TabIndex = 4;
            this.DocumentsListLiner.Text = "Перечень документов:";
            // 
            // ServiceTitleLiner
            // 
            this.ServiceTitleLiner.AutoSize = true;
            this.ServiceTitleLiner.Location = new System.Drawing.Point(5, 68);
            this.ServiceTitleLiner.Name = "ServiceTitleLiner";
            this.ServiceTitleLiner.Size = new System.Drawing.Size(122, 13);
            this.ServiceTitleLiner.TabIndex = 3;
            this.ServiceTitleLiner.Text = "Наименование услуги:";
            // 
            // ServiceTypeLiner
            // 
            this.ServiceTypeLiner.AutoSize = true;
            this.ServiceTypeLiner.Location = new System.Drawing.Point(5, 45);
            this.ServiceTypeLiner.Name = "ServiceTypeLiner";
            this.ServiceTypeLiner.Size = new System.Drawing.Size(99, 13);
            this.ServiceTypeLiner.TabIndex = 2;
            this.ServiceTypeLiner.Text = "Категория услуги:";
            // 
            // EditServiceButton
            // 
            this.EditServiceButton.Location = new System.Drawing.Point(519, 469);
            this.EditServiceButton.Name = "EditServiceButton";
            this.EditServiceButton.Size = new System.Drawing.Size(120, 23);
            this.EditServiceButton.TabIndex = 1;
            this.EditServiceButton.Text = "Изменить";
            this.EditServiceButton.UseVisualStyleBackColor = true;
            this.EditServiceButton.Click += new System.EventHandler(this.EditServiceButton_Click);
            // 
            // CreateAppointmentButton
            // 
            this.CreateAppointmentButton.Location = new System.Drawing.Point(396, 469);
            this.CreateAppointmentButton.Name = "CreateAppointmentButton";
            this.CreateAppointmentButton.Size = new System.Drawing.Size(120, 23);
            this.CreateAppointmentButton.TabIndex = 0;
            this.CreateAppointmentButton.Text = "Создать запись";
            this.CreateAppointmentButton.UseVisualStyleBackColor = true;
            this.CreateAppointmentButton.Click += new System.EventHandler(this.CreateAppointmentButton_Click);
            // 
            // SearchServicesTextBox
            // 
            this.SearchServicesTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SearchServicesTextBox.Location = new System.Drawing.Point(6, 7);
            this.SearchServicesTextBox.Name = "SearchServicesTextBox";
            this.SearchServicesTextBox.Size = new System.Drawing.Size(268, 20);
            this.SearchServicesTextBox.TabIndex = 2;
            // 
            // AddServiceButton
            // 
            this.AddServiceButton.Location = new System.Drawing.Point(352, 6);
            this.AddServiceButton.Name = "AddServiceButton";
            this.AddServiceButton.Size = new System.Drawing.Size(144, 23);
            this.AddServiceButton.TabIndex = 1;
            this.AddServiceButton.Text = "Добавить услугу";
            this.AddServiceButton.UseVisualStyleBackColor = true;
            this.AddServiceButton.Click += new System.EventHandler(this.ButtonAddService_Click);
            // 
            // SearchServicesPanel
            // 
            this.SearchServicesPanel.AutoScroll = true;
            this.SearchServicesPanel.BackColor = System.Drawing.SystemColors.Control;
            this.SearchServicesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchServicesPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SearchServicesPanel.Location = new System.Drawing.Point(6, 33);
            this.SearchServicesPanel.Name = "SearchServicesPanel";
            this.SearchServicesPanel.Size = new System.Drawing.Size(490, 471);
            this.SearchServicesPanel.TabIndex = 0;
            // 
            // AppointmentsPage
            // 
            this.AppointmentsPage.Controls.Add(this.RefreshAppointmentsButton);
            this.AppointmentsPage.Controls.Add(this.AppointmentPropertiesPanel);
            this.AppointmentsPage.Controls.Add(this.SearchAppointmentsTextBox);
            this.AppointmentsPage.Controls.Add(this.SearchAppointmentsPanel);
            this.AppointmentsPage.Location = new System.Drawing.Point(4, 22);
            this.AppointmentsPage.Name = "AppointmentsPage";
            this.AppointmentsPage.Padding = new System.Windows.Forms.Padding(3);
            this.AppointmentsPage.Size = new System.Drawing.Size(1151, 510);
            this.AppointmentsPage.TabIndex = 1;
            this.AppointmentsPage.Text = "Записи";
            this.AppointmentsPage.UseVisualStyleBackColor = true;
            // 
            // RefreshAppointmentsButton
            // 
            this.RefreshAppointmentsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.RefreshAppointmentsButton.Location = new System.Drawing.Point(430, 6);
            this.RefreshAppointmentsButton.Name = "RefreshAppointmentsButton";
            this.RefreshAppointmentsButton.Size = new System.Drawing.Size(66, 23);
            this.RefreshAppointmentsButton.TabIndex = 7;
            this.RefreshAppointmentsButton.Text = "Обновить";
            this.RefreshAppointmentsButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.RefreshAppointmentsButton.UseVisualStyleBackColor = true;
            this.RefreshAppointmentsButton.Click += new System.EventHandler(this.RefreshAppointments);
            // 
            // AppointmentPropertiesPanel
            // 
            this.AppointmentPropertiesPanel.BackColor = System.Drawing.SystemColors.Control;
            this.AppointmentPropertiesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentDaytimeLabel);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentDaytimeLiner);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentDateLabel);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentNationalityLabel);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentClientNationalityLiner);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentDateLiner);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentClientPatronymicLabel);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentClientPatronymicLiner);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentClientNameLabel);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentClientSurnameLabel);
            this.AppointmentPropertiesPanel.Controls.Add(this.PassportNumberLabel);
            this.AppointmentPropertiesPanel.Controls.Add(this.PassportSeriesLabel);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentServiceTitleLabel);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentClientSurnameLiner);
            this.AppointmentPropertiesPanel.Controls.Add(this.PassportNumberLiner);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentClientNameLiner);
            this.AppointmentPropertiesPanel.Controls.Add(this.PassportSeriesLiner);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentServiceTitleLiner);
            this.AppointmentPropertiesPanel.Controls.Add(this.EditAppointmentButton);
            this.AppointmentPropertiesPanel.Controls.Add(this.DeleteAppointmentButton);
            this.AppointmentPropertiesPanel.Location = new System.Drawing.Point(502, 7);
            this.AppointmentPropertiesPanel.Name = "AppointmentPropertiesPanel";
            this.AppointmentPropertiesPanel.Size = new System.Drawing.Size(643, 497);
            this.AppointmentPropertiesPanel.TabIndex = 6;
            // 
            // AppointmentDaytimeLabel
            // 
            this.AppointmentDaytimeLabel.AutoSize = true;
            this.AppointmentDaytimeLabel.Location = new System.Drawing.Point(185, 229);
            this.AppointmentDaytimeLabel.Name = "AppointmentDaytimeLabel";
            this.AppointmentDaytimeLabel.Size = new System.Drawing.Size(16, 13);
            this.AppointmentDaytimeLabel.TabIndex = 19;
            this.AppointmentDaytimeLabel.Text = "...";
            // 
            // AppointmentDaytimeLiner
            // 
            this.AppointmentDaytimeLiner.AutoSize = true;
            this.AppointmentDaytimeLiner.Location = new System.Drawing.Point(5, 229);
            this.AppointmentDaytimeLiner.Name = "AppointmentDaytimeLiner";
            this.AppointmentDaytimeLiner.Size = new System.Drawing.Size(82, 13);
            this.AppointmentDaytimeLiner.TabIndex = 18;
            this.AppointmentDaytimeLiner.Text = "Время записи:";
            // 
            // AppointmentDateLabel
            // 
            this.AppointmentDateLabel.AutoSize = true;
            this.AppointmentDateLabel.Location = new System.Drawing.Point(185, 206);
            this.AppointmentDateLabel.Name = "AppointmentDateLabel";
            this.AppointmentDateLabel.Size = new System.Drawing.Size(16, 13);
            this.AppointmentDateLabel.TabIndex = 17;
            this.AppointmentDateLabel.Text = "...";
            // 
            // AppointmentNationalityLabel
            // 
            this.AppointmentNationalityLabel.AutoSize = true;
            this.AppointmentNationalityLabel.Location = new System.Drawing.Point(185, 183);
            this.AppointmentNationalityLabel.Name = "AppointmentNationalityLabel";
            this.AppointmentNationalityLabel.Size = new System.Drawing.Size(16, 13);
            this.AppointmentNationalityLabel.TabIndex = 16;
            this.AppointmentNationalityLabel.Text = "...";
            // 
            // AppointmentClientNationalityLiner
            // 
            this.AppointmentClientNationalityLiner.AutoSize = true;
            this.AppointmentClientNationalityLiner.Location = new System.Drawing.Point(5, 183);
            this.AppointmentClientNationalityLiner.Name = "AppointmentClientNationalityLiner";
            this.AppointmentClientNationalityLiner.Size = new System.Drawing.Size(77, 13);
            this.AppointmentClientNationalityLiner.TabIndex = 15;
            this.AppointmentClientNationalityLiner.Text = "Гражданство:";
            // 
            // AppointmentDateLiner
            // 
            this.AppointmentDateLiner.AutoSize = true;
            this.AppointmentDateLiner.Location = new System.Drawing.Point(5, 206);
            this.AppointmentDateLiner.Name = "AppointmentDateLiner";
            this.AppointmentDateLiner.Size = new System.Drawing.Size(75, 13);
            this.AppointmentDateLiner.TabIndex = 14;
            this.AppointmentDateLiner.Text = "Дата записи:";
            // 
            // AppointmentClientPatronymicLabel
            // 
            this.AppointmentClientPatronymicLabel.AutoSize = true;
            this.AppointmentClientPatronymicLabel.Location = new System.Drawing.Point(185, 160);
            this.AppointmentClientPatronymicLabel.Name = "AppointmentClientPatronymicLabel";
            this.AppointmentClientPatronymicLabel.Size = new System.Drawing.Size(16, 13);
            this.AppointmentClientPatronymicLabel.TabIndex = 13;
            this.AppointmentClientPatronymicLabel.Text = "...";
            // 
            // AppointmentClientPatronymicLiner
            // 
            this.AppointmentClientPatronymicLiner.AutoSize = true;
            this.AppointmentClientPatronymicLiner.Location = new System.Drawing.Point(5, 160);
            this.AppointmentClientPatronymicLiner.Name = "AppointmentClientPatronymicLiner";
            this.AppointmentClientPatronymicLiner.Size = new System.Drawing.Size(101, 13);
            this.AppointmentClientPatronymicLiner.TabIndex = 12;
            this.AppointmentClientPatronymicLiner.Text = "Отчество клиента:";
            // 
            // AppointmentClientNameLabel
            // 
            this.AppointmentClientNameLabel.AutoSize = true;
            this.AppointmentClientNameLabel.Location = new System.Drawing.Point(185, 137);
            this.AppointmentClientNameLabel.Name = "AppointmentClientNameLabel";
            this.AppointmentClientNameLabel.Size = new System.Drawing.Size(16, 13);
            this.AppointmentClientNameLabel.TabIndex = 11;
            this.AppointmentClientNameLabel.Text = "...";
            // 
            // AppointmentClientSurnameLabel
            // 
            this.AppointmentClientSurnameLabel.AutoSize = true;
            this.AppointmentClientSurnameLabel.Location = new System.Drawing.Point(185, 114);
            this.AppointmentClientSurnameLabel.Name = "AppointmentClientSurnameLabel";
            this.AppointmentClientSurnameLabel.Size = new System.Drawing.Size(16, 13);
            this.AppointmentClientSurnameLabel.TabIndex = 10;
            this.AppointmentClientSurnameLabel.Text = "...";
            // 
            // PassportNumberLabel
            // 
            this.PassportNumberLabel.AutoSize = true;
            this.PassportNumberLabel.Location = new System.Drawing.Point(185, 91);
            this.PassportNumberLabel.Name = "PassportNumberLabel";
            this.PassportNumberLabel.Size = new System.Drawing.Size(16, 13);
            this.PassportNumberLabel.TabIndex = 9;
            this.PassportNumberLabel.Text = "...";
            // 
            // PassportSeriesLabel
            // 
            this.PassportSeriesLabel.AutoSize = true;
            this.PassportSeriesLabel.Location = new System.Drawing.Point(185, 68);
            this.PassportSeriesLabel.Name = "PassportSeriesLabel";
            this.PassportSeriesLabel.Size = new System.Drawing.Size(16, 13);
            this.PassportSeriesLabel.TabIndex = 8;
            this.PassportSeriesLabel.Text = "...";
            // 
            // AppointmentServiceTitleLabel
            // 
            this.AppointmentServiceTitleLabel.AutoSize = true;
            this.AppointmentServiceTitleLabel.Location = new System.Drawing.Point(185, 45);
            this.AppointmentServiceTitleLabel.Name = "AppointmentServiceTitleLabel";
            this.AppointmentServiceTitleLabel.Size = new System.Drawing.Size(16, 13);
            this.AppointmentServiceTitleLabel.TabIndex = 7;
            this.AppointmentServiceTitleLabel.Text = "...";
            // 
            // AppointmentClientSurnameLiner
            // 
            this.AppointmentClientSurnameLiner.AutoSize = true;
            this.AppointmentClientSurnameLiner.Location = new System.Drawing.Point(5, 114);
            this.AppointmentClientSurnameLiner.Name = "AppointmentClientSurnameLiner";
            this.AppointmentClientSurnameLiner.Size = new System.Drawing.Size(103, 13);
            this.AppointmentClientSurnameLiner.TabIndex = 6;
            this.AppointmentClientSurnameLiner.Text = "Фамилия клиента:";
            // 
            // PassportNumberLiner
            // 
            this.PassportNumberLiner.AutoSize = true;
            this.PassportNumberLiner.Location = new System.Drawing.Point(5, 91);
            this.PassportNumberLiner.Name = "PassportNumberLiner";
            this.PassportNumberLiner.Size = new System.Drawing.Size(94, 13);
            this.PassportNumberLiner.TabIndex = 5;
            this.PassportNumberLiner.Text = "Номер паспорта:";
            // 
            // AppointmentClientNameLiner
            // 
            this.AppointmentClientNameLiner.AutoSize = true;
            this.AppointmentClientNameLiner.Location = new System.Drawing.Point(5, 137);
            this.AppointmentClientNameLiner.Name = "AppointmentClientNameLiner";
            this.AppointmentClientNameLiner.Size = new System.Drawing.Size(76, 13);
            this.AppointmentClientNameLiner.TabIndex = 4;
            this.AppointmentClientNameLiner.Text = "Имя клиента:";
            // 
            // PassportSeriesLiner
            // 
            this.PassportSeriesLiner.AutoSize = true;
            this.PassportSeriesLiner.Location = new System.Drawing.Point(5, 68);
            this.PassportSeriesLiner.Name = "PassportSeriesLiner";
            this.PassportSeriesLiner.Size = new System.Drawing.Size(91, 13);
            this.PassportSeriesLiner.TabIndex = 3;
            this.PassportSeriesLiner.Text = "Серия паспорта:";
            // 
            // AppointmentServiceTitleLiner
            // 
            this.AppointmentServiceTitleLiner.AutoSize = true;
            this.AppointmentServiceTitleLiner.Location = new System.Drawing.Point(5, 45);
            this.AppointmentServiceTitleLiner.Name = "AppointmentServiceTitleLiner";
            this.AppointmentServiceTitleLiner.Size = new System.Drawing.Size(122, 13);
            this.AppointmentServiceTitleLiner.TabIndex = 2;
            this.AppointmentServiceTitleLiner.Text = "Наименование услуги:";
            // 
            // EditAppointmentButton
            // 
            this.EditAppointmentButton.Location = new System.Drawing.Point(498, 469);
            this.EditAppointmentButton.Name = "EditAppointmentButton";
            this.EditAppointmentButton.Size = new System.Drawing.Size(140, 23);
            this.EditAppointmentButton.TabIndex = 1;
            this.EditAppointmentButton.Text = "Изменить дату/время";
            this.EditAppointmentButton.UseVisualStyleBackColor = true;
            this.EditAppointmentButton.Click += new System.EventHandler(this.EditAppointmentButton_Click);
            // 
            // DeleteAppointmentButton
            // 
            this.DeleteAppointmentButton.Location = new System.Drawing.Point(355, 469);
            this.DeleteAppointmentButton.Name = "DeleteAppointmentButton";
            this.DeleteAppointmentButton.Size = new System.Drawing.Size(140, 23);
            this.DeleteAppointmentButton.TabIndex = 0;
            this.DeleteAppointmentButton.Text = "Удалить запись";
            this.DeleteAppointmentButton.UseVisualStyleBackColor = true;
            this.DeleteAppointmentButton.Click += new System.EventHandler(this.DeleteAppointmentButton_Click);
            // 
            // SearchAppointmentsTextBox
            // 
            this.SearchAppointmentsTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SearchAppointmentsTextBox.Location = new System.Drawing.Point(6, 7);
            this.SearchAppointmentsTextBox.Name = "SearchAppointmentsTextBox";
            this.SearchAppointmentsTextBox.Size = new System.Drawing.Size(418, 20);
            this.SearchAppointmentsTextBox.TabIndex = 5;
            // 
            // SearchAppointmentsPanel
            // 
            this.SearchAppointmentsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.SearchAppointmentsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchAppointmentsPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SearchAppointmentsPanel.Location = new System.Drawing.Point(6, 33);
            this.SearchAppointmentsPanel.Name = "SearchAppointmentsPanel";
            this.SearchAppointmentsPanel.Size = new System.Drawing.Size(490, 471);
            this.SearchAppointmentsPanel.TabIndex = 4;
            // 
            // ServedAppointmentsPage
            // 
            this.ServedAppointmentsPage.Controls.Add(this.RefreshAppointmentsHistoryButton);
            this.ServedAppointmentsPage.Controls.Add(this.SearchAppointmentsHistoryPanel);
            this.ServedAppointmentsPage.Controls.Add(this.SearchAppointmentsHistoryTextBox);
            this.ServedAppointmentsPage.Location = new System.Drawing.Point(4, 22);
            this.ServedAppointmentsPage.Name = "ServedAppointmentsPage";
            this.ServedAppointmentsPage.Size = new System.Drawing.Size(1151, 510);
            this.ServedAppointmentsPage.TabIndex = 2;
            this.ServedAppointmentsPage.Text = "История услуг";
            this.ServedAppointmentsPage.UseVisualStyleBackColor = true;
            // 
            // RefreshAppointmentsHistoryButton
            // 
            this.RefreshAppointmentsHistoryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.RefreshAppointmentsHistoryButton.Location = new System.Drawing.Point(286, 7);
            this.RefreshAppointmentsHistoryButton.Name = "RefreshAppointmentsHistoryButton";
            this.RefreshAppointmentsHistoryButton.Size = new System.Drawing.Size(66, 23);
            this.RefreshAppointmentsHistoryButton.TabIndex = 5;
            this.RefreshAppointmentsHistoryButton.Text = "Обновить";
            this.RefreshAppointmentsHistoryButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.RefreshAppointmentsHistoryButton.UseVisualStyleBackColor = true;
            this.RefreshAppointmentsHistoryButton.Click += new System.EventHandler(this.RefreshAppointmentsHistory);
            // 
            // SearchAppointmentsHistoryPanel
            // 
            this.SearchAppointmentsHistoryPanel.Location = new System.Drawing.Point(3, 34);
            this.SearchAppointmentsHistoryPanel.Name = "SearchAppointmentsHistoryPanel";
            this.SearchAppointmentsHistoryPanel.Size = new System.Drawing.Size(1145, 473);
            this.SearchAppointmentsHistoryPanel.TabIndex = 1;
            // 
            // SearchAppointmentsHistoryTextBox
            // 
            this.SearchAppointmentsHistoryTextBox.Location = new System.Drawing.Point(3, 8);
            this.SearchAppointmentsHistoryTextBox.Name = "SearchAppointmentsHistoryTextBox";
            this.SearchAppointmentsHistoryTextBox.Size = new System.Drawing.Size(277, 20);
            this.SearchAppointmentsHistoryTextBox.TabIndex = 0;
            // 
            // UpdateAppointmentsHistoryTimer
            // 
            this.UpdateAppointmentsHistoryTimer.Enabled = true;
            this.UpdateAppointmentsHistoryTimer.Interval = 60000;
            this.UpdateAppointmentsHistoryTimer.Tick += new System.EventHandler(this.AppointmentsExpirationController_Timer);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.MainFormTabControl);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1200, 600);
            this.MinimumSize = new System.Drawing.Size(1200, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Информационная система МФЦ";
            this.MainFormTabControl.ResumeLayout(false);
            this.ServicesPage.ResumeLayout(false);
            this.ServicesPage.PerformLayout();
            this.ServicePropertiesPanel.ResumeLayout(false);
            this.ServicePropertiesPanel.PerformLayout();
            this.AppointmentsPage.ResumeLayout(false);
            this.AppointmentsPage.PerformLayout();
            this.AppointmentPropertiesPanel.ResumeLayout(false);
            this.AppointmentPropertiesPanel.PerformLayout();
            this.ServedAppointmentsPage.ResumeLayout(false);
            this.ServedAppointmentsPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl MainFormTabControl;
        private System.Windows.Forms.TabPage ServicesPage;
        private System.Windows.Forms.TabPage AppointmentsPage;
        private System.Windows.Forms.TabPage ServedAppointmentsPage;
        private System.Windows.Forms.Button AddServiceButton;
        private System.Windows.Forms.Panel SearchServicesPanel;
        private System.Windows.Forms.TextBox SearchServicesTextBox;
        private System.Windows.Forms.Panel ServicePropertiesPanel;
        private System.Windows.Forms.Timer UpdateAppointmentsHistoryTimer;
        private System.Windows.Forms.Button EditServiceButton;
        private System.Windows.Forms.Button CreateAppointmentButton;
        private System.Windows.Forms.Label ServiceTypeLiner;
        private System.Windows.Forms.Label DocumentsListLiner;
        private System.Windows.Forms.Label ServiceTitleLiner;
        private System.Windows.Forms.Label GovernmentStructureLiner;
        private System.Windows.Forms.Label ServiceTimeDurationLiner;
        private System.Windows.Forms.Label ServiceTimeDurationLabel;
        private System.Windows.Forms.Label GovernmentStructureLabel;
        private System.Windows.Forms.Label ServiceTitleLabel;
        private System.Windows.Forms.Label ServiceTypeLabel;         
        private System.Windows.Forms.Panel AppointmentPropertiesPanel;
        private System.Windows.Forms.Label AppointmentClientNameLabel;
        private System.Windows.Forms.Label AppointmentClientSurnameLabel;
        private System.Windows.Forms.Label PassportNumberLabel;
        private System.Windows.Forms.Label PassportSeriesLabel;
        private System.Windows.Forms.Label AppointmentServiceTitleLabel;
        private System.Windows.Forms.Label AppointmentClientSurnameLiner;
        private System.Windows.Forms.Label PassportNumberLiner;
        private System.Windows.Forms.Label AppointmentClientNameLiner;
        private System.Windows.Forms.Label PassportSeriesLiner;
        private System.Windows.Forms.Label AppointmentServiceTitleLiner;
        private System.Windows.Forms.Button EditAppointmentButton;
        private System.Windows.Forms.Button DeleteAppointmentButton;
        private System.Windows.Forms.TextBox SearchAppointmentsTextBox;
        private System.Windows.Forms.Panel SearchAppointmentsPanel;
        private System.Windows.Forms.Panel SearchAppointmentsHistoryPanel;
        private System.Windows.Forms.TextBox SearchAppointmentsHistoryTextBox;
        private System.Windows.Forms.Label AppointmentClientPatronymicLabel;
        private System.Windows.Forms.Label AppointmentClientPatronymicLiner;
        private System.Windows.Forms.Label AppointmentDaytimeLabel;
        private System.Windows.Forms.Label AppointmentDaytimeLiner;
        private System.Windows.Forms.Label AppointmentDateLabel;
        private System.Windows.Forms.Label AppointmentNationalityLabel;
        private System.Windows.Forms.Label AppointmentClientNationalityLiner;
        private System.Windows.Forms.Label AppointmentDateLiner;
        private System.Windows.Forms.TextBox DocumentsListLabel;
        private System.Windows.Forms.Button RefreshServicesButton;
        private System.Windows.Forms.Button RefreshAppointmentsButton;
        private System.Windows.Forms.Button RefreshAppointmentsHistoryButton;
    }
}

