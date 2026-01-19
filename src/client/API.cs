using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public sealed class Appointment
    {
        public string AppointmentServiceTitle { get; }
        public string AppointmentClientPassportSeries { get; }
        public string AppointmentClientPassportNumber { get; }
        public string AppointmentClientSurname { get; }
        public string AppointmentClientName { get; }
        public string AppointmentClientPatronymic { get; }
        public string AppointmentNationality { get; }
        public string AppointmentDate { get; }
        public string AppointmentDaytime { get; }

        public Appointment(
            string appointmentServiceTitle,
            string appointmentClientPassportSeries,
            string appointmentClientPassportNumber,
            string appointmentClientSurname,
            string appointmentClientName,
            string appointmentClientPatronymic,
            string appointmentNationality,
            string appointmentDate,
            string appointmentDaytime)
        {
            AppointmentServiceTitle = appointmentServiceTitle;
            AppointmentClientPassportSeries = appointmentClientPassportSeries;
            AppointmentClientPassportNumber = appointmentClientPassportNumber;
            AppointmentClientSurname = appointmentClientSurname;
            AppointmentClientName = appointmentClientName;
            AppointmentClientPatronymic = appointmentClientPatronymic;
            AppointmentNationality = appointmentNationality;
            AppointmentDate = appointmentDate;
            AppointmentDaytime = appointmentDaytime;
        }
    }

    public sealed class Service
    {
        public string ServiceType { get; }
        public string ServiceTitle { get; }
        public List<string> ServiceRequiredDocuments { get; }
        public string ServiceGovernmentStructure { get; }
        public int ServiceDurationDays { get; }

        public Service(
            string serviceType,
            string serviceTitle,
            List<string> serviceRequiredDocuments,
            string serviceGovernmentStructure,
            int serviceDurationDays)
        {
            ServiceType = serviceType;
            ServiceTitle = serviceTitle;
            ServiceRequiredDocuments = serviceRequiredDocuments;
            ServiceGovernmentStructure = serviceGovernmentStructure;
            ServiceDurationDays = serviceDurationDays;
        }
    }

    public static class ApiFunctions
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:8000/")
        };

        private static async Task<HttpResponseMessage> PatchAsync(HttpClient client, string requestUri, HttpContent content)
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };
            return await client.SendAsync(request);
        }

        private static async Task<string> ExecuteRequestAsync(
            Func<HttpClient, Task<HttpResponseMessage>> requestFunc,
            Func<HttpResponseMessage, Task<string>> errorHandler)
        {
            HttpResponseMessage response = null;

            return await ExecuteRequestRecursiveAsync(
                requestFunc,
                errorHandler,
                response,
                3);
        }

        private static async Task<string> ExecuteRequestRecursiveAsync(
            Func<HttpClient, Task<HttpResponseMessage>> requestFunc,
            Func<HttpResponseMessage, Task<string>> errorHandler,
            HttpResponseMessage response,
            int attempts)
        {
            if (attempts <= 0)
                return "Превышено количество попыток запроса";

            try
            {
                response = await requestFunc(_httpClient);

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();

                return await errorHandler(response);
            }
            catch (HttpRequestException)
            {
                return await ExecuteRequestRecursiveAsync(
                    requestFunc,
                    errorHandler,
                    response,
                    attempts - 1);
            }
            catch (Exception ex)
            {
                return await Task.FromResult($"Общая ошибка: {ex.Message}");
            }
        }

        private static async Task<string> HandleErrorResponse(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return $"Ошибка {response.StatusCode}: {content}";
        }

        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                WriteIndented = false
            };
        }

        public class SnakeCaseNamingPolicy : JsonNamingPolicy
        {
            public override string ConvertName(string name)
            {
                if (string.IsNullOrEmpty(name))
                    return name;

                var result = new StringBuilder();
                result.Append(char.ToLower(name[0]));

                for (int i = 1; i < name.Length; i++)
                {
                    if (char.IsUpper(name[i]))
                    {
                        result.Append('_');
                        result.Append(char.ToLower(name[i]));
                    }
                    else
                    {
                        result.Append(name[i]);
                    }
                }

                return result.ToString();
            }
        }

        private static string SerializeObject(object obj)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
                WriteIndented = false
            };

            return JsonSerializer.Serialize(obj, options);
        }

        public static async Task<string> CheckConnectionToApi() =>
            await ExecuteRequestAsync(
                client => client.GetAsync("check_connection_to_api"),
                HandleErrorResponse);

        public static async Task<string> GetServices() =>
            await ExecuteRequestAsync(
                client => client.GetAsync("service/get_services"),
                HandleErrorResponse);

        public static async Task<string> GetServicesByTitle(string title) =>
            await ExecuteRequestAsync(
                client => client.GetAsync($"service/get_services_by_title?title={Uri.EscapeDataString(title)}"),
                HandleErrorResponse);

        public static async Task<string> AddService(Service service) =>
            await ExecuteRequestAsync(
                client =>
                {
                    var json = SerializeObject(service);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    return client.PostAsync("service/add_service", content);
                },
                HandleErrorResponse);

        public static async Task<string> EditServiceWithOriginal(Service originalService, Dictionary<string, object> query, Dictionary<string, object> newData)
        {
            return await ExecuteRequestAsync(
                client =>
                {
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

                    var json = SerializeObject(requestData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    return PatchAsync(client, "service/edit_service", content);
                },
                HandleErrorResponse);
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

        public static async Task<string> GetAppointments() =>
            await ExecuteRequestAsync(
                client => client.GetAsync("appointments/get_appointments"),
                HandleErrorResponse);

        public static async Task<string> GetAppointmentsByPassport(string passportNumber, string passportSeries = "")
{
    return await ExecuteRequestAsync(
        client =>
        {
            string url = $"appointments/get_appointments_by_passport?passport_number={Uri.EscapeDataString(passportNumber)}&passport_series={Uri.EscapeDataString(passportSeries)}";
            return client.GetAsync(url);
        },
        async response =>
        {
            var content = await response.Content.ReadAsStringAsync();
            return $"Ошибка {response.StatusCode}: {content}";
        });
}

        public static async Task<string> AddAppointment(Appointment appointment) =>
            await ExecuteRequestAsync(
                client =>
                {
                    var json = SerializeObject(appointment);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    return client.PostAsync("appointments/add_appointment", content);
                },
                HandleErrorResponse);

        public static async Task<string> EditAppointment(
            Appointment oldAppointment,
            string newDate,
            string newDaytime)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8000/");
                    client.Timeout = TimeSpan.FromSeconds(30);

                    var oldData = new
                    {
                        appointment_service_title = oldAppointment.AppointmentServiceTitle ?? "",
                        appointment_client_passport_series = oldAppointment.AppointmentClientPassportSeries ?? "",
                        appointment_client_passport_number = oldAppointment.AppointmentClientPassportNumber ?? "",
                        appointment_client_surname = oldAppointment.AppointmentClientSurname ?? "",
                        appointment_client_name = oldAppointment.AppointmentClientName ?? "",
                        appointment_client_patronymic = oldAppointment.AppointmentClientPatronymic ?? "",
                        appointment_nationality = oldAppointment.AppointmentNationality ?? "",
                        appointment_date = oldAppointment.AppointmentDate ?? "",
                        appointment_daytime = oldAppointment.AppointmentDaytime ?? ""
                    };

                    var newData = new
                    {
                        appointment_service_title = oldAppointment.AppointmentServiceTitle ?? "",
                        appointment_client_passport_series = oldAppointment.AppointmentClientPassportSeries ?? "",
                        appointment_client_passport_number = oldAppointment.AppointmentClientPassportNumber ?? "",
                        appointment_client_surname = oldAppointment.AppointmentClientSurname ?? "",
                        appointment_client_name = oldAppointment.AppointmentClientName ?? "",
                        appointment_client_patronymic = oldAppointment.AppointmentClientPatronymic ?? "",
                        appointment_nationality = oldAppointment.AppointmentNationality ?? "",
                        appointment_date = newDate ?? "",
                        appointment_daytime = newDaytime ?? ""
                    };

                    var requestData = new
                    {
                        old_data = oldData,
                        new_data = newData
                    };

                    var json = JsonSerializer.Serialize(requestData, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                        WriteIndented = false
                    });

                    Console.WriteLine($"Отправляемый JSON для редактирования записи:\n{json}");

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var method = new HttpMethod("PATCH");
                    var request = new HttpRequestMessage(method, "appointments/edit_appointment")
                    {
                        Content = content
                    };

                    var response = await client.SendAsync(request);
                    var responseText = await response.Content.ReadAsStringAsync();

                    Console.WriteLine($"Ответ сервера: {response.StatusCode} - {responseText}");

                    if (!response.IsSuccessStatusCode)
                    {
                        return $"Ошибка {(int)response.StatusCode}: {responseText}";
                    }

                    return responseText;
                }
            }
            catch (Exception ex)
            {
                return $"Ошибка при редактировании записи: {ex.Message}";
            }
        }

        public static async Task<string> DeleteAppointment(Appointment appointment) =>
            await ExecuteRequestAsync(
                client =>
                {
                    var json = SerializeObject(appointment);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    return client.SendAsync(new HttpRequestMessage(HttpMethod.Delete, "appointments/delete_appointment")
                    {
                        Content = content
                    });
                },
                HandleErrorResponse);

        public static async Task<string> GetAppointmentsHistory() =>
            await ExecuteRequestAsync(
                client => client.GetAsync("served_appointments/get_appointments_history"),
                HandleErrorResponse);

        public static async Task<string> GetServedAppointmentsByPassport(string passportNumber, string passportSeries = "")
        {
            return await ExecuteRequestAsync(
                client =>
                {
                    string url = $"served_appointments/get_served_appointments_by_passport?passport_number={Uri.EscapeDataString(passportNumber)}&passport_series={Uri.EscapeDataString(passportSeries)}";
                    return client.GetAsync(url);
                },
                async response =>
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return $"Ошибка {response.StatusCode}: {content}";
                });
        }

        public static async Task<string> CheckAppointmentExpiration() =>
            await ExecuteRequestAsync(
                client => client.GetAsync("served_appointments/check_appointment_expiration"),
                HandleErrorResponse);

        public static List<string> ProcessDocuments(List<string> documents, Func<string, string> formatter) =>
            ProcessDocumentsRecursive(documents, formatter, new List<string>(), 0);

        private static List<string> ProcessDocumentsRecursive(
            List<string> documents,
            Func<string, string> formatter,
            List<string> processed,
            int index)
        {
            if (index >= documents.Count)
                return processed;

            processed.Add(formatter(documents[index]));
            return ProcessDocumentsRecursive(documents, formatter, processed, index + 1);
        }

        public static Dictionary<string, object> BuildQueryRecursive(
            List<(string key, object value)> parameters,
            Dictionary<string, object> query = null,
            int index = 0)
        {
            query = query ?? new Dictionary<string, object>();

            if (index >= parameters.Count)
                return query;

            query[parameters[index].key] = parameters[index].value;
            return BuildQueryRecursive(parameters, query, index + 1);
        }

        public static Dictionary<string, object> CreateServiceQuery(
            string serviceType = null,
            string serviceTitle = null,
            List<string> serviceRequiredDocuments = null,
            string serviceGovernmentStructure = null,
            int? serviceDurationDays = null)
        {
            var parameters = new List<(string, object)>();

            AddParameterIfNotNull("service_type", serviceType, parameters);
            AddParameterIfNotNull("service_title", serviceTitle, parameters);
            AddParameterIfNotNull("service_required_documents", serviceRequiredDocuments, parameters);
            AddParameterIfNotNull("service_government_structure", serviceGovernmentStructure, parameters);

            if (serviceDurationDays.HasValue)
                parameters.Add(("service_duration_days", serviceDurationDays.Value));

            return BuildQueryRecursive(parameters);
        }

        private static void AddParameterIfNotNull(
            string key,
            object value,
            List<(string, object)> parameters)
        {
            if (value != null)
                parameters.Add((key, value));
        }
    }
}