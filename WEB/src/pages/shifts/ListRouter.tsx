import { Link } from "react-router";

export default function ShiftsListRouterPage() {
    return (
        <div className="page">
            <div className="frame frame-header">
                Смены
            </div>
            <div className="frame">
                <div className="flex flex-col gap-1">
                    <label  className="h3">Выберите тип смены:</label>

                    <Link to={"/shifts/cars"}
                        className="btn btn-secondary-outline">
                        Машинки
                    </Link>

                    <Link to={"/shifts/popcorn"}
                        className="btn btn-secondary-outline">
                        Вата | Батут
                    </Link>

                    <Link to={"/shifts/train"}
                        className="btn btn-secondary-outline">
                        Паровоз
                    </Link>

                    <Link to={"/shifts/carousel"}
                        className="btn btn-secondary-outline">
                        Карусель
                    </Link>
                </div>
            </div>
        </div>
    )
}