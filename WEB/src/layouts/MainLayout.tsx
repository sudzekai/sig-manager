import { HomeIcon, PlusIcon, UsersIcon } from "@heroicons/react/16/solid"
import { Link, Outlet } from "react-router"
import "./mainLayout.css"

export default function MainLayout() {

    const navi = (
        <nav className="fixed bg-primary bottom-2 left-2 right-2 rounded-xl md:right-auto md:top-2">
            <div className="flex flex-row md:flex-col justify-around">
                <Link className="btn btn-link text-alt"
                    to={"/users"}>
                    <UsersIcon className="size-8"></UsersIcon>
                </Link>
                <Link className="btn btn-link text-alt"
                    to={"/"}>
                    <HomeIcon className="size-8"></HomeIcon>
                </Link>
                <Link className="btn btn-link text-alt"
                    to={"/shifts/cars/open"}>
                    <PlusIcon className="size-8"></PlusIcon>
                </Link>
            </div>
        </nav>
    );

    return (
        <div className="flex flex-col md:flex-row flex-1 md:justify-center">
            <header className="bg-primary rounded-xl fixed right-2 top-2">
                <label className="flex justify-center items-center text-lg px-2 text-alt">
                    v0.3.0
                </label>
            </header>

            {navi}

            <main className="md:mx-0 md:px-10 md:container m-3 mb-15">
                <Outlet />
            </main>
        </div>
    )
}