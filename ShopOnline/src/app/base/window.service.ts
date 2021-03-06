
import {fromEvent as observableFromEvent,  BehaviorSubject
} from 'rxjs';

import {debounceTime, map} from 'rxjs/operators';
import { PLATFORM_ID, Injector } from '@angular/core';
// imports
import {
  Inject,
  Injectable
} from '@angular/core';
import { isPlatformServer } from '@angular/common';

// global function to get window
function getWindow(): any {
  return window;
}

@Injectable()
export class WindowServiceViewPort {
  private _windowSize: any;

  public window: any;
  public width: number;
  public height: number;
  public windowObservable: any;
  public scrollObservable: any;
  public scrollLock: boolean;
  private isServer: boolean;
  standardDeviceSize:any = {
    desktop: {
      width: 1200,
      height: 600
    },
    tablet: {
      width: 768,
      height: 600
    },
    phone: {
      width: 540,
      height: 730
    }
  }
  constructor(@Inject(PLATFORM_ID) public platformId: Object, private injector: Injector) {
    this.isServer = isPlatformServer(platformId);

    // assign window
    this.scrollLock = false;
    this._windowSize = this.getTheWindowSize();
    this.height = this._windowSize.height;
    this.width = this._windowSize.width;
    this.windowObservable = new BehaviorSubject(this.getTheWindowSize());

    if (!this.isServer) {
      observableFromEvent(window, 'resize', { passive: true }).pipe(
        map(this.getTheWindowSize),
        debounceTime(320))
        .subscribe(this.windowObservable);
      this.window = getWindow();
      this.scrollObservable = observableFromEvent(document, 'scroll', { passive: true });
    };
  }

  public lockScroll() {
    this.scrollLock = true;
  }

  private getTheWindowSize() {
    if (this.isServer) {
      const req  = this.injector.get('REQUEST');
      const window = this.standardDeviceSize[req.device.type];
      window.innerWidth = window.width;
      this.window = window;
      return this.window;
    };
    return {
      width: window.innerWidth,
      height: window.innerHeight
    }
  }

}
