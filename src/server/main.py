from api import *
from database import check_database_connection, read, service
import uvicorn
from fastapi import FastAPI

app = FastAPI(title="Система учёта многофункционального центра")
app.include_router(service_router)
app.include_router(appointments_router)
app.include_router(served_appointments_router)


def main():
    check_database_connection()
    uvicorn.run(host="localhost", port=8000, app="main:app", reload=True)


if __name__ == "__main__":
    main()
