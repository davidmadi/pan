import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserListComponent } from 'src/app/user-list/user-list.component'
import { UserFriendsListComponent } from 'src/app/user-friends-list/user-friends-list.component'
import { HomeComponent } from 'src/app/home/home.component'

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'users/list', component: UserListComponent },
  { path: 'user/friends/:id', component: UserFriendsListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
