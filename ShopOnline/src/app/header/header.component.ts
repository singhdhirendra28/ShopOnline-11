import { Component, OnInit } from '@angular/core';
import { BreakpointsService } from '../base/breakpoints/breakpoints.service';
import { MyCartView } from '../cart/cart.model';
import { CartService } from '../cart/cart.service';

@Component({
  selector: 'shop-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  isPanelOpen:boolean=false;
  cartItemCount: number =0;
  constructor(private cartService: CartService, public breakpointsService: BreakpointsService) { }

  ngOnInit() {
    //Get the cart data on Init.
    //this.cartService.getCart().subscribe();
    //Refresh cart data at quantity update
    this.cartService.cartObservable
      .subscribe((res: MyCartView) => {
        if (res) {
          this.cartItemCount = res.TotalItems;
        }
      })
  }

  openCartPreview(){
  this.isPanelOpen=true;
  }

}
