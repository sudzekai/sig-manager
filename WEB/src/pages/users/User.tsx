import { useEffect, useState } from "react";
import { useParams } from "react-router";
import { usersClient } from "../../api/clients/usersClient";
import type { UserInfoDto } from "../../api/types/dtos/users/UserInfoDto";

export default function UserPage() {
    const [user, setUser] = useState<UserInfoDto>();
    const { id } = useParams<{ id: string }>();

    useEffect(() => {
        async function getUser(id: number) {
            const user = await usersClient.getById(id);
            setUser(user);
        }

        getUser(Number(id));
    }, [id])

    return (
        <div className="flex flex-col gap-2">
            <label className=" doc-header">Пользователь #{user?.id}</label>
            <label>ФИО: {user?.fullName}</label>
            <label>Имя пользователя: {user?.username}</label>
            <label>Эл. почта: {user?.email}</label>
            <label>Телефон: {user?.phoneNumber}</label>
        </div>
    )
}