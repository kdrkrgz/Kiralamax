/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { UserClaimsService } from './user-claims.service';

describe('Service: UserClaims', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserClaimsService]
    });
  });

  it('should ...', inject([UserClaimsService], (service: UserClaimsService) => {
    expect(service).toBeTruthy();
  }));
});
