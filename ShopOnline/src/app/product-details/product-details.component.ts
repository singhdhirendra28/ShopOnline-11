import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { AddToCart, MyCartView, UpdateQuantity } from '../cart/cart.model';
import { CartService } from '../cart/cart.service';
import { Product } from '../category-lister/product.model';

@Component({
  selector: 'shop-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  productList: Array<Product> = [];
  product: Product | undefined;
  updateQuantity: UpdateQuantity = new UpdateQuantity;
  isItemAddedToCart: boolean = false;
  private unsubscribe: Subject<void> = new Subject<void>();
  constructor(private cartService: CartService,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    //Get the id from the query param
    this.activatedRoute.params.pipe(takeUntil(this.unsubscribe)).subscribe(
      (params: any) => {
        if (params['productId']) {
          const productId = params['productId'];
          this.cartService.getProductDetail(productId)
            .subscribe((data: any) => {
              if (data && data.Response.statusCode == "200") {
                let prod = data.Response.response;
                this.product = new Product();

                this.product.ImageName = prod.imageName;
                this.product.Price = prod.price;
                this.product.ProductId = prod.productId;
                this.product.ProductName = prod.productName;
                this.product.Promotion = prod.promotion;
                /*Find the product in the response list*/
                //set to 1 by default quantity is not modified earlier
                if (this.product !== undefined) {
                  this.product.AvailableQty = 1;
                }
              }
            }
            )
        }
      }
    )
  }
  /*This function is used to increase the quantity*/
  increaseQuantity(product: Product) {
    product.AvailableQty += 1;
  }
  //This function is used to decrese the quantity
  decreaseQuantity(product: Product) {
    //Default quantity added to the cart is 1, so quantity should not go below 1.
    if (product.AvailableQty > 1)
      product.AvailableQty -= 1;
  }
  addToCart(product: Product) {
   
    let addToCart = new AddToCart(product.AvailableQty, product.ProductId);
    //subscribe the service to add the item
    this.cartService.addToCart(addToCart)
      .subscribe((data) => {
        if (data && data.Response.statusCode == "200") {
          this.isItemAddedToCart = true;
          this.cartService.getCart().subscribe();
        }
      })
  }

  public ngOnDestroy() {
    this.unsubscribe.next();
    this.unsubscribe.complete();
  }
}
