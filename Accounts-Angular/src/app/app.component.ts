import { Component, HostListener } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  // @HostListener('window:beforeunload', ['$event'])
  // doSomething($event) {
  //   //logout account
  //   localStorage.removeItem("ID");
  // }
  title = 'Accounts-Angular';
}