import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DynamicHomeComponent } from './dynamic-home.component';

describe('FooterComponent', () => {
  let component: DynamicHomeComponent;
  let fixture: ComponentFixture<DynamicHomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DynamicHomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DynamicHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
