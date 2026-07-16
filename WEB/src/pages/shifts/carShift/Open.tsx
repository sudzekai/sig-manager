import { useEffect, useState } from "react"
import { Link, useNavigate } from "react-router";
import type { CarShiftOpenDto } from "../../../api/types/dtos/carShifts/CarShiftOpenDto";
import type { UserSimpleDto } from "../../../api/types/dtos/users/UserSimpleDto";
import type { UserPositionDto } from "../../../api/types/dtos/UserPositionDto";
import { usersClient } from "../../../api/clients/usersClient";
import { carShiftsClient } from "../../../api/clients/carShiftsClient";
import { CustomSelect } from "../../../elements/CustomSelect";

export default function CarShiftOpenPage() {
    const [shift, setShift] = useState<CarShiftOpenDto>({
        parkId: 1,
        ticketPrice: 300,
        firstTicket: 1,
        users: []
    });

    const [users, setUsers] = useState<UserSimpleDto[]>([]);
    const [selectedUsers, setSelectedUsers] = useState<UserPositionDto[]>([])

    const positions = [
        { id: 1, name: "Стажёр" },
        { id: 2, name: "Помощник" },
        { id: 3, name: "Администратор машинок" }
    ]

    const parks = [
        { id: 1, name: "Галушина" },
        { id: 2, name: "Пристань" },
        { id: 3, name: "Прага" }
    ]

    useEffect(() => {
        async function loadUsers() {
            var users = await usersClient.getAll();
            setUsers(users);
        }

        loadUsers()
    }, [])

    const navigate = useNavigate();

    const onSubmit = async (e: React.SubmitEvent, dto: CarShiftOpenDto) => {
        e.preventDefault();

        const createDto = { ...dto, users: selectedUsers };

        try {
            const shift = await carShiftsClient.open(createDto)
            navigate(`/shifts/cars/${shift.id}`)
        } catch (e) {
            console.log(e)
        }
    }

    return (
        <form className="page"
            onSubmit={(e) => onSubmit(e, shift)}>
            <div className="frame frame-header">
                Открытие смены машинок
            </div>

            <div className="frame">
                <div className="flex flex-col gap-2">

                    <div className="flex flex-col gap-2">
                        <label>Сотрудники (до 2-х):</label>

                        <CustomSelect<UserSimpleDto, true>
                            isMulti
                            options={users}
                            placeholder="Сотрудники"
                            getOptionLabel={(u) => u.fullName}
                            getOptionValue={(u) => u.id.toString()}
                            isOptionDisabled={() => selectedUsers.length >= 2}
                            onChange={(selected) => {
                                setSelectedUsers(
                                    selected.map(u => ({
                                        id: u.id,
                                        positionId: 1
                                    }))
                                );
                            }}
                        />

                        {selectedUsers.length > 0 && (
                            <>
                                <label>Должности:</label>
                                {selectedUsers.map((u, idx) => (
                                    <>
                                        <div key={idx} className="md:flex md:flex-row md:items-center">
                                            <span className="w-1/1 md:w-1/3">
                                                <i className=" bi-dot"></i> {users.find((user) => user.id == u.id)?.fullName}
                                            </span>
                                            <CustomSelect className="w-1/1 md:w-2/3 mt-1 md:m-0"
                                                options={positions}
                                                value={positions.find(p => p.id === u.positionId)}
                                                placeholder="Должность..."
                                                getOptionLabel={(u) => u.name}
                                                getOptionValue={(u) => u.id.toString()}
                                                onChange={(selected) => {
                                                    setSelectedUsers(prev =>
                                                        prev.map(user =>
                                                            user.id === u.id
                                                                ? { ...user, positionId: Number(selected?.id) }
                                                                : user
                                                        )
                                                    );
                                                }} />
                                        </div>
                                        <hr className="my-1 mx-2" />
                                    </>
                                ))}
                            </>
                        )}

                    </div>

                    <div className="flex flex-col">
                        <label>Парк:</label>
                        <CustomSelect
                            value={parks.find(p => p.id === shift.parkId)}
                            options={parks}
                            getOptionLabel={(u) => u.name}
                            getOptionValue={(u) => u.id.toString()}
                            onChange={(selected) => setShift({ ...shift, parkId: Number(selected?.id) })}
                            placeholder="Парк..." />
                    </div>
                    <div className="flex flex-col">
                        <label>Номер первого билета:</label>
                        <input type="number" className="form-control"
                            value={shift.firstTicket}
                            onChange={(e) => setShift({ ...shift, firstTicket: Number(e.target.value) })}
                            placeholder="Номер первого билета..." />
                    </div>

                    <div className="flex flex-col">
                        <label>Стоимость билета:</label>
                        <input type="number" className="form-control"
                            value={shift.ticketPrice}
                            onChange={(e) => setShift({ ...shift, ticketPrice: Number(e.target.value) })}
                            placeholder="Стоимость билета..." />
                    </div>

                    <hr className="mx-2" />

                    <div className="flex flex-row gap-2">
                        <button type="submit" className="btn btn-primary w-2/3">
                            Открыть смену
                        </button>
                        <Link to={"/shifts/router"} className="btn btn-secondary w-1/3">
                            Отмена
                        </Link>
                    </div>
                </div>
            </div>
        </form >
    )
}