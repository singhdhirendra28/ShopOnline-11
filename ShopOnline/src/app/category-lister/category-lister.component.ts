import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { AddToCart } from '../cart/cart.model';
import { CartService } from '../cart/cart.service';
import { Product } from './product.model';

@Component({
  selector: 'shop-category-lister',
  templateUrl: './category-lister.component.html',
  styleUrls: ['./category-lister.component.scss']
})
export class CategoryListerComponent implements OnInit {

  currentUrl?: string;
  productList?: Array<Product>;
  jsonData: any;
  isItemAddedToCart: boolean = false;  
  constructor(private cartService: CartService
  ) { }

  ngOnInit() {
    // subscrive the getProduct server 
    this.cartService.getProducts()
      .subscribe((data: any) => {
        if (data && data.Response.statusCode == "200") {
          let prodList = data.Response.response;
          this.productList = this.mapProduct(prodList);          
        }
      }
      )
  }

  ///map the product to the prod list for the view
  mapProduct(itmList: any): Array<Product> {
    let list = new Array<Product>();

    itmList.forEach((itm: any) => {
      let prod = new Product();
      prod.AvailableQty = itm.availableQty;
      prod.ImageName = itm.imageName;
      prod.Price = itm.price;
      prod.ProductId = itm.productId;
      prod.ProductName = itm.productName;
      prod.SalePrice=itm.SalePrice;
      prod.Promotion=itm.promotion;
      list.push(prod);
    });
    return list;
  }
  

}
