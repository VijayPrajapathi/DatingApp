import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService); // Assuming ToastrService is imported and injected correctly

  if (accountService.currentUser()){
    return true;
  }
  else{
    toastr.error('You shall not pass!'); // Show error message using Toastr
    return false;
  }
};
