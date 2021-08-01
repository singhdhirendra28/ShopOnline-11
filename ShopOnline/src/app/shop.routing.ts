import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CategoryListerComponent } from './category-lister/category-lister.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { AboutComponent } from './about/about.component';
import { JournalComponent } from './journal/journal.component';
import { DynamicHomeComponent } from './dynamic-content/dynamic-home.component';


export const ROUTES: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/sm/home'
  },
  {
    path: 'sm/home',
    component: DynamicHomeComponent
  },
  {
    path: 'sm/shop/plates',
    component: CategoryListerComponent
  },  
  {
    path: 'sm/cart',
    loadChildren: './cart/cart.module#CartModule'
  },
  {
    path: 'product/:productId',
    component: ProductDetailsComponent
  },
  {
    path: 'sm/about',
    component: AboutComponent
  },
  {
    path: 'sm/journal',
    component: JournalComponent
  },
]
