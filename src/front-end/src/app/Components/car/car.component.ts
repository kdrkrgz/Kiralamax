import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { Car } from 'src/app/Models/Car';
import { MatTable } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { CarService } from 'src/app/Services/car.service';
import { CarDataSource } from './car-datasource';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Gallery } from 'angular-gallery';
import { Photo } from 'src/app/Models/Photo';
import { environment } from 'src/environments/environment';
import { DataSource } from '@angular/cdk/collections';
import {MatSnackBar} from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { RentalAddComponent } from '../rental-add/rental-add.component';
import { CarAddComponent } from '../car-add/car-add.component';
import { Category } from 'src/app/Models/Category';
import { ResponseModel } from 'src/app/Models/responseModel';
import { LoginComponent } from '../login/login.component';
import { SnackbarService } from 'src/app/Services/snackbar.service';
import { ConfirmService } from 'src/app/Services/confirm.service';
import { AuthService } from 'src/app/Services/auth.service';
import { JwtTokenService } from 'src/app/Services/jwt-token.service';



@Component({
  selector: 'app-car',
  templateUrl: './car.component.html',
  styleUrls: ['./car.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class CarComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<Car>;
  dataSource:any = CarDataSource;

  CarsData: Car[];
  isTableExpanded: boolean = false;
  isFinished: boolean =false
  activePhotoPath: string;
  activePhotoIndex: number;
  photoPaths: Photo[];
  // dynamic lists
  brandList: {name:string}[];
  transmissionList : {name:string}[];
  categoryList: Category[];
  fuelTypeList: {name:string}[];
  // filters
  carBrandFilter: string;
  carModelFilter:string;
  carTransmissionFilter:string;
  carCategoryFilter:string;
  carFuelTypeFilter:string;
  //dialog
  selectedCar: Car;
  returnedCar: Car;
  addedCar: Car;
  // auth
  isAuthenticated: boolean;
  isUserAdmin: boolean;
  /**
   *
   */
    /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['id', 'category', 'brand', "carModel","fuelType","gear","color", "dailyPrice", "minCreditScore", "isRented", "operations"];


  constructor(
    private carService: CarService, 
    private gallery: Gallery, 
    private snackBarService: SnackbarService,
    private dialog: MatDialog, 
    private confirmService: ConfirmService,
    private authService: AuthService,
    private jwtTokenService: JwtTokenService) {
      this.GetCarsDataResult();
      this.loggedInStatus();
      this.getUserAuthority();
  }


  ngOnInit() {
    //this.dataSource = new CarDataSource(this.CarsData);
    this.displayedColumns;
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource =  this.dataSource as Car[];
    
  }

  GetCarsDataResult(){
    this.carService.GetCarsDataResult().subscribe(response => {
      let data = response.data as Car[];
      this.dataSource = new CarDataSource(data);
      this.CarsData = response.data as Car[];
      
      //filter lists (unique) -> filter removes all duplicates (v:value, i:index, a:array)
      this.brandList = (response.data.map(x => ({ name:x.brand }))).filter((v,i,a)=>a.findIndex(b=>(JSON.stringify(b) === JSON.stringify(v)))===i)
      this.transmissionList = (response.data.map(x => ({name: x.gear}))).filter((v,i,a)=>a.findIndex(t=>(JSON.stringify(t) === JSON.stringify(v)))===i)
      this.fuelTypeList = (response.data.map(x => ({name: x.fuelType}))).filter((v,i,a)=>a.findIndex(f=>(JSON.stringify(f) === JSON.stringify(v)))===i)
      this.categoryList = (response.data.map(x => ({id: x.category?.id, categoryName: x.category?.categoryName}))).filter((v,i,a)=>a.findIndex(c=>(JSON.stringify(c) === JSON.stringify(v)))===i)
      // // test
      // let br  =  cars.data.map(x => ({ brand:x.brand }));
      // let bru = br.filter((v,i,a)=>a.findIndex(t=>(JSON.stringify(t) === JSON.stringify(v)))===i)
      // console.log("unique br list :", bru )

      this.isFinished = true;
    })
  }

  getActiveCarPhoto(path:string, index: number, paths: Photo[]){
    this.activePhotoPath = path
    this.activePhotoIndex = index;
    this.photoPaths = paths;
  }

  showGallery() {
    let index = this.activePhotoIndex
    let images = this.photoPaths.map(x => ({path:environment.backend.baseRootURL +"/"+ x.path}))
    let prop = {images,index};
    this.gallery.load(prop);
}

  toggleTableRows() {
    this.isTableExpanded = !this.isTableExpanded;
    this.dataSource.data.forEach((row: any) => {
      row.isExpanded = this.isTableExpanded;
    })
  }


//* This Func Filter The Table With Given Filter Key with Given Object Property
globalFilter(filter:string,prop:string) {
  
  const filterValue = filter.trim().toLocaleLowerCase();//((event.target as HTMLInputElement)?.value).trim().toLocaleLowerCase();    //this.dataSource.filter = filterValue.trim().toLowerCase(); 
  let dsdata = this.dataSource.carData as Car[]
  filterValue ? this.table.dataSource = dsdata.filter((x:Car) => x[prop].toLocaleLowerCase().indexOf(filterValue) !== -1):this.table.dataSource = dsdata;
  
  if (this.dataSource.paginator) {
    this.dataSource.paginator.firstPage();
  }
}

//* Category Filter
categoryFilter(filter:string) {
  const filterValue = filter.trim().toLocaleLowerCase();//((event.target as HTMLInputElement)?.value).trim().toLocaleLowerCase();    //this.dataSource.filter = filterValue.trim().toLowerCase(); 
  let dsdata = this.dataSource.carData as Car[]
  
  filterValue ? this.table.dataSource = dsdata.filter((x:Car) => x.category.categoryName.toLocaleLowerCase().indexOf(filterValue) !== -1):this.table.dataSource = dsdata;
  
  if (this.dataSource.paginator) {
    this.dataSource.paginator.firstPage();
  }
}

getRowCarData(data:Car){
  this.selectedCar = data;
  // console.log("selected car", this.selectedCar)
}

//* DIALOGS //
// RentalAdd
openRentDialog(): void {
  const dialogRef = this.dialog.open(RentalAddComponent, {
    width: '2250px',
    maxHeight: 750,
    data: this.selectedCar
    
  }
  );

  dialogRef.afterClosed().subscribe(result => {
    this.returnedCar = result;
    //alert(this.returnedCar)
  });
}

// carAdd
openCarAddDialog(): void {
  this.isTableExpanded = false
  const dialogRef = this.dialog.open(CarAddComponent, {
    width: '500px',
    data:null
    
  }
  );
  
  dialogRef.afterClosed().subscribe(res => {
  })

}

deleteCategoryWithConfirm(carId: number) {
  this.confirmService.createConfirm("Araç silme").subscribe(res => {
    if (res == true) {
      this.deleteCar(carId);
    }
  })
  
}
// Car Delete
deleteCar(carId:number){
  this.carService.DeleteCar(carId).subscribe(data => {
    this.snackBarService.openSnackBarWithMessage(data)

  });
}

// Check roles 
getUserAuthority() {
   if(this.jwtTokenService.checkUserAuthority(["admin", "system.admin"]) == true ){
    this.isUserAdmin = true
   }else{
    this.isUserAdmin = false;
   }
}
loggedInStatus() {
  this.isAuthenticated = this.authService.isAuthenticated()
}

}



