import Model from "src/library/models/Model";
export interface User {
  id:number;
  email:string;
  password:string;
  //UI Properties
  opened:boolean;
  editing:boolean;
}

export class UserContext {

  public async List(pageNumber:number, pageSize:number) : Promise<User[]> {
    var result:any = await fetch("http://localhost:5276/v1/user/list?pageNumber=1&pageSize=10").then((r)=>{
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

  public Update(user:User) : Promise<User> {

    var data = JSON.stringify(user);
    return new Promise<User>((resolve, reject)=>{
      fetch('http://localhost:5276/v1/user/update', {
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


}