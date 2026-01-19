using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

public static class StartupApiChecker
{
    public static async Task<bool> EnsureApiConnectionBeforeStartup()
    {
        try
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(3);
                try
                {
                    HttpResponseMessage response = await client.GetAsync("http://localhost:8000/check_connection_to_api");
                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();

                            return true;
                        }
                        else
                        {
                            ShowErrorMessage($"{response.StatusCode}");
                        }
                    }
                }
                catch (HttpRequestException)
                {
                    ShowErrorMessage("Сервер API не отвечает");
                    return false;
                }
                catch (TaskCanceledException)
                {
                    ShowErrorMessage("API недоступен. Проверьте запуск API.");
                    return false;
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Ошибка подключения: {ex.Message}");
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrorMessage($"Ошибка при проверке соединения: {ex.Message}");
        }
        return false;
    }

    private static void ShowErrorMessage(string message)
    {
        MessageBox.Show(
            $"{message}\nПриложение будет закрыто.",
            "Ошибка соединения с API",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }
}