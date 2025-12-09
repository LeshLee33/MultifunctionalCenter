from src.server.database import (appointments, served_appointments, service,
                                 Service, ServedAppointment, Appointment,
                                 create, read, update, delete)
from src.server.utils import (ENDPOINT_SUCCESS, ENDPOINT_EXCEPTION, ENDPOINT_ALREADY_EXISTING_DATA,
                              ENDPOINT_DATA_NOT_FOUND, ENDPOINT_UPDATING_FAILURE, ENDPOINT_DELETION_FAILURE,
                              DB_SUCCESS, DB_EXCEPTION, DB_ALREADY_EXISTING_DATA, DB_DATA_NOT_FOUND,
                              DB_UPDATING_FAILURE, DB_DELETION_FAILURE)


def _change_result_code(code: int) -> int:
    mapping = {
        DB_SUCCESS: ENDPOINT_SUCCESS,
        DB_DATA_NOT_FOUND: ENDPOINT_DATA_NOT_FOUND,
        DB_UPDATING_FAILURE: ENDPOINT_UPDATING_FAILURE,
        DB_ALREADY_EXISTING_DATA: ENDPOINT_ALREADY_EXISTING_DATA,
        DB_DELETION_FAILURE: ENDPOINT_DELETION_FAILURE,
        DB_EXCEPTION: ENDPOINT_EXCEPTION
    }
    return mapping.get(code, -1)


def endpoint_get_services(query: dict) -> dict:
    try:
        result = read(collection=service, query=query)
        return dict(code=_change_result_code(result['code']), content=result['content'])
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка получения сервисов: {e}")


def endpoint_add_service(data: dict) -> dict:
    try:
        result = create(collection=service, data=data)
        return dict(code=_change_result_code(result['code']), content=result['content'])
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка добавления сервиса: {e}")


def endpoint_edit_service(query: dict, new_data: dict) -> dict:
    try:
        result = update(collection=service, query=query, new_data=new_data)
        return dict(code=_change_result_code(result['code']), content=result['content'])
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка обновления сервиса: {e}")


def endpoint_get_appointments(query: dict) -> dict:
    try:
        result = read(collection=appointments, query=query)
        return dict(code=_change_result_code(result['code']), content=result['content'])
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка получения записей: {e}")


def endpoint_add_appointment(data: dict) -> dict:
    try:
        result = create(collection=appointments, data=data)
        return dict(code=_change_result_code(result['code']), content=result['content'])
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка добавления записи: {e}")


def endpoint_edit_appointment(query: dict, new_data: dict) -> dict:
    try:
        result = update(collection=appointments, query=query, new_data=new_data)
        return dict(code=_change_result_code(result['code']), content=result['content'])
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка изменения записи: {e}")


def endpoint_delete_appointment(query: dict) -> dict:
    try:
        result = delete(collection=appointments, query=query)
        return dict(code=_change_result_code(result['code']), content=result['content'])
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка удаления записи: {e}")


def endpoint_get_appointments_history():
    try:
        result = read(collection=served_appointments, query=None)
        return dict(code=_change_result_code(result['code']), content=result['content'])
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка получения истории записей: {e}")


def endpoint_check_appointment_expiration(): #TODO
    try:
        current_data = read(collection=appointments, query=None)
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка получения записей: {e}")
