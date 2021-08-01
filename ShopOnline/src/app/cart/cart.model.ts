import { Promotion } from "../category-lister/product.model";

export class MyCartView {
    public TotalItems: number;
    public Subtotal:number=0;
    public Payable:number=0;
    public TrolleyItems: Array<CartItem>;     
    public TotalDiscount:number=0;
    constructor(totalItems:number,subTotal:number,payable:number,trolleyItems:Array<CartItem>,totalDiscount:number)    
    {
        this.TotalItems=totalItems;
        this.Subtotal=subTotal;
        this.Payable=payable;
        this.TrolleyItems=trolleyItems;
        this.TotalDiscount=totalDiscount;
    }

}
export class UpdateQuantity{
    public Quantity:number=0;
    public IsIncreasing:boolean=false;
    
}

export class AddToCart{
    public SelectedQty:number=0;        
    public ProductId!:number;
    constructor(qty:number,prodId:number){
        this.SelectedQty=qty;
        this.ProductId=prodId;
    }
}

export class CartItem{
    public Id!: number;    
    public ProductId!: number;
    public SelectedQty!: number;
    public Price!:number;
    public SalePrice!:number;
    public ProductName!:string;
    public ImageName!:string;
    public Promotion!:any;
    constructor(id:number,productId:number,selectedQty:number,price:number,salePrice:number,prodName:string,imageName:string,promotion:any){
        this.Id=id;
        this.ProductId=productId;
        this.SelectedQty=selectedQty;
        this.Price=price;
        this.SalePrice=salePrice;
        this.ProductName=prodName;
        this.ImageName=imageName;
        this.Promotion=promotion;
    }    
}