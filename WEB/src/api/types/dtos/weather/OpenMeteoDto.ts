export interface OpenMeteoDto {
    hourly: {
        time: string[];
        temperature_2m: number[];
        weather_code: number[];
    };
}

export const parkLat = 64.52656;
export const parkLong = 40.56340;