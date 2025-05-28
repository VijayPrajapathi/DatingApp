import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

export const adminGuard: CanActivateFn = (route, state) => {

  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);
  if (accountService.roles().includes('Admin') || accountService.roles().includes('Moderator')) {
    // If the user has the 'admin' or 'moderator' role, allow access
    return true;
  }
  else {
    toastr.error('You cant  enter here/ pass!'); // Show error message using Toastr
    return false;
  }
};
