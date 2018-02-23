import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPofileComponent } from './user-pofile.component';

describe('UserPofileComponent', () => {
  let component: UserPofileComponent;
  let fixture: ComponentFixture<UserPofileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserPofileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserPofileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
