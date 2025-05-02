import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TitleCasePipe } from '@angular/common';


@Component({
  selector: 'app-nav',
  imports: [FormsModule, BsDropdownModule, RouterLink, RouterLinkActive,TitleCasePipe],
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  accountService = inject(AccountService);
  router = inject(Router);
  private toastr = inject(ToastrService); // Assuming ToastrService is imported and injected correctly
  model: any = {};

  login() {
    this.accountService.login(this.model).subscribe({
      next: _ => {
        this.router.navigateByUrl('/members'); // Navigate to members page after login
      },
      error: error => {
        this.toastr.error(error.error); // Show error message using Toastr
      }
    });
  }

  logout() {
    //1this.accountService.logout(); // Assuming logout is a method in AccountService
    this.accountService.logout(); // Assuming logout is a method in AccountService
    this.router.navigateByUrl('/')
  }
}


