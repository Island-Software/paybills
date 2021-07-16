import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BillTypeListComponent } from './bill-type/bill-type-list/bill-type-list.component';
import { BillTypeDetailComponent } from './bill-type/bill-type-detail/bill-type-detail.component';
import { BillListComponent } from './bill/bill-list/bill-list.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './core/home/home.component';

const routes: Routes = [
  // {path: 'home', component: HomeComponent},
  {path: '', redirectTo: '/home', pathMatch: 'full'},  
  // This path is created to avoid the need to add "canActivate" individually to routes
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'bill-type/:id', component: BillTypeDetailComponent},
      {path: 'bill-type-list', component: BillTypeListComponent},
      {path: 'bills', component: BillListComponent}
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
