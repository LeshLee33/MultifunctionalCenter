using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

public static class AppointmentCardManager
{
    public static async Task LoadAllAppointments(Panel targetPanel)
    {
        await LoadAppointments(targetPanel, "", "");
    }

    public static async Task LoadAppointmentsByPassport(Panel targetPanel, string passportNumber, string passportSeries = "")
    {
        await LoadAppointments(targetPanel, passportNumber, passportSeries);
    }

    private static async Task LoadAppointments(Panel targetPanel, string passportNumber, string passportSeries)
    {
        if (targetPanel == null) return;

        try
        {
            ShowLoadingMessage(targetPanel);

            string response = await GetAppointmentsRecursive(
                passportNumber,
                passportSeries,
                attempt: 0,
                maxAttempts: 3);

            if (string.IsNullOrEmpty(response) || response.Contains("Ошибка"))
            {
                ShowErrorMessage(targetPanel, "Ошибка загрузки записей");
                return;
            }

            var appointments = ParseAppointmentsFromJson(response);

            if (appointments.Count == 0)
            {
                ShowNoResultsMessage(targetPanel);
                return;
            }

            DisplayAdaptiveAppointmentCards(targetPanel, appointments);
        }
        catch (Exception ex)
        {
            ShowErrorMessage(targetPanel, $"Ошибка: {ex.Message}");
        }
    }

    private static async Task<string> GetAppointmentsRecursive(
        string passportNumber,
        string passportSeries,
        int attempt,
        int maxAttempts)
    {
        if (attempt >= maxAttempts)
            return "Превышено количество попыток запроса";

        try
        {
            if (string.IsNullOrEmpty(passportNumber))
            {
                return await ApiClient.ApiFunctions.GetAppointments();
            }
            else
            {
                return await ApiClient.ApiFunctions.GetAppointmentsByPassport(passportNumber, passportSeries);
            }
        }
        catch (System.Net.Http.HttpRequestException)
        {
            // Рекурсивный повтор при ошибке сети
            return await GetAppointmentsRecursive(passportNumber, passportSeries, attempt + 1, maxAttempts);
        }
        catch (Exception ex)
        {
            return $"Ошибка: {ex.Message}";
        }
    }

    private static List<Appointment> ParseAppointmentsFromJson(string json)
    {
        var appointments = new List<Appointment>();

        try
        {
            using (var doc = JsonDocument.Parse(json))
            {
                var array = doc.RootElement;
                return ParseAppointmentsArrayRecursive(array, appointments, 0);
            }
        }
        catch
        {
            return appointments;
        }
    }

    private static List<Appointment> ParseAppointmentsArrayRecursive(
        JsonElement array,
        List<Appointment> appointments,
        int index)
    {
        if (index >= array.GetArrayLength()) return appointments;

        var element = array[index];
        var appointment = ParseAppointmentElement(element);

        var newAppointments = new List<Appointment>(appointments);
        newAppointments.Add(appointment);

        return ParseAppointmentsArrayRecursive(array, newAppointments, index + 1);
    }

    private static Appointment ParseAppointmentElement(JsonElement element)
    {
        var appointment = new Appointment();

        ProcessAppointmentPropertiesRecursive(
            element.EnumerateObject().ToList(),
            appointment,
            0);

        return appointment;
    }

    private static void ProcessAppointmentPropertiesRecursive(
        List<JsonProperty> properties,
        Appointment appointment,
        int index)
    {
        if (index >= properties.Count) return;

        var property = properties[index];
        ProcessSingleAppointmentProperty(property, appointment);

        ProcessAppointmentPropertiesRecursive(properties, appointment, index + 1);
    }

    private static void ProcessSingleAppointmentProperty(JsonProperty property, Appointment appointment)
    {
        string propName = property.Name.ToLower();
        string value = property.Value.GetString() ?? "";

        switch (propName)
        {
            case "appointment_service_title":
                appointment.AppointmentServiceTitle = value;
                break;

            case "appointment_client_passport_series":
                appointment.AppointmentClientPassportSeries = value;
                break;

            case "appointment_client_passport_number":
                appointment.AppointmentClientPassportNumber = value;
                break;

            case "appointment_client_surname":
                appointment.AppointmentClientSurname = value;
                break;

            case "appointment_client_name":
                appointment.AppointmentClientName = value;
                break;

            case "appointment_client_patronymic":
                appointment.AppointmentClientPatronymic = value;
                break;

            case "appointment_nationality":
                appointment.AppointmentNationality = value;
                break;

            case "appointment_date":
                appointment.AppointmentDate = value;
                break;

            case "appointment_daytime":
                appointment.AppointmentDaytime = value;
                break;
        }
    }

    private static void DisplayAdaptiveAppointmentCards(Panel panel, List<Appointment> appointments)
    {
        if (panel.InvokeRequired)
        {
            panel.Invoke(new Action(() => DisplayAdaptiveAppointmentCardsInternal(panel, appointments)));
            return;
        }

        DisplayAdaptiveAppointmentCardsInternal(panel, appointments);
    }

