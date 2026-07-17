import { useState } from "react"
import type { UserCreateDto } from "../../api/types/dtos/users/UserCreateDto";
import { usersClient } from "../../api/clients/usersClient";
import ErrorPopUp from "../../elements/ErrorPopUp";

export default function UserCreatePage() {
    const [state, setState] = useState(1);

    const [dto, setDto] = useState<UserCreateDto>({
        username: "",
        fullName: "",
        password: "",
        email: "",
        phoneNumber: "+7"
    });

    const [error, setError] = useState({
        username: "",
        name: "",
        surname: "",
        lastName: "",
        password: "",
        email: "",
        phoneNumber: ""
    });

    const [name, setName] = useState("");
    const [surname, setSurname] = useState("");
    const [lastName, setLastName] = useState("");

    const [password, setPassword] = useState("");
    const [strengthLevel, setStrengthLevel] = useState(0);

    const [errorMessage, setErrorMessage] = useState("");

    const validateUsername = (username: string) => {
        if (!username)
            return "Имя пользователя обязательно";

        if (username.length < 3)
            return "Имя пользователя слишком короткое";

        if (username.length > 25)
            return "Имя пользователя слишком длинное";

        if (!/^[a-zA-Z0-9_]+$/.test(username))
            return "Имя пользователя может содержать только цифры, буквы латинского алфавита и _";

        return "";
    };

    const validatePassword = (password: string) => {
        if (!password)
            return { error: "Пароль обязателен", level: 0 };

        if (password.length < 9)
            return { error: "", level: 1 };

        let level = 1;

        if (/[a-z]/.test(password) && /[A-Z]/.test(password))
            level++;

        if (/\d/.test(password))
            level++;

        if (/[!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?]/.test(password))
            level++;

        return { error: "", level };
    };

    const validatePhone = (phone: string) => {
        if (!phone.startsWith("+79"))
            return "Номер телефона должен начинаться с +79";

        if (!/^\+79\d{9}$/.test(phone))
            return "Неверный формат номера телефона";

        return "";
    };

    const validateEmail = (email: string) => {
        if (!email)
            return "Электронная почта обязательна";

        if (email.length > 255)
            return "Электронная почта слишком длинная";

        if (!/^[A-Za-z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Za-z0-9-]+(?:\.[A-Za-z0-9-]+)+$/.test(email))
            return "Некорректный адрес электронной почты";

        return "";
    };

    const validateName = (value: string) => {
        if (!value)
            return "Имя обязательно";

        if (value.length < 2)
            return "Имя слишком короткое";

        return "";
    };

    const validateSurname = (value: string) => {
        if (!value)
            return "Фамилия обязательна";

        if (value.length < 2)
            return "Фамилия слишком короткая";

        return "";
    };

    const nextState = () => {
        const usernameError = validateUsername(dto.username);
        const passwordValidation = validatePassword(dto.password);

        setError(prev => ({
            ...prev,
            username: usernameError,
            password: passwordValidation.error
        }));

        setStrengthLevel(passwordValidation.level);

        if (password !== dto.password) {
            setErrorMessage("Пароли не совпадают");
            return;
        }

        if (passwordValidation.level !== 4) {
            setErrorMessage("Недостаточно сильный пароль");
            return;
        }

        if (usernameError || passwordValidation.error)
            return;

        setErrorMessage("");
        setState(2);
    };

    const setUsername = (username: string) => {
        if (username.length > 25)
            return;

        setDto(prev => ({
            ...prev,
            username
        }));

        setError(prev => ({
            ...prev,
            username: validateUsername(username)
        }));
    };

    const setDtoPassword = (password: string) => {
        if (password.length > 24)
            return;

        const validation = validatePassword(password);

        setStrengthLevel(validation.level);

        setDto(prev => ({
            ...prev,
            password
        }));

        setError(prev => ({
            ...prev,
            password: validation.error
        }));
    };

    const setPhoneNumber = (phoneNumber: string) => {
        if (!/^[+0-9]*$/.test(phoneNumber))
            return;

        if (phoneNumber.length > 12)
            return;

        setDto(prev => ({
            ...prev,
            phoneNumber
        }));

        setError(prev => ({
            ...prev,
            phoneNumber: validatePhone(phoneNumber)
        }));
    };

    const setEmail = (email: string) => {
        if (email.length > 255)
            return;

        setDto(prev => ({
            ...prev,
            email
        }));

        setError(prev => ({
            ...prev,
            email: validateEmail(email)
        }));
    };

    const changeName = (value: string) => {
        setName(value);

        setError(prev => ({
            ...prev,
            name: validateName(value)
        }));
    };

    const changeSurname = (value: string) => {
        setSurname(value);

        setError(prev => ({
            ...prev,
            surname: validateSurname(value)
        }));
    };

    const changeLastName = (value: string) => {
        setLastName(value);
    };

    const handleRegistration = async () => {
        const errors = {
            ...error,
            phoneNumber: validatePhone(dto.phoneNumber),
            email: validateEmail(dto.email),
            name: validateName(name),
            surname: validateSurname(surname),
            lastName: ""
        };

        setError(errors);

        if (Object.values(errors).some(Boolean))
            return;

        const request = {
            ...dto,
            fullName: `${surname} ${name} ${lastName}`.trim()
        };

        const response = await usersClient.post(request);
        
        if (response.success == false)
            setErrorMessage(response.error.message);
    };

    return (
        <div>
            <div className="fixed top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2">
                <div className="frame p-3 w-87.5">
                    <div className="flex flex-col text-center mb-3">
                        <label className="text-3xl font-bold mb-2">SiG Manager</label>
                        <label className="text-2xl font-bold">Регистрация</label>
                    </div>

                    <div className="flex flex-col gap-1">
                        {state === 1 && (
                            <>
                                <div>
                                    <label>Имя пользователя:</label>
                                    <input
                                        type="text"
                                        className={`form-control ${error.username ? "invalid" : ""}`}
                                        value={dto.username}
                                        onChange={(e) => setUsername(e.target.value)}
                                        placeholder="Имя пользователя..."
                                    />
                                    {error.username && (
                                        <label className="text-danger">{error.username}</label>
                                    )}
                                </div>

                                <div className="flex flex-col">
                                    <label>Пароль:</label>
                                    <input
                                        type="password"
                                        className={`form-control ${error.password ? "invalid" : ""}`}
                                        value={dto.password}
                                        onChange={(e) => {
                                            setDtoPassword(e.target.value);
                                            setErrorMessage("");
                                        }}
                                        placeholder="Пароль..."
                                    />
                                    {error.password && (
                                        <label className="text-danger">{error.password}</label>
                                    )}

                                    <label>Сила пароля:</label>
                                    <div className="mb-2">
                                        <div
                                            className={`fixed rounded-xl left-5 h-2 border border-primary transition-all duration-200 z-10
                                            ${strengthLevel === 0 ? "bg-danger right-100" : ""}
                                            ${strengthLevel === 1 ? "bg-danger right-70" : ""}
                                            ${strengthLevel === 2 ? "bg-amber-500 right-50" : ""}
                                            ${strengthLevel === 3 ? "bg-amber-500 right-30" : ""}
                                            ${strengthLevel === 4 ? "bg-success right-5" : ""}`}
                                        />
                                        <div className="fixed rounded-xl h-2 left-5 right-5 bg-primary border border-primary" />
                                    </div>
                                </div>

                                <div>
                                    <label>Повторите пароль:</label>
                                    <input
                                        type="password"
                                        className={`form-control ${password && password !== dto.password ? "invalid" : ""}`}
                                        value={password}
                                        onChange={(e) => {
                                            setPassword(e.target.value);
                                            setErrorMessage("");
                                        }}
                                        placeholder="Пароль..."
                                    />
                                    {password && password !== dto.password && (
                                        <label className="text-danger">
                                            Пароли не совпадают
                                        </label>
                                    )}
                                </div>

                                {errorMessage && (
                                    <label className="text-danger">{errorMessage}</label>
                                )}

                                <button
                                    className="btn btn-primary mt-2"
                                    onClick={nextState}
                                >
                                    <i className="bi-arrow-up-right me-1"></i>
                                    Продолжить
                                </button>
                            </>
                        )}

                        {state === 2 && (
                            <>
                                <label>Номер телефона:</label>
                                <input
                                    type="text"
                                    className={`form-control ${error.phoneNumber ? "invalid" : ""}`}
                                    value={dto.phoneNumber}
                                    onChange={(e) => setPhoneNumber(e.target.value)}
                                    placeholder="Номер телефона..."
                                />
                                {error.phoneNumber && (
                                    <label className="text-danger">{error.phoneNumber}</label>
                                )}

                                <label>Электронная почта:</label>
                                <input
                                    type="text"
                                    className={`form-control ${error.email ? "invalid" : ""}`}
                                    value={dto.email}
                                    onChange={(e) => setEmail(e.target.value)}
                                    placeholder="Электронная почта..."
                                />
                                {error.email && (
                                    <label className="text-danger">{error.email}</label>
                                )}

                                <label>Фамилия:</label>
                                <input
                                    type="text"
                                    className={`form-control ${error.surname ? "invalid" : ""}`}
                                    value={surname}
                                    onChange={(e) => changeSurname(e.target.value)}
                                    placeholder="Фамилия..."
                                />
                                {error.surname && (
                                    <label className="text-danger">{error.surname}</label>
                                )}

                                <label>Имя:</label>
                                <input
                                    type="text"
                                    className={`form-control ${error.name ? "invalid" : ""}`}
                                    value={name}
                                    onChange={(e) => changeName(e.target.value)}
                                    placeholder="Имя..."
                                />
                                {error.name && (
                                    <label className="text-danger">{error.name}</label>
                                )}

                                <label>Отчество:</label>
                                <input
                                    type="text"
                                    className={`form-control ${error.lastName ? "invalid" : ""}`}
                                    value={lastName}
                                    onChange={(e) => changeLastName(e.target.value)}
                                    placeholder="Отчество..."
                                />
                                {error.lastName && (
                                    <label className="text-danger">{error.lastName}</label>
                                )}

                                <button
                                    className="btn btn-primary mt-2"
                                    onClick={handleRegistration}
                                >
                                    Зарегистрироваться
                                </button>

                                <button
                                    className="btn btn-secondary mt-2"
                                    onClick={() => setState(1)}
                                >
                                    Назад
                                </button>
                            </>
                        )}

                    </div>
                </div>
            </div>
            {errorMessage &&
                (
                    <ErrorPopUp header="Ошибка" onExit={() => setErrorMessage("")} message={errorMessage}></ErrorPopUp>
                )}

        </div>

    )
}