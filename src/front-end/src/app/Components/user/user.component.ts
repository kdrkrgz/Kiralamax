import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { UserData } from 'src/app/Models/UserData';
import { ConfirmService } from 'src/app/Services/confirm.service';
import { SnackbarService } from 'src/app/Services/snackbar.service';
import { UserService } from 'src/app/Services/user.service';
import { ConfirmComponent } from '../confirm/confirm.component';
import { UserUpdateComponent } from '../user-update/user-update.component';
import { UserDataSource } from './user-datasource';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<UserData>;
  dataSource: any = UserDataSource;
  UsersData: UserData[];
  isFinished: boolean = false;
  selectedUser: UserData
  //filters
  userDataEmailFilter: string;


  displayedColumns = ["userId", "customerId", "email", "firstName", "lastName", "status", "companyName", "operations"];

  constructor(
    private userService: UserService,
    private snackBarService: SnackbarService,
    private confirmService: ConfirmService,
    public dialog: MatDialog) {
    this.GetUsersDataResult();
  }

  ngOnInit() {
    this.displayedColumns;
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource as UserData[];
  }

  GetUsersDataResult() {
    this.userService.getUsers().subscribe(response => {
      let data = response.data as UserData[]
      this.dataSource = new UserDataSource(data),
        this.isFinished = true;
    })
  }

  userEmailFilter(userEmail: string) {
    const filterValue = userEmail.trim().toLocaleLowerCase();
    let usersData = this.dataSource.data as UserData[];
    filterValue ? this.table.dataSource = usersData.filter((x: UserData) => x.email.toLocaleLowerCase().indexOf(filterValue) !== -1) : this.table.dataSource = usersData;

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  getSelectedUserData(data: UserData) {
    this.selectedUser = data;
  }

  openUserUpdateDialog() {
    const dialogRef = this.dialog.open(UserUpdateComponent, {
      width: '300px',
      maxHeight: 750,
      data: this.selectedUser

    }
    );
  }

  deleteUserWithConfirm(userEmail: string) {
    this.confirmService.createConfirm("Kullanıcı silme").subscribe(res => {
      if (res == true) {
        this.deleteUser(userEmail);
      }
    })
  }

  deleteUser(userEmail: string) {
    this.userService.deleteUser(userEmail).subscribe(response => {
      this.snackBarService.openSnackBarWithMessage(response)
    })
  }
  
}
