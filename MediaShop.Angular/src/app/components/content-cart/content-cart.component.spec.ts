import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContentCartComponent } from './content-cart.component';

describe('ContentCartComponent', () => {
  let component: ContentCartComponent;
  let fixture: ComponentFixture<ContentCartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContentCartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContentCartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
