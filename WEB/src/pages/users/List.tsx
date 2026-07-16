import { useEffect, useState } from "react";
import type { UserSimpleDto } from "../../api/types/dtos/users/UserSimpleDto";
import { usersClient } from "../../api/clients/usersClient";
import { useNavigate } from "react-router";

export default function UserListPage() {
    const [users, setUsers] = useState<UserSimpleDto[]>();
    const navigate = useNavigate();

    useEffect(() => {
        async function getUsers() {
            try {
                const users = await usersClient.getAll();
                setUsers(users);
            } catch (error) {
                console.error('Ошибка загрузки:', error);
            }
        }

        getUsers();
    }, []);

    return (
        <div className="page">
            <div className="frame frame-header">
                Пользователи
            </div>

            <div className="frame">
                <table>
                    <thead>
                        <tr>
                            <th>ФИО</th>
                            <th>Имя пользователя</th>
                        </tr>
                    </thead>
                    <tbody>
                        {users?.map((user, i) => (
                            <tr key={user.id || i}
                                onClick={() => navigate(`/users/${user.id}`)}
                                className=" cursor-pointer">
                                <td>{user.fullName}</td>
                                <td className="max-w-25 truncate">{user.username}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>

        </div>

    )
}