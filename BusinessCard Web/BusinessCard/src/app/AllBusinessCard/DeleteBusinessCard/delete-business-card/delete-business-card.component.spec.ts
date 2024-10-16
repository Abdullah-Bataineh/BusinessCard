import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteBusinessCardComponent } from './delete-business-card.component';

describe('DeleteBusinessCardComponent', () => {
  let component: DeleteBusinessCardComponent;
  let fixture: ComponentFixture<DeleteBusinessCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeleteBusinessCardComponent]
    });
    fixture = TestBed.createComponent(DeleteBusinessCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
