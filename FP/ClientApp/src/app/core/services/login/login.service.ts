import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ILoginResponse, IUser } from 'src/app/shared/interfaces';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(
    private _http: HttpClient,
    private _router: Router,
    private _toastr: ToastrService
  ) { }

  private readonly _url: string = environment.url;

  public login(user: IUser): Observable<ILoginResponse> {
    return this._http.post<ILoginResponse>(`${this._url}/api/account/login`, { username: user.login, password: user.password }).pipe(
      catchError((error) => {
        if (error instanceof HttpErrorResponse) {
          if (error.status == 401) {
            this._toastr.warning('Invalid data!', '', {
              timeOut: 3000
            });
            this._router.navigate(['/']);
          }
        }
        return of(error);
      })
    )
  }

  public logout(): void {
    sessionStorage.clear();
    this._router.navigate(['']);
  }
}
