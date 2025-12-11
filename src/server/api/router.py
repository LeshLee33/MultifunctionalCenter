from typing import List
from fastapi import APIRouter, HTTPException, Query
from fastapi.responses import JSONResponse
from src.server.database import Service, Appointment, ServedAppointment
from .endpoints import (endpoint_get_services, endpoint_add_service, endpoint_edit_service, endpoint_get_appointments,
                        endpoint_add_appointment, endpoint_edit_appointment, endpoint_delete_appointment,
                        endpoint_get_appointments_history, endpoint_appointments_expiration_controller)
from src.server.utils import ENDPOINT_SUCCESS, ENDPOINT_DATA_NOT_FOUND, ENDPOINT_ALREADY_EXISTING_DATA, \
    ENDPOINT_UPDATING_FAILURE

router = APIRouter()
service_router = APIRouter()
appointments_router = APIRouter()
served_appointments_router = APIRouter()


@service_router.get("/service/get_services", response_model=None)
def get_services() -> JSONResponse | HTTPException:
    try:
        result = endpoint_get_services(query=None)
        if result['code'] == ENDPOINT_SUCCESS and result['content'] != []:
            return JSONResponse(status_code=200, content=result['content'])
        elif result['code'] == ENDPOINT_DATA_NOT_FOUND:
            raise HTTPException(status_code=404, detail="Услуги по данному запросу не найдены")
        elif result['code'] != ENDPOINT_SUCCESS:
            raise HTTPException(status_code=400, detail=f"Ошибка получения списка услуг: {result['content']}")
        else:
            raise HTTPException(status_code=400, detail="Услуги по данному запросу не найдены, возможно некорректный запрос")
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Ошибка выполнения запроса: {e}")


@service_router.get("/service/get_services_by_title", response_model=None)
def get_services_by_title(title: str = Query()) -> JSONResponse | HTTPException:
    try:
        result = endpoint_get_services(query=dict(service_title={"$regex": title}))
        if result['code'] == ENDPOINT_SUCCESS and result['content'] != []:
            return JSONResponse(status_code=200, content=result['content'])
        elif result['code'] == ENDPOINT_DATA_NOT_FOUND:
            raise HTTPException(status_code=404, detail="Услуги по данному запросу не найдены")
        elif result['code'] != ENDPOINT_SUCCESS:
            raise HTTPException(status_code=400, detail=f"Ошибка получения списка услуг: {result['content']}")
        else:
            raise HTTPException(status_code=400, detail="Услуги по данному запросу не найдены")
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Ошибка выполнения запроса: {e}")


@service_router.post("/service/add_service", response_model=None)
def add_service(data: dict) -> JSONResponse | HTTPException:
    try:
        result = endpoint_add_service(data=data)
        if result['code'] == ENDPOINT_SUCCESS and result['content'] != []:
            return JSONResponse(status_code=200, content=result['content'])
        elif result['code'] == ENDPOINT_ALREADY_EXISTING_DATA:
            raise HTTPException(status_code=404, detail="Услуга с данными параметрами уже существует")
        elif result['code'] != ENDPOINT_SUCCESS:
            raise HTTPException(status_code=400, detail=f"Ошибка добавления услуги: {result['content']}")
        else:
            raise HTTPException(status_code=400, detail="Услуга не была добавлена")
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Ошибка выполнения запроса: {e}")


@service_router.patch("/service/edit_service", response_model=None)
def edit_service(query: dict, new_data: dict) -> JSONResponse | HTTPException:
    try:
        result = endpoint_edit_service(query=query,
                                       new_data={"$set": dict(
                                           service_type=new_data['service_type'],
                                           service_title=new_data['service_title'],
                                           service_required_documents=new_data['service_required_documents'],
                                           service_government_structure=new_data['service_government_structure'],
                                           service_duration_days=new_data['service_duration_days'])}
                                       )
        if result['code'] == ENDPOINT_SUCCESS and result['content'] != []:
            return JSONResponse(status_code=200, content=result['content'])
        elif result['code'] == ENDPOINT_DATA_NOT_FOUND:
            raise HTTPException(status_code=404, detail="Услуга с данными параметрами не существует")
        elif result['code'] == ENDPOINT_UPDATING_FAILURE:
            raise HTTPException(status_code=400, detail="Услуга не была обновлена")
        elif result['code'] != ENDPOINT_SUCCESS:
            raise HTTPException(status_code=400, detail=f"Ошибка обновления услуги: {result['content']}")
        else:
            raise HTTPException(status_code=400, detail="Услуга не была обновлена")
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Ошибка выполнения запроса: {e}")


@appointments_router.get("/appointments/get_appointments", response_model=None)
def get_appointments() -> JSONResponse | HTTPException:
    try:
        result = endpoint_get_appointments(query=None)
        if result['code'] == ENDPOINT_SUCCESS and result['content'] != []:
            return JSONResponse(status_code=200, content=result['content'])
        elif result['code'] == ENDPOINT_DATA_NOT_FOUND:
            raise HTTPException(status_code=404, detail="Записи по данному запросу не найдены")
        elif result['code'] != ENDPOINT_SUCCESS:
            raise HTTPException(status_code=400, detail=f"Ошибка получения списка записей: {result['content']}")
        else:
            raise HTTPException(status_code=400, detail="Записи по данному запросу не найдены, возможно некорректный запрос")
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Ошибка выполнения запроса: {e}")


