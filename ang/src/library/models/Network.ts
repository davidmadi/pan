import { HttpClient, HttpHandler, HttpHeaderResponse } from "@angular/common/http";
import Model from "src/library/models/Model";
import {User} from "./User"
import { environment } from '../../environments/environment';

export interface Network {
  id:number;
  primary:User;
  friend:User;
  relationship:string;
  delete:boolean;
  //UI Properties
  editing:boolean;
}


export class NetworkContext {

  public async ListNetwork(userId:number) : Promise<Network[]> {
    var result:any = await fetch(`${environment.apiUrl}/v1/network/list?userId=${userId}`).then((r)=>{
      return r.json();
    });
    return new Promise<Network[]>((resolve, reject) => {
      if (result.result) {
        resolve(result.result as Network[]);
      }
      else {
        reject();
      }
    })
  }

  public UpdateNetwork(network:Network) : Promise<Network> {
    var data = JSON.stringify(network);
    return new Promise<Network>((resolve, reject)=>{
      fetch(`${environment.apiUrl}/v1/network/persist?delete=${network.delete}`, {
        method: "POST", // *GET, POST, PUT, DELETE, etc.
        headers: {
          "Content-Type": "application/json",
        },
        body: data, // body data type must match "Content-Type" header
      }).then((res) =>{
        res.json().then(v=>{
          resolve((v as Model<Network>).result);
        });
      }).catch(err => {
        reject(err);
      });
    });
  }
}