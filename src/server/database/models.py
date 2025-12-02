from typing import List
from pydantic import BaseModel


class Appointment(BaseModel):
    appointment_service_title: str
    appointment_client_passport_series: str
    appointment_client_passport_number: str
    appointment_client_surname: str
    appointment_client_name: str
    appointment_client_patronymic: str
    appointment_nationality: str
    appointment_date: str
    appointment_daytime: str


class ServedAppointment(BaseModel):
    appointment_service_title: str
    appointment_client_passport_series: str
    appointment_client_passport_number: str
    appointment_client_surname: str
    appointment_client_name: str
    appointment_client_patronymic: str
    appointment_nationality: str
    appointment_date: str
    appointment_daytime: str


class Service(BaseModel):
    service_type: str
    service_title: str
    service_required_documents: list[str]
    service_government_structure: str
    service_duration_days: int
