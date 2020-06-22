import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbPaginationModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule, HttpHeaders } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { IndexComponent } from './index/index.component';
import { AccountsComponent } from './accounts/accounts.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FieldErrorDisplayComponent } from './field-error-display/field-error-display.component';

@NgModule({
   declarations: [
      AppComponent,
      LoginComponent,
      IndexComponent,
      AccountsComponent,
      FieldErrorDisplayComponent
   ],
   imports: [
	 BrowserModule,
	 AppRoutingModule,
	 NgbModule,
	 NgbPaginationModule,
	 NgbAlertModule,
	 HttpClientModule,
	 FormsModule,
	 ReactiveFormsModule
	],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
