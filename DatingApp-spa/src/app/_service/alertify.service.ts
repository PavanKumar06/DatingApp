import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs';

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

constructor() { }

 // tslint:disable-next-line: typedef
 confirm(message: string, okCallback: () => any) { // We will describe the okCallBack fn in our component
   alertify.confirm(message, (e: any) => { // e is an event like clicking ok
     if (e) {
       okCallback();
     } else {}
   });
 }

 // tslint:disable-next-line: typedef
 success(message: string) {
   alertify.success(message);
 }

 // tslint:disable-next-line: typedef
 error(message: string) {
  alertify.error(message);
}

 // tslint:disable-next-line: typedef
 warning(message: string) {
  alertify.warning(message);
}

 // tslint:disable-next-line: typedef
 message(message: string) {
  alertify.message(message);
}

}
