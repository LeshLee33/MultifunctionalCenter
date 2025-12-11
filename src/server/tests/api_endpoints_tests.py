from src.server.api import (endpoint_get_services, endpoint_add_service, endpoint_edit_service,
                            endpoint_get_appointments, endpoint_add_appointment, endpoint_edit_appointment,
                            endpoint_delete_appointment, endpoint_get_appointments_history,
                            endpoint_appointments_expiration_controller)
from src.server.utils import ENDPOINT_ALREADY_EXISTING_DATA, ENDPOINT_SUCCESS

TEST_SERVICE_ADDING_DATA = dict(service_type="test_service_type",
                                service_title="test_service_title",
                                service_required_documents=[
                                    "test_document_1",
                                    "test_document_2"
                                ],
                                service_government_structure="test_gov_structure",
                                service_duration_days=0)

TEST_SERVICE_UPDATE_DATA = dict(service_type="test_service_type",
                                service_title="test_title",
                                service_required_documents=[
                                    "test_document_1",
                                    "test_document_2"
                                ],
                                service_government_structure="test_government_structure",
                                service_duration_days=0)

TEST_APPOINTMENT_ADDING_DATA = dict(appointment_service_title="test_service_title",
                                    appointment_client_passport_series="0000",
                                    appointment_client_passport_number="000000",
                                    appointment_client_surname="test_client_surname",
                                    appointment_client_name="test_client_name",
                                    appointment_client_patronymic="test_client_patronymic",
                                    appointment_nationality="test_country",
                                    appointment_date="12.12.2025",
                                    appointment_daytime="10:00")


class TestClassEndpoints:
    def test_endpoint_services_getting(self):
        query = dict(service_type="Паспортные услуги")
        result = endpoint_get_services(query=query)
        print(result)
        assert len(result['content']) >= 1 and not isinstance(result['content'], str)


    def test_endpoint_services_adding(self):
        result = endpoint_add_service(data=TEST_SERVICE_ADDING_DATA)
        print(result)
        assert result['code'] == ENDPOINT_SUCCESS


    def test_endpoint_services_adding_existing_data(self):
        endpoint_add_service(data=TEST_SERVICE_ADDING_DATA)
        result = endpoint_add_service(data=TEST_SERVICE_ADDING_DATA)
        print(result)
        assert result['code'] == ENDPOINT_ALREADY_EXISTING_DATA


    def test_endpoint_services_editing(self):
        result = endpoint_edit_service(query=TEST_SERVICE_ADDING_DATA,
                                       new_data={"$set": dict(
                                           service_type=TEST_SERVICE_UPDATE_DATA['service_type'],
                                           service_title=TEST_SERVICE_UPDATE_DATA['service_title'],
                                           service_required_documents=TEST_SERVICE_UPDATE_DATA['service_required_documents'],
                                           service_government_structure=TEST_SERVICE_UPDATE_DATA['service_government_structure'],
                                           service_duration_days=TEST_SERVICE_UPDATE_DATA['service_duration_days'])})
        print(result)
        assert result['code'] == ENDPOINT_SUCCESS


    def test_endpoint_appointments_getting(self):
        query = dict(appointment_service_title={"$regex": "Получение"})
        result = endpoint_get_appointments(query=query)
        print(result)
        assert len(result['content']) >= 1 and not isinstance(result['content'], str)


    def test_endpoint_appointments_adding(self):
        result = endpoint_add_appointment(data=TEST_APPOINTMENT_ADDING_DATA)
        print(result)
        assert result['code'] == ENDPOINT_SUCCESS


    def test_endpoint_appointments_adding_existing_data(self):
        endpoint_add_appointment(data=TEST_APPOINTMENT_ADDING_DATA)
        result = endpoint_add_appointment(data=TEST_APPOINTMENT_ADDING_DATA)
        print(result)
        assert result['code'] == ENDPOINT_ALREADY_EXISTING_DATA


    def test_endpoint_appointment_editing(self):
        result = endpoint_edit_appointment(query=TEST_APPOINTMENT_ADDING_DATA,
                                           new_data={"$set": dict(appointment_service_title="test_title")})
        print(result)
        assert result['code'] == ENDPOINT_SUCCESS


    def test_endpoint_appointment_deletion(self):
        result = endpoint_delete_appointment(query=dict(appointment_client_name="test_client_name"))
        print(result)
        assert result['code'] == ENDPOINT_SUCCESS


    def test_endpoint_appointments_history_getting(self):
        result = endpoint_get_appointments_history()
        print(result)
        assert len(result['content']) >= 1 and not isinstance(result['content'], str)


    def test_endpoint_appointments_expiration_control(self):
        result = endpoint_appointments_expiration_controller()
        print(result)
        assert result['code'] == ENDPOINT_SUCCESS
