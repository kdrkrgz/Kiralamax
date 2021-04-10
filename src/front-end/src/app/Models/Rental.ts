export interface Rental {
    [key: string]: any,
    rentId: number,
    carId: number,
    rentDate: string,
    returnDate: string,
    customerName: string,
    customerLastName: string,
    carBrand: string,
    carModel: string,
    dailyPrice: number,
    totalRentDays: number,
    totalPrice: number;
    isFinished: boolean;
}
