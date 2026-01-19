using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

public static class ServiceCardManager
{
    public static async Task LoadServicesIntoPanel(Panel targetPanel, string searchText = "")
    {
        if (targetPanel == null) return;

        try
        {
            ShowLoadingMessage(targetPanel);

            string response = await GetServicesFromApi(searchText);

            if (string.IsNullOrEmpty(response) || response.Contains("Ошибка"))
            {
                ShowErrorMessage(targetPanel, "Услуг не найдено");
                return;
            }

            var services = ParseServicesFromJson(response);

            if (services.Count == 0)
            {
                ShowNoResultsMessage(targetPanel);
                return;
            }

            DisplayAdaptiveServiceCards(targetPanel, services);
        }
        catch (Exception ex)
        {
            ShowErrorMessage(targetPanel, $"Ошибка: {ex.Message}");
        }
    }

    private static async Task<string> GetServicesFromApi(string searchText)
    {
        if (string.IsNullOrEmpty(searchText))
        {
            return await ApiClient.ApiFunctions.GetServices();
        }
        else
        {
            return await ApiClient.ApiFunctions.GetServicesByTitle(searchText);
        }
    }

    private static List<ApiClient.Service> ParseServicesFromJson(string json)
    {
        var services = new List<ApiClient.Service>();

        try
        {
            using (var doc = JsonDocument.Parse(json))
            {
                var array = doc.RootElement;
                return ParseServicesArrayRecursive(array, services, 0);
            }
        }
        catch
        {
            return services;
        }
    }

    private static List<ApiClient.Service> ParseServicesArrayRecursive(
        JsonElement array,
        List<ApiClient.Service> services,
        int index)
    {
        if (index >= array.GetArrayLength()) return services;

        var element = array[index];
        var service = ParseServiceElement(element);

        var newServices = new List<ApiClient.Service>(services);
        newServices.Add(service);

        return ParseServicesArrayRecursive(array, newServices, index + 1);
    }

    private static ApiClient.Service ParseServiceElement(JsonElement element)
    {
        string type = "";
        string title = "";
        List<string> documents = new List<string>();
        string government = "";
        int duration = 0;

        foreach (var property in element.EnumerateObject())
        {
            ProcessServiceProperty(
                property,
                ref type,
                ref title,
                ref documents,
                ref government,
                ref duration);
        }

        return new ApiClient.Service(type, title, documents, government, duration);
    }

    private static void ProcessServiceProperty(
        JsonProperty property,
        ref string type,
        ref string title,
        ref List<string> documents,
        ref string government,
        ref int duration)
    {
        switch (property.Name.ToLower())
        {
            case "service_type":
                type = property.Value.GetString() ?? "";
                break;

            case "service_title":
                title = property.Value.GetString() ?? "";
                break;

            case "service_required_documents":
                if (property.Value.ValueKind == JsonValueKind.Array)
                {
                    documents = ParseDocumentsArrayRecursive(property.Value, new List<string>(), 0);
                }
                break;

            case "service_government_structure":
                government = property.Value.GetString() ?? "";
                break;

            case "service_duration_days":
                duration = property.Value.GetInt32();
                break;
        }
    }

    private static List<string> ParseDocumentsArrayRecursive(
        JsonElement array,
        List<string> documents,
        int index)
    {
        if (index >= array.GetArrayLength()) return documents;

        var document = array[index].GetString() ?? "";
        var newDocuments = new List<string>(documents);
        newDocuments.Add(document);

        return ParseDocumentsArrayRecursive(array, newDocuments, index + 1);
    }

    private static void DisplayAdaptiveServiceCards(Panel panel, List<ApiClient.Service> apiServices)
    {
        if (panel.InvokeRequired)
        {
            panel.Invoke(new Action(() => DisplayAdaptiveServiceCardsInternal(panel, apiServices)));
            return;
        }

        DisplayAdaptiveServiceCardsInternal(panel, apiServices);
    }

    private static void DisplayAdaptiveServiceCardsInternal(Panel panel, List<ApiClient.Service> apiServices)
    {
        panel.Controls.Clear();
        panel.AutoScroll = false;

        int panelWidth = panel.ClientSize.Width;

        CreateServiceCardsRecursive(panel, apiServices, panelWidth, 0, 5);

        SetupAutoScroll(panel);
    }

