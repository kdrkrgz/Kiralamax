import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { Rental } from 'src/app/Models/Rental';
import { ConfirmService } from 'src/app/Services/confirm.service';
import { RentalService } from 'src/app/Services/rental.service';
import { SnackbarService } from 'src/app/Services/snackbar.service';
import { RentalDataSource } from './rental-datasource';

@Component({
  selector: 'app-rental',
  templateUrl: './rental.component.html',
  styleUrls: ['./rental.component.css']
})
export class RentalComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<Rental>;
  dataSource: any = RentalDataSource;

  RentalsData: Rental[];
  isFinished: boolean = false;
  //filters
  rentIsFinishedFilter: string = " ";

  displayedColumns = ["rentId", "rentDate", "returnDate", "customerName", "customerLastName", "carBrand", "carModel", "dailyPrice", "totalRentDays", "totalPrice", "vatTotalPrice", "isFinished", "operations"];

  constructor(
    private rentalService: RentalService,
    private snackBarService: SnackbarService,
    public dialog: MatDialog,
    private confirmService: ConfirmService) {
    this.GetRentalsDataResult();
  }

  ngOnInit() {
    //this.GetRentalsDataResult();
    //this.dataSource = new CarDataSource(this.CarsData);

    this.displayedColumns;
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource as Rental[];
  }

  GetRentalsDataResult() {
    this.rentalService.GetRentalsDataResult().subscribe(response => {
      let data = response.data as Rental[]
      this.dataSource = new RentalDataSource(data),
        //console.log("rental data :", response)
        this.RentalsData = response.data as Rental[]
      this.isFinished = true;
    })
  }

  // rentStatusFilter 
  rentStatusFilter(isRentFinished: string) {
    let dsdata = this.dataSource.rentalData as Rental[]
    if (isRentFinished == "continued") {
      this.table.dataSource = dsdata.filter((x: Rental) => x.isFinished == false)
    }
    else if (isRentFinished == "finished") {
      this.table.dataSource = dsdata.filter((x: Rental) => x.isFinished == true)
    }
    else {
      this.table.dataSource = dsdata
    }



    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  deleteRentalWithConfirm(rentalId: number) {
    this.confirmService.createConfirm("Kiralama silme").subscribe(res => {
      if (res == true) {
        this.deleteRental(rentalId);
      }
    })
  }

  deleteRental(rentalId: number) {
    this.rentalService.DeleteRent(rentalId).subscribe(response => {
      this.snackBarService.openSnackBarWithMessage(response);
    })
  }

  complateRentalWithConfirm(carId:number){
    this.confirmService.createConfirm("Kiralama sonlandırma").subscribe(res => {
      if (res == true) {
        this.complateRental(carId);
      }
    })
  }

  complateRental(carId: number) {
    this.rentalService.ComplateRent(carId).subscribe(response => {
      this.snackBarService.openSnackBarWithMessage(response);
    })
  }

}
