import type { UserInfoDto } from "../types/dtos/users/UserInfoDto";
import type { UserSimpleDto } from "../types/dtos/users/UserSimpleDto";
import type { UserCreateDto } from "../types/dtos/users/UserCreateDto";

import { client } from "./base";
import type { ResponseEnvelope } from "../types/responses/ResponseEnvelope";

export const usersClient = {
    getAll: async (): Promise<ResponseEnvelope<UserSimpleDto[]>> => {
        var response = await client.get<UserSimpleDto[]>("users");
        return response;
    },

    getById: async (id: number): Promise<ResponseEnvelope<UserInfoDto>> => {
        var response = await client.get<UserInfoDto>(`users/${id}`);
        return response;
    },

    post: async (body: UserCreateDto): Promise<ResponseEnvelope<UserInfoDto>> => {
        var response = await client.post<UserInfoDto>(`users`, body);
        return response;
    }
}