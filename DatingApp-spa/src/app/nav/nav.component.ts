import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_service/auth.service';
import { AlertifyService } from '../_service/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  photoUrl: string;

  constructor(public authServices: AuthService, private alertify: AlertifyService, private router: Router) { }

  // tslint:disable-next-line: typedef
  ngOnInit() {
    this.authServices.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
  }

  // tslint:disable-next-line: typedef
  login() {
    // console.log(this.model);
    this.authServices.login(this.model).subscribe(next => { // Always subscribe to an "observable"
      this.alertify.success('logged in successfully');
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.router.navigate(['/members']);
    });
  }

  // tslint:disable-next-line: typedef
  loggedIn() {
    // const token = localStorage.getItem('token');
    // return !!token; // The "!!" will return a true or false value
    return this.authServices.loggedIn();
  }

  // tslint:disable-next-line: typedef
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authServices.decodedToken = null;
    this.authServices.currentUser = null;
    this.alertify.message('logged out');
    this.router.navigate(['/home']);
  }

}
