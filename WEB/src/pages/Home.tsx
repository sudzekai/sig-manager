import { useEffect, useState } from "react"
import { parkLat, parkLong, type OpenMeteoDto } from "../api/types/dtos/weather/OpenMeteoDto"
import { weatherClient } from "../api/clients/weatherClient";
import { Link } from "react-router";

export default function Home() {
    const [weatherData, setWeatherData] = useState<OpenMeteoDto>(
        {
            hourly: {
                time: [],
                temperature_2m: [],
                weather_code: [],
            }
        }
    );

    const getIcon = (code: number) => {
        if (code === 0) return "bi bi-sun-fill text-amber-300";
        if (code === 1) return "bi bi-cloud-sun-fill text-amber-200";
        if (code === 2) return "bi bi-cloud-sun-fill text-gray-400";
        if (code === 3) return "bi bi-clouds-fill text-gray-500";

        if (code === 45 || code === 48)
            return "bi bi-cloud-fog2-fill text-gray-400";

        if (code >= 51 && code <= 57)
            return "bi bi-cloud-drizzle-fill text-sky-300";

        if (code >= 61 && code <= 67)
            return "bi bi-cloud-rain-fill text-sky-500";

        if (code >= 71 && code <= 77)
            return "bi bi-cloud-snow-fill text-slate-300";

        if (code >= 80 && code <= 82)
            return "bi bi-cloud-rain-heavy-fill text-sky-600";

        if (code >= 85 && code <= 86)
            return "bi bi-cloud-snow-fill text-slate-200";

        if (code === 95 || code === 96 || code === 99)
            return "bi bi-cloud-lightning-rain-fill text-violet-400";

        return "bi bi-question-circle";
    };



    useEffect(() => {
        async function loadWeather() {
            const data = await weatherClient.getHourly(parkLat, parkLong);
            setWeatherData(data);
        }


        loadWeather();
    }, [])

    const filtered = weatherData.hourly.time
        .map((_, i) => ({
            time: weatherData.hourly.time[i],
            temperature: weatherData.hourly.temperature_2m[i],
            weatherCode: weatherData.hourly.weather_code[i],
        }))
        .filter((_, i) => i % 3 === 0);

    return (
        <div className="flex flex-col gap-3">
            <div className="frame frame-header">
                Главная
            </div>

            <div className="frame flex flex-col gap-2">
                <label className="h3">SiG Manager</label>
                <p>SiG Manager - система учёта сотрудников, смен и расходников ИП "Гладких Иван Юрьевич" с дополнительными функциональными возможностями в виде: подсчёта выручки, смен, зарплат сотрудников</p>
                <p>На данный момент система <span className="text-red-400">находится в alfa тестировании</span>, и активно разрабатывается</p>
            </div>

            <div className="frame flex flex-col gap-2">
                <label className="h3">Погода</label>
                <p>
                    Прогноз погоды на следующие 48 с промежутком в 3 часа предоставлен:
                    <Link to={"https://open-meteo.com/"} className="ms-1 text-accent underline">OpenMeteo</Link>
                </p>
                <p>
                    Для получения более подробного прогноза погоды:
                    <Link to={"https://www.gismeteo.ru/weather-arkhangelsk-3915/"} className="ms-1 text-accent underline me-1">GisMeteo</Link>
                    или
                    <Link to={"https://yandex.ru/pogoda/ru/arhangelsk"} className="ms-1 text-accent underline">Яндекс Погода</Link>
                </p>
                <div className="overflow-x-auto">
                    <table>
                        <thead>
                            <tr>
                                <th className="px-2 w-1">Время</th>
                                {filtered.map((p, i) => {
                                    const date = new Date(p.time);
                                    return (
                                        <th className="px-2" key={i}>{date.getHours().toString().padStart(2, "0")}:{date.getMinutes().toString().padStart(2, "0")}</th>
                                    )
                                })}
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td className="px-1 text-nowrap">Темп. °C</td>
                                {filtered.map((p, i) => (
                                    <td className="text-nowrap text-center px-1" key={i}>{Math.round(p.temperature)}°C</td>
                                ))}
                            </tr>
                            <tr>
                                <td className="px-1">Погода</td>
                                {filtered.map((p, i) => (
                                    <td className="text-nowrap text-center px-1" key={i}>
                                        <i className={`${getIcon(p.weatherCode)} text-2xl`}></i>
                                    </td>
                                ))}
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <div className="frame flex flex-col gap-2">
                <label className="h3">Уведомления</label>
                <div className="quote ms-2">
                    Добавлен прогноз погоды
                </div>
            </div>

            <div className="frame flex flex-col gap-2">
                <label className="h3">Сотрудник месяца</label>
                <p>В будущем тут будет сотрудник с наибольшим количеством смен в месяце :)</p>
            </div>
        </div>
    )
}