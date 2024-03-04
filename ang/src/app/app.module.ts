import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UserListComponent } from './user-list/user-list.component';
import { MatListModule } from '@angular/material/list';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatIconModule} from '@angular/material/icon';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatBadgeModule} from '@angular/material/badge';
import { UploadFileComponent } from './components/upload-file/upload-file.component';
import {MatDialogModule} from '@angular/material/dialog';
import {HttpClientModule} from '@angular/common/http'
import {MatPaginatorModule} from '@angular/material/paginator';
import { UserFriendsListComponent } from './user-friends-list/user-friends-list.component';
import {MatSelectModule} from '@angular/material/select';
import { ProfilePictureComponent } from './components/profile-picture/profile-picture.component';
import {MatChipsModule} from '@angular/material/chips';
import { HomeComponent } from './home/home.component';
import { UserPaymentsComponent } from './user-payments/user-payments.component';
import { UserMakePaymentComponent } from './user-make-payment/user-make-payment.component';

@NgModule({
  declarations: [
    AppComponent,
    UserListComponent,
    UploadFileComponent,
    UserFriendsListComponent,
    ProfilePictureComponent,
    HomeComponent,
    UserPaymentsComponent,
    UserMakePaymentComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatListModule,
    MatExpansionModule,
    MatIconModule,
    MatFormFieldModule,
    MatButtonModule,
    MatInputModule,
    MatBadgeModule,
    MatDialogModule,
    HttpClientModule,
    MatPaginatorModule,
    MatSelectModule,
    MatChipsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
