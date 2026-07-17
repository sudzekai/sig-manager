import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router";
import { usersClient } from "../../../api/clients/usersClient";
import { carShiftsClient } from "../../../api/clients/carShiftsClient";
import type { CarShiftInfoDto } from "../../../api/types/dtos/carShifts/CarShiftInfoDto";
import type { UserInfoDto } from "../../../api/types/dtos/users/UserInfoDto";

export default function CarShiftInfoPage() {
    const { id } = useParams<{ id: string }>();

    const [shift, setShift] = useState<CarShiftInfoDto>();
    const [shiftUsers, setShiftUsers] = useState<UserInfoDto[]>([]);

    const navigate = useNavigate();

    useEffect(() => {
        async function loadData() {
            const response = await carShiftsClient.getById(Number(id));
            const shiftData = response.data;
            setShift(shiftData);

            if (shiftData.users && shiftData.users.length > 0) {
                const usersPromises = shiftData.users.map(async (userRef) => {
                    return (await usersClient.getById(userRef.id)).data;
                });

                const usersData = await Promise.all(usersPromises);
                setShiftUsers(usersData);
            }
        }

        loadData();
    }, [id]);


    return (
        <div className="page">
            <div className="frame frame-header">
                Информация о смене машинок #{id}
            </div>

            <div className="frame">
                <div className="flex-col flex gap-2">
                    <div className="flex flex-col gap-1">
                        <label className="font-semibold">Общая информация:</label>

                        <div className="flex flex-col quote ms-2">
                            <label>Парк: {shift?.parkId == 1 ? "Галушина" : shift?.parkId == 2 ? "Пристань" : "Прага"}</label>
                            <label>Статус: {shift?.status == "opened" ? "открыта" : "закрыта"}</label>
                        </div>
                    </div>

                    <hr className="mt-1 mx-2" />

                    <div className="flex flex-col gap-1">
                        <label className="font-semibold">Сотрудники:</label>
                        <table>
                            <thead>
                                <tr>
                                    <th>ФИО</th>
                                    <th>Должность</th>
                                </tr>
                            </thead>
                            <tbody>
                                {shiftUsers.length > 0 ? (
                                    <>
                                        {shiftUsers.map((user) => {
                                            const position = shift?.users?.find(u => u.id === user.id)?.positionId;
                                            return (
                                                <tr key={user.id} onClick={() => navigate(`/users/${user.id}`)}>
                                                    <td>{user.fullName}</td>
                                                    <td>{position == 1 ? "Стажёр" : position == 2 ? "Помощник" : "Администратор машинок"}</td>
                                                </tr>
                                            );
                                        })}
                                    </>
                                ) : (
                                    <tr>
                                        <td colSpan={2} className="text-center">Нет сотрудников</td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div>

                    <hr className="mt-1 mx-2" />

                    <div className="flex flex-col gap-1">
                        <label className="font-semibold">Время:</label>

                        <ul className="quote ms-2">
                            <li>Дата: {shift?.createdAt ? new Date(shift.createdAt).toLocaleDateString() : ""}</li>
                            <li>Время открытия: {shift?.createdAt ? new Date(shift.createdAt).toLocaleTimeString() : ""}</li>
                            {shift?.status == "closed" && (
                                <>
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
                                </>
                            )}

                        </ul>
                    </div>

                    <hr className="mt-1 mx-2" />

                    <div className="flex flex-col gap-1">
                        <label className="font-semibold">Билеты:</label>
                        <ul className="quote ms-2">
                            <li>Номер первого билета: {shift?.firstTicket}</li>
                            {shift?.status == "closed" && (
                                <>
                                    <li>Номер последнего билета: {shift?.lastTicket ?? ""}</li>
                                    <li>Итого билетов: {shift?.totalTickets ?? ""}</li>
                                </>
                            )}
                            <li>Стоимость билета: {shift?.ticketPrice}</li>
                        </ul>
                    </div>

                    {shift?.status == "closed" && (
                        <>
                            <hr className="mt-1 mx-2" />

                            <div className=" flex flex-col gap-1">
                                <label className="font-semibold">Выручка:</label>
                                <ul className="quote ms-2">
                                    <li>Сумма наличных: {shift?.cash ?? ""}</li>
                                    <li>Сумма безнал: {shift?.cashLess ?? ""}</li>
                                    <li>Недостача: {shift?.difference ?? ""}</li>
                                </ul>
                            </div>
                        </>
                    )}


                    {shift?.status == "opened" && (
                        <>
                            <hr className="mt-1 mx-2" />
                            <Link to={`/shifts/cars/${id}/close`} className="btn btn-primary">
                                Закрыть смену
                            </Link>
                        </>
                    )}
                </div>
            </div>
        </div>
    )
}