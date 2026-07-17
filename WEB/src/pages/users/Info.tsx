import { useEffect, useState } from "react";
import { useParams } from "react-router";
import { usersClient } from "../../api/clients/usersClient";
import type { UserInfoDto } from "../../api/types/dtos/users/UserInfoDto";

export default function UserInfoPage() {
    const [user, setUser] = useState<UserInfoDto>();
    const { id } = useParams<{ id: string }>();

    useEffect(() => {
        async function getUser(id: number) {
            const user = await usersClient.getById(id);
            setUser(user.data);
        }

        getUser(Number(id));
    }, [id])

    return (
        <div className="page">
            <div className="frame frame-header">
                Пользователь #{user?.id}
            </div>

            <div className="frame">
                <div className="flex flex-col gap-1">
                    <table>
                        <thead>
                            <tr>
                                <th colSpan={2} className="text-center">{user?.username}</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>ФИО</td>
                                <td>{user?.fullName}</td>
                            </tr>
                            <tr>
                                <td>Эл. почта</td>
                                <td>{user?.email}</td>
                            </tr>
                            <tr>
                                <td>Телефон</td>
                                <td>{user?.phoneNumber}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div className="flex flex-row gap-2 mt-2">
                    <a href={`tel:${user?.phoneNumber}`} className="btn btn-primary-outline w-1/2">
                        <i className=" bi-telephone me-1"></i>
                        Позвонить
                    </a>
                    <a href={`tg://resolve?domain=${user?.username}`} className="btn btn-primary-outline w-1/2">
                        <i className="bi-telegram me-1"></i>
                        Написать
                    </a>
                </div>
            </div>
        </div>
    )
}