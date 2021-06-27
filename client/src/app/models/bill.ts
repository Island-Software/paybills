import { BillType } from "./bill-type";

export interface Bill {
    id: number;
    value: number;
    month: number;
    year: number;
    billType: BillType;
}