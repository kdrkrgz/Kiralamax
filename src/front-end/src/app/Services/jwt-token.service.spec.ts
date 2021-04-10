/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { JwtTokenService } from './jwt-token.service';

describe('Service: JwtToken', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [JwtTokenService]
    });
  });

  it('should ...', inject([JwtTokenService], (service: JwtTokenService) => {
    expect(service).toBeTruthy();
  }));
});
