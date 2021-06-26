import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './models/user';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'PayBills client app';
  users: any;

  constructor(private accountService: AccountService) {}

  ngOnInit() {   
    this.setCurrentUser();
  }

  setCurrentUser() {
    // Added the "!" to avoid an error. Assuming that will never return null
    const user: User = JSON.parse(localStorage.getItem('user')!);
    this.accountService.setCurrentUser(user);
  }  
}
