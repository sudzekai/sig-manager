import { use, useEffect, useState } from "react";
import { useParams } from "react-router";
import { usersClient } from "../../api/clients/usersClient";
import type { UserInfoDto } from "../../api/types/dtos/users/UserInfoDto";
import { EnvelopeIcon, PhoneIcon } from "@heroicons/react/16/solid";

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
        <div className="page">
            <div className="frame frame-header">
                Пользователь #{user?.id}
            </div>

            <div className="frame">
                <div className="flex flex-col gap-2">
                    <label>ФИО: {user?.fullName}</label>
                    <label>Имя пользователя: {user?.username}</label>
                    <label>Эл. почта: {user?.email}</label>
                    <label>Телефон: {user?.phoneNumber}</label>
                </div>
            </div>
            <div className="flex flex-row gap-2">
                <a href={`tel:${user?.phoneNumber}`} className="btn btn-primary-outline w-1/2">
                    <PhoneIcon className="size-6 me-1"></PhoneIcon>
                    Позвонить
                </a>
                <a href={`tg://resolve?domain=${user?.username}`} className="btn btn-primary-outline w-1/2">
                    <EnvelopeIcon className="size-6 me-1"></EnvelopeIcon>
                    Написать в Telegram
                </a>
            </div>
        </div>
    )
}