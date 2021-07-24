import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './core/nav/nav.component'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { HomeComponent } from './core/home/home.component';
import { RegisterComponent } from './core/register/register.component';
import { BillTypeDetailComponent } from './bill-type/bill-type-detail/bill-type-detail.component';
import { ToastrModule } from 'ngx-toastr';
import { SharedModule } from './modules/shared.module';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { BillListComponent } from './bill/bill-list/bill-list.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import '@angular/common/locales/global/pt';
import { BillTypeListComponent } from './bill-type/bill-type-list/bill-type-list.component';
import { TextInputComponent } from './forms/text-input/text-input.component';
import { BillRegisterComponent } from './bill/bill-register/bill-register.component';
import { BillTypeRegisterComponent } from './bill-type/bill-type-register/bill-type-register.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BillEditComponent } from './bill/bill-edit/bill-edit.component';
import { NgxCurrencyModule } from 'ngx-currency';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    BillListComponent,
    BillTypeListComponent,
    BillTypeDetailComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    TextInputComponent,
    BillRegisterComponent,
    BillTypeRegisterComponent,
    BillEditComponent
  ],
  imports: [
    BrowserModule,
    NgxCurrencyModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule,
    ModalModule.forRoot(),
    ReactiveFormsModule,
    NgbModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: LOCALE_ID, useValue: 'pt'}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
