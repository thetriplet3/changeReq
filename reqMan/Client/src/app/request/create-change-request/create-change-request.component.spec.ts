import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateChangeRequestComponent } from './create-change-request.component';

describe('CreateChangeRequestComponent', () => {
  let component: CreateChangeRequestComponent;
  let fixture: ComponentFixture<CreateChangeRequestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateChangeRequestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateChangeRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
