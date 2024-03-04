import { Component } from '@angular/core';
import { UserContext, User } from 'src/library/models/User';
import {ActivatedRoute} from '@angular/router';
import { NetworkContext } from 'src/library/models/Network';
import { map } from 'rxjs';

@Component({
  selector: 'app-user-payments',
  templateUrl: './user-payments.component.html',
  styleUrls: ['./user-payments.component.scss']
})
export class UserPaymentsComponent {
  protected user:User = {} as User;

  constructor(protected route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.reload();
  }

  private reload(){
    // this.route.params.pipe(map((p) => {
    //   return p['id']
    // })).subscribe((idVal:string)=>{
    //   new UserContext().Find(Number(idVal)).then((result)=>{
    //     this.user = result;
    //   });
    // });
  }
}
