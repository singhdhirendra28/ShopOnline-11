import { TestBed, async } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { BreakpointsService } from './base/breakpoints/breakpoints.service';
import { WindowService } from './base/window.service';
import { CartPreviewComponent } from './cart/cart-preview/cart-preview.component';
import { CartService } from './cart/cart.service';
import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';
import { ShopComponent } from './shop.component';

describe('AppComponent', () => {
  
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        ShopComponent,HeaderComponent,FooterComponent,CartPreviewComponent
      ],
      providers:[CartService,BreakpointsService,WindowService]
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(ShopComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'shopping-mission'`, () => {
    const fixture = TestBed.createComponent(ShopComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title).toEqual('shopping-mission');
  });  
});
