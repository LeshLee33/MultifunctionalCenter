from src.server.utils import (DB_SUCCESS, DB_EXCEPTION, DB_DATA_NOT_FOUND,
                              DB_ALREADY_EXISTING_DATA, DB_DELETION_FAILURE,
                              DB_UPDATING_FAILURE)

def create(collection, data):
    try:
        current_data = read(collection=collection, query=data)
        if current_data['content'] == []:
            collection.insert_one(data)

            if len(read(collection=collection, query=data)['content']) == 1:
                return dict(code=DB_SUCCESS, content="Успешное добавление")
            else:
                return dict(code=DB_DATA_NOT_FOUND, content=f"Новые данные не найдены в коллекции")

        return dict(code=DB_ALREADY_EXISTING_DATA, content=f"В коллекции уже есть документ с данными параметрами")
    except Exception as e:
        return dict(code=DB_EXCEPTION, content=f"Ошибка добавления в коллекцию: {e}")


def read(collection, query):
    try:
        result = list(collection.find(query, dict(_id=0))) if query is not None else list(collection.find({}, dict(_id=0)))
        return dict(code=DB_SUCCESS, content=result)
    except Exception as e:
        return dict(code=DB_EXCEPTION, content=f"Ошибка чтения из коллекции: {e}")


def update(collection, query, new_data):
    try:
        current_data = read(collection=collection, query=query)
        if current_data['content'] != [] and len(current_data['content']) == 1:
            collection.update_one(filter=query, update=new_data)

            if read(collection=collection, query=current_data['content'][0])['content'] == []:
                return dict(code=DB_SUCCESS, content="Успешное обновление")
            else:
                return dict(code=DB_UPDATING_FAILURE, content="Данные не были обновлены")

        return dict(code=DB_DATA_NOT_FOUND, content="Данные не найдены в коллекции")
    except Exception as e:
        return dict(code=DB_EXCEPTION, content=f"Ошибка обновления записи в коллекции: {e}")


def delete(collection, query):
    try:
        current_data = read(collection=collection, query=query)
        if current_data['content'] != [] and len(current_data['content']) == 1:
            collection.delete_one(filter=query)

            if read(collection, query=query)['content'] == []:
                return dict(code=DB_SUCCESS, content="Успешное удаление")
            else:
                return dict(code=DB_DELETION_FAILURE, content="Данные не были удалены")

        return dict(code=DB_DATA_NOT_FOUND, content="Данные не найдены в коллекции")
    except Exception as e:
        return dict(code=DB_EXCEPTION, content=f"Ошибка удаления записи из коллекции: {e}")
