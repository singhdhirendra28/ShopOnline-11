import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { MyCartComponent } from "./my-cart/my-cart.component";
import { CartPreviewComponent } from './cart-preview/cart-preview.component';




export const cartRoutes: Routes = [
    {
        path: 'sm/cart',
        component: MyCartComponent
      }
  ]

  @NgModule({
    imports: [            
      FormsModule,
      ReactiveFormsModule,
      CommonModule,
      RouterModule.forChild(cartRoutes),      
    ],
    declarations: [
        MyCartComponent,
        CartPreviewComponent        
    ],
    exports:[
      CartPreviewComponent
    ]
  })
  export class CartModule { }
  