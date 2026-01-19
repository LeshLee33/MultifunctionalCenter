using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Forms;

namespace client
{
    public partial class AddServiceForm : Form
    {
        public Service CreatedService { get; private set; }

        public AddServiceForm()
        {
            InitializeComponent();
        }

        private async void ButtonConfirm_Click(object sender, EventArgs e)
        {
            btnConfirm.Enabled = false;
            btnCancel.Enabled = false;

            try
            {
                if (string.IsNullOrWhiteSpace(txtType.Text) ||
                    string.IsNullOrWhiteSpace(txtTitle.Text) ||
                    string.IsNullOrWhiteSpace(txtGovStructure.Text) ||
                    string.IsNullOrWhiteSpace(txtDuration.Text))
                {
                    MessageBox.Show("Тип, наименование, ведомство и срок обязательны для заполнения.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtDuration.Text, out int duration) || duration <= 0)
                {
                    MessageBox.Show("Срок исполнения должен быть положительным числом.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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

                Cursor = Cursors.WaitCursor;

                var newService = new ApiClient.Service(
                    serviceType: txtType.Text.Trim(),
                    serviceTitle: txtTitle.Text.Trim(),
                    serviceRequiredDocuments: documents,
                    serviceGovernmentStructure: txtGovStructure.Text.Trim(),
                    serviceDurationDays: duration
                );

                var result = await ApiClient.ApiFunctions.AddService(newService);

                if (result.Contains("Ошибка"))
                {
                    MessageBox.Show($"Ошибка при добавлении услуги:\n{result}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Услуга успешно добавлена!",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
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

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}