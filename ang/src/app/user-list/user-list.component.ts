import { Component, OnInit } from '@angular/core';
import { UserContext, User } from 'src/library/models/User';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

  protected users:User[] = new Array<User>();

  ngOnInit(): void {
    new UserContext().List(0, 10).then((result)=>{
      this.users = result;
    })
  }

  saveUser(user:User){
    var ctx = new UserContext();
    ctx.Update(user).then((result) =>{
      this.ngOnInit();
    });

  }


}