@appointments_router.get("/appointments/get_appointment", response_model=None)
def get_appointment(query: Appointment = Query()):
    try:
        result = endpoint_get_appointments(query=dict(
            appointment_service_title=query.appointment_service_title,
            appointment_client_passport_series=query.appointment_client_passport_series,
            appointment_client_passport_number=query.appointment_client_passport_number,
            appointment_client_surname=query.appointment_client_surname,
            appointment_client_name=query.appointment_client_name,
            appointment_client_patronymic=query.appointment_client_patronymic,
            appointment_nationality=query.appointment_nationality,
            appointment_date=query.appointment_date,
            appointment_daytime=query.appointment_daytime)
        )
        if result['code'] == ENDPOINT_SUCCESS and result['content'] != []:
            return JSONResponse(status_code=200, content=result['content'])
        elif result['code'] == ENDPOINT_DATA_NOT_FOUND:
            raise HTTPException(status_code=404, detail="Запись по данному запросу не найдена")
        elif result['code'] != ENDPOINT_SUCCESS:
            raise HTTPException(status_code=400, detail=f"Ошибка получения записи: {result['content']}")
        else:
            raise HTTPException(status_code=400, detail="Запись по данному запросу не найдена, возможно некорректный запрос")
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Ошибка выполнения запроса: {e}")


@appointments_router.post("/appointments/add_appointment", response_model=None)
def add_appointment(data: dict):
    try:
        result = endpoint_add_appointment(data=data)
        if result['code'] == ENDPOINT_SUCCESS and result['content'] != []:
            return JSONResponse(status_code=200, content=result['content'])
        elif result['code'] == ENDPOINT_ALREADY_EXISTING_DATA:
            raise HTTPException(status_code=404, detail="Запись с данными параметрами уже существует")
        elif result['code'] != ENDPOINT_SUCCESS:
            raise HTTPException(status_code=400, detail=f"Ошибка добавления записи: {result['content']}")
        else:
            raise HTTPException(status_code=400, detail="Запись не была добавлена")
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Ошибка выполнения запроса: {e}")


@appointments_router.patch("/appointments/edit_appointment", response_model=None)
def edit_appointment(query: dict, new_data: dict):
    try:
        result = endpoint_edit_appointment(query=query,
                                           new_data={"$set": dict(
                                               appointment_service_title=new_data['appointment_service_title'],
                                               appointment_client_passport_series=new_data['appointment_client_passport_series'],
                                               appointment_client_passport_number=new_data['appointment_client_passport_number'],
                                               appointment_client_surname=new_data['appointment_client_surname'],
                                               appointment_client_name=new_data['appointment_client_name'],
                                               appointment_client_patronymic=new_data['appointment_client_patronymic'],
                                               appointment_nationality=new_data['appointment_nationality'],
                                               appointment_date=new_data['appointment_date'],
                                               appointment_daytime=new_data['appointment_daytime'])}
                                           )
        if result['code'] == ENDPOINT_SUCCESS and result['content'] != []:
            return JSONResponse(status_code=200, content=result['content'])
        elif result['code'] == ENDPOINT_DATA_NOT_FOUND:
            raise HTTPException(status_code=404, detail="Запись с данными параметрами не существует")
        elif result['code'] == ENDPOINT_UPDATING_FAILURE:
            raise HTTPException(status_code=400, detail="Запись не была обновлена")
        elif result['code'] != ENDPOINT_SUCCESS:
            raise HTTPException(status_code=400, detail=f"Ошибка обновления записи: {result['content']}")
        else:
            raise HTTPException(status_code=400, detail="Запись не была обновлена, возможно некорректный запрос")
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Ошибка выполнения запроса: {e}")


@appointments_router.post("/appointments/delete_appointment", response_model=None)
def delete_appointment(query: dict):
    try:
        result = endpoint_delete_appointment(query=query)
        if result['code'] == ENDPOINT_SUCCESS and result['content'] != []:
            return JSONResponse(status_code=200, content=result['content'])
        elif result['code'] == ENDPOINT_DATA_NOT_FOUND:
            raise HTTPException(status_code=404, detail="Запись по данному запросу не найдена")
        elif result['code'] != ENDPOINT_SUCCESS:
            raise HTTPException(status_code=400, detail=f"Ошибка удаления записи: {result['content']}")
        else:
            raise HTTPException(status_code=400,
                                detail="Запись по данному запросу не найдена, возможно некорректный запрос")
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Ошибка выполнения запроса: {e}")


@served_appointments_router.get("/served_appointments/get_appointments_history", response_model=None)
def get_appointments_history():
    try:
        result = endpoint_get_appointments_history()
        if result['code'] == ENDPOINT_SUCCESS and result['content'] != []:
            return JSONResponse(status_code=200, content=result['content'])
        elif result['code'] == ENDPOINT_DATA_NOT_FOUND:
            raise HTTPException(status_code=404, detail="История записей не найдена")
        elif result['code'] != ENDPOINT_SUCCESS:
            raise HTTPException(status_code=400, detail=f"Ошибка получения истории записей: {result['content']}")
        else:
            raise HTTPException(status_code=400,
                                detail="История записей по данному запросу не найдена, возможно некорректный запрос")
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Ошибка выполнения запроса: {e}")


@served_appointments_router.get("/served_appointments/check_appointment_expiration", response_model=None)
def check_appointment_expiration():
    try:
        result = endpoint_appointments_expiration_controller()
        if result['code'] == ENDPOINT_SUCCESS and result['content'] != []:
            return JSONResponse(status_code=200, content=result['content'])
        elif result['code'] == ENDPOINT_DATA_NOT_FOUND:
            raise HTTPException(status_code=404, detail="Записи для переноса не были найдены")
        elif result['code'] != ENDPOINT_SUCCESS:
            raise HTTPException(status_code=400, detail=f"Ошибка переноса записей: {result['content']}")
        else:
            raise HTTPException(status_code=400, detail="Некорректный запрос")
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Ошибка выполнения запроса: {e}")
