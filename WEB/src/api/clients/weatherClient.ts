import type { OpenMeteoDto } from "../types/dtos/weather/OpenMeteoDto";
import { api } from "./base";

export const weatherClient = {
    getHourly: async (latitude: number, longitude: number): Promise<OpenMeteoDto> => {
        const response = await api.get<OpenMeteoDto>(
            "https://api.open-meteo.com/v1/forecast",
            {
                params: {
                    latitude,
                    longitude,
                    hourly: "temperature_2m,weather_code",
                    forecast_hours: 48,
                    timezone: "auto"
                }
            }
        );

        return response.data;
    }
};