import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { Category } from 'src/app/Models/Category';
import { CategoryService } from 'src/app/Services/category.service';
import { ConfirmService } from 'src/app/Services/confirm.service';
import { SnackbarService } from 'src/app/Services/snackbar.service';
import { CategoryAddComponent } from '../category-add/category-add.component';
import { CategoryUpdateComponent } from '../category-update/category-update.component';
import { CategoryDataSource } from './category-datasource';



@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<Category>;
  dataSource: any = CategoryDataSource;

  CategoriesData: Category[];
  isFinished: boolean = false;
  selectedCategoy: Category;
  displayedColumns = ['id', 'categoryName', "operations"];

  constructor(
    private categoryService: CategoryService,
    private snackBarService: SnackbarService,
    private confirmService: ConfirmService,
    public dialog: MatDialog
  ) {
    this.GetCategoriesDataResult();
  }

  ngOnInit() {
    this.displayedColumns;
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource as Category[];
  }

  GetCategoriesDataResult() {
    this.categoryService.GetCategories().subscribe(response => {
      let data = response.data as Category[];
      this.dataSource = new CategoryDataSource(data.map(x => ({ id: x.id, categoryName: x.categoryName })));
      this.CategoriesData = response.data as Category[];
      this.isFinished = true;
    })
  }
  
  deleteCategoryWithConfirm(categoryId: number) {
    this.confirmService.createConfirm("Kategori silme").subscribe(res => {
      if (res == true) {
        this.deleteCategory(categoryId);
      }
    })
    
  }

  deleteCategory(categoryId: number) {
    this.categoryService.DeleteCategory(categoryId).subscribe(response => {
      this.snackBarService.openSnackBarWithMessage(response);
    })
  }

  getSelectedCategoryData(data: Category) {
    this.selectedCategoy = data;
  }

  openUpdateCategoryDialog() {
    const dialogRef = this.dialog.open(CategoryUpdateComponent, {
      width: '750px',
      maxHeight: 750,
      data: this.selectedCategoy
    }
    );
  }

  openCategoryAddDialog() {
    const dialogRef = this.dialog.open(CategoryAddComponent, {
      width: '750px',
      maxHeight: 750,
      data: null
    }
    );
  }

}
