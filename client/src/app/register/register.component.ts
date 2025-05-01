import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, Output, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  imports: [FormsModule,CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
 model: any = {};
private accountService = inject(AccountService)
@Output() CancelRegister = new EventEmitter();

 register(){
  //console.log(this.model);
  this.accountService.register(this.model).subscribe({
    next : response =>{
      console.log(response);
      this.cancel();
    },
    error: error =>{
      console.log(error);
    }
  })

 }

 cancel(){
  this.CancelRegister.emit(false);
  //console.log('cancelled');
  // Add your cancellation logic here
  // For example, you can reset the model or navigate to another page
  // this.model = {};
  // this.router.navigate(['/home']);
  // Or simply close the registration form
  // this.registerMode = false;
 }
}
