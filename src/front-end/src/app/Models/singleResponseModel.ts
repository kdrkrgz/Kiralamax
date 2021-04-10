import { ResponseModel } from "./responseModel";

export interface SingleResponseModel<Type> extends ResponseModel {
    data: Type
}