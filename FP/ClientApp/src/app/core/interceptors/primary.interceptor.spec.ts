import { TestBed } from '@angular/core/testing';

import { PrimaryInterceptor } from './primary.interceptor';

describe('PrimaryInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      PrimaryInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: PrimaryInterceptor = TestBed.inject(PrimaryInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
