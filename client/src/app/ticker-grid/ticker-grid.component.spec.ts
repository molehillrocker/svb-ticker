import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TickerGridComponent } from './ticker-grid.component';

describe('TickerGridComponent', () => {
  let component: TickerGridComponent;
  let fixture: ComponentFixture<TickerGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TickerGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TickerGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
