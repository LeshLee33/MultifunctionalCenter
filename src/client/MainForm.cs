using ApiClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ApiClient.ApiFunctions;
using static CardBuilders;
using static PanelCardLayout;
using static ServiceCardManager;

namespace client
{
    public partial class MainForm : Form
    {
        private Timer _appointmentsSearchTimer;
        private Timer _servedAppointmentsSearchTimer;
        private bool _isInitialized = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeSearchTimers();

            this.Load += MainForm_Load;
        }

        private void InitializeSearchTimers()
        {
            // Таймер для поиска по паспорту в записях
            _appointmentsSearchTimer = new Timer { Interval = 500 };
            _appointmentsSearchTimer.Tick += (s, e) =>
            {
                _appointmentsSearchTimer.Stop();
                if (_isInitialized)
                {
                    SearchAppointmentsByPassportAsync();
                }
            };

            // Таймер для поиска по паспорту в истории
            _servedAppointmentsSearchTimer = new Timer { Interval = 500 };
            _servedAppointmentsSearchTimer.Tick += (s, e) =>
            {
                _servedAppointmentsSearchTimer.Stop();
                if (_isInitialized)
                {
                    SearchServedAppointmentsByPassportAsync();
                }
            };
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            bool isConnected = await StartupApiChecker.EnsureApiConnectionBeforeStartup();

            if (!isConnected)
            {
                this.Close();
                return;
            }

            CardBuilders.SetDetailPanels(servicePanel: ServicePropertiesPanel, appointmentPanel: AppointmentPropertiesPanel);
            ServiceCardManager.InitializeSearchHandler(SearchServicesTextBox, SearchServicesPanel);
            InitializeAppointmentsSearchHandler();
            InitializeServedAppointmentsSearchHandler();
            await InitializeAllPanelsAsync();
            _isInitialized = true;
        }

        private void InitializeAppointmentsSearchHandler()
        {
            SearchAppointmentsTextBox.TextChanged += (s, e) =>
            {
                _appointmentsSearchTimer.Stop();
                _appointmentsSearchTimer.Start();
            };
        }

        private void InitializeServedAppointmentsSearchHandler()
        {
            SearchAppointmentsHistoryTextBox.TextChanged += (s, e) =>
            {
                _servedAppointmentsSearchTimer.Stop();
                _servedAppointmentsSearchTimer.Start();
            };
        }

        private async Task InitializeAllPanelsAsync()
        {
            await SearchServicesAsync();
            await SearchAppointmentsAsync();
            await SearchServedAppointmentsAsync();
        }

        private async Task SearchServicesAsync()
        {
            await ServiceCardManager.LoadServicesIntoPanel(SearchServicesPanel);
            SearchServicesPanel.Resize += (s, args) =>
            {
                ServiceCardManager.HandlePanelResize(SearchServicesPanel);
            };
        }

        private async Task SearchAppointmentsAsync()
        {
            await AppointmentCardManager.LoadAllAppointments(SearchAppointmentsPanel);
            SearchAppointmentsPanel.Resize += (s, args) =>
            {
                AppointmentCardManager.HandlePanelResize(SearchAppointmentsPanel);
            };
        }

        private async Task SearchServedAppointmentsAsync()
        {
            await ServedAppointmentCardManager.LoadAllServedAppointments(SearchAppointmentsHistoryPanel);
            SearchAppointmentsHistoryPanel.Resize += (s, args) =>
            {
                ServedAppointmentCardManager.HandlePanelResize(SearchAppointmentsHistoryPanel);
            };
        }

