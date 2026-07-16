import { Link } from "react-router";

export default function NotFound() {
    return (
        <div className=" fixed top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2">
            <div className="flex flex-col gap-4">
                <label className="text-center text-2xl font-semibold text-nowrap">
                    404
                </label>
                <label className="text-center text-2xl font-semibold text-nowrap">
                    Страница не найдена
                </label>
                <Link to={"/"}
                    className="btn btn-primary-outline mt-2">
                    На главную
                </Link>
            </div>

        </div>
    )
}