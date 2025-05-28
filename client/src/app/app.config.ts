import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
// Removed duplicate import of provideAnimations

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr, ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { errorsInterceptor } from './_interceptor/errors.interceptor';
import { jwtInterceptor } from './_interceptor/jwt.interceptor';
import { loadingInterceptor } from './_interceptor/loading.interceptor';
import { TimeagoModule } from "ngx-timeago";
import { ModalModule } from 'ngx-bootstrap/modal';

export const appConfig: ApplicationConfig = {
  providers:
   [provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes),
    provideHttpClient(withInterceptors([errorsInterceptor, jwtInterceptor,loadingInterceptor])),
    provideAnimations(),
    provideToastr({
      positionClass: 'toast-bottom-right',
    }),
    importProvidersFrom(NgxSpinnerModule,TimeagoModule.forRoot(), ModalModule.forRoot())
  ]
};
