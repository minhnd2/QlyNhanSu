import { Component, OnInit } from '@angular/core';
import "../../assets/resources/css/font-face.css";
import { AccountsService } from '../service/accounts.service';
import { Accounts } from '../service/model/accounts';
import { AccountSearch } from '../service/model/accountSearch';
import { AccountResult } from '../service/model/accountResult';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';

// npm install inputmask --save
import * as Inputmask from "inputmask"

declare var $: any;

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.css']
})
export class AccountsComponent implements OnInit {

  constructor(private accountService: AccountsService, private formBuilder: FormBuilder,) { }

  accountsList: Accounts[];
  accountResult: AccountResult;
  accountSearch = new AccountSearch();
  totalRow = 0;

  isAddNew = true;
  accountItem = new Accounts();

  form: FormGroup;

  ngOnInit() {
    this.search();
    this.form = this.formBuilder.group({
      mail: [null, [Validators.required, Validators.email, Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$')]],
      name: [null, Validators.required],
      password: [null, Validators.required],
      phone: [null, [Validators.required, Validators.pattern('^[0-9]{4} [0-9]{3} [0-9]{3}$')]],
    });
    Inputmask().mask(document.getElementById("hf-phone"));
  }

  Logout() {
    localStorage.removeItem("ID");
  }

  search() {
    this.accountService.search(this.accountSearch).subscribe(res => {
      this.accountResult = res;
      this.accountsList = res.ListAccount;
      this.totalRow = res.TotalRow;
    });
  }

  pageChange(page: number) {
    this.accountSearch.PageIndex = page;
    this.search();
  }

  add() {
    this.form.reset();
    this.accountItem.IsStatus = true;
    this.accountItem.CreateDate = new Date();
    this.isAddNew = true;
  }

  edit(item) {
    this.accountItem.ID = item.ID;
    this.accountItem.Name = item.Name;
    this.accountItem.Mail = item.Mail;
    this.accountItem.Phone = item.Phone;
    //this.templateItem.CreateDate = item.CreateDate;
    this.accountItem.CreateDate = item.CreateDate;
    this.accountItem.IsStatus = item.IsStatus;

    //this.form.get("password").setErrors({'incorrect': true});
    this.form.get("password").setErrors(null);
    this.isAddNew = false;
  }

  save() {
    if (this.form.valid) {
      $('#modalCreate').modal('hide');
      $('#modalConfirmSave').modal('show');
    } else {
      this.validateAllFormFields(this.form);
    }

  }

  confirmSave() {
    if (this.isAddNew) {
      this.accountService.insert(this.accountItem).subscribe(res => {
        this.search();
        $('#modalConfirmSave').modal('hide');
      }, error => {
        console.log(error.error.Message);
      });
    }
    else {
      this.accountService.update(this.accountItem).subscribe(res => {
        this.search();
        $('#modalConfirmSave').modal('hide');
      }, error => {
        console.log(error.error.Message);
      });
    }
  }

  deleteAccount() {
    $("#modalViewDelete").modal('hide');
    $('#modalConfirmDelete').modal('show');
  }

  confirmDel() {
    this.accountService.delete(this.accountItem).subscribe(res => {
      this.search();
      $('#modalConfirmDelete').modal('hide');
    }, error => {
      console.log(error.error.Message);
    });
  }

  isFieldValid(field: string) {
    return !this.form.get(field).valid && this.form.get(field).touched;
  }

  displayFieldCss(field: string) {
    return {
      'has-error': this.isFieldValid(field),
      'has-feedback': this.isFieldValid(field)
    };
  }

  validateAllFormFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllFormFields(control);
      }
    });
  }

}
