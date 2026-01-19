using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

public static class CardBuilders
{
    private static class CardStyles
    {
        public static readonly Color AppointmentColor = Color.FromArgb(240, 248, 255); // AliceBlue
        public static readonly Color ServedAppointmentColor = Color.FromArgb(240, 255, 240); // Honeydew
        public static readonly Color ServiceColor = Color.FromArgb(255, 250, 240); // FloralWhite

        public static readonly Font TitleFont = new Font("Segoe UI", 10, FontStyle.Bold);
        public static readonly Font ValueFont = new Font("Segoe UI", 9, FontStyle.Regular);
        public static readonly Font SmallFont = new Font("Segoe UI", 8, FontStyle.Italic);

        public static readonly int CardWidth = 320;
        public static readonly int LineHeight = 22;
        public static readonly int PaddingTopBottom = 10;
        public static readonly int PaddingLeftRight = 10;
        public static readonly int MinCardHeight = 80;
    }

    private static Panel _servicePropertiesPanel = null;
    private static Panel _appointmentPropertiesPanel = null;

    private static Button _closeCardButton = null;

    private static Control _currentOpenedCard = null;

    public class CardData
    {
        public string DataType { get; }
        public Dictionary<string, object> Data { get; }

        public CardData(string dataType, Dictionary<string, object> data)
        {
            DataType = dataType;
            Data = data ?? new Dictionary<string, object>();
        }
    }

    private static ApiClient.Service _selectedService = null;

    public static ApiClient.Service SelectedService
    {
        get => _selectedService;
        private set => _selectedService = value;
    }

    public static Panel ServicePropertiesPanel
    {
        get => _servicePropertiesPanel;
        set => _servicePropertiesPanel = value;
    }

    public static Panel AppointmentPropertiesPanel
    {
        get => _appointmentPropertiesPanel;
        set => _appointmentPropertiesPanel = value;
    }

    public static void SetDetailPanels(Panel servicePanel, Panel appointmentPanel)
    {
        ServicePropertiesPanel = servicePanel;
        AppointmentPropertiesPanel = appointmentPanel;
    }

    private static void InitializeCloseButton()
    {
        if (_closeCardButton == null)
        {
            _closeCardButton = new Button
            {
                Name = "CloseCardButton",
                Text = "Закрыть",
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.System,
                Visible = false
            };

            _closeCardButton.FlatAppearance.BorderColor = Color.Gray;
            _closeCardButton.FlatAppearance.BorderSize = 1;
            _closeCardButton.Click += (s, e) => CloseCardButton_Click();
        }
    }

    private static void CloseCardButton_Click()
    {
        ClearDetailsPanels();
        if (_currentOpenedCard != null)
        {
            _currentOpenedCard.Focus();
            _currentOpenedCard = null;
        }
        HideCloseButton();
        SelectedService = null;
    }

    private static void ShowCloseButton(Panel panel)
    {
        if (_closeCardButton != null && panel != null)
        {
            if (!panel.Controls.Contains(_closeCardButton))
            {
                panel.Controls.Add(_closeCardButton);
            }

            _closeCardButton.Location = new Point(
                panel.Width - _closeCardButton.Width - 10,
                10
            );

            _closeCardButton.BringToFront();
            _closeCardButton.Visible = true;
        }
    }

    private static void HideCloseButton()
    {
        if (_closeCardButton != null)
        {
            _closeCardButton.Visible = false;
        }
    }

    private static void ClearDetailsPanels()
    {
        ClearPanelLabels(_servicePropertiesPanel);
        ClearPanelLabels(_appointmentPropertiesPanel);
    }

    private static void ClearPanelLabels(Panel panel)
    {
        if (panel == null) return;

        foreach (Control control in panel.Controls)
        {
            if (control.Name != null && control.Name.EndsWith("Label", StringComparison.OrdinalIgnoreCase))
            {
                ClearControlText(control);
            }
        }
    }

    private static void ClearControlText(Control control)
    {
        if (control is Label label)
        {
            label.Text = "...";
        }
        else if (control is TextBox textBox && control.Name == "DocumentsListLabel")
        {
            textBox.Text = "...";
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.ReadOnly = true;
        }
    }

