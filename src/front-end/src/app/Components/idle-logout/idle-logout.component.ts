import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BnNgIdleService } from 'bn-ng-idle';
import { Subscription, timer } from 'rxjs';
import { AuthService } from 'src/app/Services/auth.service';
import { RentalService } from 'src/app/Services/rental.service';
import { SnackbarService } from 'src/app/Services/snackbar.service';

@Component({
  selector: 'app-idle-logout',
  templateUrl: './idle-logout.component.html',
  styleUrls: ['./idle-logout.component.css']
})
export class IdleLogoutComponent implements OnInit {

  timeLeft: number = 30;
  subscribeTimer:number;

  constructor(
    public dialogRef: MatDialogRef<IdleLogoutComponent>,
    @Inject(MAT_DIALOG_DATA) public data: boolean,
    private rentalService: RentalService,
    private snackBarService: SnackbarService,
    private authService : AuthService,
    private bnIdle: BnNgIdleService
    ) {
     }

  ngOnInit() {
    this.startLogOutTimer();
    this.checkOutOfTime();
  }

  onYesClick(){
    this.dialogRef.close(true);
  }
  onNoClick(): void {
    this.dialogRef.close(false);
  }

  checkOutOfTime(){
    setTimeout(() => {
      this.authService.isAuthenticated() ? this.onNoClick() : this.bnIdle.stopTimer();
      this.snackBarService.openErrorSnackBar("Uzun süre işlem yapılmadığı için sistemden çıkış yapıldı.")
      this.bnIdle.stopTimer();
    }, 28000);
  }

  startLogOutTimer(){
    const source = timer(1000, 1000);
    const abc = source.subscribe(val => {
      this.subscribeTimer = this.timeLeft - val;
    });
  }
  


}
