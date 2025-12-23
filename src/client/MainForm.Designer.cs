namespace client
{
    partial class MainForm
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
            this.ServicePropertiesPanel = new System.Windows.Forms.Panel();
            this.DocumentsListLabel = new System.Windows.Forms.Label();
            this.ServiceTimeDurationLabel = new System.Windows.Forms.Label();
            this.GovernmentStructureLabel = new System.Windows.Forms.Label();
            this.ServiceTitleLabel = new System.Windows.Forms.Label();
            this.ServiceTypeLabel = new System.Windows.Forms.Label();
            this.ServiceTimeDurationLabelLeft = new System.Windows.Forms.Label();
            this.GovernmentStructureLabelLeft = new System.Windows.Forms.Label();
            this.DocumentsListLabelLeft = new System.Windows.Forms.Label();
            this.ServiceTitleLabelLeft = new System.Windows.Forms.Label();
            this.ServiceTypeLabelLeft = new System.Windows.Forms.Label();
            this.EditServiceButton = new System.Windows.Forms.Button();
            this.CreateAppointmentButton = new System.Windows.Forms.Button();
            this.SearchServicesTextBox = new System.Windows.Forms.TextBox();
            this.AddServiceButton = new System.Windows.Forms.Button();
            this.SearchServicesPanel = new System.Windows.Forms.Panel();
            this.AppointmentsPage = new System.Windows.Forms.TabPage();
            this.AppointmentPropertiesPanel = new System.Windows.Forms.Panel();
            this.AppointmentClientNameLabel = new System.Windows.Forms.Label();
            this.AppointmentClientSurnameLabel = new System.Windows.Forms.Label();
            this.PassportNumberLabel = new System.Windows.Forms.Label();
            this.PassportSeriesLabel = new System.Windows.Forms.Label();
            this.AppointmentServiceTitleLabel = new System.Windows.Forms.Label();
            this.AppointmentClientSurnameLabelLeft = new System.Windows.Forms.Label();
            this.PassportNumberLabelLeft = new System.Windows.Forms.Label();
            this.AppointmentClientNameLabelLeft = new System.Windows.Forms.Label();
            this.PassportSeriesLabelLeft = new System.Windows.Forms.Label();
            this.AppointmentServiceTitleLabelLeft = new System.Windows.Forms.Label();
            this.EditAppointmentButton = new System.Windows.Forms.Button();
            this.DeleteAppointmentButton = new System.Windows.Forms.Button();
            this.SearchAppointmentsTextBox = new System.Windows.Forms.TextBox();
            this.SearchAppointmentsPanel = new System.Windows.Forms.Panel();
            this.ServedAppointmentsPage = new System.Windows.Forms.TabPage();
            this.SearchAppointmentsHistoryPanel = new System.Windows.Forms.Panel();
            this.SearchAppointmentsHistoryTextBox = new System.Windows.Forms.TextBox();
            this.UpdateAppointmentsHistoryTimer = new System.Windows.Forms.Timer(this.components);
            this.AppointmentClientPatronymicLabel = new System.Windows.Forms.Label();
            this.AppointmentClientPatronymicLabelLeft = new System.Windows.Forms.Label();
            this.AppointmentDaytimeLabel = new System.Windows.Forms.Label();
            this.AppointmentDaytimeLabelLeft = new System.Windows.Forms.Label();
            this.AppointmentDateLabel = new System.Windows.Forms.Label();
            this.AppointmentNationalityLabel = new System.Windows.Forms.Label();
            this.AppointmentNationalityLabelLeft = new System.Windows.Forms.Label();
            this.AppointmentDateLabelLeft = new System.Windows.Forms.Label();
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
            this.MainFormTabControl.Size = new System.Drawing.Size(859, 536);
            this.MainFormTabControl.TabIndex = 0;
            // 
            // ServicesPage
            // 
            this.ServicesPage.Controls.Add(this.ServicePropertiesPanel);
            this.ServicesPage.Controls.Add(this.SearchServicesTextBox);
            this.ServicesPage.Controls.Add(this.AddServiceButton);
            this.ServicesPage.Controls.Add(this.SearchServicesPanel);
            this.ServicesPage.Location = new System.Drawing.Point(4, 22);
            this.ServicesPage.Name = "ServicesPage";
            this.ServicesPage.Padding = new System.Windows.Forms.Padding(3);
            this.ServicesPage.Size = new System.Drawing.Size(851, 510);
            this.ServicesPage.TabIndex = 0;
            this.ServicesPage.Text = "Услуги";
            this.ServicesPage.UseVisualStyleBackColor = true;
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
            this.ServicePropertiesPanel.Controls.Add(this.ServiceTimeDurationLabelLeft);
            this.ServicePropertiesPanel.Controls.Add(this.GovernmentStructureLabelLeft);
            this.ServicePropertiesPanel.Controls.Add(this.DocumentsListLabelLeft);
            this.ServicePropertiesPanel.Controls.Add(this.ServiceTitleLabelLeft);
            this.ServicePropertiesPanel.Controls.Add(this.ServiceTypeLabelLeft);
            this.ServicePropertiesPanel.Controls.Add(this.EditServiceButton);
            this.ServicePropertiesPanel.Controls.Add(this.CreateAppointmentButton);
            this.ServicePropertiesPanel.Location = new System.Drawing.Point(241, 7);
            this.ServicePropertiesPanel.Name = "ServicePropertiesPanel";
            this.ServicePropertiesPanel.Size = new System.Drawing.Size(604, 497);
            this.ServicePropertiesPanel.TabIndex = 3;
            // 
            // DocumentsListLabel
            // 
            this.DocumentsListLabel.AutoSize = true;
            this.DocumentsListLabel.Location = new System.Drawing.Point(185, 117);
            this.DocumentsListLabel.Name = "DocumentsListLabel";
            this.DocumentsListLabel.Size = new System.Drawing.Size(16, 13);
            this.DocumentsListLabel.TabIndex = 11;
            this.DocumentsListLabel.Text = "...";
            // 
            // ServiceTimeDurationLabel
            // 
            this.ServiceTimeDurationLabel.AutoSize = true;
            this.ServiceTimeDurationLabel.Location = new System.Drawing.Point(185, 94);
            this.ServiceTimeDurationLabel.Name = "ServiceTimeDurationLabel";
            this.ServiceTimeDurationLabel.Size = new System.Drawing.Size(16, 13);
            this.ServiceTimeDurationLabel.TabIndex = 10;
            this.ServiceTimeDurationLabel.Text = "...";
            // 
            // GovernmentStructureLabel
            // 
            this.GovernmentStructureLabel.AutoSize = true;
            this.GovernmentStructureLabel.Location = new System.Drawing.Point(185, 71);
            this.GovernmentStructureLabel.Name = "GovernmentStructureLabel";
            this.GovernmentStructureLabel.Size = new System.Drawing.Size(16, 13);
            this.GovernmentStructureLabel.TabIndex = 9;
            this.GovernmentStructureLabel.Text = "...";
            // 
            // ServiceTitleLabel
            // 
            this.ServiceTitleLabel.AutoSize = true;
            this.ServiceTitleLabel.Location = new System.Drawing.Point(185, 48);
            this.ServiceTitleLabel.Name = "ServiceTitleLabel";
            this.ServiceTitleLabel.Size = new System.Drawing.Size(16, 13);
            this.ServiceTitleLabel.TabIndex = 8;
            this.ServiceTitleLabel.Text = "...";
            // 
            // ServiceTypeLabel
            // 
            this.ServiceTypeLabel.AutoSize = true;
            this.ServiceTypeLabel.Location = new System.Drawing.Point(185, 25);
            this.ServiceTypeLabel.Name = "ServiceTypeLabel";
            this.ServiceTypeLabel.Size = new System.Drawing.Size(16, 13);
            this.ServiceTypeLabel.TabIndex = 7;
            this.ServiceTypeLabel.Text = "...";
            // 
            // ServiceTimeDurationLabelLeft
            // 
            this.ServiceTimeDurationLabelLeft.AutoSize = true;
            this.ServiceTimeDurationLabelLeft.Location = new System.Drawing.Point(5, 94);
            this.ServiceTimeDurationLabelLeft.Name = "ServiceTimeDurationLabelLeft";
            this.ServiceTimeDurationLabelLeft.Size = new System.Drawing.Size(122, 13);
            this.ServiceTimeDurationLabelLeft.TabIndex = 6;
            this.ServiceTimeDurationLabelLeft.Text = "Срок оказания услуги:";
            // 
            // GovernmentStructureLabelLeft
            // 
            this.GovernmentStructureLabelLeft.AutoSize = true;
            this.GovernmentStructureLabelLeft.Location = new System.Drawing.Point(5, 71);
            this.GovernmentStructureLabelLeft.Name = "GovernmentStructureLabelLeft";
            this.GovernmentStructureLabelLeft.Size = new System.Drawing.Size(145, 13);
            this.GovernmentStructureLabelLeft.TabIndex = 5;
            this.GovernmentStructureLabelLeft.Text = "Ответственное ведомство:";
            // 
            // DocumentsListLabelLeft
            // 
            this.DocumentsListLabelLeft.AutoSize = true;
            this.DocumentsListLabelLeft.Location = new System.Drawing.Point(5, 117);
            this.DocumentsListLabelLeft.Name = "DocumentsListLabelLeft";
            this.DocumentsListLabelLeft.Size = new System.Drawing.Size(122, 13);
            this.DocumentsListLabelLeft.TabIndex = 4;
            this.DocumentsListLabelLeft.Text = "Перечень документов:";
            // 
            // ServiceTitleLabelLeft
            // 
            this.ServiceTitleLabelLeft.AutoSize = true;
            this.ServiceTitleLabelLeft.Location = new System.Drawing.Point(5, 48);
            this.ServiceTitleLabelLeft.Name = "ServiceTitleLabelLeft";
            this.ServiceTitleLabelLeft.Size = new System.Drawing.Size(122, 13);
            this.ServiceTitleLabelLeft.TabIndex = 3;
            this.ServiceTitleLabelLeft.Text = "Наименование услуги:";
            // 
            // ServiceTypeLabelLeft
            // 
            this.ServiceTypeLabelLeft.AutoSize = true;
            this.ServiceTypeLabelLeft.Location = new System.Drawing.Point(5, 25);
            this.ServiceTypeLabelLeft.Name = "ServiceTypeLabelLeft";
            this.ServiceTypeLabelLeft.Size = new System.Drawing.Size(99, 13);
            this.ServiceTypeLabelLeft.TabIndex = 2;
            this.ServiceTypeLabelLeft.Text = "Категория услуги:";
            // 
            // EditServiceButton
            // 
            this.EditServiceButton.Location = new System.Drawing.Point(480, 471);
            this.EditServiceButton.Name = "EditServiceButton";
            this.EditServiceButton.Size = new System.Drawing.Size(120, 23);
            this.EditServiceButton.TabIndex = 1;
            this.EditServiceButton.Text = "Изменить";
            this.EditServiceButton.UseVisualStyleBackColor = true;
            // 
            // CreateAppointmentButton
            // 
            this.CreateAppointmentButton.Location = new System.Drawing.Point(357, 471);
            this.CreateAppointmentButton.Name = "CreateAppointmentButton";
            this.CreateAppointmentButton.Size = new System.Drawing.Size(120, 23);
            this.CreateAppointmentButton.TabIndex = 0;
            this.CreateAppointmentButton.Text = "Создать запись";
            this.CreateAppointmentButton.UseVisualStyleBackColor = true;
            // 
            // SearchServicesTextBox
            // 
            this.SearchServicesTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SearchServicesTextBox.Location = new System.Drawing.Point(6, 7);
            this.SearchServicesTextBox.Name = "SearchServicesTextBox";
            this.SearchServicesTextBox.Size = new System.Drawing.Size(229, 20);
            this.SearchServicesTextBox.TabIndex = 2;
            // 
            // AddServiceButton
            // 
            this.AddServiceButton.Location = new System.Drawing.Point(7, 478);
            this.AddServiceButton.Name = "AddServiceButton";
            this.AddServiceButton.Size = new System.Drawing.Size(228, 23);
            this.AddServiceButton.TabIndex = 1;
            this.AddServiceButton.Text = "Добавить услугу";
            this.AddServiceButton.UseVisualStyleBackColor = true;
            // 
            // SearchServicesPanel
            // 
            this.SearchServicesPanel.BackColor = System.Drawing.SystemColors.Control;
            this.SearchServicesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchServicesPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SearchServicesPanel.Location = new System.Drawing.Point(6, 33);
            this.SearchServicesPanel.Name = "SearchServicesPanel";
            this.SearchServicesPanel.Size = new System.Drawing.Size(229, 438);
            this.SearchServicesPanel.TabIndex = 0;
            // 
            // AppointmentsPage
            // 
            this.AppointmentsPage.Controls.Add(this.AppointmentPropertiesPanel);
            this.AppointmentsPage.Controls.Add(this.SearchAppointmentsTextBox);
            this.AppointmentsPage.Controls.Add(this.SearchAppointmentsPanel);
            this.AppointmentsPage.Location = new System.Drawing.Point(4, 22);
            this.AppointmentsPage.Name = "AppointmentsPage";
            this.AppointmentsPage.Padding = new System.Windows.Forms.Padding(3);
            this.AppointmentsPage.Size = new System.Drawing.Size(851, 510);
            this.AppointmentsPage.TabIndex = 1;
            this.AppointmentsPage.Text = "Записи";
            this.AppointmentsPage.UseVisualStyleBackColor = true;
            // 
            // AppointmentPropertiesPanel
            // 
            this.AppointmentPropertiesPanel.BackColor = System.Drawing.SystemColors.Control;
            this.AppointmentPropertiesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentDaytimeLabel);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentDaytimeLabelLeft);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentDateLabel);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentNationalityLabel);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentNationalityLabelLeft);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentDateLabelLeft);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentClientPatronymicLabel);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentClientPatronymicLabelLeft);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentClientNameLabel);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentClientSurnameLabel);
            this.AppointmentPropertiesPanel.Controls.Add(this.PassportNumberLabel);
            this.AppointmentPropertiesPanel.Controls.Add(this.PassportSeriesLabel);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentServiceTitleLabel);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentClientSurnameLabelLeft);
            this.AppointmentPropertiesPanel.Controls.Add(this.PassportNumberLabelLeft);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentClientNameLabelLeft);
            this.AppointmentPropertiesPanel.Controls.Add(this.PassportSeriesLabelLeft);
            this.AppointmentPropertiesPanel.Controls.Add(this.AppointmentServiceTitleLabelLeft);
            this.AppointmentPropertiesPanel.Controls.Add(this.EditAppointmentButton);
            this.AppointmentPropertiesPanel.Controls.Add(this.DeleteAppointmentButton);
            this.AppointmentPropertiesPanel.Location = new System.Drawing.Point(241, 7);
            this.AppointmentPropertiesPanel.Name = "AppointmentPropertiesPanel";
            this.AppointmentPropertiesPanel.Size = new System.Drawing.Size(604, 497);
            this.AppointmentPropertiesPanel.TabIndex = 6;
            // 
            // AppointmentClientNameLabel
            // 
            this.AppointmentClientNameLabel.AutoSize = true;
            this.AppointmentClientNameLabel.Location = new System.Drawing.Point(185, 117);
            this.AppointmentClientNameLabel.Name = "AppointmentClientNameLabel";
            this.AppointmentClientNameLabel.Size = new System.Drawing.Size(16, 13);
            this.AppointmentClientNameLabel.TabIndex = 11;
            this.AppointmentClientNameLabel.Text = "...";
            // 
            // AppointmentClientSurnameLabel
            // 
            this.AppointmentClientSurnameLabel.AutoSize = true;
            this.AppointmentClientSurnameLabel.Location = new System.Drawing.Point(185, 94);
            this.AppointmentClientSurnameLabel.Name = "AppointmentClientSurnameLabel";
            this.AppointmentClientSurnameLabel.Size = new System.Drawing.Size(16, 13);
            this.AppointmentClientSurnameLabel.TabIndex = 10;
            this.AppointmentClientSurnameLabel.Text = "...";
            // 
            // PassportNumberLabel
            // 
            this.PassportNumberLabel.AutoSize = true;
            this.PassportNumberLabel.Location = new System.Drawing.Point(185, 71);
            this.PassportNumberLabel.Name = "PassportNumberLabel";
            this.PassportNumberLabel.Size = new System.Drawing.Size(16, 13);
            this.PassportNumberLabel.TabIndex = 9;
            this.PassportNumberLabel.Text = "...";
            // 
            // PassportSeriesLabel
            // 
            this.PassportSeriesLabel.AutoSize = true;
            this.PassportSeriesLabel.Location = new System.Drawing.Point(185, 48);
            this.PassportSeriesLabel.Name = "PassportSeriesLabel";
            this.PassportSeriesLabel.Size = new System.Drawing.Size(16, 13);
            this.PassportSeriesLabel.TabIndex = 8;
            this.PassportSeriesLabel.Text = "...";
            // 
            // AppointmentServiceTitleLabel
            // 
            this.AppointmentServiceTitleLabel.AutoSize = true;
            this.AppointmentServiceTitleLabel.Location = new System.Drawing.Point(185, 25);
            this.AppointmentServiceTitleLabel.Name = "AppointmentServiceTitleLabel";
            this.AppointmentServiceTitleLabel.Size = new System.Drawing.Size(16, 13);
            this.AppointmentServiceTitleLabel.TabIndex = 7;
            this.AppointmentServiceTitleLabel.Text = "...";
            // 
            // AppointmentClientSurnameLabelLeft
            // 
            this.AppointmentClientSurnameLabelLeft.AutoSize = true;
            this.AppointmentClientSurnameLabelLeft.Location = new System.Drawing.Point(5, 94);
            this.AppointmentClientSurnameLabelLeft.Name = "AppointmentClientSurnameLabelLeft";
            this.AppointmentClientSurnameLabelLeft.Size = new System.Drawing.Size(103, 13);
            this.AppointmentClientSurnameLabelLeft.TabIndex = 6;
            this.AppointmentClientSurnameLabelLeft.Text = "Фамилия клиента:";
            // 
            // PassportNumberLabelLeft
            // 
            this.PassportNumberLabelLeft.AutoSize = true;
            this.PassportNumberLabelLeft.Location = new System.Drawing.Point(5, 71);
            this.PassportNumberLabelLeft.Name = "PassportNumberLabelLeft";
            this.PassportNumberLabelLeft.Size = new System.Drawing.Size(94, 13);
            this.PassportNumberLabelLeft.TabIndex = 5;
            this.PassportNumberLabelLeft.Text = "Номер паспорта:";
            // 
            // AppointmentClientNameLabelLeft
            // 
            this.AppointmentClientNameLabelLeft.AutoSize = true;
            this.AppointmentClientNameLabelLeft.Location = new System.Drawing.Point(5, 117);
            this.AppointmentClientNameLabelLeft.Name = "AppointmentClientNameLabelLeft";
            this.AppointmentClientNameLabelLeft.Size = new System.Drawing.Size(76, 13);
            this.AppointmentClientNameLabelLeft.TabIndex = 4;
            this.AppointmentClientNameLabelLeft.Text = "Имя клиента:";
            // 
            // PassportSeriesLabelLeft
            // 
            this.PassportSeriesLabelLeft.AutoSize = true;
            this.PassportSeriesLabelLeft.Location = new System.Drawing.Point(5, 48);
            this.PassportSeriesLabelLeft.Name = "PassportSeriesLabelLeft";
            this.PassportSeriesLabelLeft.Size = new System.Drawing.Size(91, 13);
            this.PassportSeriesLabelLeft.TabIndex = 3;
            this.PassportSeriesLabelLeft.Text = "Серия паспорта:";
            // 
            // AppointmentServiceTitleLabelLeft
            // 
            this.AppointmentServiceTitleLabelLeft.AutoSize = true;
            this.AppointmentServiceTitleLabelLeft.Location = new System.Drawing.Point(5, 25);
            this.AppointmentServiceTitleLabelLeft.Name = "AppointmentServiceTitleLabelLeft";
            this.AppointmentServiceTitleLabelLeft.Size = new System.Drawing.Size(122, 13);
            this.AppointmentServiceTitleLabelLeft.TabIndex = 2;
            this.AppointmentServiceTitleLabelLeft.Text = "Наименование услуги:";
            // 
            // EditAppointmentButton
            // 
            this.EditAppointmentButton.Location = new System.Drawing.Point(459, 469);
            this.EditAppointmentButton.Name = "EditAppointmentButton";
            this.EditAppointmentButton.Size = new System.Drawing.Size(140, 23);
            this.EditAppointmentButton.TabIndex = 1;
            this.EditAppointmentButton.Text = "Изменить дату/время";
            this.EditAppointmentButton.UseVisualStyleBackColor = true;
            // 
            // DeleteAppointmentButton
            // 
            this.DeleteAppointmentButton.Location = new System.Drawing.Point(316, 469);
            this.DeleteAppointmentButton.Name = "DeleteAppointmentButton";
            this.DeleteAppointmentButton.Size = new System.Drawing.Size(140, 23);
            this.DeleteAppointmentButton.TabIndex = 0;
            this.DeleteAppointmentButton.Text = "Удалить запись";
            this.DeleteAppointmentButton.UseVisualStyleBackColor = true;
            // 
            // SearchAppointmentsTextBox
            // 
            this.SearchAppointmentsTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SearchAppointmentsTextBox.Location = new System.Drawing.Point(6, 7);
            this.SearchAppointmentsTextBox.Name = "SearchAppointmentsTextBox";
            this.SearchAppointmentsTextBox.Size = new System.Drawing.Size(229, 20);
            this.SearchAppointmentsTextBox.TabIndex = 5;
            // 
            // SearchAppointmentsPanel
            // 
            this.SearchAppointmentsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.SearchAppointmentsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchAppointmentsPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SearchAppointmentsPanel.Location = new System.Drawing.Point(6, 33);
            this.SearchAppointmentsPanel.Name = "SearchAppointmentsPanel";
            this.SearchAppointmentsPanel.Size = new System.Drawing.Size(229, 471);
            this.SearchAppointmentsPanel.TabIndex = 4;
            // 
            // ServedAppointmentsPage
            // 
            this.ServedAppointmentsPage.Controls.Add(this.SearchAppointmentsHistoryPanel);
            this.ServedAppointmentsPage.Controls.Add(this.SearchAppointmentsHistoryTextBox);
            this.ServedAppointmentsPage.Location = new System.Drawing.Point(4, 22);
            this.ServedAppointmentsPage.Name = "ServedAppointmentsPage";
            this.ServedAppointmentsPage.Size = new System.Drawing.Size(851, 510);
            this.ServedAppointmentsPage.TabIndex = 2;
            this.ServedAppointmentsPage.Text = "История услуг";
            this.ServedAppointmentsPage.UseVisualStyleBackColor = true;
            // 
            // SearchAppointmentsHistoryPanel
            // 
            this.SearchAppointmentsHistoryPanel.Location = new System.Drawing.Point(3, 34);
            this.SearchAppointmentsHistoryPanel.Name = "SearchAppointmentsHistoryPanel";
            this.SearchAppointmentsHistoryPanel.Size = new System.Drawing.Size(845, 473);
            this.SearchAppointmentsHistoryPanel.TabIndex = 1;
            // 
            // SearchAppointmentsHistoryTextBox
            // 
            this.SearchAppointmentsHistoryTextBox.Location = new System.Drawing.Point(3, 8);
            this.SearchAppointmentsHistoryTextBox.Name = "SearchAppointmentsHistoryTextBox";
            this.SearchAppointmentsHistoryTextBox.Size = new System.Drawing.Size(254, 20);
            this.SearchAppointmentsHistoryTextBox.TabIndex = 0;
            // 
            // UpdateAppointmentsHistoryTimer
            // 
            this.UpdateAppointmentsHistoryTimer.Enabled = true;
            this.UpdateAppointmentsHistoryTimer.Interval = 60000;
            // 
            // AppointmentClientPatronymicLabel
            // 
            this.AppointmentClientPatronymicLabel.AutoSize = true;
            this.AppointmentClientPatronymicLabel.Location = new System.Drawing.Point(185, 140);
            this.AppointmentClientPatronymicLabel.Name = "AppointmentClientPatronymicLabel";
            this.AppointmentClientPatronymicLabel.Size = new System.Drawing.Size(16, 13);
            this.AppointmentClientPatronymicLabel.TabIndex = 13;
            this.AppointmentClientPatronymicLabel.Text = "...";
            // 
            // AppointmentClientPatronymicLabelLeft
            // 
            this.AppointmentClientPatronymicLabelLeft.AutoSize = true;
            this.AppointmentClientPatronymicLabelLeft.Location = new System.Drawing.Point(5, 140);
            this.AppointmentClientPatronymicLabelLeft.Name = "AppointmentClientPatronymicLabelLeft";
            this.AppointmentClientPatronymicLabelLeft.Size = new System.Drawing.Size(101, 13);
            this.AppointmentClientPatronymicLabelLeft.TabIndex = 12;
            this.AppointmentClientPatronymicLabelLeft.Text = "Отчество клиента:";
            // 
            // AppointmentDaytimeLabel
            // 
            this.AppointmentDaytimeLabel.AutoSize = true;
            this.AppointmentDaytimeLabel.Location = new System.Drawing.Point(185, 209);
            this.AppointmentDaytimeLabel.Name = "AppointmentDaytimeLabel";
            this.AppointmentDaytimeLabel.Size = new System.Drawing.Size(16, 13);
            this.AppointmentDaytimeLabel.TabIndex = 19;
            this.AppointmentDaytimeLabel.Text = "...";
            // 
            // AppointmentDaytimeLabelLeft
            // 
            this.AppointmentDaytimeLabelLeft.AutoSize = true;
            this.AppointmentDaytimeLabelLeft.Location = new System.Drawing.Point(5, 209);
            this.AppointmentDaytimeLabelLeft.Name = "AppointmentDaytimeLabelLeft";
            this.AppointmentDaytimeLabelLeft.Size = new System.Drawing.Size(82, 13);
            this.AppointmentDaytimeLabelLeft.TabIndex = 18;
            this.AppointmentDaytimeLabelLeft.Text = "Время записи:";
            // 
            // AppointmentDateLabel
            // 
            this.AppointmentDateLabel.AutoSize = true;
            this.AppointmentDateLabel.Location = new System.Drawing.Point(185, 186);
            this.AppointmentDateLabel.Name = "AppointmentDateLabel";
            this.AppointmentDateLabel.Size = new System.Drawing.Size(16, 13);
            this.AppointmentDateLabel.TabIndex = 17;
            this.AppointmentDateLabel.Text = "...";
            // 
            // AppointmentNationalityLabel
            // 
            this.AppointmentNationalityLabel.AutoSize = true;
            this.AppointmentNationalityLabel.Location = new System.Drawing.Point(185, 163);
            this.AppointmentNationalityLabel.Name = "AppointmentNationalityLabel";
            this.AppointmentNationalityLabel.Size = new System.Drawing.Size(16, 13);
            this.AppointmentNationalityLabel.TabIndex = 16;
            this.AppointmentNationalityLabel.Text = "...";
            // 
            // AppointmentNationalityLabelLeft
            // 
            this.AppointmentNationalityLabelLeft.AutoSize = true;
            this.AppointmentNationalityLabelLeft.Location = new System.Drawing.Point(5, 163);
            this.AppointmentNationalityLabelLeft.Name = "AppointmentNationalityLabelLeft";
            this.AppointmentNationalityLabelLeft.Size = new System.Drawing.Size(77, 13);
            this.AppointmentNationalityLabelLeft.TabIndex = 15;
            this.AppointmentNationalityLabelLeft.Text = "Гражданство:";
            // 
            // AppointmentDateLabelLeft
            // 
            this.AppointmentDateLabelLeft.AutoSize = true;
            this.AppointmentDateLabelLeft.Location = new System.Drawing.Point(5, 186);
            this.AppointmentDateLabelLeft.Name = "AppointmentDateLabelLeft";
            this.AppointmentDateLabelLeft.Size = new System.Drawing.Size(75, 13);
            this.AppointmentDateLabelLeft.TabIndex = 14;
            this.AppointmentDateLabelLeft.Text = "Дата записи:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.MainFormTabControl);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(900, 600);
            this.MinimumSize = new System.Drawing.Size(900, 600);
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
        private System.Windows.Forms.Label ServiceTypeLabelLeft;
        private System.Windows.Forms.Label DocumentsListLabelLeft;
        private System.Windows.Forms.Label ServiceTitleLabelLeft;
        private System.Windows.Forms.Label GovernmentStructureLabelLeft;
        private System.Windows.Forms.Label ServiceTimeDurationLabelLeft;
        private System.Windows.Forms.Label DocumentsListLabel;
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
        private System.Windows.Forms.Label AppointmentClientSurnameLabelLeft;
        private System.Windows.Forms.Label PassportNumberLabelLeft;
        private System.Windows.Forms.Label AppointmentClientNameLabelLeft;
        private System.Windows.Forms.Label PassportSeriesLabelLeft;
        private System.Windows.Forms.Label AppointmentServiceTitleLabelLeft;
        private System.Windows.Forms.Button EditAppointmentButton;
        private System.Windows.Forms.Button DeleteAppointmentButton;
        private System.Windows.Forms.TextBox SearchAppointmentsTextBox;
        private System.Windows.Forms.Panel SearchAppointmentsPanel;
        private System.Windows.Forms.Panel SearchAppointmentsHistoryPanel;
        private System.Windows.Forms.TextBox SearchAppointmentsHistoryTextBox;
        private System.Windows.Forms.Label AppointmentClientPatronymicLabel;
        private System.Windows.Forms.Label AppointmentClientPatronymicLabelLeft;
        private System.Windows.Forms.Label AppointmentDaytimeLabel;
        private System.Windows.Forms.Label AppointmentDaytimeLabelLeft;
        private System.Windows.Forms.Label AppointmentDateLabel;
        private System.Windows.Forms.Label AppointmentNationalityLabel;
        private System.Windows.Forms.Label AppointmentNationalityLabelLeft;
        private System.Windows.Forms.Label AppointmentDateLabelLeft;
    }
}

