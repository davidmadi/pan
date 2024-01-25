export interface User {
  id:number;
  email:string;
  password:string;
  //UI Properties
  opened:boolean;
}

export class UserContext {

  public async List(pageNumber:number, pageSize:number) : Promise<User[]> {
    var result:any = await fetch("http://localhost:5276/v1/api/tax/user/list?pageNumber=1&pageSize=10").then((r)=>{
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