    private static void DisplayAdaptiveAppointmentCardsInternal(Panel panel, List<Appointment> appointments)
    {
        panel.Controls.Clear();
        panel.AutoScroll = false;

        int panelWidth = panel.ClientSize.Width;

        CreateAppointmentCardsRecursive(panel, appointments, panelWidth, 0, 5);

        SetupAutoScroll(panel);
    }

    private static void CreateAppointmentCardsRecursive(
        Panel panel,
        List<Appointment> appointments,
        int panelWidth,
        int index,
        int currentY)
    {
        if (index >= appointments.Count) return;

        var appointment = appointments[index];

        var card = CreateAdaptiveAppointmentCard(appointment, panelWidth);

        card.Location = new Point(5, currentY);

        panel.Controls.Add(card);

        CreateAppointmentCardsRecursive(
            panel,
            appointments,
            panelWidth,
            index + 1,
            currentY + card.Height + 5);
    }

    private static Button CreateAdaptiveAppointmentCard(Appointment appointment, int panelWidth)
    {
        int cardWidth = CalculateCardWidth(panelWidth);

        var card = CardBuilders.BuildAppointmentCard(appointment);

        card.Width = cardWidth;
        card.Height = CalculateCardHeightForText(card.Text, cardWidth);

        return card;
    }

    private static int CalculateCardWidth(int panelWidth)
    {
        return Math.Max(panelWidth - 15, 100);
    }

    private static void SetupAutoScroll(Panel panel)
    {
        if (panel.Controls.Count == 0) return;

        int maxBottom = CalculateMaxBottomRecursive(panel.Controls, 0, 0);

        panel.AutoScroll = maxBottom > panel.ClientSize.Height;
    }

    private static int CalculateMaxBottomRecursive(Control.ControlCollection controls, int index, int currentMax)
    {
        if (index >= controls.Count) return currentMax;

        var control = controls[index];
        int bottom = control.Bottom;
        int newMax = Math.Max(currentMax, bottom);

        return CalculateMaxBottomRecursive(controls, index + 1, newMax);
    }

    public static void HandlePanelResize(Panel panel)
    {
        if (panel.InvokeRequired)
        {
            panel.Invoke(new Action(() => HandlePanelResizeInternal(panel)));
            return;
        }

        HandlePanelResizeInternal(panel);
    }

    private static void HandlePanelResizeInternal(Panel panel)
    {
        if (panel.Controls.Count == 0) return;

        int panelWidth = panel.ClientSize.Width;
        int cardWidth = CalculateCardWidth(panelWidth);

        UpdateCardsRecursive(panel.Controls, cardWidth, 0, 5, panel);
    }

    private static void UpdateCardsRecursive(
        Control.ControlCollection controls,
        int cardWidth,
        int index,
        int currentY,
        Panel panel)
    {
        if (index >= controls.Count)
        {
            SetupAutoScroll(panel);
            return;
        }

        var control = controls[index];
        if (control is Button card)
        {
            card.Width = cardWidth;
            card.Height = CalculateCardHeightForText(card.Text, cardWidth);
            card.Location = new Point(5, currentY);

            UpdateCardsRecursive(controls, cardWidth, index + 1, currentY + card.Height + 5, panel);
        }
        else
        {
            UpdateCardsRecursive(controls, cardWidth, index + 1, currentY, panel);
        }
    }

    private static int CalculateCardHeightForText(string text, int width)
    {
        if (string.IsNullOrEmpty(text)) return 80;

        try
        {
            using (var bitmap = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(bitmap))
            {
                var font = new Font("Segoe UI", 9, FontStyle.Regular);

                var size = g.MeasureString(text, font, width - 16);

                return Math.Max(80, Math.Min((int)Math.Ceiling(size.Height) + 16, 250));
            }
        }
        catch
        {
            return 100;
        }
    }

    private static void ShowLoadingMessage(Panel panel)
    {
        SetPanelMessage(panel, "Загрузка записей...", Color.Black);
    }

    private static void ShowErrorMessage(Panel panel, string message)
    {
        SetPanelMessage(panel, message, Color.Red);
    }

    private static void ShowNoResultsMessage(Panel panel)
    {
        SetPanelMessage(panel, "Записи не найдены", Color.Gray);
    }

    private static void SetPanelMessage(Panel panel, string message, Color color)
    {
        if (panel.InvokeRequired)
        {
            panel.Invoke(new Action(() => SetPanelMessageInternal(panel, message, color)));
            return;
        }

        SetPanelMessageInternal(panel, message, color);
    }

    private static void SetPanelMessageInternal(Panel panel, string message, Color color)
    {
        panel.Controls.Clear();
        panel.AutoScroll = false;

        var label = new Label
        {
            Text = message,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            ForeColor = color,
            Font = new Font("Segoe UI", 10)
        };

        panel.Controls.Add(label);
    }

    public static void InitializeSearchHandler(TextBox searchBox, Panel resultsPanel, int debounceMs = 300)
    {
        var timer = new Timer { Interval = debounceMs };

        searchBox.TextChanged += (s, e) =>
        {
            timer.Stop();
            timer.Start();
        };

        timer.Tick += async (s, e) =>
        {
            timer.Stop();
            string searchText = searchBox.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                await LoadAllAppointments(resultsPanel);
            }
            else
            {
                await LoadAppointmentsByPassport(resultsPanel, searchText);
            }
        };
    }
}