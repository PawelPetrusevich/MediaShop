import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExecutePaymentComponent } from './execute-payment.component';

describe('ExecutePaymentComponent', () => {
  let component: ExecutePaymentComponent;
  let fixture: ComponentFixture<ExecutePaymentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExecutePaymentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExecutePaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
