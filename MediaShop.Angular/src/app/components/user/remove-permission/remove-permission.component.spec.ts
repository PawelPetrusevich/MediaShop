import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RemovePermissionComponent } from './remove-permission.component';

describe('RemovePermissionComponent', () => {
  let component: RemovePermissionComponent;
  let fixture: ComponentFixture<RemovePermissionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RemovePermissionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RemovePermissionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
