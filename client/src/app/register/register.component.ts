import { CommonModule, JsonPipe, NgIf } from '@angular/common';
import { Component, EventEmitter, inject, Input, OnInit, Output, output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { DatePickerComponent } from '../_forms/date-picker/date-picker.component';
import { TextInputComponent } from '../_forms/text-input/text-input.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, CommonModule,NgIf,DatePickerComponent,TextInputComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {

  private accountService = inject(AccountService);
  private router = inject(Router);
  private fb = inject(FormBuilder);
  @Output() CancelRegister = new EventEmitter();
  registerForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  maxDate = new Date();

  ngOnInit(): void {
    this.createForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18)
  }


  createForm() {
    this.registerForm = this.fb.group({
      gender: ['male'],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), 
          Validators.maxLength(8)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]],
    });
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    })
  }

  matchValues(matchTo: string): ValidatorFn{
  return (control : AbstractControl )=>{
    return control.value === control.parent?.get(matchTo)?.value? null:{isMatching: true}
  }
  }

  register() {
    const dob = this.getDateOnly(this.registerForm.get('dateOfBirth')?.value);
    this.registerForm.patchValue({dateOfBirth: dob});
    this.accountService.register(this.registerForm.value).subscribe({
      next : _ => this.router.navigateByUrl('/members'),
      error: error =>{
        this.validationErrors = error; // Show error message using Toastr
      }
    })

  }

  cancel() {
    this.CancelRegister.emit(false);
  }

  private getDateOnly(dob: string | undefined) {
    if (!dob) return;
    return new Date(dob).toISOString().slice(0, 10);
  } 
}
