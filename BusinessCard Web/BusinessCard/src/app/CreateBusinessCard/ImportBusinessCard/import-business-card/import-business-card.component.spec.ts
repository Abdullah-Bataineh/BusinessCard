import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportBusinessCardComponent } from './import-business-card.component';

describe('ImportBusinessCardComponent', () => {
  let component: ImportBusinessCardComponent;
  let fixture: ComponentFixture<ImportBusinessCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ImportBusinessCardComponent]
    });
    fixture = TestBed.createComponent(ImportBusinessCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
