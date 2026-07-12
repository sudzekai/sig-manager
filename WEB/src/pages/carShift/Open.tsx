import { useState } from "react"
import type { CarShiftOpenDto } from "../../api/types/dtos/carShifts/CarShiftOpenDto"
import { carShiftsClient } from "../../api/clients/carShiftsClient";

export default function CarShiftOpenPage() {
    const [shift, setShift] = useState<CarShiftOpenDto>({
        parkId: 1,
        tikcetPrice: 300,
        firstTicket: 1,
        users: []
    });

    // const navigate = useNavigate();

    const onSubmit = async (e: React.SubmitEvent, dto: CarShiftOpenDto) => {
        e.preventDefault();

        await carShiftsClient.open(dto);
    }

    return (
        <form className="flex flex-col gap-2"
            onSubmit={(e) => onSubmit(e, shift)}>
            <label className="doc-header">
                Открытие смены машинок
            </label>

            <input type="number"
                onChange={(e) => setShift({ ...shift, parkId: Number(e.target.value) })}
                placeholder="Идентификатор парка" />
            <input type="number"
                onChange={(e) => setShift({ ...shift, firstTicket: Number(e.target.value) })}
                placeholder="Идентификатор парка" />
            <input type="number" value={shift.tikcetPrice}
                onChange={(e) => setShift({ ...shift, tikcetPrice: Number(e.target.value) })}
                placeholder="Стоимость билета..." />

            <button type="submit" className="btn">
                Открыть смену
            </button>
        </form>
    )
}