from fastapi import APIRouter, HTTPException, Query
from ..database import appointments, served_appointments, service

router = APIRouter()
service_router = APIRouter()
appointments_router = APIRouter()
served_appointments_router = APIRouter()


@service_router.get("/service/get_services_list")
def get_services():
    pass


@service_router.get("/service/get_service")
def get_service():
    pass


@service_router.get("/service/get_service_by_title")
def get_service_by_title():
    pass


@service_router.post("/service/add_service")
def add_service():
    pass


@service_router.patch("/service/edit_service")
def edis_service():
    pass


@appointments_router.get("/appointments/get_appointments")
def get_appointments():
    pass


@appointments_router.get("/appointments/get_appointment")
def get_appointment():
    pass


@appointments_router.post("/appointments/add_appointment")
def add_appointment():
    pass


@appointments_router.patch("/appointments/edit_appointment")
def edit_appointment():
    pass


@appointments_router.post("/appointments/delete_appointment")
def delete_appointment():
    pass


@served_appointments_router.get("/served_appointments/get_appointments_history")
def check_appointment_expiration():
    pass


@served_appointments_router.get("/served_appointments/check_appointment_expiration")
def check_appointment_expiration():
    pass
