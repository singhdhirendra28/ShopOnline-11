import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/category-lister/product.model';
import { CartItem, MyCartView } from '../cart.model';
import { CartService } from '../cart.service';

@Component({
  selector: 'shop-cart-preview',
  templateUrl: './cart-preview.component.html',
  styleUrls: ['./cart-preview.component.scss']
})
export class CartPreviewComponent implements OnInit {

  myCartView!: MyCartView;

  constructor(private cartService: CartService) { }

  ngOnInit() {
//Get the payment summary on Init.
this.cartService.getCart().subscribe();
//This is for getting the cart payment summary.Refresh cart data at quantity update.
this.cartService.cartObservable
  .subscribe((res: MyCartView) => {
    if (res) {
      this.myCartView = res;
    }
  })
  }
  deleteItem(product: CartItem) {
    
  }
}
