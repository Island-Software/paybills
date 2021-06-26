import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BillTypeComponent } from './bill-type/bill-type.component';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  // This path is created to avoid the need to add "canActivate" individually to routes
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'bill-type', component: BillTypeComponent}
    ]
  },
  // ** = invalid route
  {path: '**', component: HomeComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
