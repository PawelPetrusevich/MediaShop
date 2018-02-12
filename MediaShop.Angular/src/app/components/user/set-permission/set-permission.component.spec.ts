import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SetPermissionComponent } from './set-permission.component';

describe('SetPermissionComponent', () => {
  let component: SetPermissionComponent;
  let fixture: ComponentFixture<SetPermissionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SetPermissionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SetPermissionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