    public static Button BuildAppointmentCard(Appointment appointment)
    {
        var data = new Dictionary<string, object>
        {
            ["AppointmentServiceTitle"] = appointment.AppointmentServiceTitle ?? "",
            ["AppointmentClientPassportSeries"] = appointment.AppointmentClientPassportSeries ?? "",
            ["AppointmentClientPassportNumber"] = appointment.AppointmentClientPassportNumber ?? "",
            ["AppointmentClientSurname"] = appointment.AppointmentClientSurname ?? "",
            ["AppointmentClientName"] = appointment.AppointmentClientName ?? "",
            ["AppointmentClientPatronymic"] = appointment.AppointmentClientPatronymic ?? "",
            ["AppointmentNationality"] = appointment.AppointmentNationality ?? "",
            ["AppointmentDate"] = appointment.AppointmentDate ?? "",
            ["AppointmentDaytime"] = appointment.AppointmentDaytime ?? ""
        };

        return CreateCardButton(
            color: CardStyles.AppointmentColor,
            lines: BuildAppointmentLines(appointment),
            data: new CardData("Appointment", data)
        );
    }

    public static Button BuildServedAppointmentCard(ServedAppointment servedAppointment)
    {
        var data = new Dictionary<string, object>
        {
            ["AppointmentServiceTitle"] = servedAppointment.AppointmentServiceTitle ?? "",
            ["AppointmentClientPassportSeries"] = servedAppointment.AppointmentClientPassportSeries ?? "",
            ["AppointmentClientPassportNumber"] = servedAppointment.AppointmentClientPassportNumber ?? "",
            ["AppointmentClientSurname"] = servedAppointment.AppointmentClientSurname ?? "",
            ["AppointmentClientName"] = servedAppointment.AppointmentClientName ?? "",
            ["AppointmentClientPatronymic"] = servedAppointment.AppointmentClientPatronymic ?? "",
            ["AppointmentNationality"] = servedAppointment.AppointmentNationality ?? "",
            ["AppointmentDate"] = servedAppointment.AppointmentDate ?? "",
            ["AppointmentDaytime"] = servedAppointment.AppointmentDaytime ?? ""
        };

        return CreateCardButton(
            color: CardStyles.ServedAppointmentColor,
            lines: BuildServedAppointmentLines(servedAppointment),
            data: new CardData("ServedAppointment", data)
        );
    }

    public static Button BuildServiceCard(Service service)
    {
        string documentsString = "";
        if (service.ServiceRequiredDocuments != null && service.ServiceRequiredDocuments.Count > 0)
        {
            documentsString = string.Join(", ", service.ServiceRequiredDocuments);
        }

        var data = new Dictionary<string, object>
        {
            ["ServiceType"] = service.ServiceType ?? "",
            ["ServiceTitle"] = service.ServiceTitle ?? "",
            ["ServiceRequiredDocuments"] = documentsString,
            ["ServiceGovernmentStructure"] = service.ServiceGovernmentStructure ?? "",
            ["ServiceDurationDays"] = service.ServiceDurationDays
        };

        return CreateCardButton(
            color: CardStyles.ServiceColor,
            lines: BuildServiceLines(service),
            data: new CardData("Service", data)
        );
    }

    public static Button BuildAdaptiveServiceCard(Service service, int cardWidth)
    {
        string documentsString = "";
        if (service.ServiceRequiredDocuments != null && service.ServiceRequiredDocuments.Count > 0)
        {
            documentsString = string.Join(", ", service.ServiceRequiredDocuments);
        }

        var data = new Dictionary<string, object>
        {
            ["ServiceType"] = service.ServiceType ?? "",
            ["ServiceTitle"] = service.ServiceTitle ?? "",
            ["ServiceRequiredDocuments"] = documentsString,
            ["ServiceGovernmentStructure"] = service.ServiceGovernmentStructure ?? "",
            ["ServiceDurationDays"] = service.ServiceDurationDays
        };

        var lines = BuildAdaptiveServiceLines(service, cardWidth);

        return CreateAdaptiveCardButton(
            color: CardStyles.ServiceColor,
            lines: lines,
            data: new CardData("Service", data),
            width: cardWidth
        );
    }

    private static Button CreateCardButton(Color color, List<string> lines, CardData data)
    {
        var button = new Button
        {
            Size = new Size(CardStyles.CardWidth, CalculateCardHeight(lines)),
            BackColor = color,
            FlatStyle = FlatStyle.Flat,
            TextAlign = ContentAlignment.TopLeft,
            Padding = new Padding(CardStyles.PaddingLeftRight, CardStyles.PaddingTopBottom,
                                 CardStyles.PaddingLeftRight, CardStyles.PaddingTopBottom),
            Font = CardStyles.ValueFont,
            Tag = data
        };

        button.FlatAppearance.BorderColor = Color.Gray;
        button.FlatAppearance.BorderSize = 1;
        button.Click += (s, e) => CardButton_Click(button);
        button.Text = FormatButtonText(lines);

        AdjustButtonHeightForLongText(button);

        return button;
    }

