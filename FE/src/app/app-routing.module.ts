import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { 
    path: 'make-order', 
    loadComponent: () => import('./features/make-order/make-order.component').then(c => c.MakeOrderComponent) 
  },
  { 
    path: 'order-list', 
    loadComponent: () => import('./features/detail-list/detail-list.component').then(c => c.DetailListComponent) 
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
