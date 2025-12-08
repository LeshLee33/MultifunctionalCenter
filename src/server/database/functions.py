from .models import Service, ServedAppointment, Appointment

def create(collection, data):
    try:
        current_data = read(collection=collection, query=data)
        if current_data == []:
            collection.insert_one(data)
            if len(read(collection=collection, query=data)) == 1:
                return dict(code=0, message="Успешное добавление")
            else:
                return dict(code=1, message=f"Новые данные не найдены в коллекции")
        return dict(code=2, message=f"В коллекции уже есть документ с данными параметрами")
    except Exception as e:
        return dict(code=3, message=f"Ошибка добавления в коллекцию: {e}")


def read(collection, query):
    try:
        result = list(collection.find(query))
        return result if len(result) >= 1 else []
    except Exception as e:
        return dict(code=1, message=f"Ошибка чтения из коллекции: {e}")


def update(collection, query, new_data):
    try:
        current_data = read(collection=collection, query=query)
        if current_data != [] and len(current_data) == 1:
            collection.update_one(filter=query, update=new_data)

            if read(collection=collection, query=dict(current_data[0])) == []:
                return dict(code=0, message="Успешное обновление")
            else:
                return dict(code=1, message="Данные не были обновлены")

        return dict(code=2, message="Данные не найдены в коллекции")
    except Exception as e:
        return dict(code=3, message=f"Ошибка обновления записи в коллекции: {e}")


def delete(collection, query):
    try:
        current_data = read(collection=collection, query=query)
        if current_data != [] and len(current_data) == 1:
            collection.delete_one(filter=query)

            if read(collection, query=query) == []:
                return dict(code=0, message="Успешное удаление")
            else:
                return dict(code=1, message="Данные не были удалены")

        return dict(code=2, message="Данные не найдены в коллекции")
    except Exception as e:
        return dict(code=3, message=f"Ошибка удаления записи из коллекции: {e}")
