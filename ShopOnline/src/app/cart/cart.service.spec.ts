
import { HttpClient } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed, inject, async, getTestBed } from '@angular/core/testing';
import { CartService } from './cart.service';

describe('Cart service', () => {
  let service: CartService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [
    CartService,HttpClient
      ]
    });
    service = TestBed.get(CartService);
    httpMock = TestBed.get(HttpTestingController);
  });

//   it('should be created', () => {
//     expect(service).toBeTruthy();
//   });
});