    private static Button CreateAdaptiveCardButton(Color color, List<string> lines, CardData data, int width)
    {
        string buttonText = FormatAdaptiveText(lines, width - 20);

        int height = CalculateAdaptiveCardHeight(buttonText, width);

        var button = new Button
        {
            Width = width,
            Height = Math.Max(height, 60),
            BackColor = color,
            FlatStyle = FlatStyle.Flat,
            TextAlign = ContentAlignment.TopLeft,
            Padding = new Padding(8, 8, 8, 8),
            Font = new Font("Segoe UI", 9, FontStyle.Regular),
            Tag = data
        };

        button.FlatAppearance.BorderColor = Color.Gray;
        button.FlatAppearance.BorderSize = 1;
        button.Click += (s, e) => CardButton_Click(button);
        button.Text = buttonText;

        return button;
    }

    private static void CardButton_Click(Button btn)
    {
        if (btn.Tag is CardData cardData)
        {
            ShowCardDetails(cardData, btn);

            if (cardData.DataType == "Service")
            {
                SelectedService = ExtractServiceFromCard(btn);
                Console.WriteLine($"Сохранена услуга: {SelectedService?.ServiceTitle}");
            }
        }
    }

    private static int CalculateCardHeight(List<string> lines)
    {
        int baseHeight = Math.Max(CardStyles.PaddingTopBottom * 2 + lines.Count * CardStyles.LineHeight,
                                 CardStyles.MinCardHeight);

        foreach (var line in lines)
        {
            if (line.Length > 30)
            {
                baseHeight += (int)Math.Ceiling((double)line.Length / 30) * CardStyles.LineHeight;
            }
        }

        return baseHeight;
    }

    private static void AdjustButtonHeightForLongText(Button button)
    {
        if (string.IsNullOrEmpty(button.Text)) return;

        using (var g = button.CreateGraphics())
        {
            int availableWidth = button.Width - (button.Padding.Left + button.Padding.Right + 4);
            var textSize = g.MeasureString(button.Text, button.Font, availableWidth);

            int requiredHeight = (int)Math.Ceiling(textSize.Height) +
                                button.Padding.Top + button.Padding.Bottom + 4;

            if (requiredHeight > button.Height)
            {
                button.Height = Math.Max(requiredHeight, CardStyles.MinCardHeight);
            }
        }
    }

    private static int CalculateAdaptiveCardHeight(string text, int width)
    {
        if (string.IsNullOrEmpty(text)) return 60;

        using (var bitmap = new Bitmap(1, 1))
        using (var g = Graphics.FromImage(bitmap))
        {
            var font = new Font("Segoe UI", 9, FontStyle.Regular);

            var size = g.MeasureString(text, font, width - 16);

            return Math.Max(60, Math.Min((int)Math.Ceiling(size.Height) + 16, 200));
        }
    }

    private static string FormatButtonText(List<string> lines)
    {
        return string.Join("\n", lines);
    }

    private static string FormatAdaptiveText(List<string> lines, int maxWidth)
    {
        var formattedLines = new List<string>();

        foreach (var line in lines)
        {
            formattedLines.Add(WrapTextForWidth(line, maxWidth));
        }

        return string.Join("\n", formattedLines);
    }

    private static string WrapTextForWidth(string text, int maxWidth)
    {
        if (string.IsNullOrEmpty(text)) return text;

        if (text.Length <= 30) return text;

        using (var bitmap = new Bitmap(1, 1))
        using (var g = Graphics.FromImage(bitmap))
        {
            var font = new Font("Segoe UI", 9, FontStyle.Regular);

            var size = g.MeasureString(text, font);
            if (size.Width <= maxWidth)
                return text;

            var words = text.Split(' ');
            var resultLines = new List<string>();
            var currentLine = "";

            foreach (var word in words)
            {
                var testLine = string.IsNullOrEmpty(currentLine) ? word : currentLine + " " + word;
                var testSize = g.MeasureString(testLine, font);

                if (testSize.Width > maxWidth && !string.IsNullOrEmpty(currentLine))
                {
                    resultLines.Add(currentLine);
                    currentLine = word;
                }
                else
                {
                    currentLine = testLine;
                }
            }

            if (!string.IsNullOrEmpty(currentLine))
                resultLines.Add(currentLine);

            return string.Join("\n", resultLines);
        }
    }

