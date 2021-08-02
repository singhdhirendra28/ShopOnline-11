import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { ROUTES} from './shop.routing';
import { ShopComponent as ShopComponent } from './shop.component';
import { HeaderModule } from './header/header.module';
import { FooterModule } from './footer/footer.module';
import { CategoryListerComponent } from './category-lister/category-lister.component';
import { RouterModule } from '@angular/router';
import { ProductService } from './product-service/product.service';
import { HttpClientModule } from '@angular/common/http';
import { CartModule } from './cart/cart.module';
import { CartService } from './cart/cart.service';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { BreakpointsService } from './base/breakpoints/breakpoints.service';
import { WindowServiceViewPort } from './base/window.service';
import { AboutComponent } from './about/about.component';
import { JournalComponent } from './journal/journal.component';

@NgModule({
  declarations: [
    ShopComponent,
    CategoryListerComponent,
    ProductDetailsComponent,
    AboutComponent,
    JournalComponent     
  ],
  imports: [
    BrowserModule,    
    HeaderModule,
    FooterModule,
    HttpClientModule,
    RouterModule.forRoot(ROUTES, {onSameUrlNavigation: 'reload' }),
    CartModule
  ],
  providers: [ProductService,CartService,BreakpointsService,WindowServiceViewPort],
  bootstrap: [ShopComponent]
})
export class ShopModule { }
