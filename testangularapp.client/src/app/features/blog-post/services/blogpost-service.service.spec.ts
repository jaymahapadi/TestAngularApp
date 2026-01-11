import { TestBed } from '@angular/core/testing';

import { BlogpostServiceService } from './blogpost-service.service';

describe('BlogpostServiceService', () => {
  let service: BlogpostServiceService;

  beforeEach(() => { 
    TestBed.configureTestingModule({});
    service = TestBed.inject(BlogpostServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
