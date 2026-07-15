import { HomeIcon,  UsersIcon } from "@heroicons/react/16/solid"
import { Link, Outlet } from "react-router"
import "./mainLayout.css"

export default function MainLayout() {

    const navi = (
        <nav className="fixed frame bottom-2 left-2 right-2 md:right-auto md:top-2 z-10">
            <div className="flex flex-row md:flex-col justify-between gap-2">
                <Link className="btn btn-link text-alt"
                    to={"/users"}>
                    <UsersIcon className="size-8"></UsersIcon>
                </Link>
                <Link className="btn btn-link text-alt"
                    to={"/"}>
                    <HomeIcon className="size-8"></HomeIcon>
                </Link>
                <Link className="btn btn-secondary"
                    to={"/test/styles"}>
                    Тесты
                </Link>
            </div>
        </nav>
    );

    return (
        <div className="flex flex-col md:flex-row flex-1 md:justify-center">
            {navi}

            <main className="md:container m-3 mb-15">
                <Outlet />
            </main>
        </div>
    )
}