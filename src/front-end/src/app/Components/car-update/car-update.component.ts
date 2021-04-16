import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Car } from 'src/app/Models/Car';
import { Category } from 'src/app/Models/Category';
import { Photo } from 'src/app/Models/Photo';
import { CarService } from 'src/app/Services/car.service';
import { CategoryService } from 'src/app/Services/category.service';
import { ConfirmService } from 'src/app/Services/confirm.service';
import { SnackbarService } from 'src/app/Services/snackbar.service';

@Component({
  selector: 'app-car-update',
  templateUrl: './car-update.component.html',
  styleUrls: ['./car-update.component.css']
})
export class CarUpdateComponent implements OnInit {

  carUpdateFormGroup: FormGroup;
  car: Car;
  categories: Category[];
  // file upload
  photoList: FileList;
  selectedCat :string;
  category = new FormControl('', Validators.required);



  constructor(
    public dialogRef: MatDialogRef<CarUpdateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Car,
    private _formBuilder: FormBuilder,
    private carService: CarService,
    private snackBarService: SnackbarService,
    private categoryService: CategoryService,
    private confirmService: ConfirmService
  ) {
    this.selectedCat = this.data.category.id.toString();
    this.car = data;
  }

  ngOnInit() {
    this.getCategories();
    this.createCarUpdateForm();
    console.log("Dialoga gelen data", this.data)
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  createCarUpdateForm() {
    this.carUpdateFormGroup = this._formBuilder.group({
      brand: ['', Validators.required],
      carModel: ['', Validators.required],
      gear: new FormControl({value:this.car.gear},[Validators.required]),
      fuelType: ['', Validators.required],
      modelYear: ['', Validators.required],
      engineCapacity: ['', Validators.required],
      color: ['', Validators.required],
      categoryId: ['', Validators.required],
      dailyPrice: ['', Validators.required],
      minCreditScore: ['', Validators.required]
    });
  }

  handleFileInput(files: FileList) {
    if (files.length) {
      this.photoList = files;
    }
  }

  getCategories() {
    this.categoryService.GetCategories().subscribe(response => {
      // this.carUpdateFormGroup.controls['categoryId'].setValue(this.data.categoryId);            
      this.categories = response.data;
      this.carUpdateFormGroup.controls['categoryId'].setValue(this.data.categoryId);   
    });
  }

  updateCarWithConfirm() {
    this.confirmService.createConfirm("Araç Fotoğrafı Silme").subscribe(res => {
      if (res == true) {
        this.updateCar();
      }
    })
  }

  updateCar(){

  }

  deleteCarPhoto(photo:Photo){
  console.log("photo :", photo)
    this.carService.DeleteCarPhoto(photo.id).subscribe(response => {
      this.snackBarService.openSnackBarWithMessage(response);
      response.isSuccess ? this.car.photos = this.data.photos.filter(obj => obj !== photo): null;
    })
  }

  deleteCarPhotoWithConfirm(photo:Photo){
    this.confirmService.createConfirm("Araç Fotoğrafı Silme").subscribe(res => {
      if (res == true) {
        this.deleteCarPhoto(photo);
      }
    })
  }



}
