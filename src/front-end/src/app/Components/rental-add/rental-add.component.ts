import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Car } from 'src/app/Models/Car';
import { RentalService } from 'src/app/Services/rental.service';
import { DatePipe } from '@angular/common';
import { SnackbarService } from 'src/app/Services/snackbar.service';
import { Customer } from 'src/app/Models/Customer';
import { CustomerService } from 'src/app/Services/customer.service';
import { CreditScore } from 'src/app/Models/CreditScore';
import { CreditScoreService } from 'src/app/Services/credit-score.service';
import { PaymentService } from 'src/app/Services/payment.service';
import { CreditCard } from 'src/app/Models/CreditCard';
import { CreditCardService } from 'src/app/Services/credit-card.service';
import { ConfirmService } from 'src/app/Services/confirm.service';
import { JwtTokenService } from 'src/app/Services/jwt-token.service';


@Component({
  selector: 'app-rental-add',
  templateUrl: './rental-add.component.html',
  styleUrls: ['./rental-add.component.css']
})

// reant-add component olarak değişecek
export class RentalAddComponent implements OnInit {
  isLinear = true;
  customerFormGroup: FormGroup;
  cardFormGroup: FormGroup;
  rentDateFormGroup: FormGroup;
  rentCar: Car[];
  customers: Customer[];
  carId: number;
  totalRentDays: number;
  CustomerCreditScore: number;
  isCreditScoreEnough: boolean = true;
  rentAddFormData: FormData;
  // payment step
  customerCreditCards: CreditCard[];
  panelOpenState = false;
  selectedCreditCard: CreditCard;
  // date picker
  minDate: Date;
  maxDate: Date;
  // auth
  isUserAdmin: boolean;
  activeCustomer: Customer;

  constructor(
    public dialogRef: MatDialogRef<RentalAddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Car,
    private _formBuilder: FormBuilder,
    private rentalService: RentalService,
    private datePipe: DatePipe,
    private snackBarService: SnackbarService,
    private customerService: CustomerService,
    private creditScoreService: CreditScoreService,
    private paymentService: PaymentService,
    private creditCardService: CreditCardService,
    private confirmService: ConfirmService,
    private jwtTokenService: JwtTokenService) {
    this.getUserAuthority();
    this.rentAddFormData = new FormData();
  }


  ngOnInit() {
    this.getCustomers();
    this.createAllForms();
  }

  // FORMS
  createCustomerForm() {
    this.customerFormGroup = this._formBuilder.group({
      customerId: ['', Validators.required]
    });
  }

  createRentDateForm() {
    const currentYear = new Date().getFullYear();
    this.minDate = new Date(currentYear - 1, 12, 93);
    this.maxDate = new Date(currentYear + 1, 11, 31);
    this.rentDateFormGroup = this._formBuilder.group({
      rentDate: ['', Validators.required],
      returnDate: ['', Validators.required]
    });
  }


  createPaymentForm() {
    this.cardFormGroup = this._formBuilder.group({
      cardOwner: ['', Validators.required],
      cardNumber: new FormControl("", [Validators.required, Validators.pattern('^[ 0-9]*$'), Validators.minLength(12)]),
      expirationDate: ['', Validators.required],
      cvvCode: ['', Validators.required],
      willBeRecorded: [false, Validators.required]
    });
  }

  private createAllForms() {
    this.createCustomerForm();
    this.createRentDateForm();
    this.createPaymentForm();
  }


  onNoClick(): void {
    this.dialogRef.close();
  }

  getTotalRentDays() {
    let rentDate = this.rentDateFormGroup.get("rentDate")?.value;
    let returnDate = this.rentDateFormGroup.get("returnDate")?.value;
    let dateRentDate = new Date(rentDate)
    let datereturnDate = new Date(returnDate);
    let days = Math.floor((datereturnDate.getTime() - dateRentDate.getTime()) / 1000 / 60 / 60 / 24); // bugünü getirmiyor.
    this.totalRentDays = days;
  }
  // API CALLS
  addRent() {
    if (this.rentDateFormGroup && this.cardFormGroup) {
      let creditCardModel: CreditCard;
      if (!this.selectedCreditCard) {
        creditCardModel = Object.assign({}, this.cardFormGroup.value)
      } else {
        creditCardModel = this.selectedCreditCard;
      }
      //let rentDateModel = Object.assign({}, this.rentDateFormGroup.value)
      //let customerModel = Object.assign({}, this.customerFormGroup.value)
      // date transforms
      let formRentDate = this.rentDateFormGroup.get('rentDate')?.value
      let formReturnDate = this.rentDateFormGroup.get('returnDate')?.value
      let rentDate = this.datePipe.transform(formRentDate, "yyyy-MM-dd hh:mm:ss")
      let returnDate = this.datePipe.transform(formReturnDate, "yyyy-MM-dd hh:mm:ss")

      //const formData = new FormData();

      // Car and Customer
      this.rentAddFormData.append("carId", this.data.id.toString())
      if (this.isUserAdmin) {
        this.rentAddFormData.append("customerId", this.customerFormGroup.get('customerId')?.value)
      } else {       
        this.rentAddFormData.append("customerId", this.activeCustomer.customerId.toString())
      }
      // rent dates
      this.rentAddFormData.append("rentDate", rentDate ? rentDate.toString() : "");
      this.rentAddFormData.append("returnDate", returnDate ? returnDate.toString() : "");

      // Card Datas
      if (!this.selectedCreditCard) {
        this.rentAddFormData.append("cardOwner", this.cardFormGroup.get('cardOwner')?.value)
        this.rentAddFormData.append("cardNumber", this.cardFormGroup.get('cardNumber')?.value)
        this.rentAddFormData.append("expirationDate", this.cardFormGroup.get('expirationDate')?.value)
        this.rentAddFormData.append("cvvCode", this.cardFormGroup.get('cvvCode')?.value)
        this.rentAddFormData.append("willBeRecorded", this.cardFormGroup.get('willBeRecorded')?.value)
      }

      // formData.forEach((value, key) => {
      //   console.log("FormDatas Key : " + key + " value: " + value)
      // });
      // this.rentAddFormData.forEach((value, key) => {
      //   console.log("FormDatas Key : " + key + " value: " + value)
      // });

      this.payWithConfirm(creditCardModel, this.rentAddFormData);

    }
  }