    private static List<string> BuildAppointmentLines(Appointment appointment)
    {
        var fio = $"{appointment.AppointmentClientSurname ?? ""} {appointment.AppointmentClientName ?? ""} {appointment.AppointmentClientPatronymic ?? ""}".Trim();
        var passport = $"{appointment.AppointmentClientPassportSeries ?? ""} {appointment.AppointmentClientPassportNumber ?? ""}".Trim();

        return new List<string>
        {
            $"Паспорт: {passport}",
            $"ФИО: {fio}",
            $"Услуга: {appointment.AppointmentServiceTitle ?? ""}",
            $"Дата: {appointment.AppointmentDate ?? ""}",
            $"Время: {appointment.AppointmentDaytime ?? ""}",
            $"Гражданство: {appointment.AppointmentNationality ?? ""}"
        };
    }

    private static List<string> BuildServedAppointmentLines(ServedAppointment servedAppointment)
    {
        var fio = $"{servedAppointment.AppointmentClientSurname ?? ""} {servedAppointment.AppointmentClientName ?? ""} {servedAppointment.AppointmentClientPatronymic ?? ""}".Trim();
        var passport = $"{servedAppointment.AppointmentClientPassportSeries ?? ""} {servedAppointment.AppointmentClientPassportNumber ?? ""}".Trim();

        return new List<string>
        {
            $"Паспорт: {passport}",
            $"ФИО: {fio}",
            $"Услуга: {servedAppointment.AppointmentServiceTitle ?? ""}",
            $"Дата приёма: {servedAppointment.AppointmentDate ?? ""}",
            $"Время: {servedAppointment.AppointmentDaytime ?? ""}",
            $"Гражданство: {servedAppointment.AppointmentNationality ?? ""}",
        };
    }

    private static List<string> BuildServiceLines(Service service)
    {
        return new List<string>
        {
            $"Тип: {service.ServiceType ?? ""}",
            $"Название: {service.ServiceTitle ?? ""}"
        };
    }

    private static List<string> BuildAdaptiveServiceLines(Service service, int cardWidth)
    {
        var lines = new List<string>
        {
            $"Тип: {service.ServiceType ?? ""}",
            $"Название: {service.ServiceTitle ?? ""}"
        };

        return lines;
    }

    private static void ShowCardDetails(CardData cardData, Control card)
    {
        if (cardData == null) return;

        _currentOpenedCard = card;
        InitializeCloseButton();
        ClearDetailsPanels();

        if (cardData.DataType == "Service" && _servicePropertiesPanel != null)
        {
            DisplayServiceDetails(cardData);
            ShowCloseButton(_servicePropertiesPanel);
        }
        else if (cardData.DataType == "Appointment" && _appointmentPropertiesPanel != null)
        {
            DisplayAppointmentDetails(cardData);
            ShowCloseButton(_appointmentPropertiesPanel);
        }
    }

    private static ApiClient.Service ExtractServiceFromCard(Button card)
    {
        if (card.Tag is CardData cardData && cardData.DataType == "Service")
        {
            var data = cardData.Data;

            List<string> documents = new List<string>();
            if (data.ContainsKey("ServiceRequiredDocuments") &&
                data["ServiceRequiredDocuments"] is string docsStr && !string.IsNullOrEmpty(docsStr))
            {
                documents = docsStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                  .Select(d => d.Trim())
                                  .ToList();
            }

            int duration = 0;
            if (data.ContainsKey("ServiceDurationDays") &&
                data["ServiceDurationDays"] is int dur)
            {
                duration = dur;
            }
            else if (data.ContainsKey("ServiceDurationDays") &&
                     int.TryParse(data["ServiceDurationDays"].ToString(), out int parsedDur))
            {
                duration = parsedDur;
            }

            return new ApiClient.Service(
                serviceType: data.ContainsKey("ServiceType") ? data["ServiceType"].ToString() : "",
                serviceTitle: data.ContainsKey("ServiceTitle") ? data["ServiceTitle"].ToString() : "",
                serviceRequiredDocuments: documents,
                serviceGovernmentStructure: data.ContainsKey("ServiceGovernmentStructure")
                    ? data["ServiceGovernmentStructure"].ToString()
                    : "",
                serviceDurationDays: duration
            );
        }

        return null;
    }

    public static ApiClient.Service GetSelectedService()
    {
        return SelectedService;
    }

    public static void ClearSelectedService()
    {
        SelectedService = null;
    }

