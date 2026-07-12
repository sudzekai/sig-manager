import type { UserPositionDto } from "../UserPositionDto"

export type CarShiftOpenDto = {
    users: UserPositionDto[],
    firstTicket: number,
    tikcetPrice: number,
    parkId: number
}