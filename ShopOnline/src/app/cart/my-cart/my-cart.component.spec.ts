import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { Product } from 'src/app/category-lister/product.model';

import { MyCartComponent } from './my-cart.component';
import { RouterTestingModule } from '@angular/router/testing';
import { CartService } from '../cart.service';
import { BreakpointsService } from 'src/app/base/breakpoints/breakpoints.service';
import { WindowService } from 'src/app/base/window.service';

describe('MyCartComponent', () => {
  let component: MyCartComponent;
  let fixture: ComponentFixture<MyCartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule],
      declarations: [MyCartComponent],
      providers: [CartService,BreakpointsService,WindowService]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyCartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  
});
