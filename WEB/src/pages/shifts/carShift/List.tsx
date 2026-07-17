import { useEffect, useState } from "react";
import type { CarShiftSimpleDto } from "../../../api/types/dtos/carShifts/CarShiftSimpleDto";
import { carShiftsClient } from "../../../api/clients/carShiftsClient";
import { useNavigate } from "react-router";
import { CustomSelect } from "../../../elements/CustomSelect";

export default function CarShiftListPage() {
    const [shifts, setShifts] = useState<CarShiftSimpleDto[]>();
    const [currentOffset, setCurrentOffset] = useState(0);
    const [limit, setLimit] = useState(12);

    const [filters, setFilters] = useState<{
        status?: string;
        date?: string;
    }>({
        status: undefined,
        date: undefined,
    });

    const navigate = useNavigate();

    useEffect(() => {
        async function loadShifts() {
            var response = await carShiftsClient.getAll({ limit: limit });
            setShifts(response.data);
        };
        setLimit(12);
        loadShifts();
    }, [])

    const statuses = [
        { value: "opened", label: "Открыта" },
        { value: "closed", label: "Закрыта" }
    ]
    const dates = [
        { value: "today", label: "За сегодня" },
        { value: "week", label: "За неделю" },
        { value: "month", label: "За месяц" },
        { value: "year", label: "За год" },
    ]

    const handleUpdate = async (offset = currentOffset) => {
        const request: {
            createdAtStart?: string;
            createdAtEnd?: string;
            status?: string;
            offset: number;
            limit: number;
        } = {
            offset,
            limit
        };

        if (filters.status) {
            request.status = filters.status;
        }

        if (filters.date) {
            const startDate = new Date();
            const endDate = new Date();

            switch (filters.date) {
                case "today":
                    startDate.setHours(0, 0, 0, 0);
                    endDate.setHours(23, 59, 59, 999);
                    break;

                case "week":
                    startDate.setDate(startDate.getDate() - 7);
                    startDate.setHours(0, 0, 0, 0);
                    endDate.setHours(23, 59, 59, 999);
                    break;

                case "month":
                    startDate.setMonth(startDate.getMonth() - 1);
                    startDate.setHours(0, 0, 0, 0);
                    endDate.setHours(23, 59, 59, 999);
                    break;

                case "year":
                    startDate.setFullYear(startDate.getFullYear() - 1);
                    startDate.setHours(0, 0, 0, 0);
                    endDate.setHours(23, 59, 59, 999);
                    break;
            }

            request.createdAtStart = startDate.toISOString();
            request.createdAtEnd = endDate.toISOString();
        }

        const response = await carShiftsClient.getAll(request);
        setShifts(response.data);
    };

    const nextPage = async () => {
        const newOffset = currentOffset + limit;
        setCurrentOffset(newOffset);
        await handleUpdate(newOffset);
    };

    const previousPage = async () => {
        const newOffset = Math.max(0, currentOffset - limit);
        setCurrentOffset(newOffset);
        await handleUpdate(newOffset);
    };

    const firstPage = async () => {
        const newOffset = Math.max(0, 0);
        setCurrentOffset(newOffset);
        await handleUpdate(newOffset);
    }

    return (
        <div className="page" >
            <div className="frame frame-header">
                Смены машинок
            </div>

            <div className="frame page">
                <div className="flex flex-row gap-2">
                    <CustomSelect placeholder="Статус"
                        value={statuses.find(s => s.value == filters.status)}
                        onChange={selected => setFilters(prev => ({ ...prev, status: selected?.value }))}
                        options={statuses} />

                    <CustomSelect placeholder="Промежуток"
                        value={dates.find(s => s.value == filters.date)}
                        onChange={selected => setFilters(prev => ({ ...prev, date: selected?.value }))}
                        options={dates} />
                </div>

                <button className="btn btn-primary-outline"
                    onClick={() => {
                        firstPage()
                    }}>
                    <i className=" bi-arrow-clockwise me-1"></i>
                    Обновить
                </button>

                <table>
                    <thead>
                        <tr>
                            <th className="">#</th>
                            <th className="">Дата</th>
                            <th className="">Время</th>
                            <th className="">Билеты</th>
                        </tr>
                    </thead>
                    <tbody>
                        {shifts?.map((shift, idx) => {
                            const openDate = new Date(shift.createdAt)
                            var closeDate = null;

                            if (shift.closedAt)
                                closeDate = new Date(shift.closedAt)

                            return (
                                <tr key={idx} onClick={() => navigate(`/shifts/cars/${shift.id}`)}>
                                    <td>{shift.id}</td>
                                    <td>
                                        {openDate.toLocaleDateString("ru-RU", {
                                            day: "numeric",
                                            month: "long",
                                        })}
                                    </td>
                                    <td>
                                        {openDate.getHours().toString().padStart(2, "0")}:
                                        {openDate.getMinutes().toString().padStart(2, "0")}
                                        {closeDate && (
                                            <>
                                                -{closeDate.getHours().toString().padStart(2, "0")}:
                                                {openDate.getMinutes().toString().padStart(2, "0")}
                                            </>
                                        )}
                                    </td>
                                    <td>
                                        {shift.firstTicket}
                                        {shift.lastTicket &&
                                            (
                                                <>
                                                    -{shift.lastTicket}
                                                </>
                                            )}
                                    </td>
                                </tr>
                            )
                        })}
                    </tbody>
                </table>
            </div>

            <div className="frame flex flex-row gap-2">
                <button className="btn btn-secondary flex-1"
                    disabled={currentOffset < limit}
                    onClick={previousPage}>
                    <i className=" bi-arrow-left" />
                </button>
                <button className="btn btn-secondary flex-1"
                    disabled={shifts && shifts.length < limit}
                    onClick={nextPage}>
                    <i className=" bi-arrow-right" />
                </button>
            </div>
        </div >
    )
}