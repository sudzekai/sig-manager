import { useEffect, useState } from "react"
import type { CarShiftOpenDto } from "../../api/types/dtos/carShifts/CarShiftOpenDto"
import { carShiftsClient } from "../../api/clients/carShiftsClient";
import { useNavigate } from "react-router";
import type { UserSimpleDto } from "../../api/types/dtos/users/UserSimpleDto";
import { usersClient } from "../../api/clients/usersClient";
import type { UserPositionDto } from "../../api/types/dtos/UserPositionDto";

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

    useEffect(() => {
        async function loadUsers() {
            var users = await usersClient.getAll();
            setUsers(users);
        }

        loadUsers()
    }, [])

    const navigate = useNavigate();

    const addSelect = () => {
        if (selectedUsers.length < 2) {
            setSelectedUsers([...selectedUsers, { id: 0, positionId: 1 }])
        }
    }

    const removeSelect = (index: number) => {
        const newSelected = selectedUsers.filter((_, i) => i !== index)
        setSelectedUsers(newSelected)
        const shiftUsers = newSelected.map(u => ({
            id: u.id,
            positionId: u.positionId
        }))
        setShift({ ...shift, users: shiftUsers as any })
    }

    const updateUser = (index: number, userId: number) => {
        const newSelected = [...selectedUsers]
        newSelected[index] = { ...newSelected[index], id: userId }
        setSelectedUsers(newSelected)
        const shiftUsers = newSelected.map(u => ({
            id: u.id,
            positionId: u.positionId
        }))
        setShift({ ...shift, users: shiftUsers as any })
    }

    const updatePosition = (index: number, positionId: number) => {
        const newSelected = [...selectedUsers]
        newSelected[index] = { ...newSelected[index], positionId }
        setSelectedUsers(newSelected)
        const shiftUsers = newSelected.map(u => ({
            id: u.id,
            positionId: u.positionId
        }))
        setShift({ ...shift, users: shiftUsers as any })
    }

    const getAvailableUsers = (currentIndex: number) => {
        const selectedIds = selectedUsers
            .filter((_, i) => i !== currentIndex && selectedUsers[i].id !== 0)
            .map(u => u.id)
        return users.filter(user => !selectedIds.includes(user.id))
    }

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
        <form className="flex flex-col gap-2"
            onSubmit={(e) => onSubmit(e, shift)}>
            <label className="doc-header">
                Открытие смены машинок
            </label>

            <div className="flex flex-col gap-2">
                <label>Сотрудники (до 2-х):</label>
                {selectedUsers.map((item, index) => (
                    <div key={index} className="flex flex-row gap-2 flex-wrap justify-between ">
                        <select
                            value={item.id}
                            onChange={(e) => updateUser(index, Number(e.target.value))}
                            className="form-control flex-1"
                        >
                            <option value={0}>Выберите сотрудника</option>
                            {getAvailableUsers(index).map(user => (
                                <option key={user.id} value={user.id}>
                                    {user.fullName}
                                </option>
                            ))}
                        </select>

                        <select
                            value={item.positionId}
                            onChange={(e) => updatePosition(index, Number(e.target.value))}
                            className="form-control flex-1 "
                        >
                            {positions.map(pos => (
                                <option key={pos.id} value={pos.id}>
                                    {pos.name}
                                </option>
                            ))}
                        </select>

                        <button
                            type="button"
                            onClick={() => removeSelect(index)}
                            className="btn btn-danger aspect-square items-center"
                        >
                            ✕
                        </button>
                    </div>
                ))}

                {selectedUsers.length < 2 && (
                    <button
                        type="button"
                        onClick={addSelect}
                        className="btn btn-primary"
                    >
                        + Добавить сотрудника
                    </button>
                )}
            </div>

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