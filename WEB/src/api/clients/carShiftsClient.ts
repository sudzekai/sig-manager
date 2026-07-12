import type { CarShiftInfoDto } from "../types/dtos/carShifts/CarShiftInfoDto";
import type { CarShiftOpenDto } from "../types/dtos/carShifts/CarShiftOpenDto";
import { client } from "./base";

export const carShiftsClient = {
    open: async (body: CarShiftOpenDto): Promise<CarShiftInfoDto> => {
        var response = await client.get<CarShiftInfoDto>("shifts/cars");
        return response;
    }
}