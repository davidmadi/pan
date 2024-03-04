import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserMakePaymentComponent } from './user-make-payment.component';

describe('UserMakePaymentComponent', () => {
  let component: UserMakePaymentComponent;
  let fixture: ComponentFixture<UserMakePaymentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserMakePaymentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserMakePaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
