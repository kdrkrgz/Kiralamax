/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ConfirmService } from './confirm.service';

describe('Service: Confirm', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ConfirmService]
    });
  });

  it('should ...', inject([ConfirmService], (service: ConfirmService) => {
    expect(service).toBeTruthy();
  }));
});
