import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_service/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private authServices: AuthService) { }

  // tslint:disable-next-line: typedef
  ngOnInit() {
  }

  // tslint:disable-next-line: typedef
  login() {
    // console.log(this.model);
    this.authServices.login(this.model).subscribe(next => { // Always subscribe to an "observable"
      console.log('logged in successfully');
    }, error => {
      console.log('Failed to log in');
    });
  }

  // tslint:disable-next-line: typedef
  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token; // The "!!" will return a true or false value
  }

  // tslint:disable-next-line: typedef
  logout() {
    localStorage.removeItem('token');
    console.log('logged out');
  }

}
