import { Link } from "react-router";

export default function ShiftsOpenRouterPage() {
    return (
        <div className="page">
            <div className="frame frame-header">
                Открытие смены
            </div>
            <div className="frame">
                <div className="flex flex-col gap-1">
                    <label  className="h3">Выберите тип смены:</label>

                    <Link to={"/shifts/cars/open"}
                        className="btn btn-secondary-outline">
                        Машинки
                    </Link>

                    <Link to={"/shifts/popcorn/open"}
                        className="btn btn-secondary-outline">
                        Вата | Батут
                    </Link>

                    <Link to={"/shifts/train/open"}
                        className="btn btn-secondary-outline">
                        Паровоз
                    </Link>

                    <Link to={"/shifts/carousel/open"}
                        className="btn btn-secondary-outline">
                        Карусель
                    </Link>
                </div>
            </div>
        </div>
    )
}