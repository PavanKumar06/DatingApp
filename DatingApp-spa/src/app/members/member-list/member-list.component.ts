import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/user';
import { UserService } from '../../_service/user.service';
import { AlertifyService } from '../../_service/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  users: User[];

  constructor(private userService: UserService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  // tslint:disable-next-line: typedef
  ngOnInit() {
    // this.loadUsers();
    this.route.data.subscribe(data => {
      // tslint:disable-next-line: no-string-literal
      this.users = data['users'];
    });
  }

  // // tslint:disable-next-line: typedef
  // loadUsers() {
  //   this.userService.getUsers().subscribe((users: User[]) => {// getUsers returns an observable so we must subscribe to it
  //     this.users = users; // users of type user array is what we are returning from our subscribe method
  //   }, error => {
  //     this.alertify.error(error);
  //   });
  // }
}
