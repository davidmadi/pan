import { Component } from '@angular/core';
import { UserContext, User, Network } from 'src/library/models/User';
import {ActivatedRoute} from '@angular/router';
import {Observable, map} from 'rxjs';
import { NetworkContext } from 'src/library/models/Network';

@Component({
  selector: 'app-user-friends-list',
  templateUrl: './user-friends-list.component.html',
  styleUrls: ['./user-friends-list.component.scss']
})
export class UserFriendsListComponent {

  protected networks:Network[] = new Array<Network>();
  protected primary:User = {} as User;

  constructor(protected route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.reload();
  }

  private reload(){
    this.route.params.pipe(map((p) => {
      return p['id']
    })).subscribe((idVal:string)=>{
      new UserContext().Find(Number(idVal)).then((result)=>{
        this.primary = result;
      });
      new NetworkContext().ListNetwork(Number(idVal)).then((result)=>{
        this.networks = result;
      });
    });
  }


  save(network:Network){
    var ctx = new NetworkContext();
    network.delete = false;
    ctx.UpdateNetwork(network).then((result) =>{
      this.reload();
    });
  }


  delete(network:Network){
    var ctx = new NetworkContext();
    network.delete = true;
    ctx.UpdateNetwork(network).then((result) =>{
      this.reload();
    });
  }

}
