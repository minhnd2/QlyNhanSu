import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { AccountsService } from '../service/accounts.service';
import { AuthService } from '../service/Auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  styles: [`
  .has-error{
    border: 1px solid red;
    border-radius: 5px;
  }
  `]
})
export class LoginComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,
    private accountService: AccountsService,
    private authService: AuthService,
    private router: Router) { }

  msgAlert = "";
  disable = false;

  loginItem = {
    Mail: '',
    PassWord: '',
  };

  form: FormGroup;

  ngOnInit() {
    if (this.authService.isLogged()) {
      this.router.navigate([""]);
    }
    this.form = this.formBuilder.group({
      mail: [null, [Validators.required, Validators.email]],
      password: [null, Validators.required],
    });
  }

  login() {
    if (this.form.valid) {
      this.accountService.login(this.loginItem).subscribe(res => {
        this.disable = false;
        this.msgAlert = "";

        localStorage.setItem("ID", this.loginItem.Mail);
        this.router.navigate([""]);
      }, error => {
        this.disable = true;
        this.msgAlert = error.error.Message;
      });
    } else {
      this.validateAllFormFields(this.form);
    }
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
