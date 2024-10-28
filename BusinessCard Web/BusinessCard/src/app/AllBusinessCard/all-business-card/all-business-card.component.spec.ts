import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllBusinessCardComponent } from './all-business-card.component';

describe('AllBusinessCardComponent', () => {
  let component: AllBusinessCardComponent;
  let fixture: ComponentFixture<AllBusinessCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AllBusinessCardComponent]
    });
    fixture = TestBed.createComponent(AllBusinessCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
