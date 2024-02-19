import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-profile-picture',
  templateUrl: './profile-picture.component.html',
  styleUrls: ['./profile-picture.component.scss']
})
export class ProfilePictureComponent {

  @Input()
  public url:string = "";
  @Input()
  public title:string = "";
  @Input()
  public size:string = "";

}
