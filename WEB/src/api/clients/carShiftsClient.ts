import type { CarShiftCloseDto } from "../types/dtos/carShifts/CarShiftCloseDto";
import type { CarShiftInfoDto } from "../types/dtos/carShifts/CarShiftInfoDto";
import type { CarShiftOpenDto } from "../types/dtos/carShifts/CarShiftOpenDto";
import type { CarShiftSimpleDto } from "../types/dtos/carShifts/CarShiftSimpleDto";
import type { ResponseEnvelope } from "../types/responses/ResponseEnvelope";
import { client } from "./base";

export const carShiftsClient = {
    getAll: async (
        params?: {
            createdAtStart?: string,
            createdAtEnd?: string,
            closedAtStart?: string,
            closedAtEnd?: string,
            status?: string,
            limit?: number,
            offset?: number
        }): Promise<ResponseEnvelope<CarShiftSimpleDto[]>> => {
        var response = await client.get<CarShiftSimpleDto[]>(`shifts/cars`,
            {
                params: {
                    createdAtStart: params?.createdAtStart,
                    createdAtEnd: params?.createdAtEnd,
                    closedAtStart: params?.closedAtStart,
                    closedAtEnd: params?.closedAtEnd,
                    status: params?.status,
                    limit: params?.limit || 25,
                    offset: params?.offset || 0
                }
            }
        );
        return response;
    },
    getById: async (id: number): Promise<ResponseEnvelope<CarShiftInfoDto>> => {
        var response = await client.get<CarShiftInfoDto>(`shifts/cars/${id}`);
        return response;
    },
    open: async (body: CarShiftOpenDto): Promise<ResponseEnvelope<CarShiftInfoDto>> => {
        var response = await client.post<CarShiftInfoDto>("shifts/cars", body);
        return response;
    },
    close: async (id: number, body: CarShiftCloseDto): Promise<ResponseEnvelope<CarShiftInfoDto>> => {
        var response = await client.put<CarShiftInfoDto>(`shifts/cars/${id}`, body);
        return response;
    }
}