    public static bool IsServiceCardOpened()
    {
        return _currentOpenedCard != null &&
               _currentOpenedCard is Button button &&
               button.Tag is CardData cardData &&
               cardData.DataType == "Service";
    }

    public static Control GetCurrentOpenedCard()
    {
        return _currentOpenedCard;
    }

    private static Control FindControlByName(Control parent, string name)
    {
        if (parent == null) return null;

        if (parent.Name == name)
            return parent;

        foreach (Control child in parent.Controls)
        {
            var result = FindControlByName(child, name);
            if (result != null)
                return result;
        }

        return null;
    }

    private static string GetValueOrDefault(Dictionary<string, object> dict, string key, string defaultValue = "...")
    {
        if (dict != null && dict.ContainsKey(key) && dict[key] != null)
            return dict[key].ToString();
        return defaultValue;
    }

    private static void DisplayServiceDetails(CardData cardData)
    {
        SetControlText(_servicePropertiesPanel, "ServiceTypeLabel", GetValueOrDefault(cardData.Data, "ServiceType"));
        SetControlText(_servicePropertiesPanel, "ServiceTitleLabel", GetValueOrDefault(cardData.Data, "ServiceTitle"));
        SetControlText(_servicePropertiesPanel, "GovernmentStructureLabel", GetValueOrDefault(cardData.Data, "ServiceGovernmentStructure"));

        if (cardData.Data.ContainsKey("ServiceDurationDays"))
        {
            var days = GetValueOrDefault(cardData.Data, "ServiceDurationDays", "0");
            SetControlText(_servicePropertiesPanel, "ServiceTimeDurationLabel", $"{days}");
        }
        else
        {
            SetControlText(_servicePropertiesPanel, "ServiceTimeDurationLabel", "...");
        }

        SetDocumentsListText(cardData);
    }

    private static void SetDocumentsListText(CardData cardData)
    {
        var documentsListControl = FindControlByName(_servicePropertiesPanel, "DocumentsListLabel") as TextBox;
        if (documentsListControl == null) return;

        if (cardData.Data.ContainsKey("ServiceRequiredDocuments") &&
            cardData.Data["ServiceRequiredDocuments"] is string docsString && !string.IsNullOrEmpty(docsString))
        {
            documentsListControl.Text = docsString;
            documentsListControl.Multiline = true;
            documentsListControl.ScrollBars = ScrollBars.Vertical;
            documentsListControl.ReadOnly = true;

            AdjustTextBoxHeight(documentsListControl);
        }
        else
        {
            documentsListControl.Text = "...";
        }
    }

    private static void AdjustTextBoxHeight(TextBox textBox)
    {
        if (string.IsNullOrEmpty(textBox.Text)) return;

        var lineCount = textBox.Text.Split('\n').Length;

        var desiredHeight = Math.Min(lineCount * 20, 100);

        textBox.Height = Math.Max(desiredHeight, lineCount > 1 ? 60 : 30);
    }

    private static void DisplayAppointmentDetails(CardData cardData)
    {
        SetControlText(_appointmentPropertiesPanel, "AppointmentServiceTitleLabel", GetValueOrDefault(cardData.Data, "AppointmentServiceTitle"));
        SetControlText(_appointmentPropertiesPanel, "PassportSeriesLabel", GetValueOrDefault(cardData.Data, "AppointmentClientPassportSeries"));
        SetControlText(_appointmentPropertiesPanel, "PassportNumberLabel", GetValueOrDefault(cardData.Data, "AppointmentClientPassportNumber"));
        SetControlText(_appointmentPropertiesPanel, "AppointmentClientSurnameLabel", GetValueOrDefault(cardData.Data, "AppointmentClientSurname"));
        SetControlText(_appointmentPropertiesPanel, "AppointmentClientNameLabel", GetValueOrDefault(cardData.Data, "AppointmentClientName"));
        SetControlText(_appointmentPropertiesPanel, "AppointmentClientPatronymicLabel", GetValueOrDefault(cardData.Data, "AppointmentClientPatronymic"));
        SetControlText(_appointmentPropertiesPanel, "AppointmentDateLabel", GetValueOrDefault(cardData.Data, "AppointmentDate"));
        SetControlText(_appointmentPropertiesPanel, "AppointmentDaytimeLabel", GetValueOrDefault(cardData.Data, "AppointmentDaytime"));
        SetControlText(_appointmentPropertiesPanel, "AppointmentNationalityLabel", GetValueOrDefault(cardData.Data, "AppointmentNationality"));
    }

