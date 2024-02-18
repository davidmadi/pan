import { Component, Inject, ViewChild, Input } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogRef,
} from '@angular/material/dialog';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.scss']
})

export class UploadFileComponent{
  @ViewChild('inputFile')
  protected inputFile: any;
  protected showUpload:boolean = false;
  protected title:string;
  protected message: string;

  constructor(
    public dialogRef: MatDialogRef<UploadFileComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.title = this.data.title;
    this.message = this.data.message;
    this.dialogRef.afterClosed().subscribe(()=>{
      this.data.file = this.inputFile;
    });
  }

  protected cancel(){
    this.dialogRef.close();
  }

}
