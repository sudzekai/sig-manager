import { HomeIcon, PlusIcon, UsersIcon } from "@heroicons/react/16/solid"
import { Link, Outlet } from "react-router"

export default function MainLayout() {
    return (
        <div className="flex flex-col md:flex-row">
            <header className="bg-primary rounded-xl fixed right-2 top-2">
                <label className="flex justify-center items-center text-lg px-2 text-alt">
                    v3.0.0
                </label>
            </header>

            <main className="fixed top-2 bottom-2 left-2 right-2">
                <Outlet />
            </main>

            <footer className="md:hidden fixed bottom-2 left-2 right-2 rounded-xl py-1 px-2 text-alt bg-primary flex flex-row justify-center">
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
            </footer>
        </div>
    )
}