from api import *
from database import check_database_connection
import uvicorn
from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware

app = FastAPI(title="Система учёта многофункционального центра")
app.include_router(router)
app.include_router(service_router)
app.include_router(appointments_router)
app.include_router(served_appointments_router)

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)


def main():
    check_database_connection()
    uvicorn.run(host="localhost", port=8000, app="main:app", reload=True)


if __name__ == "__main__":
    main()
