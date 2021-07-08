// import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  // users: any;

  // constructor(private http: HttpClient) { }
  constructor() { }

  ngOnInit(): void {
    // this.getUsers();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }
  
  // Triggered by an emmiter on child component
  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

}
