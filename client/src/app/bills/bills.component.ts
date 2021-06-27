import { Component, OnInit } from '@angular/core';
import { Bill } from '../models/bill';
import { User } from '../models/user';
import { UsersService } from '../services/users.service';

@Component({
  selector: 'app-bills',
  templateUrl: './bills.component.html',
  styleUrls: ['./bills.component.css']
})
export class BillsComponent implements OnInit {
  user!: User;
  bills: Bill[] = [];
  username: string | null | undefined;

  constructor(private usersService: UsersService) { }

  ngOnInit(): void {   
    this.loadUser(); 
  }

  loadUser() {
    this.username = JSON.parse(localStorage.getItem('user')!).username;
    this.usersService.getUser(this.username!).subscribe(user => {
      this.user = user;     
      console.log('user: ' + user);
      console.log('bills: ' + user.bills);      
    });    
  }

}