    private static void CreateServiceCardsRecursive(
        Panel panel,
        List<ApiClient.Service> apiServices,
        int panelWidth,
        int index,
        int currentY)
    {
        if (index >= apiServices.Count) return;

        var apiService = apiServices[index];

        var card = CreateAdaptiveServiceCard(apiService, panelWidth - 10);

        card.Location = new Point(5, currentY);

        panel.Controls.Add(card);

        CreateServiceCardsRecursive(
            panel,
            apiServices,
            panelWidth,
            index + 1,
            currentY + card.Height + 5);
    }

    private static Button CreateAdaptiveServiceCard(ApiClient.Service apiService, int panelWidth)
    {
        var service = new Service
        {
            ServiceType = apiService.ServiceType,
            ServiceTitle = apiService.ServiceTitle,
            ServiceRequiredDocuments = apiService.ServiceRequiredDocuments,
            ServiceGovernmentStructure = apiService.ServiceGovernmentStructure,
            ServiceDurationDays = apiService.ServiceDurationDays
        };

        int cardWidth = CalculateCardWidth(panelWidth);

        return CardBuilders.BuildAdaptiveServiceCard(service, cardWidth);
    }

    private static int CalculateCardWidth(int panelWidth)
    {
        return Math.Max(panelWidth - 15, 100);
    }

    private static void SetupAutoScroll(Panel panel)
    {
        if (panel.Controls.Count == 0) return;

        int maxBottom = 0;
        foreach (Control control in panel.Controls)
        {
            maxBottom = Math.Max(maxBottom, control.Bottom);
        }

        panel.AutoScroll = maxBottom > panel.ClientSize.Height;
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

        int currentY = 5;

        foreach (Control control in panel.Controls)
        {
            if (control is Button card)
            {
                card.Width = cardWidth;

                card.Height = CalculateCardHeightForText(card.Text, cardWidth);

                card.Location = new Point(5, currentY);

                currentY += card.Height + 5;
            }
        }

        SetupAutoScroll(panel);
    }

    private static int CalculateCardHeightForText(string text, int width)
    {
        if (string.IsNullOrEmpty(text)) return 60;

        try
        {
            using (var bitmap = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(bitmap))
            {
                var font = new Font("Segoe UI", 9, FontStyle.Regular);

                var size = g.MeasureString(text, font, width - 16);

                return Math.Max(60, Math.Min((int)Math.Ceiling(size.Height) + 16, 200));
            }
        }
        catch
        {
            return 80;
        }
    }

    private static void ShowLoadingMessage(Panel panel)
    {
        SetPanelMessage(panel, "Загрузка услуг...", Color.Black);
    }

    private static void ShowErrorMessage(Panel panel, string message)
    {
        SetPanelMessage(panel, message, Color.Black);
    }

    private static void ShowNoResultsMessage(Panel panel)
    {
        SetPanelMessage(panel, "Услуги не найдены", Color.Gray);
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
            await LoadServicesIntoPanel(resultsPanel, searchBox.Text);
        };
    }

    public static async Task LoadAllServices(Panel targetPanel)
    {
        await LoadServicesIntoPanel(targetPanel, "");
    }

    public static ApiClient.Service GetSelectedServiceFromPanel(Panel panel)
    {
        if (panel == null || panel.Controls.Count == 0) return null;

        foreach (Control control in panel.Controls)
        {
            if (control is Button card && card.Focused)
            {
                return ExtractServiceFromCard(card);
            }
        }

        return null;
    }

    private static ApiClient.Service ExtractServiceFromCard(Button card)
    {
        if (card.Tag is CardBuilders.CardData cardData && cardData.DataType == "Service")
        {
            var data = cardData.Data;

            List<string> documents = new List<string>();
            if (data.ContainsKey("ServiceRequiredDocuments") &&
                data["ServiceRequiredDocuments"] is string docsStr)
            {
                if (!string.IsNullOrEmpty(docsStr))
                {
                    documents = docsStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                      .Select(d => d.Trim())
                                      .ToList();
                }
            }

            return new ApiClient.Service(
                serviceType: data.ContainsKey("ServiceType") ? data["ServiceType"].ToString() : "",
                serviceTitle: data.ContainsKey("ServiceTitle") ? data["ServiceTitle"].ToString() : "",
                serviceRequiredDocuments: documents,
                serviceGovernmentStructure: data.ContainsKey("ServiceGovernmentStructure")
                    ? data["ServiceGovernmentStructure"].ToString()
                    : "",
                serviceDurationDays: data.ContainsKey("ServiceDurationDays") &&
                                   int.TryParse(data["ServiceDurationDays"].ToString(), out int duration)
                    ? duration
                    : 0
            );
        }

        return null;
    }
}