import { HttpClientTestingModule } from '@angular/common/http/testing';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { CartService } from '../cart/cart.service';
import { Product } from '../category-lister/product.model';

import { ProductDetailsComponent } from './product-details.component';

describe('ProductDetailsComponent', () => {
  const fakeActivatedRoute = {
    snapshot: { data:{} }
  } as ActivatedRoute;

  let component: ProductDetailsComponent;
  let fixture: ComponentFixture<ProductDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports:[HttpClientTestingModule],
      declarations: [ ProductDetailsComponent ],
      providers:[CartService,{provide: ActivatedRoute, useValue: fakeActivatedRoute}]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

});
