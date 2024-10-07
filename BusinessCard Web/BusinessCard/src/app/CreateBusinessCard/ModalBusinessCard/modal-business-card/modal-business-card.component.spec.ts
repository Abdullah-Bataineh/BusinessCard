import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalBusinessCardComponent } from './modal-business-card.component';

describe('ModalBusinessVardComponent', () => {
  let component: ModalBusinessCardComponent;
  let fixture: ComponentFixture<ModalBusinessCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModalBusinessCardComponent]
    });
    fixture = TestBed.createComponent(ModalBusinessCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
