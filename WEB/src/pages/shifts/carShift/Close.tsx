import { useNavigate, useParams } from "react-router";
import { useState } from "react";
import type { CarShiftCloseDto } from "../../../api/types/dtos/carShifts/CarShiftCloseDto";
import { carShiftsClient } from "../../../api/clients/carShiftsClient";

export default function CarShiftClosePage() {
    const { id } = useParams<{ id: string }>();

    const [shift, setShift] = useState<CarShiftCloseDto>({
        lastTicket: 1,
        cash: 0,
        cashless: 0
    });

    const navigate = useNavigate();

    const onSubmit = async (e: React.SubmitEvent, dto: CarShiftCloseDto) => {
        e.preventDefault();

        await carShiftsClient.close(Number(id), dto);
        navigate(`/shifts/cars/${id}`);
    }

    return (
        <form className="flex flex-col gap-2"
            onSubmit={(e) => onSubmit(e, shift)}>
            <label className="doc-header">
                Закрытие смены машинок #{id}
            </label>

            <div className="flex flex-col">
                <label>Номер последнего билета:</label>
                <input type="number" className="form-control"
                    value={shift.lastTicket}
                    onChange={(e) => setShift({ ...shift, lastTicket: Number(e.target.value) })}
                    placeholder="Номер последнего билета..." />
            </div>
            <div className="flex flex-col">
                <label>Сумма наличных:</label>
                <input type="number" className="form-control"
                    value={shift.cash}
                    onChange={(e) => setShift({ ...shift, cash: Number(e.target.value) })}
                    placeholder="Сумма наличных..." />
            </div>

            <div className="flex flex-col">
                <label>Сумма безнал:</label>
                <input type="number" className="form-control"
                    value={shift.cashless}
                    onChange={(e) => setShift({ ...shift, cashless: Number(e.target.value) })}
                    placeholder="Сумма безнал..." />
            </div>
            <button type="submit" className="btn btn-primary mt-2">
                Закрыть смену
            </button>
        </form>
    )
}