    private static void SetControlText(Panel panel, string controlName, string text)
    {
        var control = FindControlByName(panel, controlName);
        if (control == null) return;

        if (control is Label label)
            label.Text = text;
        else if (control is TextBox textBox)
            textBox.Text = text;
    }

    public static Func<Appointment, Control> AppointmentCardBuilder => BuildAppointmentCard;
    public static Func<ServedAppointment, Control> ServedAppointmentCardBuilder => BuildServedAppointmentCard;
    public static Func<Service, Control> ServiceCardBuilder => BuildServiceCard;
    public static Func<Service, int, Control> AdaptiveServiceCardBuilder => BuildAdaptiveServiceCard;

    public static Func<IEnumerable<Appointment>, Panel> CreateAppointmentsPanel(int columns = 3)
    {
        return appointments => PanelCardLayout.AddCardsToPanel(
            new Panel { AutoScroll = true, Dock = DockStyle.Fill, Padding = new Padding(10) },
            appointments,
            AppointmentCardBuilder,
            columns,
            horizontalSpacing: 15,
            verticalSpacing: 15,
            cardMargin: new Padding(5)
        );
    }

    public static Func<IEnumerable<ServedAppointment>, Panel> CreateServedAppointmentsPanel(int columns = 3)
    {
        return servedAppointments => PanelCardLayout.AddCardsToPanel(
            new Panel { AutoScroll = true, Dock = DockStyle.Fill, Padding = new Padding(10) },
            servedAppointments,
            ServedAppointmentCardBuilder,
            columns,
            horizontalSpacing: 15,
            verticalSpacing: 15,
            cardMargin: new Padding(5)
        );
    }

    public static Func<IEnumerable<Service>, Panel> CreateServicesPanel(int columns = 3)
    {
        return services => PanelCardLayout.AddCardsToPanel(
            new Panel { AutoScroll = true, Dock = DockStyle.Fill, Padding = new Padding(10) },
            services,
            ServiceCardBuilder,
            columns,
            horizontalSpacing: 15,
            verticalSpacing: 15,
            cardMargin: new Padding(5)
        );
    }

    public static Func<IEnumerable<Service>, int, Panel> CreateAdaptiveServicesPanel()
    {
        return (services, panelWidth) =>
        {
            var panel = new Panel
            {
                AutoScroll = true,
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.SystemColors.Control
            };

            return CreateAdaptiveSingleColumnPanel(panel, services.ToList(), panelWidth, 0, 5);
        };
    }

    private static Panel CreateAdaptiveSingleColumnPanel(
        Panel panel,
        List<Service> services,
        int panelWidth,
        int index,
        int currentY)
    {
        if (index >= services.Count)
        {
            SetupPanelAutoScroll(panel);
            return panel;
        }

        var service = services[index];
        int cardWidth = Math.Max(panelWidth - 15, 100);

        var card = BuildAdaptiveServiceCard(service, cardWidth);

        card.Location = new System.Drawing.Point(5, currentY);

        panel.Controls.Add(card);

        return CreateAdaptiveSingleColumnPanel(
            panel,
            services,
            panelWidth,
            index + 1,
            currentY + card.Height + 5);
    }

    private static void SetupPanelAutoScroll(Panel panel)
    {
        if (panel.Controls.Count > 0)
        {
            int maxY = 0;
            foreach (System.Windows.Forms.Control control in panel.Controls)
            {
                maxY = Math.Max(maxY, control.Bottom);
            }

            panel.AutoScroll = maxY > panel.ClientSize.Height;
        }
    }

    public static Dictionary<string, object> ExtractCardData(Control card)
    {
        if (card is Button button && button.Tag is CardData cardData)
        {
            return cardData.Data;
        }
        return new Dictionary<string, object>();
    }

    public static string GetCardDataType(Control card)
    {
        if (card is Button button && button.Tag is CardData cardData)
        {
            return cardData.DataType;
        }
        return string.Empty;
    }

    public static IEnumerable<Control> FilterCardsByType(IEnumerable<Control> cards, string dataType)
    {
        return cards.Where(card => GetCardDataType(card) == dataType);
    }

    public static Control FindCardByFieldValue(IEnumerable<Control> cards, string fieldName, object value)
    {
        foreach (var card in cards)
        {
            var data = ExtractCardData(card);
            if (data.ContainsKey(fieldName) && data[fieldName] != null && data[fieldName].Equals(value))
            {
                return card;
            }
        }
        return null;
    }
}