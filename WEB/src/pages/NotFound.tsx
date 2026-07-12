import { Link } from "react-router";

export default function NotFound() {
    return (
        <div className=" fixed top-1/2 left-1/4 -translate-x-1/6 -translate-y-1/2">
            <div className="flex flex-col gap-4">
                <h1 className="text-center">
                    Страница не найдена
                </h1>
                <Link to={"/"}
                    className="btn btn-primary-outline">
                    На главную
                </Link>
            </div>

        </div>
    )
}