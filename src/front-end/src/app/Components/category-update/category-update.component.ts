import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Category } from 'src/app/Models/Category';
import { CategoryService } from 'src/app/Services/category.service';
import { JwtTokenService } from 'src/app/Services/jwt-token.service';
import { SnackbarService } from 'src/app/Services/snackbar.service';

@Component({
  selector: 'app-category-update',
  templateUrl: './category-update.component.html',
  styleUrls: ['./category-update.component.css']
})
export class CategoryUpdateComponent implements OnInit {
  categoryUpdateForm: FormGroup;
  categoryData:Category; // gelmiyor.


  constructor(
    public dialogRef: MatDialogRef<CategoryUpdateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Category,
    private _formBuilder: FormBuilder,
    private snackBarService: SnackbarService,
    private categoryService: CategoryService,
    public dialog: MatDialog,
    private tokenService: JwtTokenService) {
      this.categoryData = data;
  }

  ngOnInit() {    
    this.createCategoryUpdateForm();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  createCategoryUpdateForm(){
    this.categoryUpdateForm = this._formBuilder.group({
      id:['', Validators.required],
      categoryName:['', Validators.required],
    })
    this.categoryUpdateForm.controls["id"].setValue(this.data.id)
  }

  updateCategory(){
    if(this.categoryUpdateForm.valid){
      this.categoryService.UpdateCategory(this.categoryUpdateForm.value).subscribe(response => {
        this.snackBarService.openSnackBarWithMessage(response);
      })
    }
  }



}
