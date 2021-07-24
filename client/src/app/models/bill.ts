import { BillType } from "./bill-type";

export interface Bill {
    id: number;
    value: number;
    month: number;
    year: number;
    billType: BillType;
}

export interface NewBillDto {
    value: number;
    month: number;
    year: number;
    typeId: number;
    userId: number;
}

export interface UpdateBillDto {
    id: number;
    value: number;
    month: number;
    year: number;
    typeId: number;
    userId: number;
}