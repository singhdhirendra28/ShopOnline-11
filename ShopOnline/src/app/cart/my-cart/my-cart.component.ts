import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { BreakpointsService } from 'src/app/base/breakpoints/breakpoints.service';
import { AddToCart, CartItem, MyCartView, UpdateQuantity } from '../cart.model';
import { CartService } from '../cart.service';

@Component({
  selector: 'shop-my-cart',
  templateUrl: './my-cart.component.html',
  styleUrls: ['./my-cart.component.scss']
})
export class MyCartComponent implements OnInit {
  jsonData: any;
  updateQuantity!: UpdateQuantity;
  myCartView!: MyCartView;
  constructor(private cartService: CartService, public breakpointsService: BreakpointsService,
    private cd: ChangeDetectorRef) { }

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
  /*This method will delete the item from the cart */
  deleteItem(product: CartItem) {
    if (product != null) {
      this.cartService.deleteCartItem(product)
        .subscribe((data) => {
          if (data && data.Response.statusCode == "200") {
            this.cartService.getCart().subscribe();
          }
        })
    }
  }
  //increse the quantity from the cart
  increaseQuantity(product: CartItem) {
    product.SelectedQty += 1;
    let addToCart = new AddToCart(product.SelectedQty, product.ProductId);
    //subscribe the service to add the item
    this.updateCartQuantity(addToCart);
  }
  //increse the quantity from the cart
  decreaseQuantity(product: CartItem) {
    if (product.SelectedQty > 1) {
      product.SelectedQty -= 1;
      let addToCart = new AddToCart(product.SelectedQty, product.ProductId);
      //subscribe the service to add the item
      this.updateCartQuantity(addToCart);
    }
  }

  updateCartQuantity(addToCart: AddToCart) {
    //call the update service
    this.cartService.addToCart(addToCart)
      .subscribe((data) => {
        if (data && data.Response.statusCode == "200") {
          this.cartService.getCart().subscribe();
        }
      })
  }
}
