import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { HeaderComponent } from "./header.component";
import { FormsModule } from '@angular/forms';
import { CommonModule } from "@angular/common";
import { CartModule } from "../cart/cart.module";
const COMPONENTS: any[] = [
    HeaderComponent
  ]
  
@NgModule({
    imports: [     
      FormsModule,
      RouterModule,     
      CommonModule,
      CartModule   
    ],
    exports: COMPONENTS,
    declarations: COMPONENTS,       
  })
  export class HeaderModule { }