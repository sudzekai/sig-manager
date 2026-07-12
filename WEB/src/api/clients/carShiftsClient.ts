import type { CarShiftCloseDto } from "../types/dtos/carShifts/CarShiftCloseDto";
import type { CarShiftInfoDto } from "../types/dtos/carShifts/CarShiftInfoDto";
import type { CarShiftOpenDto } from "../types/dtos/carShifts/CarShiftOpenDto";
import { client } from "./base";

export const carShiftsClient = {
    getById: async (id: number) : Promise<CarShiftInfoDto> => {
        var response = await client.get<CarShiftInfoDto>(`shifts/cars/${id}`);
        return response;
    },
    open: async (body: CarShiftOpenDto): Promise<CarShiftInfoDto> => {
        var response = await client.post<CarShiftInfoDto>("shifts/cars", body);
        return response;
    },
    close: async (id: number, body: CarShiftCloseDto) : Promise<CarShiftInfoDto> => {
        var response = await client.put<CarShiftInfoDto>(`shifts/cars/${id}`, body);
        return response;
    }
}