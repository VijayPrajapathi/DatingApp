import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, Output, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  imports: [FormsModule,CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
 model: any = {};
private accountService = inject(AccountService)
private toastr = inject(ToastrService); // Assuming ToastrService is imported and injected correctly
@Output() CancelRegister = new EventEmitter();

 register(){
  //console.log(this.model);
  this.accountService.register(this.model).subscribe({
    next : response =>{
      console.log(response);
      this.cancel();
    },
    error: error =>{
      this.toastr.error(error.error); // Show error message using Toastr
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
