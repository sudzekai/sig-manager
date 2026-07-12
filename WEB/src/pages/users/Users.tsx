import { useEffect, useState } from "react";
import type { UserSimpleDto } from "../../api/types/dtos/users/UserSimpleDto";
import { usersClient } from "../../api/clients/usersClient";
import { useNavigate } from "react-router";

export default function UsersPage() {
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
        <table className="ring-1">
            <thead>
                <tr>
                    <th>Имя пользователя</th>
                    <th>ФИО</th>
                </tr>
            </thead>
            <tbody>
                {users?.map((user, i) => (
                    <tr key={user.id || i} 
                        onClick={() => navigate(`/users/${user.id}`)}
                        className=" cursor-pointer">
                        <td>{user.username}</td>
                        <td>{user.fullName}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    )
}