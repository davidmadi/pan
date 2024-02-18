import { Component, OnInit } from '@angular/core';
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

  constructor(public dialog: MatDialog, public handler:HttpHandler) {}

  ngOnInit(): void {
    new UserContext().List(0, 10).then((result)=>{
      this.users = result;
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
      // alert(uploadDataFile);
      var file = uploadDataFile.file.nativeElement.files[0];
      if (file) {
        // var reader = new FileReader();
        // reader.readAsText(file);
        // reader.onload = function (evt:any) {
          var u = new UserContext();
          u.UploadFile(file, handler).then((res:any)=>{
            if (res.result && res.result.url) {
              user.profilePicture = res.result.url;
            }
          })
        // }
        // reader.onerror = function (evt:any) {
        //     alert("error reading file");
        // }
      }
    })
  }
}
