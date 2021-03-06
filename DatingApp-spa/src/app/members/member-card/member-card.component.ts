import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  @Input() user: User; // We are going to pass the user from our parent component

  constructor() { }

  // tslint:disable-next-line: typedef
  ngOnInit() {
  }

}
