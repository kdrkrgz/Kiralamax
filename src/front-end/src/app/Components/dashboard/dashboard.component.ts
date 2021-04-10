import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs/operators';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { CarService } from 'src/app/Services/car.service';
import { RentalService } from 'src/app/Services/rental.service';
import { CustomerService } from 'src/app/Services/customer.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  cards:any;
  carCount : number;
  customerCount:number;
  expectedGain:number;
  activeRentalsCount:number;
  finishedRentalsCount:number;
  activeCarsCount:number;

  constructor(
    private breakpointObserver: BreakpointObserver,
    private carService:CarService,
    private rentalService: RentalService,
    private customerService: CustomerService,
    ) {}

  ngOnInit() {
    this.getCarCount();
    this.getCustomerCount();
    this.getExpectedGain();
    //this.createCards();
  }

  getCarCount(){
      this.carService.GetCars().subscribe(response => {
        this.carCount = response.data.length;
        let activeCarsCount: number = 0;
        response.data.forEach(function(element){
          !element.isRented ? activeCarsCount +=1:0;
        })
        this.activeCarsCount = activeCarsCount;
      })
  }

  getCustomerCount(){
    this.customerService.getCustomers().subscribe(response => {
      this.customerCount = response.data.length;
    })
  }
  getExpectedGain(){
    this.rentalService.GetRentalsDataResult().subscribe(response => {
      let totalPrice:number = 0;
      let activeRentalsCount: number = 0;
      let finishedRentlsCount: number = 0;
      response.data.forEach(function(element) {
        if(!element.isFinished){
          totalPrice += element.totalPrice;
          activeRentalsCount +=1
        }
        else{
          finishedRentlsCount +=1
        }
      })
       this.expectedGain= totalPrice
       this.activeRentalsCount = activeRentalsCount;
       this.finishedRentalsCount = finishedRentlsCount;
    })
  }


  createCards(){
    /** Based on the screen size, switch from standard to one column per row */
    this.cards = this.breakpointObserver.observe(Breakpoints.Handset).pipe(
      map(({ matches }) => {
        if (matches) {
          return [
            { title: 'Araçlar', cols: 1, rows: 1, carCount:this.carCount },
            { title: 'Kiralamalar', cols: 1, rows: 1 },
            { title: 'Haftalık Kazanç', cols: 2, rows: 1 },
          ];
        }
  
        return [
          { title: 'Araçlar', cols: 1, rows: 1 },
          { title: 'Kiralamalar', cols: 1, rows: 1 },
          { title: 'Haftalık Kazanç', cols: 2, rows: 1 },
        ];
      })
    );
  }
}
