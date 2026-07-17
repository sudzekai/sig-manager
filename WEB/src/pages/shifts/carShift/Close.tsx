import { useNavigate, useParams } from "react-router";
import { useState } from "react";
import type { CarShiftCloseDto } from "../../../api/types/dtos/carShifts/CarShiftCloseDto";
import { carShiftsClient } from "../../../api/clients/carShiftsClient";
import ErrorPopUp from "../../../elements/ErrorPopUp";

export default function CarShiftClosePage() {
    const { id } = useParams<{ id: string }>();

    const [errorMessage, setErrorMessage] = useState("");

    const [shift, setShift] = useState<CarShiftCloseDto>({
        lastTicket: 1,
        cash: 0,
        cashless: 0
    });

    const navigate = useNavigate();

    const onSubmit = async () => {

        const response = await carShiftsClient.close(Number(id), shift);
        if (response.success)
            navigate(`/shifts/cars/${id}`);
        else
            setErrorMessage(response.error.message);
    }

    return (
        <div className="page">
            {errorMessage && (
                <ErrorPopUp header="Ошибка" message={errorMessage} onExit={() => setErrorMessage("")}></ErrorPopUp>
            )}
            <div className="frame frame-header">
                Закрытие смены машинок #{id}
            </div>

            <div className="frame">
                <div className="flex flex-col gap-1">
                    <label>Номер последнего билета:</label>
                    <input type="number" className="form-control"
                        value={shift.lastTicket}
                        onChange={(e) => setShift({ ...shift, lastTicket: Number(e.target.value) })}
                        placeholder="Номер последнего билета..." />
                </div>
                <div className="flex flex-col gap-1">
                    <label>Сумма наличных:</label>
                    <input type="number" className="form-control"
                        value={shift.cash}
                        onChange={(e) => setShift({ ...shift, cash: Number(e.target.value) })}
                        placeholder="Сумма наличных..." />
                </div>

                <div className="flex flex-col gap-1">
                    <label>Сумма безнал:</label>
                    <input type="number" className="form-control"
                        value={shift.cashless}
                        onChange={(e) => setShift({ ...shift, cashless: Number(e.target.value) })}
                        placeholder="Сумма безнал..." />
                </div>

                <hr className="mt-2 mx-2" />

                <button type="submit" className="btn btn-primary mt-2 w-1/1" onClick={() => onSubmit()}>
                    Закрыть смену
                </button>
            </div>
        </div>
    )
}