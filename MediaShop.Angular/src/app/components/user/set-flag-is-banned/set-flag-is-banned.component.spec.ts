import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SetFlagIsBannedComponent } from './set-flag-is-banned.component';

describe('SetFlagIsBannedComponent', () => {
  let component: SetFlagIsBannedComponent;
  let fixture: ComponentFixture<SetFlagIsBannedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SetFlagIsBannedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SetFlagIsBannedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
