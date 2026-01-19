using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class EditServiceForm : Form
    {
        private readonly ApiClient.Service _originalService;
        private readonly Dictionary<string, object> _query;

        public EditServiceForm(ApiClient.Service service, Dictionary<string, object> query)
        {
            InitializeComponent();
            _originalService = service ?? throw new ArgumentNullException(nameof(service));
            _query = query ?? throw new ArgumentNullException(nameof(query));

            FillFormWithCurrentValues();
        }

        private void FillFormWithCurrentValues()
        {
            try
            {
                txtType.Text = _originalService.ServiceType ?? "";
                txtTitle.Text = _originalService.ServiceTitle ?? "";
                txtGovStructure.Text = _originalService.ServiceGovernmentStructure ?? "";
                txtDuration.Text = _originalService.ServiceDurationDays.ToString();

                if (_originalService.ServiceRequiredDocuments != null &&
                    _originalService.ServiceRequiredDocuments.Count > 0)
                {
                    txtDocuments.Text = string.Join(", ", _originalService.ServiceRequiredDocuments);
                }
                else
                {
                    txtDocuments.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных услуги: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ButtonConfirm_Click(object sender, EventArgs e)
        {
            btnConfirm.Enabled = false;
            btnCancel.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                if (!ValidateInput())
                {
                    return;
                }

                var documents = PrepareDocuments();
                if (!int.TryParse(txtDuration.Text, out int duration))
                {
                    MessageBox.Show("Некорректное значение срока исполнения",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!HasChanges(documents, duration))
                {
                    MessageBox.Show("Нет изменений для сохранения",
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }

                var newData = new Dictionary<string, object>
                {
                    ["service_type"] = txtType.Text.Trim(),
                    ["service_title"] = txtTitle.Text.Trim(),
                    ["service_government_structure"] = txtGovStructure.Text.Trim(),
                    ["service_duration_days"] = duration,
                    ["service_required_documents"] = documents
                };

                var result = await ApiClient.ApiFunctions.EditServiceWithOriginal(_originalService, _query, newData);

                if (result.Contains("Ошибка 422") || result.Contains("422"))
                {
                    Console.WriteLine("Пробуем прямой вызов API...");
                    result = await EditServiceDirectCall(_originalService, _query, newData);
                }

                if (result.Contains("Ошибка") || result.IndexOf("error", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    MessageBox.Show($"Ошибка при редактировании услуги:\n{result}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Услуга успешно обновлена!",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Ошибка соединения с API: {httpEx.Message}",
                    "Сетевая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnConfirm.Enabled = true;
                btnCancel.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtType.Text))
            {
                MessageBox.Show("Тип услуги обязателен для заполнения",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtType.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Наименование услуги обязательно для заполнения",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtGovStructure.Text))
            {
                MessageBox.Show("Ответственное ведомство обязательно для заполнения",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGovStructure.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDuration.Text))
            {
                MessageBox.Show("Срок исполнения обязателен для заполнения",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDuration.Focus();
                return false;
            }

            if (!int.TryParse(txtDuration.Text, out int duration) || duration <= 0)
            {
                MessageBox.Show("Срок исполнения должен быть положительным целым числом",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDuration.Focus();
                txtDuration.SelectAll();
                return false;
            }

            return true;
        }

        private List<string> PrepareDocuments()
        {
            var documents = new List<string>();

            if (!string.IsNullOrWhiteSpace(txtDocuments.Text))
            {
                var docParts = txtDocuments.Text.Split(',');
                foreach (var part in docParts)
                {
                    var trimmed = part.Trim();
                    if (!string.IsNullOrWhiteSpace(trimmed))
                    {
                        documents.Add(trimmed);
                    }
                }
            }

            return documents;
        }

        private bool HasChanges(List<string> newDocuments, int newDuration)
        {
            if (txtType.Text.Trim() != _originalService.ServiceType ||
                txtTitle.Text.Trim() != _originalService.ServiceTitle ||
                txtGovStructure.Text.Trim() != _originalService.ServiceGovernmentStructure ||
                newDuration != _originalService.ServiceDurationDays)
            {
                return true;
            }

            var originalDocs = _originalService.ServiceRequiredDocuments ?? new List<string>();
            return !AreDocumentListsEqual(originalDocs, newDocuments);
        }

        private bool AreDocumentListsEqual(List<string> list1, List<string> list2)
        {
            if (list1 == null && list2 == null) return true;
            if (list1 == null || list2 == null) return false;
            if (list1.Count != list2.Count) return false;

            var sorted1 = new List<string>(list1);
            var sorted2 = new List<string>(list2);
            sorted1.Sort();
            sorted2.Sort();

            for (int i = 0; i < sorted1.Count; i++)
            {
                if (sorted1[i] != sorted2[i])
                    return false;
            }

            return true;
        }

        private async Task<string> EditServiceDirectCall(ApiClient.Service originalService, Dictionary<string, object> query, Dictionary<string, object> newData)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8000/");
                    client.Timeout = TimeSpan.FromSeconds(30);

                    var newDataService = new
                    {
                        service_type = newData.ContainsKey("service_type") ?
                            (newData["service_type"]?.ToString() ?? "") :
                            originalService.ServiceType ?? "",

                        service_title = newData.ContainsKey("service_title") ?
                            (newData["service_title"]?.ToString() ?? "") :
                            originalService.ServiceTitle ?? "",

                        service_required_documents = newData.ContainsKey("service_required_documents") ?
                            GetDocumentsList(newData["service_required_documents"]) :
                            originalService.ServiceRequiredDocuments ?? new List<string>(),

                        service_government_structure = newData.ContainsKey("service_government_structure") ?
                            (newData["service_government_structure"]?.ToString() ?? "") :
                            originalService.ServiceGovernmentStructure ?? "",

                        service_duration_days = newData.ContainsKey("service_duration_days") ?
                            GetDurationValue(newData["service_duration_days"]) :
                            originalService.ServiceDurationDays
                    };

                    var queryService = new
                    {
                        service_type = originalService.ServiceType ?? "",
                        service_title = originalService.ServiceTitle ?? "",
                        service_required_documents = originalService.ServiceRequiredDocuments ?? new List<string>(),
                        service_government_structure = originalService.ServiceGovernmentStructure ?? "",
                        service_duration_days = originalService.ServiceDurationDays
                    };

                    var requestData = new
                    {
                        query = queryService,
                        new_data = newDataService
                    };

                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = false
                    };

                    var json = JsonSerializer.Serialize(requestData, options);

                    Console.WriteLine($"Прямой вызов - Отправляемый JSON:\n{json}");

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await SendPatchRequest(client, "service/edit_service", content);

                    var responseText = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        return $"Ошибка {(int)response.StatusCode}: {responseText}";
                    }

                    return responseText;
                }
            }
            catch (Exception ex)
            {
                return $"Ошибка при прямом вызове API: {ex.Message}";
            }
        }

        private static async Task<HttpResponseMessage> SendPatchRequest(HttpClient client, string requestUri, HttpContent content)
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };
            return await client.SendAsync(request);
        }

        private static List<string> GetDocumentsList(object documentsObj)
        {
            if (documentsObj is List<string> documentsList)
                return documentsList;

            if (documentsObj is string documentsStr && !string.IsNullOrEmpty(documentsStr))
                return documentsStr.Split(',').Select(d => d.Trim()).ToList();

            return new List<string>();
        }

        private static int GetDurationValue(object durationObj)
        {
            if (durationObj is int intValue)
                return intValue;

            if (durationObj is long longValue)
                return (int)longValue;

            if (durationObj != null && int.TryParse(durationObj.ToString(), out int parsedValue))
                return parsedValue;

            return 0;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtDocuments_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(txtDocuments,
                "Введите наименования документов через запятую\nПример: Паспорт, Заявление, СНИЛС");
        }

        private void txtDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вернуть все значения к исходным?\nНесохраненные изменения будут потеряны.",
                "Сброс изменений", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                FillFormWithCurrentValues();
            }
        }
    }
}