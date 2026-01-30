import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { environment } from '../../../environments/environment';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const usuario = this.authenticationService.valorUsuarioActual;
    const esApiUrl = request.url.startsWith(environment.apiUrl);
    if (usuario && usuario.token && esApiUrl) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${usuario.token}`
        }
      });
    }

    return next.handle(request);
  }
}
