import { Bill } from "./bill";

export interface User {
    id: number;
    userName: string;
    created: Date;
    lastActive: Date;
    bills: Bill[];
}