  payWithConfirm(creditCardModel: CreditCard, formData: FormData) {
    this.confirmService.createConfirm("Ödeme işlemi").subscribe(res => {
      if (res == true) {
        this.pay(creditCardModel, formData);
      }
    })
  }

  pay(creditCardModel: CreditCard, formData: FormData) {
    this.paymentService.pay(creditCardModel).subscribe(payResponse => {
      this.snackBarService.openSnackBarWithMessage(payResponse);
      if (payResponse.isSuccess) {
        this.AddRental(formData)
      }
    }, responseError => {
      this.snackBarService.openErrorSnackBar(responseError.error.message)
    })

  }

  AddRental(formData: FormData) {
    this.rentalService.AddRent(formData).subscribe(response => {
      this.snackBarService.openSnackBarWithMessage(response);
      response.isSuccess ? this.dialogRef.close() : null;
    }, responseError => {
      if (responseError.error.ValidationErrors.length > 0) {
        for (let i = 0; i < responseError.error.ValidationErrors.length; i++) {
          this.snackBarService.openErrorSnackBar(responseError.error.ValidationErrors[i].ErrorMessage, "kapat")
        }
      }
    })
  }


  getCustomers() {
    if (this.isUserAdmin) {
      this.customers.push(this.activeCustomer)
    }
    else {
      this.customerService.getCustomers().subscribe(response => {
        this.customers = response.data as Customer[];
      })
    }
  }

  getCustomerCreditScore() {
    this.creditScoreService.getUserCreditScore().subscribe(response => {
      this.CustomerCreditScore = response.data.creditScore;
      this.checkCreditScore();
    })
  }

  checkCreditScore() {
    this.isCreditScoreEnough = true;
    if (this.CustomerCreditScore <= (this.data.minCreditScore as number)) {
      this.isCreditScoreEnough = false;
      this.snackBarService.openErrorSnackBar("Kredi notunuz yetersiz olduğu için bu aracı kiralayamazsınız.")
    }
  }

  getCustomerCreditCards(customerId: number) {
    if(!this.isUserAdmin){

      this.creditCardService.getCustomerCreditCards(this.activeCustomer.customerId).subscribe(response => {
        this.customerCreditCards = response.data;
      });
      
    }else{
    this.creditCardService.getCustomerCreditCards(customerId).subscribe(response => {
      this.customerCreditCards = response.data;
    });
  }
  }


  // Select Saved Credit Card for Payment
  selectSavedCreditCard(creditCardId: number) {
    this.selectedCreditCard = this.customerCreditCards.find(x => x.id == creditCardId) as CreditCard;
    this.rentAddFormData.append("cardOwner", this.selectedCreditCard.cardOwner)
    this.rentAddFormData.append("cardNumber", this.selectedCreditCard.cardNumber)
    this.rentAddFormData.append("expirationDate", this.selectedCreditCard.expirationDate)
    this.rentAddFormData.append("cvvCode", this.selectedCreditCard.cvvCode)
    this.addRent();
  }

  // Delete Customer Credit Card
  deleteCustomerCreditCardWithConfirm(creditCardId: number) {
    this.confirmService.createConfirm("Kredi kartı silme").subscribe(res => {
      if (res == true) {
        this.deleteCustomerCreditCard(creditCardId);
      }
    })
  }

  deleteCustomerCreditCard(creditCardId: number) {
    this.creditCardService.deleteCustomerCreditCard(creditCardId).subscribe(response => {
      this.snackBarService.openSnackBarWithMessage(response);
    })
  }

  getUserAuthority() {
    if (this.jwtTokenService.checkUserAuthority(["admin", "system.admin"]) == true) {
      this.isUserAdmin = true
    }
    else {
      this.isUserAdmin = false
      this.getCustomerCreditScore();
      this.customerService.getCustomer(this.jwtTokenService.getJwtUserEmail()).subscribe(response => {
        this.activeCustomer = response.data;
      })
    }
  }


}
