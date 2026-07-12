import { useState } from "react"
import type { CarShiftOpenDto } from "../../api/types/dtos/carShifts/CarShiftOpenDto"
import { carShiftsClient } from "../../api/clients/carShiftsClient";
import { useNavigate } from "react-router";

export default function CarShiftOpenPage() {
    const [shift, setShift] = useState<CarShiftOpenDto>({
        parkId: 1,
        ticketPrice: 300,
        firstTicket: 1,
        users: []
    });

    const navigate = useNavigate();

    const onSubmit = async (e: React.SubmitEvent, dto: CarShiftOpenDto) => {
        e.preventDefault();

        try {
            const shift = await carShiftsClient.open(dto);
            navigate(`/shifts/cars/${shift.id}`);
        } catch (e) {
            console.log(e);
        }
    }

    return (
        <form className="flex flex-col gap-2"
            onSubmit={(e) => onSubmit(e, shift)}>
            <label className="doc-header">
                Открытие смены машинок
            </label>

            <div className="flex flex-col">
                <label>Парк:</label>
                <input type="number" className="form-control"
                    value={shift.parkId}
                    onChange={(e) => setShift({ ...shift, parkId: Number(e.target.value) })}
                    placeholder="Идентификатор парка" />
            </div>
            <div className="flex flex-col">
                <label>Номер первого билета:</label>
                <input type="number" className="form-control"
                    value={shift.firstTicket}
                    onChange={(e) => setShift({ ...shift, firstTicket: Number(e.target.value) })}
                    placeholder="Номер первого билета" />
            </div>

            <div className="flex flex-col">
                <label>Стоимость билета:</label>
                <input type="number" className="form-control"
                    value={shift.ticketPrice}
                    onChange={(e) => setShift({ ...shift, ticketPrice: Number(e.target.value) })}
                    placeholder="Стоимость билета..." />
            </div>
            <button type="submit" className="btn btn-primary mt-2">
                Открыть смену
            </button>
        </form>
    )
}