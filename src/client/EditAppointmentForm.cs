using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace client
{
    public partial class EditAppointmentForm : Form
    {
        private readonly Appointment _originalAppointment;

        public EditAppointmentForm(Appointment appointment)
        {
            InitializeComponent();
            _originalAppointment = appointment ?? throw new ArgumentNullException(nameof(appointment));

            txtServiceTitle.Text = _originalAppointment.AppointmentServiceTitle;
            txtServiceTitle.ReadOnly = true;
            txtServiceTitle.BackColor = System.Drawing.SystemColors.Control;

            txtPassportSeries.Text = _originalAppointment.AppointmentClientPassportSeries;
            txtPassportSeries.ReadOnly = true;
            txtPassportSeries.BackColor = System.Drawing.SystemColors.Control;

            txtPassportNumber.Text = _originalAppointment.AppointmentClientPassportNumber;
            txtPassportNumber.ReadOnly = true;
            txtPassportNumber.BackColor = System.Drawing.SystemColors.Control;

            txtSurname.Text = _originalAppointment.AppointmentClientSurname;
            txtSurname.ReadOnly = true;
            txtSurname.BackColor = System.Drawing.SystemColors.Control;

            txtName.Text = _originalAppointment.AppointmentClientName;
            txtName.ReadOnly = true;
            txtName.BackColor = System.Drawing.SystemColors.Control;

            txtPatronymic.Text = _originalAppointment.AppointmentClientPatronymic;
            txtPatronymic.ReadOnly = true;
            txtPatronymic.BackColor = System.Drawing.SystemColors.Control;

            txtNationality.Text = _originalAppointment.AppointmentNationality;
            txtNationality.ReadOnly = true;
            txtNationality.BackColor = System.Drawing.SystemColors.Control;

            txtDate.Text = _originalAppointment.AppointmentDate;
            txtTime.Text = _originalAppointment.AppointmentDaytime;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                if (!ValidateInput())
                {
                    return;
                }

                if (!HasChanges())
                {
                    MessageBox.Show("Нет изменений для сохранения",
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }

                var oldAppointment = new ApiClient.Appointment(
                    appointmentServiceTitle: _originalAppointment.AppointmentServiceTitle,
                    appointmentClientPassportSeries: _originalAppointment.AppointmentClientPassportSeries,
                    appointmentClientPassportNumber: _originalAppointment.AppointmentClientPassportNumber,
                    appointmentClientSurname: _originalAppointment.AppointmentClientSurname,
                    appointmentClientName: _originalAppointment.AppointmentClientName,
                    appointmentClientPatronymic: _originalAppointment.AppointmentClientPatronymic,
                    appointmentNationality: _originalAppointment.AppointmentNationality,
                    appointmentDate: _originalAppointment.AppointmentDate,
                    appointmentDaytime: _originalAppointment.AppointmentDaytime
                );

                var result = await ApiClient.ApiFunctions.EditAppointment(
                    oldAppointment: oldAppointment,
                    newDate: txtDate.Text.Trim(),
                    newDaytime: txtTime.Text.Trim()
                );

                if (result.Contains("Ошибка") || result.IndexOf("error", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    MessageBox.Show($"Ошибка при изменении записи:\n{result}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Дата и время записи успешно изменены!",
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
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private bool ValidateInput()
        {
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

        private bool HasChanges()
        {
            return txtDate.Text.Trim() != _originalAppointment.AppointmentDate ||
                   txtTime.Text.Trim() != _originalAppointment.AppointmentDaytime;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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