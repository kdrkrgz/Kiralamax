import { ResponseModel } from "./responseModel";

export interface ListResponseModel<Type> extends ResponseModel {
    data:Type[];
}
