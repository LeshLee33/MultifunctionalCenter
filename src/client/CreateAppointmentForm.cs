using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace client
{
    public partial class CreateAppointmentForm : Form
    {
        private readonly string _serviceTitle;

        public CreateAppointmentForm(string serviceTitle)
        {
            InitializeComponent();
            _serviceTitle = serviceTitle ?? throw new ArgumentNullException(nameof(serviceTitle));

            txtServiceTitle.Text = _serviceTitle;
            txtServiceTitle.ReadOnly = true;
            txtServiceTitle.BackColor = System.Drawing.SystemColors.Control;

            txtDate.Text = DateTime.Now.ToString("dd.MM.yyyy");

            txtTime.Text = "10:00";
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            btnCreate.Enabled = false;
            btnCancel.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                if (!ValidateInput())
                {
                    return;
                }

                var appointment = new ApiClient.Appointment(
                    appointmentServiceTitle: _serviceTitle,
                    appointmentClientPassportSeries: txtPassportSeries.Text.Trim(),
                    appointmentClientPassportNumber: txtPassportNumber.Text.Trim(),
                    appointmentClientSurname: txtSurname.Text.Trim(),
                    appointmentClientName: txtName.Text.Trim(),
                    appointmentClientPatronymic: txtPatronymic.Text.Trim(),
                    appointmentNationality: txtNationality.Text.Trim(),
                    appointmentDate: txtDate.Text.Trim(),
                    appointmentDaytime: txtTime.Text.Trim()
                );

                var result = await ApiClient.ApiFunctions.AddAppointment(appointment);

                if (result.Contains("Ошибка") || result.IndexOf("error", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    MessageBox.Show($"Ошибка при создании записи:\n{result}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Запись успешно создана!",
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
                btnCreate.Enabled = true;
                btnCancel.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtPassportNumber.Text))
            {
                MessageBox.Show("Номер паспорта обязателен для заполнения",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassportNumber.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSurname.Text))
            {
                MessageBox.Show("Фамилия клиента обязательна для заполнения",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSurname.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Имя клиента обязательно для заполнения",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }


            if (string.IsNullOrWhiteSpace(txtNationality.Text))
            {
                MessageBox.Show("Гражданство обязательно для заполнения",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNationality.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDate.Text))
            {
                MessageBox.Show("Дата записи обязательна для заполнения",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDate.Focus();
                return false;
            }

            if (!IsValidDate(txtDate.Text))
            {
                MessageBox.Show("Дата должна быть в формате дд.мм.гггг (например, 25.12.2023)",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDate.Focus();
                txtDate.SelectAll();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTime.Text))
            {
                MessageBox.Show("Время записи обязательно для заполнения",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTime.Focus();
                return false;
            }

            if (!IsValidTime(txtTime.Text))
            {
                MessageBox.Show("Время должно быть в формате чч:мм (например, 14:30)",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTime.Focus();
                txtTime.SelectAll();
                return false;
            }

            return true;
        }

        private bool IsValidDate(string date)
        {
            var regex = new Regex(@"^(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[0-2])\.\d{4}$");
            if (!regex.IsMatch(date))
                return false;

            var parts = date.Split('.');
            if (parts.Length != 3)
                return false;

            if (int.TryParse(parts[0], out int day) &&
                int.TryParse(parts[1], out int month) &&
                int.TryParse(parts[2], out int year))
            {
                try
                {
                    var dateTime = new DateTime(year, month, day);
                    return dateTime >= DateTime.Today;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        private bool IsValidTime(string time)
        {
            var regex = new Regex(@"^([01][0-9]|2[0-3]):([0-5][0-9])$");
            return regex.IsMatch(time);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtPassportSeries_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPassportNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtDate_TextChanged(object sender, EventArgs e)
        {
            var text = txtDate.Text;
            if (text.Length == 2 && !text.Contains("."))
            {
                txtDate.Text = text + ".";
                txtDate.SelectionStart = txtDate.Text.Length;
            }
            else if (text.Length == 5 && text.Count(c => c == '.') == 1)
            {
                txtDate.Text = text + ".";
                txtDate.SelectionStart = txtDate.Text.Length;
            }
        }

        private void txtTime_TextChanged(object sender, EventArgs e)
        {
            var text = txtTime.Text;
            if (text.Length == 2 && !text.Contains(":"))
            {
                txtTime.Text = text + ":";
                txtTime.SelectionStart = txtTime.Text.Length;
            }
        }

        private void txtTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ':')
            {
                e.Handled = true;
            }
        }
    }
}