import {
    Injectable
  } from '@angular/core';
  
  import {
    WindowServiceViewPort
  } from '../window.service';
  
  @Injectable()
  export class BreakpointsService {
    public windowService: any;
    public isXS: boolean = false;
    public isSM: boolean = false;
    public isMD: boolean = false;
    public isLG: boolean = false;
    public windowSize: any;
    public breakpoints: any;
    isBrowser: boolean = false;
  
    public get getFormat(): string {
      return this.setFormat();
    }
  
    public get isMobile(): boolean {
      const format: string = this.getFormat;
      return format === 'xs' || format === 'sm';
    }
  
    constructor(public windowRef: WindowServiceViewPort) {
      this.windowService = windowRef;
      this.windowSize = {
        width: windowRef.width,
        height: windowRef.height
      };
      this.breakpoints = {
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
  
      // window observable to check widow resize
      windowRef.windowObservable.subscribe(
        (value:number) => {
          this.windowSize = value;
          this.setFormat();
        }
      )
    }
  
    private isFormat(format:any): boolean {
      return (format.min <= this.windowSize.width && this.windowSize.width < format.max);
    }
  
    private checkBreakPoints(): void {
      this.isXS = this.isFormat(this.breakpoints.xs);
      this.isSM = this.isFormat(this.breakpoints.sm);
      this.isMD = this.isFormat(this.breakpoints.md);
      this.isLG = this.isFormat(this.breakpoints.lg);
    }
  
    private setFormat(): string {
      this.checkBreakPoints();
      return this.isXS ? 'xs' : this.isSM ? 'sm' : this.isMD ? 'md' : 'lg';
    }
  }
  
  