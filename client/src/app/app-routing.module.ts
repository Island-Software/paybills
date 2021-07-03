import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BillTypeListComponent } from './bill-type-list/bill-type-list.component';
import { BillTypeComponent } from './bill-type/bill-type.component';
import { BillsComponent } from './bills/bills.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  // {path: 'home', component: HomeComponent},
  {path: '', redirectTo: '/home', pathMatch: 'full'},  
  // This path is created to avoid the need to add "canActivate" individually to routes
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'bill-type/:id', component: BillTypeComponent},
      {path: 'bill-type-list', component: BillTypeListComponent},
      {path: 'bills', component: BillsComponent}
    ]
  },
  {path: 'home', component: HomeComponent},
  {path: 'errors', component: TestErrorsComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: 'not-found', component: NotFoundComponent},
  // ** = invalid route
  {path: '**', component: NotFoundComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
