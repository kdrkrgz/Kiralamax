import { AccountModel } from "./AccountModel";

export interface PasswordResetModel extends AccountModel {
    userEmail:string,
    password:string,
    code:string;
}