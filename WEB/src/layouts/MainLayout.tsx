import { Link, Outlet } from "react-router"
import "./mainLayout.css"
import { useEffect, useState } from "react";
import { getCookie, setCookie } from "../scripts/cookies";

export default function MainLayout() {
    const [isSideBarOpened, setIsSideBarOpened] = useState(false);
    const [currentTheme, setCurrentTheme] = useState<string>(() => {
        return getCookie<string>("theme") ?? "light";
    });

    useEffect(() => {
        const elem = document.getElementById("site-root");

        if (!elem) return;

        elem.classList.remove("light", "dark");
        elem.classList.add(currentTheme);

        setCookie("theme", currentTheme);
    }, [currentTheme]);

    const changeTheme = () => {
        const elem = document.getElementById("site-root");
        if (elem) {
            setCurrentTheme(prev => prev == "light" ? "dark" : "light");
        }
    };

    const navi = (
        <nav className="fixed frame bottom-2 left-2 right-2 md:right-auto md:top-2 z-10">
            <div className="flex flex-row md:flex-col gap-2">
                <button onClick={() => setIsSideBarOpened(!isSideBarOpened)} className="btn btn-icon flex-1">
                    <i className="bi-list"></i>
                    Меню
                </button>

                <Link className="btn btn-icon flex-1"
                    to={"/"}>
                    <i className="bi-house-fill"></i>
                    Главная
                </Link>

                <Link className="btn btn-icon flex-1"
                    to={"/users"}>
                    <i className="bi-people-fill"></i>
                    Пользователи
                </Link>

                <Link className="btn btn-icon flex-1"
                    to={"/shifts/open/router"}>
                    <i className="bi-plus-lg"></i>
                    Смена
                </Link>
            </div>
        </nav>
    );

    const sideBar = (
        <div className={`transition-all duration-200 w-0 ${isSideBarOpened ? "w-8/12 md:w-2/12 md:px-2" : "p-0"} fixed top-0 overflow-hidden z-20 h-1/1 frame rounded-none p-0 py-2`}>
            <div className="flex flex-col gap-2">
                <label className="text-2xl md:text-3xl font-bold px-3">SiG Manager</label>

                <hr className="mx-2" />

                <Link className="btn text-start btn-lg"
                    to={"/"} onClick={() => setIsSideBarOpened(false)}>
                    <i className="bi bi-house-fill me-1"></i>
                    Главная
                </Link>

                <Link className="btn text-start btn-lg"
                    to={"/users"} onClick={() => setIsSideBarOpened(false)}>
                    <i className="bi-people-fill me-1"></i>
                    Пользователи
                </Link>

                <Link className="btn text-start btn-lg"
                    to={"/register"} onClick={() => setIsSideBarOpened(false)}>
                    <i className="bi-person-plus-fill me-1"></i>
                    Регистрация
                </Link>

                <hr className="mx-2" />

                <Link className="btn text-start btn-lg"
                    to={"/shifts/router"} onClick={() => setIsSideBarOpened(false)}>
                    <i className="bi-clipboard me-1"></i>
                    Смены
                </Link>

                <hr className="mx-2" />

                <button className="btn btn-link text-alt text-start btn-lg"
                    onClick={() => {
                        changeTheme();
                        setIsSideBarOpened(false);
                    }}>
                    {currentTheme == "light" && (
                        <i className=" bi-moon-fill me-1"></i>
                    )}
                    {currentTheme == "dark" && (
                        <i className="bi-sun-fill me-1"></i>
                    )}

                    {currentTheme == "light" ? "Тёмная" : "Светлая"} тема
                </button>
            </div>
        </div>
    )

    return (
        <div>


            <div className={`flex flex-col md:flex-row flex-1 md:justify-center`}>
                {navi}

                <main className="md:container m-2 mb-22">
                    <Outlet />
                </main>
            </div>

            <div
                className={`${isSideBarOpened ? "backdrop-blur transition-all duration-200 fixed" : "hidden"} z-20 inset-0`}
                onClick={() => setIsSideBarOpened(false)}
            >
            </div>

            {sideBar}
        </div>
    )
}