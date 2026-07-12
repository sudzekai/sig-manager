import type { UserInfoDto } from "../types/dtos/users/UserInfoDto";
import type { UserSimpleDto } from "../types/dtos/users/UserSimpleDto";
import { client } from "./base";

export const usersClient = {
    getAll: async () : Promise<UserSimpleDto[]> => {
        var response = await client.get<UserSimpleDto[]>("users");
        return response;
    },

    getById: async(id: number) : Promise<UserInfoDto> => {
        var response = await client.get<UserInfoDto>(`users/${id}`);
        return response;
    }
}