import { useEffect, useState } from "react";
import { Link, useParams } from "react-router";
import type { CarShiftInfoDto } from "../../api/types/dtos/carShifts/CarShiftInfoDto";
import { carShiftsClient } from "../../api/clients/carShiftsClient";
import { usersClient } from "../../api/clients/usersClient";
import type { UserInfoDto } from "../../api/types/dtos/users/UserInfoDto";

export default function CarShiftInfoPage() {
    const { id } = useParams<{ id: string }>();

    const [shift, setShift] = useState<CarShiftInfoDto>();
    const [shiftUsers, setShiftUsers] = useState<UserInfoDto[]>([]);

    useEffect(() => {
        async function loadData() {
            // Загружаем смену
            const shiftData = await carShiftsClient.getById(Number(id));
            setShift(shiftData);

            // Загружаем каждого пользователя из смены
            if (shiftData.users && shiftData.users.length > 0) {
                const usersPromises = shiftData.users.map(async (userRef) => {
                    return await usersClient.getById(userRef.id);
                });
                const usersData = await Promise.all(usersPromises);
                setShiftUsers(usersData);
            }
        }

        loadData();
    }, [id]);


    return (
        <div className="flex flex-col gap-2">
            <label className="doc-header">Информация о смене машинок #{id}</label>

            <label>Парк: {shift?.parkId}</label>
            <label>Статус: {shift?.status}</label>
            
            <hr className="my-1" />

            <label className="font-semibold">Сотрудники:</label>
            {shiftUsers.length > 0 ? (
                <ul>
                    {shiftUsers.map((user) => {
                        const position = shift?.users?.find(u => u.id === user.id)?.positionId;
                        return (
                            <li key={user.id}>
                                {user.fullName} - {position || "Не указана"}
                            </li>
                        );
                    })}
                </ul>
            ) : (
                <p>Нет сотрудников</p>
            )}
            
            <hr className="my-1" />
            
            <label className="font-semibold">Время:</label>
            <ul>
                <li>Дата: {shift?.createdAt ? new Date(shift.createdAt).toLocaleDateString() : ""}</li>
                <li>Время открытия: {shift?.createdAt ? new Date(shift.createdAt).toLocaleTimeString() : ""}</li>
                <li>Время закрытия: {shift?.closedAt ? new Date(shift.closedAt).toLocaleTimeString() : ""}</li>
                <li>Продолжительность смены: {
                    shift?.createdAt && shift?.closedAt
                        ? (() => {
                            const diff = new Date(shift.closedAt).getTime() - new Date(shift.createdAt).getTime();
                            const hours = Math.floor(diff / (1000 * 60 * 60));
                            const minutes = Math.floor((diff % (1000 * 60 * 60)) / (1000 * 60));
                            return `${hours}ч ${minutes}м`;
                        })()
                        : ""
                }
                </li>
            </ul>

            <hr className="my-1" />

            <label className="font-semibold">Билеты:</label>
            <ul>
                <li>Номер первого билета: {shift?.firstTicket}</li>
                <li>Номер последнего билета: {shift?.lastTicket ?? ""}</li>
                <li>Итого билетов: {shift?.totalTickets ?? ""}</li>
                <li>Стоимость билета: {shift?.ticketPrice}</li>
            </ul>

            <hr className="my-1" />
            <label className="font-semibold">Выручка:</label>
            <ul>
                <li>Сумма наличных: {shift?.cash ?? ""}</li>
                <li>Сумма безнал: {shift?.cashLess ?? ""}</li>
                <li>Недостача: {shift?.difference ?? ""}</li>
            </ul>

            <Link to={`/shifts/cars/${id}/close`} className="btn btn-primary mt-2">
                Закрыть смену
            </Link>
        </div>
    )
}