/* tslint:disable:no-unused-variable */
import {
    TestBed, getTestBed, async, inject
  } from '@angular/core/testing';
  
  import {
    BreakpointsService
  } from './breakpoints.service';
  
  import {
    WindowServiceViewPort
  } from '../window.service';
  
  import {
    BehaviorSubject
  } from 'rxjs';
  
  describe('Service: BreakpointsService', () => {
    let _mockWindowService: WindowServiceViewPort;
    let _breakPointService: BreakpointsService;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
          providers:[WindowServiceViewPort]
        })
        .compileComponents();
      }));
    
  
    beforeEach(() => {
      _mockWindowService = <WindowServiceViewPort>{};
    });
  
    afterEach(() => {
      _mockWindowService = <WindowServiceViewPort>{};
    });
  
    it('should determine the breakpoints correctly', () => {
      let format: string;
      const data: any = [
        { window: { width: 400 }, type: 'xs' },
        { window: { width: 800 }, type: 'sm' },
        { window: { width: 1000 }, type: 'md' },
        { window: { width: 1300 }, type: 'lg' }
      ];
  
      for (let i = 0; i < data.length; i++) {
        const item: any = data[i];
  
        // assemble
        _mockWindowService.width = item.window.width;
        _mockWindowService.windowObservable = new BehaviorSubject(function() { return item.window; }());
        _breakPointService = new BreakpointsService(_mockWindowService);
        _breakPointService.windowSize = {
          width: _mockWindowService.width,
          height: _mockWindowService.height
        };
        _breakPointService.breakpoints = {
          xs: {
            min: 0,
            max: 768,
          },
          sm: {
            min: 768,
            max: 992
          },
          md: {
            min: 992,
            max: 1200
          },
          lg: {
            min: 1200,
            max: Number.MAX_VALUE
          }
        }
        // act
        format = _breakPointService.getFormat;
  
        // assert
        expect(item.type).toBe(format);
      }
    })
  });
  