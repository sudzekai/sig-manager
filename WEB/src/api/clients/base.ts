import axios, { type AxiosRequestConfig } from "axios";
import type { ResponseEnvelope } from "../types/responses/ResponseEnvelope";

export const api = axios.create({
    baseURL: 'http://localhost:8080/api',
    timeout: 30000,
    headers: {
        'Content-Type': 'application/json',
    },
});

export const client = {
    get: async <T>(endpoint: string, params?: AxiosRequestConfig<any>): Promise<ResponseEnvelope<T>> => {
        try {
            const response = await api.get<ResponseEnvelope<T>>(endpoint, params);
            return response.data;
        } catch (error) {
            return (error as any).response?.data;
        }
    },

    post: async <T>(endpoint: string, body: any): Promise<ResponseEnvelope<T>> => {
        try {
            const response = await api.post<ResponseEnvelope<T>>(endpoint, body);
            return response.data;
        } catch (error) {
            return (error as any).response?.data;
        }
    },

    put: async <T>(endpoint: string, body: any): Promise<ResponseEnvelope<T>> => {
        try {
            const response = await api.put<ResponseEnvelope<T>>(endpoint, body);
            return response.data;
        } catch (error) {
            return (error as any).response?.data;
        }
    },

    delete: async <T>(endpoint: string): Promise<ResponseEnvelope<T>> => {
        try {
            const response = await api.delete<ResponseEnvelope<T>>(endpoint);
            return response.data;
        } catch (error) {
            return (error as any).response?.data;
        }
    },
};