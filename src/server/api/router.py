from typing import List
from fastapi import APIRouter, HTTPException, Query


router = APIRouter()
service_router = APIRouter()
appointments_router = APIRouter()
served_appointments_router = APIRouter()
from .endpoints import *


@service_router.get("/service/get_services_list", response_model=List[Service])
def get_services():
    pass


@service_router.get("/service/get_service_by_title", response_model=Service)
def get_service_by_title():
    pass


@service_router.post("/service/add_service")
def add_service():
    pass


@service_router.patch("/service/edit_service")
def edit_service():
    pass


@appointments_router.get("/appointments/get_appointments", response_model=List[Appointment])
def get_appointments():
    pass


@appointments_router.get("/appointments/get_appointment", response_model=Appointment)
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
def get_appointments_history():
    pass


@served_appointments_router.get("/served_appointments/check_appointment_expiration")
def check_appointment_expiration():
    pass
