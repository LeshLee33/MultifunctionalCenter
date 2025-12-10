from datetime import datetime
from src.server.database import (appointments, served_appointments, service, create, read, update, delete)
from src.server.utils import (ENDPOINT_SUCCESS, ENDPOINT_EXCEPTION, ENDPOINT_ALREADY_EXISTING_DATA,
                              ENDPOINT_DATA_NOT_FOUND, ENDPOINT_UPDATING_FAILURE, ENDPOINT_DELETION_FAILURE,
                              DB_SUCCESS, DB_EXCEPTION, DB_ALREADY_EXISTING_DATA,
                              DB_DATA_NOT_FOUND, DB_UPDATING_FAILURE, DB_DELETION_FAILURE)


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


def _convert_to_datetime(date_str: str, time_str: str) -> datetime:
    format_string = "%d.%m.%Y %H:%M:%S"
    datetime_str = date_str + " " + time_str + ":00"
    return datetime.strptime(datetime_str, format_string)


def _compare_datetime(appointment: dict) -> bool:
    current_datetime = datetime.now()
    appointment_date = appointment['appointment_date']
    appointment_time = appointment['appointment_daytime']
    appointment_datetime = _convert_to_datetime(date_str=appointment_date, time_str=appointment_time)
    return current_datetime >= appointment_datetime


def _move_appointment_to_served_list(appointment: dict) -> dict:
    try:
        current_appointment = read(collection=appointments, query=appointment)
        if len(current_appointment['content']) != 1:
            return dict(code=ENDPOINT_DATA_NOT_FOUND, content=f"Не найдено записей по текущему запросу")
        delete(collection=appointments, query=appointment)
        move_appointment_result = create(collection=served_appointments, data=appointment)
        return dict(code=_change_result_code(move_appointment_result['code']), content=move_appointment_result['content'])
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка перемещения записи: {e}")


def _process_appointments_recursive(appointments_list: list[dict], errors: list):
    if len(appointments_list) != 0:
        appointment = appointments_list[-1]
        result = _move_appointment_to_served_list(appointment=appointment)
        if result['code'] != DB_SUCCESS:
            errors.append(result)
        return _process_appointments_recursive(appointments_list=appointments_list[:-1], errors=errors)
    return errors


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


def endpoint_appointments_expiration_controller():
    try:
        current_data = read(collection=appointments, query=None)
        filtered_data = list(filter(_compare_datetime, current_data['content']))
        errors: list = _process_appointments_recursive(appointments_list=filtered_data, errors=[])
        return dict(code=ENDPOINT_SUCCESS, content=errors)
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка перемещения записей: {e}")