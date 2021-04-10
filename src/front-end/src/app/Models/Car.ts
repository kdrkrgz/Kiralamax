import { Category } from "./Category";
import { Photo } from "./Photo";

export interface Car {
    [key: string]: any;
    id:number;
    brand:string;
    carModel:string;
    color:string;
    gear:string;
    isRented:boolean;
    dailyPrice:number;
    minCreditScore:number;
    fuelType: string;
    modelYear:number;
    engineCapacity:number;
    photos: Photo[];
    category: Category;
}
