import axios from "axios";
import type { ResponseEnvelope } from "../types/responses/ResponseEnvelope";

export const api = axios.create({
    baseURL: 'http://webshvets.ru:8080/api',
    timeout: 30000,
    headers: {
        'Content-Type': 'application/json',
    },
});

export const client = {
    get: async <T>(endpoint: string): Promise<T> => {
        const response = await api.get<ResponseEnvelope<T>>(endpoint);
        return response.data.data;
    },

    post: async <T>(endpoint: string, body: any): Promise<T> => {
        const response = await api.post<ResponseEnvelope<T>>(endpoint, body);
        return response.data.data;
    },

    put: async <T>(endpoint: string, body: any): Promise<T> => {
        const response = await api.put<ResponseEnvelope<T>>(endpoint, body);
        return response.data.data;
    },

    delete: async <T>(endpoint: string): Promise<T> => {
        const response = await api.delete<ResponseEnvelope<T>>(endpoint);
        return response.data.data;
    }
}