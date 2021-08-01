import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { of } from "rxjs/internal/observable/of";
import { AddToCart, CartItem, MyCartView } from "./cart.model";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
import { ApiResponse } from "../base/apiResponse";


@Injectable()
export class CartService {
    /*url configuration- Start*/
    private baseUrl: string = "http://localhost:49499/";
    private endpoint: string = "api/Trolley/GetCart";
    private endpointProductList: string = "api/Trolley/GetProducts";
    private endpointProductDetail: string = "api/Trolley/GetProductDetail";
    private endpointAddItem: string = "api/Trolley/AddToCart";
    private endpointDeleteItem: string = "api/Trolley/DeleteItem";
    /*url configuration- Start*/
    public cartObservable = new BehaviorSubject<MyCartView>(new MyCartView(0, 0, 0, [],0));
    private cartSummary!: MyCartView;    
    jsonData: any;
    constructor(private httpClient: HttpClient) {        
    }

    public addToCart(addtoCart: AddToCart): Observable<ApiResponse | undefined> {
        const url = this.baseUrl+this.endpointAddItem;
        return this.httpClient.post(url, addtoCart).pipe(
            map(response => {
                return new ApiResponse(response);
            }));
    }
    public deleteCartItem(addtoCart: AddToCart): Observable<ApiResponse | undefined> {
        const url = `${this.baseUrl}/${this.endpointDeleteItem}/${addtoCart.ProductId}`;
        return this.httpClient.delete(url).pipe(
            map(response => {
                return new ApiResponse(response);
            }));
    }
    public getCart(): Observable<MyCartView | undefined> {
        //call the api to get the cart count in the backend 
        //As of now, just hard coding it because 6 items are there in the JSON file  
        const url = this.baseUrl + this.endpoint;
        return this.httpClient.get(url).pipe(
            map((data: any) => {
                let cartList = new Array<CartItem>();
                if (data && data.response) {
                    data.response.items.forEach((itm: any) => {
                        let cartItem = new CartItem(itm.id, itm.productId, itm.selectedQty, itm.price, itm.salePrice, itm.productName, itm.imageName,itm.promotion);
                        cartList.push(cartItem);
                    });
                    //Check if payment summary populated
                    if (data.response.paymentSummary) {
                        let summary = data.response.paymentSummary;
                        this.cartSummary = new MyCartView(data.response.totalItems, summary.subTotal, summary.payable, cartList,summary.totalDiscount);
                    }
                    //Check if payment summary not populated
                    else {
                        this.cartSummary = new MyCartView(cartList.length, 0, 0, cartList,0);
                    }
                    this.cartObservable.next(this.cartSummary);
                    return this.cartSummary;
                }
                else {
                    return this.cartSummary;
                }
            }))
    }

    public getProducts(): Observable<ApiResponse | undefined> {
        //call the api to get the cart count in the backend         
        const url = this.baseUrl + this.endpointProductList;
        return this.httpClient.get(url).pipe(
            map((response) => {
                return new ApiResponse(response);
            })
        )
    }

    public getProductDetail(prodId: number): Observable<ApiResponse | undefined> {
        //call the api to get the cart count in the backend         
        const url = `${this.baseUrl}/${this.endpointProductDetail}/${prodId}`;
        return this.httpClient.get(url).pipe(
            map((response) => {
                return new ApiResponse(response);
            })
        )
    }    
}