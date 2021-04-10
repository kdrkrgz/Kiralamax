/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CreditCardService } from './credit-card.service';

describe('Service: CreditCard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CreditCardService]
    });
  });

  it('should ...', inject([CreditCardService], (service: CreditCardService) => {
    expect(service).toBeTruthy();
  }));
});
