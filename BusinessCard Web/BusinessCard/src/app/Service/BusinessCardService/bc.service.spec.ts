import { TestBed } from '@angular/core/testing';

import { BCService } from './bc.service';

describe('BCService', () => {
  let service: BCService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BCService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
