export default function Home() {
    return (
        <div className="flex flex-col gap-3">
            <div className="frame frame-header">
                Главная
            </div>

            <div className="frame flex flex-col gap-2">
                <h3>SiG Manager</h3>
                <p>SiG Manager - система учёта сотрудников, смен и расходников ИП "Гладких Иван Юрьевич" с дополнительными функциональными возможностями в виде: подсчёта выручки, смен, зарплат сотрудников</p>
                <p>На данный момент система <span className="text-red-400">находится в alfa тестировании</span>, и активно разрабатывается</p>
            </div>
        </div>
    )
}