import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBusinesscardComponent } from './create-businesscard.component';

describe('CreateBusinesscardComponent', () => {
  let component: CreateBusinesscardComponent;
  let fixture: ComponentFixture<CreateBusinesscardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateBusinesscardComponent]
    });
    fixture = TestBed.createComponent(CreateBusinesscardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
