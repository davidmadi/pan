export interface User {
  Id:number;
  Email:string;
  Password:string;
  //UI Properties
  Opened:boolean;
}

export class UserContext {

  public async List(pageNumber:number, pageSize:number) : Promise<User[]> {
    var result:any = await fetch("http://localhost:5276/v1/api/tax/user/list").then((r)=>{
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


}