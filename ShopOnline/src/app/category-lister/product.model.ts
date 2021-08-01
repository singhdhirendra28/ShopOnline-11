export class Product {
    // public title!: string;
    // public brand!: string;
    // public price!: number;
    // public description!: string;
    // public image!: string;
    // public productId!: number;
    // //set the default value as 1
    // public quantity:number=1;

    public Price!: number;
    public ProductName!: string;
    public ImageName!: string;
    public ProductId!: number;    
    public AvailableQty:number=0;
    public SalePrice!:number;
    public Promotion!:any;
  }
  export class Promotion{
    public PromoId!: number;
    public Name!: string;
    public Definition!: string;
    public Level!: string;    
    public Type!:string;
    public CalculateIn!:string;
    public PromoDiscount!:string;
  }
  