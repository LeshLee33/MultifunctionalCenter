from fastapi import APIRouter, HTTPException, Query
from fastapi.responses import JSONResponse
from src.server.database import Service, Appointment
from .handlers import (handler_get_services, handler_add_service, handler_edit_service, handler_get_appointments,
                        handler_add_appointment, handler_edit_appointment, handler_delete_appointment,
                        handler_get_appointments_history, handler_appointments_expiration_controller)
from src.server.utils import ENDPOINT_SUCCESS, ENDPOINT_DATA_NOT_FOUND, ENDPOINT_ALREADY_EXISTING_DATA, \
    ENDPOINT_UPDATING_FAILURE

router = APIRouter()
service_router = APIRouter()
appointments_router = APIRouter()
served_appointments_router = APIRouter()


@router.get("/check_connection_to_api", response_model=None)
def check_connection_to_api() -> JSONResponse:
    return JSONResponse(status_code=200, content="Соединение с API активно")


@service_router.get("/service/get_services", response_model=None)
def get_services() -> JSONResponse | HTTPException:
    try:
        result = handler_get_services(query=None)
        if result['code'] == ENDPOINT_SUCCESS and result['content'] != []:
            return JSONResponse(status_code=200, content=result['content'])
        elif result['code'] == ENDPOINT_DATA_NOT_FOUND:
            raise HTTPException(status_code=404, detail="Услуги по данному запросу не найдены")
        elif result['code'] != ENDPOINT_SUCCESS:
            raise HTTPException(status_code=400, detail=f"Ошибка получения списка услуг: {result['content']}")
        else:
            raise HTTPException(status_code=400, detail="Услуги по данному запросу не найдены, исполнение запроса остановлено")
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Ошибка выполнения запроса: {e}")


@service_router.get("/service/get_services_by_title", response_model=None)
def get_services_by_title(title: str = Query()) -> JSONResponse | HTTPException:
    try:
        result = handler_get_services(query=dict(service_title={"$regex": title}))
        if result['code'] == ENDPOINT_SUCCESS and result['content'] != []:
            return JSONResponse(status_code=200, content=result['content'])
        elif result['code'] == ENDPOINT_DATA_NOT_FOUND:
            raise HTTPException(status_code=404, detail="Услуги по данному запросу не найдены")
        elif result['code'] != ENDPOINT_SUCCESS:
            raise HTTPException(status_code=400, detail=f"Ошибка получения списка услуг: {result['content']}")
        else:
            raise HTTPException(status_code=400, detail="Услуги по данному запросу не найдены, исполнение запроса остановлено")
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Ошибка выполнения запроса: {e}")


@service_router.post("/service/add_service", response_model=None)
def add_service(data: Service) -> JSONResponse | HTTPException:
    try:
        result = handler_add_service(data=dict(
                                           service_type=data.service_type,
                                           service_title=data.service_title,
                                           service_required_documents=data.service_required_documents,
                                           service_government_structure=data.service_government_structure,
                                           service_duration_days=data.service_duration_days))
        if result['code'] == ENDPOINT_SUCCESS and result['content'] != []:
            return JSONResponse(status_code=200, content=result['content'])
        elif result['code'] == ENDPOINT_ALREADY_EXISTING_DATA:
            raise HTTPException(status_code=403, detail="Услуга с данными параметрами уже существует")
        elif result['code'] != ENDPOINT_SUCCESS:
            raise HTTPException(status_code=400, detail=f"Ошибка добавления услуги: {result['content']}")
        else:
            raise HTTPException(status_code=400, detail="Услуга не была добавлена")
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Ошибка выполнения запроса: {e}")


