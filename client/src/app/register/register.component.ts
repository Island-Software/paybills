import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // Property to receive data from parent component
  // @Input() usersFromHomeComponent: any;
  // Property to send data to parent component
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onUserText() {
    this.model.password = this.model.username;
  }

  onKey(event: any) { // without type info
    this.model.password = this.model.username;
  }

  register() {
    this.accountService.register(this.model).subscribe(response => {      
      this.cancel();
    }, error => {
      console.log(error);
      this.toastr.error(error.error);
    });
  }

  cancel() {
    // Works like an event
    this.cancelRegister.emit(false);
  }
}
