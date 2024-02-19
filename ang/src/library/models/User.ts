import { HttpClient, HttpHandler, HttpHeaderResponse } from "@angular/common/http";
import Model from "src/library/models/Model";
import { environment } from '../../environments/environment';

export interface User {
  id:number;
  email:string;
  fullName:string;
  password:string;
  profilePicture:string;
  //UI Properties
  opened:boolean;
  editing:boolean;
}

export interface Network {
  id:number;
  primary:User;
  friend:User;
  relationship:string;
  delete:boolean;
  //UI Properties
  editing:boolean;
}


export class UserContext {

  public async List(pageNumber:number, pageSize:number) : Promise<User[]> {
    var result:any = await fetch(`${environment.apiUrl}/v1/user/list?pageNumber=${pageNumber}&pageSize=${pageSize}`).then((r)=>{
      return r.json();
    });
    return new Promise<User[]>((resolve, reject) => {
      if (result.result) {
        resolve(result.result as User[]);
      }
      else {
        reject();
      }
    })
  }

  public async Find(userId:number) : Promise<User> {
    var result:any = await fetch(`${environment.apiUrl}/v1/user/get?id=${userId}`).then((r)=>{
      return r.json();
    });
    return new Promise<User>((resolve, reject) => {
      if (result.result) {
        resolve(result.result as User);
      }
      else {
        reject();
      }
    })
  }

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

  public Update(user:User) : Promise<User> {
    var data = JSON.stringify(user);
    return new Promise<User>((resolve, reject)=>{
      fetch(`${environment.apiUrl}/v1/user/update`, {
        method: "POST", // *GET, POST, PUT, DELETE, etc.
        headers: {
          "Content-Type": "application/json",
        },
        body: data, // body data type must match "Content-Type" header
      }).then((res) =>{
        res.json().then(v=>{
          resolve((v as Model<User>).result);
        });
      }).catch(err => {
        reject(err);
      });
    });
  }

  public UploadFile(file:any, handler:HttpHandler) : Promise<HttpHeaderResponse> {
    var url:string = `${environment.apiUrl}/v1/upload/image`;

    var httpClient = new HttpClient(handler)
    const formData = new FormData();
    formData.append("file", file);

    return new Promise<HttpHeaderResponse>((resolve, reject) => {
      httpClient.post(url, formData, {
        reportProgress: true,
        observe: 'events'
      })
      .pipe(
      ).pipe().forEach((next:any)=>{
        if (next.body) {
          resolve(next.body);
        }
      })
    });
  }
}