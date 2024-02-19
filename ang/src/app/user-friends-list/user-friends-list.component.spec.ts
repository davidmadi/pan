import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserFriendsListComponent } from './user-friends-list.component';

describe('UserFriendsListComponent', () => {
  let component: UserFriendsListComponent;
  let fixture: ComponentFixture<UserFriendsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserFriendsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserFriendsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
