from datetime import datetime, date
from src.server.database import (appointments, served_appointments, service, create, read, update, delete)
from src.server.utils import (ENDPOINT_SUCCESS, ENDPOINT_EXCEPTION, ENDPOINT_ALREADY_EXISTING_DATA,
                              ENDPOINT_DATA_NOT_FOUND, ENDPOINT_UPDATING_FAILURE, ENDPOINT_DELETION_FAILURE,
                              ENDPOINT_MOVE_TO_HISTORY_FAILURE, DB_SUCCESS, DB_EXCEPTION, DB_ALREADY_EXISTING_DATA,
                              DB_DATA_NOT_FOUND, DB_UPDATING_FAILURE, DB_DELETION_FAILURE, ENDPOINT_INCORRECT_DATE,
                              ENDPOINT_INCORRECT_TIME)

DATETIME_FORMAT_STRING = "%d.%m.%Y %H:%M:%S"
STATUS_CODES_MAPPING = {
    DB_SUCCESS: ENDPOINT_SUCCESS,
    DB_DATA_NOT_FOUND: ENDPOINT_DATA_NOT_FOUND,
    DB_UPDATING_FAILURE: ENDPOINT_UPDATING_FAILURE,
    DB_ALREADY_EXISTING_DATA: ENDPOINT_ALREADY_EXISTING_DATA,
    DB_DELETION_FAILURE: ENDPOINT_DELETION_FAILURE,
    DB_EXCEPTION: ENDPOINT_EXCEPTION
}


def _change_result_code(code: int) -> int:
    return STATUS_CODES_MAPPING.get(code, -1)


def _convert_to_datetime(date_str: str, time_str: str) -> datetime:
    return datetime.strptime((date_str + " " + time_str + ":00"), DATETIME_FORMAT_STRING)


def _compare_datetime(appointment: dict) -> bool:
    appointment_datetime = _convert_to_datetime(
        date_str=appointment['appointment_date'],
        time_str=appointment['appointment_daytime']
    )
    return datetime.now() >= appointment_datetime


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


def _process_expired_appointments_recursive(appointments_list: list[dict], errors: list):
    if len(appointments_list) != 0:
        appointment = appointments_list[-1]
        result = _move_appointment_to_served_list(appointment=appointment)
        if result['code'] != ENDPOINT_SUCCESS:
            errors.append(result)
        return _process_expired_appointments_recursive(appointments_list=appointments_list[:-1], errors=errors)
    return errors


def _check_date(date_str: str):
    try:
        appointment_date = datetime.strptime(date_str, "%d.%m.%Y").date()
        print(appointment_date)
        today = date.today()
        if appointment_date > today:
            return dict(check=True, content="Дата корректна")
        else:
            return dict(check=False, content="Дата записи не может быть меньше текущей")
    except Exception as e:
        return dict(check=False, content=f"Дата некорректна: {e}")


def _check_time(time_str: str):
    try:
        appointment_time = datetime.strptime(time_str, "%H:%M")
        if appointment_time.hour > 21 or appointment_time.hour < 7:
            return dict(check=False, content="Время некорректно, неправильно указаны часы")
        if appointment_time.minute > 59 or appointment_time.minute < 0:
            return dict(check=False, content="Время некорректно, неправильно указаны минуты")
        return dict(check=True, content="Время корректно")
    except Exception as e:
        return dict(check=False, content=f"Время некорректно: {e}")


def _check_service_existence(service_title):
    if (read(collection=service, query=dict(service_title=service_title))['code'] != DB_SUCCESS
            or read(collection=service, query=dict(service_title=service_title))['content'] == []):
        return False
    return True


def handler_get_services(query: dict | None) -> dict:
    try:
        result = read(collection=service, query=query)
        return dict(code=_change_result_code(result['code']), content=result['content'])
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка получения сервисов: {e}")


def handler_add_service(data: dict) -> dict:
    try:
        result = create(collection=service, data=data)
        return dict(code=_change_result_code(result['code']), content=result['content'])
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка добавления сервиса: {e}")


def handler_edit_service(query: dict, new_data: dict) -> dict:
    try:
        result = update(collection=service, query=query, new_data=new_data)
        return dict(code=_change_result_code(result['code']), content=result['content'])
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка обновления сервиса: {e}")


def handler_get_appointments(query: dict | None) -> dict:
    try:
        result = read(collection=appointments, query=query)
        return dict(code=_change_result_code(result['code']), content=result['content'])
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка получения записей: {e}")


def handler_add_appointment(data: dict) -> dict:
    try:
        if not _check_service_existence(data['appointment_service_title']):
            return dict(code=ENDPOINT_DATA_NOT_FOUND, content="Услуга с таким названием не найдена")
        if not _check_date(data['appointment_date'])['check']:
            return dict(code=ENDPOINT_INCORRECT_DATE, content="Введена некорректная дата")
        if not _check_time(data['appointment_daytime'])['check']:
            return dict(code=ENDPOINT_INCORRECT_TIME, content="Введено некорректное время")

        result = create(collection=appointments, data=data)
        return dict(code=_change_result_code(result['code']), content=result['content'])
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка добавления записи: {e}")


def handler_edit_appointment(query: dict, new_data: dict) -> dict:
    try:
        if not _check_service_existence(query['appointment_service_title']):
            return dict(code=ENDPOINT_DATA_NOT_FOUND, content="Услуга с таким названием не найдена")
        if not _check_date(new_data['$set']['appointment_date'])['check']:
            return dict(code=ENDPOINT_INCORRECT_DATE, content="Введена некорректная дата")
        if not _check_time(new_data['$set']['appointment_daytime'])['check']:
            return dict(code=ENDPOINT_INCORRECT_TIME, content="Введено некорректное время")

        result = update(collection=appointments, query=query, new_data=new_data)
        return dict(code=_change_result_code(result['code']), content=result['content'])
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка изменения записи: {e}")


def handler_delete_appointment(query: dict) -> dict:
    try:
        result = delete(collection=appointments, query=query)
        return dict(code=_change_result_code(result['code']), content=result['content'])
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка удаления записи: {e}")


def handler_get_appointments_history(query: dict | None):
    try:
        result = read(collection=served_appointments, query=query)
        return dict(code=_change_result_code(result['code']), content=result['content'])
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка получения истории записей: {e}")


def handler_appointments_expiration_controller():
    try:
        current_data = read(collection=appointments, query=None)
        filtered_data = list(filter(_compare_datetime, current_data['content']))
        errors: list = _process_expired_appointments_recursive(appointments_list=filtered_data, errors=[])
        if len(errors) != 0:
            return dict(code=ENDPOINT_MOVE_TO_HISTORY_FAILURE, content=errors)
        return dict(code=ENDPOINT_SUCCESS, content="Успешное перемещение просроченных записей")
    except Exception as e:
        return dict(code=ENDPOINT_EXCEPTION, content=f"Ошибка перемещения записей: {e}")