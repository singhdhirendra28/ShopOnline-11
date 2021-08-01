import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { FormsModule } from '@angular/forms';
import { FooterComponent } from "./footer.component";
const COMPONENTS: any[] = [
    FooterComponent
  ]
  
@NgModule({
    imports: [     
      FormsModule,
      RouterModule,
    ],
    exports: COMPONENTS,
    declarations: COMPONENTS,       
  })
  export class FooterModule { }