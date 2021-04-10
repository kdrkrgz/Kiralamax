import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Category } from 'src/app/Models/Category';
import { CategoryService } from 'src/app/Services/category.service';
import { JwtTokenService } from 'src/app/Services/jwt-token.service';
import { SnackbarService } from 'src/app/Services/snackbar.service';

@Component({
  selector: 'app-category-add',
  templateUrl: './category-add.component.html',
  styleUrls: ['./category-add.component.css']
})
export class CategoryAddComponent implements OnInit {
  categoryAddForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<CategoryAddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: null,
    private _formBuilder: FormBuilder,
    private snackBarService: SnackbarService,
    private categoryService: CategoryService,
    public dialog: MatDialog,
    private tokenService: JwtTokenService) {
  }

  ngOnInit() {
    this.createCategoryAddForm();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  createCategoryAddForm(){
    this.categoryAddForm = this._formBuilder.group({
      categoryName: ['',Validators.required]
    })
  }

  addCategory(){
    let categoryModel:Category = Object.assign({}, this.categoryAddForm.value);
    if(this.categoryAddForm.valid){
      this.categoryService.AddCategory(categoryModel).subscribe(response => {
        this.snackBarService.openSnackBarWithMessage(response);
      })
    }
  }
}
