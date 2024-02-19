import { Component, OnInit } from '@angular/core';
import {PageEvent} from '@angular/material/paginator';
import { UserContext, User } from 'src/library/models/User';
import {
  MatDialog,
} from '@angular/material/dialog';
import { UploadFileComponent } from '../components/upload-file/upload-file.component';
import { HttpHandler } from '@angular/common/http';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

  protected users:User[] = new Array<User>();
  protected showUpload:boolean = false;
  protected pageLength:number = 1;
  protected pageSize:number = 10;
  protected pageIndex:number = 0;

  constructor(public dialog: MatDialog, public handler:HttpHandler) {}

  ngOnInit(): void {
    this.reload();
  }

  protected pageChange(e: PageEvent) {
    this.pageLength = e.length;
    this.pageSize = e.pageSize;
    this.pageIndex = e.pageIndex;
    this.reload();
  }

  private reload(){
    new UserContext().List(this.pageIndex, this.pageSize).then((result)=>{
      this.users = result;
      this.pageLength = 100;
    });
  }

  saveUser(user:User){
    var ctx = new UserContext();
    ctx.Update(user).then((result) =>{
      this.ngOnInit();
    });
  }

  openUploadFile(user:User):void{
    var uploadDataFile:any = {
      file: null,
      title: "Profile picture",
      message: "Please select an image to be uploaded"
    };
    var handler = this.handler;
    const dialogRef = this.dialog.open(UploadFileComponent, {
      data: uploadDataFile
    });    
    dialogRef.afterClosed().subscribe(()=>{
      var file = uploadDataFile.file.nativeElement.files[0];
      if (file) {
        var u = new UserContext();
        u.UploadFile(file, handler).then((res:any)=>{
          if (res.result && res.result.url) {
            user.profilePicture = res.result.url;
            user.editing = true;
          }
        })
      }
    })
  }

}