        private async Task SearchAppointmentsByPassportAsync()
        {
            string searchText = SearchAppointmentsTextBox.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                await AppointmentCardManager.LoadAllAppointments(SearchAppointmentsPanel);
            }
            else
            {
                var passportData = ParsePassportInput(searchText);
                await AppointmentCardManager.LoadAppointmentsByPassport(
                    SearchAppointmentsPanel,
                    passportData.Item1,
                    passportData.Item2);
            }
        }

        private async Task SearchServedAppointmentsByPassportAsync()
        {
            string searchText = SearchAppointmentsHistoryTextBox.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                await ServedAppointmentCardManager.LoadAllServedAppointments(SearchAppointmentsHistoryPanel);
            }
            else
            {
                var passportData = ParsePassportInput(searchText);
                await ServedAppointmentCardManager.LoadServedAppointmentsByPassport(
                    SearchAppointmentsHistoryPanel,
                    passportData.Item1,
                    passportData.Item2);
            }
        }

        private (string passportNumber, string passportSeries) ParsePassportInput(string input)
        {
            input = input.Trim();

            if (string.IsNullOrEmpty(input))
            {
                return (string.Empty, string.Empty);
            }

            // Разбиваем по пробелам и удаляем пустые элементы
            var parts = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                return (string.Empty, string.Empty);
            }

            if (parts.Length == 1)
            {
                // Только номер паспорта
                string passportNumber = parts[0].Trim();
                return (passportNumber, string.Empty);
            }
            else if (parts.Length >= 2)
            {
                // Серия и номер паспорта
                // Берем последнюю часть как номер, все остальное как серию
                string passportNumber = parts[parts.Length - 1].Trim();
                string passportSeries = string.Join(" ", parts.Take(parts.Length - 1)).Trim();

                return (passportNumber, passportSeries);
            }

            return (string.Empty, string.Empty);
        }

        private async void ButtonAddService_Click(object sender, EventArgs e)
        {
            using (var form = new AddServiceForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Service newService = form.CreatedService;
                    await SearchServicesAsync();
                }
            }
        }

        private async void EditServiceButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedService = CardBuilders.GetSelectedService();

                if (selectedService == null)
                {
                    selectedService = CreateServiceFromPanelDetails();
                }

                if (selectedService == null)
                {
                    MessageBox.Show("Сначала выберите услугу, нажав на её карточку.",
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Console.WriteLine($"Редактируем услугу: {selectedService.ServiceTitle}");

                var query = new Dictionary<string, object>
                {
                    ["service_type"] = selectedService.ServiceType ?? "",
                    ["service_title"] = selectedService.ServiceTitle ?? "",
                    ["service_required_documents"] = selectedService.ServiceRequiredDocuments ?? new List<string>(),
                    ["service_government_structure"] = selectedService.ServiceGovernmentStructure ?? "",
                    ["service_duration_days"] = selectedService.ServiceDurationDays
                };

                using (var form = new EditServiceForm(selectedService, query))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        await ServiceCardManager.LoadAllServices(SearchServicesPanel);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании услуги: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ApiClient.Service CreateServiceFromPanelDetails()
        {
            try
            {
                if (string.IsNullOrEmpty(ServiceTypeLabel.Text) || ServiceTypeLabel.Text == "..." ||
                    string.IsNullOrEmpty(ServiceTitleLabel.Text) || ServiceTitleLabel.Text == "...")
                {
                    return null;
                }

                List<string> documents = new List<string>();
                if (DocumentsListLabel.Text != "..." && !string.IsNullOrEmpty(DocumentsListLabel.Text))
                {
                    documents = ParseDocumentsRecursive(DocumentsListLabel.Text, new List<string>());
                }

                int duration = 0;
                if (!int.TryParse(ServiceTimeDurationLabel.Text, out duration))
                {
                    duration = 0;
                }

                return new ApiClient.Service(
                    serviceType: ServiceTypeLabel.Text,
                    serviceTitle: ServiceTitleLabel.Text,
                    serviceRequiredDocuments: documents,
                    serviceGovernmentStructure: GovernmentStructureLabel.Text,
                    serviceDurationDays: duration
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании услуги из панели: {ex.Message}");
                return null;
            }
        }

        private List<string> ParseDocumentsRecursive(string documentText, List<string> result, int startIndex = 0)
        {
            if (string.IsNullOrEmpty(documentText) || startIndex >= documentText.Length)
                return result;

            int nextComma = documentText.IndexOf(',', startIndex);

            if (nextComma == -1)
            {
                string document = documentText.Substring(startIndex).Trim();
                if (!string.IsNullOrEmpty(document))
                {
                    result.Add(document);
                }
                return result;
            }

            string documentPart = documentText.Substring(startIndex, nextComma - startIndex).Trim();
            if (!string.IsNullOrEmpty(documentPart))
            {
                result.Add(documentPart);
            }

            return ParseDocumentsRecursive(documentText, result, nextComma + 1);
        }

        private void CreateAppointmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedService = CardBuilders.GetSelectedService();

                if (selectedService == null)
                {
                    selectedService = CreateServiceFromPanelDetails();
                }

                if (selectedService == null)
                {
                    MessageBox.Show("Сначала выберите услугу, нажав на её карточку.",
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var form = new CreateAppointmentForm(selectedService.ServiceTitle))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        SearchAppointmentsAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании записи: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditAppointmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedAppointment = GetSelectedAppointment();

                if (selectedAppointment == null)
                {
                    MessageBox.Show("Сначала выберите запись, нажав на её карточку.",
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var form = new EditAppointmentForm(selectedAppointment))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        SearchAppointmentsAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении записи: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DeleteAppointmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedAppointment = GetSelectedAppointment();

                if (selectedAppointment == null)
                {
                    MessageBox.Show("Сначала выберите запись, нажав на её карточку.",
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить запись?\n\n" +
                    $"Услуга: {selectedAppointment.AppointmentServiceTitle}\n" +
                    $"Клиент: {selectedAppointment.AppointmentClientSurname} {selectedAppointment.AppointmentClientName}\n" +
                    $"Дата: {selectedAppointment.AppointmentDate} {selectedAppointment.AppointmentDaytime}",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    DeleteAppointmentButton.Enabled = false;
                    EditAppointmentButton.Enabled = false;
                    Cursor = Cursors.WaitCursor;

                    try
                    {
                        var appointment = new ApiClient.Appointment(
                            appointmentServiceTitle: selectedAppointment.AppointmentServiceTitle,
                            appointmentClientPassportSeries: selectedAppointment.AppointmentClientPassportSeries,
                            appointmentClientPassportNumber: selectedAppointment.AppointmentClientPassportNumber,
                            appointmentClientSurname: selectedAppointment.AppointmentClientSurname,
                            appointmentClientName: selectedAppointment.AppointmentClientName,
                            appointmentClientPatronymic: selectedAppointment.AppointmentClientPatronymic,
                            appointmentNationality: selectedAppointment.AppointmentNationality,
                            appointmentDate: selectedAppointment.AppointmentDate,
                            appointmentDaytime: selectedAppointment.AppointmentDaytime
                        );

                        var deleteResult = await ApiClient.ApiFunctions.DeleteAppointment(appointment);

                        if (deleteResult.Contains("Ошибка") || deleteResult.IndexOf("error", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            MessageBox.Show($"Ошибка при удалении записи:\n{deleteResult}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Запись успешно удалена!",
                                "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ClearAppointmentDetailsPanel();

                            await SearchAppointmentsAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении записи: {ex.Message}",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        DeleteAppointmentButton.Enabled = true;
                        EditAppointmentButton.Enabled = true;
                        Cursor = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Appointment GetSelectedAppointment()
        {
            try
            {
                if (AppointmentServiceTitleLabel.Text != "..." &&
                    !string.IsNullOrEmpty(AppointmentServiceTitleLabel.Text))
                {
                    return new Appointment
                    {
                        AppointmentServiceTitle = AppointmentServiceTitleLabel.Text,
                        AppointmentClientPassportSeries = PassportSeriesLabel.Text,
                        AppointmentClientPassportNumber = PassportNumberLabel.Text,
                        AppointmentClientSurname = AppointmentClientSurnameLabel.Text,
                        AppointmentClientName = AppointmentClientNameLabel.Text,
                        AppointmentClientPatronymic = AppointmentClientPatronymicLabel.Text,
                        AppointmentNationality = AppointmentNationalityLabel.Text,
                        AppointmentDate = AppointmentDateLabel.Text,
                        AppointmentDaytime = AppointmentDaytimeLabel.Text
                    };
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private void ClearAppointmentDetailsPanel()
        {
            ClearLabelRecursive(AppointmentPropertiesPanel.Controls, 0);
        }

        private void ClearLabelRecursive(Control.ControlCollection controls, int index)
        {
            if (index >= controls.Count) return;

            var control = controls[index];
            if (control.Name != null && control.Name.EndsWith("Label", StringComparison.OrdinalIgnoreCase))
            {
                if (control is Label label)
                {
                    label.Text = "...";
                }
                else if (control is TextBox textBox)
                {
                    textBox.Text = "...";
                }
            }

            ClearLabelRecursive(controls, index + 1);
        }

        public async void RefreshServices(object sender, EventArgs e)
        {
            SearchServicesTextBox.Text = "";
            await SearchServicesAsync();
        }

        public async void RefreshAppointments(object sender, EventArgs e)
        {
            SearchAppointmentsTextBox.Text = "";
            await SearchAppointmentsAsync();
        }

        public async void RefreshAppointmentsHistory(object sender, EventArgs e)
        {
            SearchAppointmentsHistoryTextBox.Text = "";
            await SearchServedAppointmentsAsync();
        }

        private async void StartAppointmentExpirationChecking()
        {
            await ApiClient.ApiFunctions.CheckAppointmentExpiration();
            await SearchAppointmentsAsync();
            await SearchServedAppointmentsAsync();
        }

        public void AppointmentsExpirationController_Timer(object sender, EventArgs e)
        {
            if (_isInitialized)
            {
                StartAppointmentExpirationChecking();
            }
        }
    }
}