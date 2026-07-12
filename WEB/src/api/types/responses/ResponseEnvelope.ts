import type { Error } from "./Error";

export interface ResponseEnvelope<T = any> {
    success: boolean,
    data: T,
    error: Error
}