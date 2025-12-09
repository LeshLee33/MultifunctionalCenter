import pymongo as pm

client = pm.MongoClient('mongodb://localhost:27017/')
database = client.MultifunctionalCenter

service = database.Service
appointments = database.Appointments
served_appointments = database.ServedAppointments


def check_database_connection():
    try:
        client.admin.command('ping')
        print("Подключение к MongoDB успешно!")
    except Exception as e:
        print(f"Ошибка подключения: {e}")
