using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

public static class ServedAppointmentCardManager
{
    public static async Task LoadAllServedAppointments(Panel targetPanel)
    {
        await LoadServedAppointments(targetPanel, "", "");
    }

    public static async Task LoadServedAppointmentsByPassport(Panel targetPanel, string passportNumber, string passportSeries = "")
    {
        await LoadServedAppointments(targetPanel, passportNumber, passportSeries);
    }

    private static async Task LoadServedAppointments(Panel targetPanel, string passportNumber, string passportSeries)
    {
        if (targetPanel == null) return;

        try
        {
            ShowLoadingMessage(targetPanel);

            string response = await GetServedAppointmentsRecursive(
                passportNumber,
                passportSeries,
                attempt: 0,
                maxAttempts: 3);

            if (string.IsNullOrEmpty(response) || response.Contains("Ошибка"))
            {
                ShowErrorMessage(targetPanel, "Ошибка загрузки истории");
                return;
            }

            var servedAppointments = ParseServedAppointmentsFromJson(response);

            if (servedAppointments.Count == 0)
            {
                ShowNoResultsMessage(targetPanel);
                return;
            }

            DisplayAdaptiveServedAppointmentCards(targetPanel, servedAppointments);
        }
        catch (Exception ex)
        {
            ShowErrorMessage(targetPanel, $"Ошибка: {ex.Message}");
        }
    }

    private static async Task<string> GetServedAppointmentsRecursive(
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
                return await ApiClient.ApiFunctions.GetAppointmentsHistory();
            }
            else
            {
                return await ApiClient.ApiFunctions.GetServedAppointmentsByPassport(passportNumber, passportSeries);
            }
        }
        catch (System.Net.Http.HttpRequestException)
        {
            return await GetServedAppointmentsRecursive(passportNumber, passportSeries, attempt + 1, maxAttempts);
        }
        catch (Exception ex)
        {
            return $"Ошибка: {ex.Message}";
        }
    }

    private static List<ServedAppointment> ParseServedAppointmentsFromJson(string json)
    {
        var servedAppointments = new List<ServedAppointment>();

        try
        {
            using (var doc = JsonDocument.Parse(json))
            {
                var array = doc.RootElement;
                return ParseServedAppointmentsArrayRecursive(array, servedAppointments, 0);
            }
        }
        catch
        {
            return servedAppointments;
        }
    }

    private static List<ServedAppointment> ParseServedAppointmentsArrayRecursive(
        JsonElement array,
        List<ServedAppointment> servedAppointments,
        int index)
    {
        if (index >= array.GetArrayLength()) return servedAppointments;

        var element = array[index];
        var servedAppointment = ParseServedAppointmentElement(element);

        var newServedAppointments = new List<ServedAppointment>(servedAppointments);
        newServedAppointments.Add(servedAppointment);

        return ParseServedAppointmentsArrayRecursive(array, newServedAppointments, index + 1);
    }

    private static ServedAppointment ParseServedAppointmentElement(JsonElement element)
    {
        var servedAppointment = new ServedAppointment();

        ProcessServedAppointmentPropertiesRecursive(
            element.EnumerateObject().ToList(),
            servedAppointment,
            0);

        return servedAppointment;
    }

    private static void ProcessServedAppointmentPropertiesRecursive(
        List<JsonProperty> properties,
        ServedAppointment servedAppointment,
        int index)
    {
        if (index >= properties.Count) return;

        var property = properties[index];
        ProcessSingleServedAppointmentProperty(property, servedAppointment);

        ProcessServedAppointmentPropertiesRecursive(properties, servedAppointment, index + 1);
    }

    private static void ProcessSingleServedAppointmentProperty(JsonProperty property, ServedAppointment servedAppointment)
    {
        string propName = property.Name.ToLower();
        string value = property.Value.GetString() ?? "";

        switch (propName)
        {
            case "appointment_service_title":
                servedAppointment.AppointmentServiceTitle = value;
                break;

            case "appointment_client_passport_series":
                servedAppointment.AppointmentClientPassportSeries = value;
                break;

            case "appointment_client_passport_number":
                servedAppointment.AppointmentClientPassportNumber = value;
                break;

            case "appointment_client_surname":
                servedAppointment.AppointmentClientSurname = value;
                break;

            case "appointment_client_name":
                servedAppointment.AppointmentClientName = value;
                break;

            case "appointment_client_patronymic":
                servedAppointment.AppointmentClientPatronymic = value;
                break;

            case "appointment_nationality":
                servedAppointment.AppointmentNationality = value;
                break;

            case "appointment_date":
                servedAppointment.AppointmentDate = value;
                break;

            case "appointment_daytime":
                servedAppointment.AppointmentDaytime = value;
                break;
        }
    }

    private static void DisplayAdaptiveServedAppointmentCards(Panel panel, List<ServedAppointment> servedAppointments)
    {
        if (panel.InvokeRequired)
        {
            panel.Invoke(new Action(() => DisplayAdaptiveServedAppointmentCardsInternal(panel, servedAppointments)));
            return;
        }

        DisplayAdaptiveServedAppointmentCardsInternal(panel, servedAppointments);
    }

    private static void DisplayAdaptiveServedAppointmentCardsInternal(Panel panel, List<ServedAppointment> servedAppointments)
    {
        panel.Controls.Clear();
        panel.AutoScroll = false;

        int panelWidth = panel.ClientSize.Width;

        CreateServedAppointmentCardsRecursive(panel, servedAppointments, panelWidth, 0, 5);

        SetupAutoScroll(panel);
    }

    private static void CreateServedAppointmentCardsRecursive(
        Panel panel,
        List<ServedAppointment> servedAppointments,
        int panelWidth,
        int index,
        int currentY)
    {
        if (index >= servedAppointments.Count) return;

        var servedAppointment = servedAppointments[index];

        var card = CreateAdaptiveServedAppointmentCard(servedAppointment, panelWidth);

        card.Location = new Point(5, currentY);

        panel.Controls.Add(card);

        CreateServedAppointmentCardsRecursive(
            panel,
            servedAppointments,
            panelWidth,
            index + 1,
            currentY + card.Height + 5);
    }

    private static Button CreateAdaptiveServedAppointmentCard(ServedAppointment servedAppointment, int panelWidth)
    {
        int cardWidth = CalculateCardWidth(panelWidth);

        var card = CardBuilders.BuildServedAppointmentCard(servedAppointment);

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
        SetPanelMessage(panel, "Загрузка истории...", Color.Black);
    }

    private static void ShowErrorMessage(Panel panel, string message)
    {
        SetPanelMessage(panel, message, Color.Red);
    }

    private static void ShowNoResultsMessage(Panel panel)
    {
        SetPanelMessage(panel, "История не найдена", Color.Gray);
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
                await LoadAllServedAppointments(resultsPanel);
            }
            else
            {
                await LoadServedAppointmentsByPassport(resultsPanel, searchText);
            }
        };
    }
}