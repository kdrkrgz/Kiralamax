import { Customer } from "./Customer";

export interface UserData {
    id: number;
    email:string;
    firstName: string;
    lastName:string;
    customerId: number;
    status:boolean;
    companyName:string;
}