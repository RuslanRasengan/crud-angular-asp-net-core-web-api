import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IRegisterResponse, IUser } from 'src/app/shared/interfaces';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  constructor(
    private _http: HttpClient,
    private _router: Router,
    private _toastr: ToastrService) { }


  private readonly _url: string = environment.url;

  public register(user: IUser): Observable<IRegisterResponse> {
    return this._http.post<IRegisterResponse>(`${this._url}/api/account/register`, {
      username: user.login,
      email: user.email,
      password: user.password
    }).pipe(
      catchError((error) => {
        if (error instanceof HttpErrorResponse) {
          this._toastr.warning('Error!', '', {
            timeOut: 3000
          });
        }
        return of(error);
      })
    )
  }

}
