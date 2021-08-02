import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyCartComponent } from './my-cart.component';
import { RouterTestingModule } from '@angular/router/testing';
import { CartService } from '../cart.service';
import { BreakpointsService } from 'src/app/base/breakpoints/breakpoints.service';
import { WindowServiceViewPort } from 'src/app/base/window.service';
import { AddToCart, CartItem } from '../cart.model';
import { ChangeDetectorRef } from '@angular/core';
import { of } from 'rxjs';

describe('MyCartComponent', () => {
  let component: MyCartComponent;
  let fixture: ComponentFixture<MyCartComponent>;
  
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule],
      declarations: [MyCartComponent],
      providers: [
        { provide: CartService, useClass: class { data$ = of([]); } },
        { provide: BreakpointsService, useClass: class { } },            
        { provide: ChangeDetectorRef, useClass: class { } },  
        { provide: WindowServiceViewPort, useClass: class { } },  
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyCartComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {    
    expect(component).toBeTruthy();    
  });

  beforeEach(() => {     
    spyOn(component, 'updateCartQuantity');
    spyOn(component, 'increaseQuantity');
    spyOn(component, 'decreaseQuantity');
  });

  
  it('should updateCartQuantity have been called', () => {
    const addtoCart = new AddToCart(1,3);
    component.updateCartQuantity(addtoCart);
    expect(component.updateCartQuantity).toHaveBeenCalled();
  });

  it('should increaseQuantity have been called', () => {
    const addtoCart = new CartItem(1,1,1,1,1,'Red','sas',null);
    component.increaseQuantity(addtoCart);
    expect(component.increaseQuantity).toHaveBeenCalled();
  });

  it('should decreaseQuantity have been called', () => {
    const addtoCart = new CartItem(1,1,1,1,1,'Red','sas',null);
    component.decreaseQuantity(addtoCart);
    expect(component.decreaseQuantity).toHaveBeenCalled();
  });

});
