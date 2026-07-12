import type { UserPositionDto } from "../UserPositionDto";

export type CarShiftInfoDto = {
    id: number;
    parkId: number;
    status: string;
    createdAt: Date;
    closedAt?: Date;
    cash?: number;
    cashLess?: number;
    total?: number;
    difference?: number;
    receiptPhotoFileName?: string;
    firstTicket: number;
    lastTicket?: number;
    totalTickets?: number;
    ticketPrice: number;
    users: UserPositionDto[];
}