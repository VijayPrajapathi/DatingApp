import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { environment } from '../../../environments/environment';
@Component({
  selector: 'app-test-errors',
  imports: [CommonModule],
  templateUrl: './test-errors.component.html',
  styleUrl: './test-errors.component.css'
})
export class TestErrorsComponent {
baseUrl = environment.apiUrl;
private  http = inject(HttpClient);
validationErrors: string[] = [];

get400Error() {
  this.http.get(this.baseUrl + 'buggy/bad-request').subscribe({
    next : Response => console.log(Response),
    error: error => console.error(error)
  })
}

get401Error() {
  this.http.get(this.baseUrl + 'buggy/auth').subscribe({
    next : Response => console.log(Response),
    error: error => console.error(error)
  })
}

get404Error() {
  this.http.get(this.baseUrl + 'buggy/not-found').subscribe({
    next : Response => console.log(Response),
    error: error => console.error(error)
  })
}

get500Error() {
  this.http.get(this.baseUrl + 'buggy/server-error').subscribe({
    next : Response => console.log(Response),
    error: error => console.error(error)
  })
}


get400ValidationError() {
  this.http.post(this.baseUrl + 'account/register', {}).subscribe({
    next : Response => console.log(Response),
    error: error => {console.error(error);
    this.validationErrors = error;
    }
  })
}

}
