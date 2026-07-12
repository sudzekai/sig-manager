import { useEffect, useState } from "react";
import { Link, useParams } from "react-router";
import type { CarShiftInfoDto } from "../../api/types/dtos/carShifts/CarShiftInfoDto";
import { carShiftsClient } from "../../api/clients/carShiftsClient";

export default function CarShiftInfoPage() {
    const { id } = useParams<{ id: string }>();

    const [shift, setShift] = useState<CarShiftInfoDto>();

    useEffect(() => {
        async function loadShift(id: number) {
            const shift = await carShiftsClient.getById(id);
            setShift(shift);
        }

        loadShift(Number(id))
    }, [])

    return (
        <div className="flex flex-col gap-2">
            <label className="doc-header">Информация о смене машинок #{id}</label>

            <label>Парк: {shift?.parkId}</label>
            <label>Статус: {shift?.status}</label>
            <hr className="my-1" />
            <label>Дата: {shift?.createdAt ? new Date(shift.createdAt).toLocaleDateString() : ""}</label>
            <label>Время открытия: {shift?.createdAt ? new Date(shift.createdAt).toLocaleString() : ""}</label>
            <label>Время закрытия: {shift?.closedAt ? new Date(shift.closedAt).toLocaleString() : ""}</label>
            <hr className="my-1" />
            <label>Номер первого билета: {shift?.firstTicket}</label>
            <label>Номер последнего билета: {shift?.lastTicket ?? ""}</label>
            <label>Итого билетов: {shift?.totalTickets ?? ""}</label>
            <label>Стоимость билета: {shift?.ticketPrice}</label>
            <hr className="my-1" />
            <label>Сумма наличных: {shift?.cash ?? ""}</label>
            <label>Сумма безнал: {shift?.cashLess ?? ""}</label>
            <label>Недостача: {shift?.difference ?? ""}</label>

            <Link to={`/shifts/cars/${id}/close`} className="btn btn-primary mt-2">
                Закрыть смену
            </Link>
        </div>
    )
}