@service_router.patch("/service/edit_service", response_model=None)
def edit_service(query: Service, new_data: Service) -> JSONResponse | HTTPException:
    try:
        result = handler_edit_service(query=dict(
                                           service_type=query.service_type,
                                           service_title=query.service_title,
                                           service_required_documents=query.service_required_documents,
                                           service_government_structure=query.service_government_structure,
                                           service_duration_days=query.service_duration_days),
                                       new_data={"$set": dict(
                                           service_type=new_data.service_type,
                                           service_title=new_data.service_title,
                                           service_required_documents=new_data.service_required_documents,
                                           service_government_structure=new_data.service_government_structure,
                                           service_duration_days=new_data.service_duration_days)}
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
        result = handler_get_appointments(query=None)
        if result['code'] == ENDPOINT_SUCCESS:
            return JSONResponse(status_code=200, content=result['content'])
        elif result['code'] == ENDPOINT_DATA_NOT_FOUND:
            return JSONResponse(status_code=200, content=[])
        elif result['code'] != ENDPOINT_SUCCESS and result['code'] != ENDPOINT_DATA_NOT_FOUND:
            raise HTTPException(status_code=400, detail=f"Ошибка получения списка записей: {result}")
        else:
            raise HTTPException(status_code=404, detail=f"Записи по данному запросу не найдены, возможно некорректный запрос: {result['content']}")
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Ошибка выполнения запроса: {e}")


@appointments_router.get("/appointments/get_appointments_by_passport", response_model=None)
def get_appointments_by_passport(passport_number: str, passport_series: str = "") -> JSONResponse | HTTPException:
    try:
        result = handler_get_appointments(query=dict(
            appointment_client_passport_series={"$regex": passport_series},
            appointment_client_passport_number={"$regex": passport_number})
        ) if passport_series != "" else handler_get_appointments(query=dict(
            appointment_client_passport_number={"$regex": passport_number})
        )
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


@appointments_router.post("/appointments/add_appointment", response_model=None)
def add_appointment(data: Appointment):
    try:
        result = handler_add_appointment(data=dict(
            appointment_service_title=data.appointment_service_title,
            appointment_client_passport_series=data.appointment_client_passport_series,
            appointment_client_passport_number=data.appointment_client_passport_number,
            appointment_client_surname=data.appointment_client_surname,
            appointment_client_name=data.appointment_client_name,
            appointment_client_patronymic=data.appointment_client_patronymic,
            appointment_nationality=data.appointment_nationality,
            appointment_date=data.appointment_date,
            appointment_daytime=data.appointment_daytime))
        if result['code'] == ENDPOINT_SUCCESS and result['content'] != []:
            return JSONResponse(status_code=200, content=result['content'])
        elif result['code'] == ENDPOINT_ALREADY_EXISTING_DATA:
            raise HTTPException(status_code=403, detail="Запись с данными параметрами уже существует")
        elif result['code'] != ENDPOINT_SUCCESS:
            raise HTTPException(status_code=400, detail=f"Ошибка добавления записи: {result['content']}")
        else:
            raise HTTPException(status_code=400, detail="Запись не была добавлена")
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Ошибка выполнения запроса: {e}")


@appointments_router.patch("/appointments/edit_appointment", response_model=None)
def edit_appointment(old_data: Appointment, new_data: Appointment):
    try:
        result = handler_edit_appointment(query=dict(
            appointment_service_title=old_data.appointment_service_title,
            appointment_client_passport_series=old_data.appointment_client_passport_series,
            appointment_client_passport_number=old_data.appointment_client_passport_number,
            appointment_date=old_data.appointment_date,
            appointment_daytime=old_data.appointment_daytime),
                                           new_data={"$set": dict(
            appointment_service_title=new_data.appointment_service_title,
            appointment_client_passport_series=new_data.appointment_client_passport_series,
            appointment_client_passport_number=new_data.appointment_client_passport_number,
            appointment_client_surname=new_data.appointment_client_surname,
            appointment_client_name=new_data.appointment_client_name,
            appointment_client_patronymic=new_data.appointment_client_patronymic,
            appointment_nationality=new_data.appointment_nationality,
            appointment_date=new_data.appointment_date,
            appointment_daytime=new_data.appointment_daytime)}
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


@appointments_router.delete("/appointments/delete_appointment", response_model=None)
def delete_appointment(query: Appointment):
    try:
        result = handler_delete_appointment(query=dict(
            appointment_service_title=query.appointment_service_title,
            appointment_client_passport_series=query.appointment_client_passport_series,
            appointment_client_passport_number=query.appointment_client_passport_number,
            appointment_client_surname=query.appointment_client_surname,
            appointment_client_name=query.appointment_client_name,
            appointment_client_patronymic=query.appointment_client_patronymic,
            appointment_nationality=query.appointment_nationality,
            appointment_date=query.appointment_date,
            appointment_daytime=query.appointment_daytime))
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
        result = handler_get_appointments_history(query=None)
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


@served_appointments_router.get("/served_appointments/get_served_appointments_by_passport", response_model=None)
def get_served_appointments_by_passport(passport_number: str, passport_series: str = "") -> JSONResponse | HTTPException:
    try:
        result = handler_get_appointments_history(query=dict(
            appointment_client_passport_series={"$regex": passport_series},
            appointment_client_passport_number={"$regex": passport_number})
        ) if passport_series != "" else handler_get_appointments_history(query=dict(
            appointment_client_passport_number={"$regex": passport_number})
        )
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


@served_appointments_router.get("/served_appointments/check_appointment_expiration", response_model=None)
def check_appointment_expiration():
    try:
        result = handler_appointments_expiration_controller()
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
