import type { UserPositionDto } from "../UserPositionDto"

export type CarShiftOpenDto = {
    users: UserPositionDto[],
    firstTicket: number,
    ticketPrice: number,
    parkId: number
}