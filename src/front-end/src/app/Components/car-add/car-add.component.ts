import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import {MatSnackBar} from '@angular/material/snack-bar';
import { Car } from 'src/app/Models/Car';
import { Category } from 'src/app/Models/Category';
import { CarService } from 'src/app/Services/car.service';
import { CategoryService } from 'src/app/Services/category.service';
import { SnackbarService } from 'src/app/Services/snackbar.service';

@Component({
  selector: 'app-car-add',
  templateUrl: './car-add.component.html',
  styleUrls: ['./car-add.component.css']
})
export class CarAddComponent implements OnInit {

  isLinear = true;
  carAddFormGroup: FormGroup;
  car: Car;
  categories: Category[];
  // file upload
  photoList: FileList;

  constructor(
    public dialogRef: MatDialogRef<CarAddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Car,
    private _formBuilder: FormBuilder,
    private carService: CarService,
    private snackBarService: SnackbarService,
    private categoryService: CategoryService
    ) {}

  ngOnInit():void {
    this.getCategories();
    this.carAddForm();
  }


  carAddForm(){
    this.carAddFormGroup = this._formBuilder.group({
      brand: ['', Validators.required],
      carModel: ['', Validators.required],
      gear: ['', Validators.required],
      fuelType:['', Validators.required],
      modelYear: ['', Validators.required],
      engineCapacity: ['', Validators.required],
      color: ['', Validators.required],
      categoryId: ['', Validators.required],
      dailyPrice: ['', Validators.required],
      minCreditScore: ['', Validators.required]
    });
    console.log(this.carAddFormGroup);
    
  }

  onNoClick(): void {
    this.dialogRef.close();
  }



  addCar(){
    if(this.carAddFormGroup.valid)
    {
    //let carModel = Object.assign({}, this.carAddFormGroup.value)
    // images
    const formData = new FormData();
    for (let i = 0; i < this.photoList.length; i++) {
      const file = this.photoList[i];
      formData.append("images[]", file, file.name)      
    }
    formData.append("brand", this.carAddFormGroup.get('brand')?.value);
    formData.append("carModel", this.carAddFormGroup.get('carModel')?.value);
    formData.append("gear", this.carAddFormGroup.get('gear')?.value);
    formData.append("fuelType", this.carAddFormGroup.get('fuelType')?.value);
    formData.append("modelYear", this.carAddFormGroup.get('modelYear')?.value);
    formData.append("engineCapacity", this.carAddFormGroup.get('engineCapacity')?.value);
    formData.append("color", this.carAddFormGroup.get('color')?.value);
    formData.append("categoryId", this.carAddFormGroup.get('categoryId')?.value);
    formData.append("dailyPrice", this.carAddFormGroup.get('dailyPrice')?.value);
    formData.append("minCreditScore", this.carAddFormGroup.get('minCreditScore')?.value);

    // console.log("form data images: ", formData.getAll("images[]"));
    // console.log("form data brand: ", formData.getAll("brand"));

    // car add request via CarService
    this.carService.AddCar(formData).subscribe(reseponse => {
      console.log("car service return data :", reseponse);
      this.snackBarService.openSnackBarWithMessage(reseponse)
    }, responseError => {
      if(responseError.error.ValidationErrors.length > 0)
      {
        console.log("Error Messages: ", responseError.error.ValidationErrors);
          for (let i = 0; i < responseError.error.ValidationErrors.length; i++) {
            this.snackBarService.openErrorSnackBar(responseError.error.ValidationErrors[i].ErrorMessage) // backend hata döndüğünde fırlatılacak snackbar apideki exception middleware'a göre
          }
      }
    })
    }
    else{
      this.snackBarService.openErrorSnackBar("Araç bilgileri eksik yada hatalı.")
    }
  }

  handleFileInput(files:FileList){
  if(files.length) {
    this.photoList = files;
  }
  }

  getCategories(){
    this.categoryService.GetCategories().subscribe(response => {
      this.categories = response.data;
    });
  }


}
