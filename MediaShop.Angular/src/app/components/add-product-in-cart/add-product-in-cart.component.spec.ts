import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddProductInCartComponent } from './add-product-in-cart.component';

describe('AddProductInCartComponent', () => {
  let component: AddProductInCartComponent;
  let fixture: ComponentFixture<AddProductInCartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddProductInCartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddProductInCartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
