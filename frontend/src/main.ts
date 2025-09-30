import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { AppComponent } from './app/app.component';

function authInterceptor(req: any, next: any) {
  const token = localStorage.getItem('token');
  if (token) req = req.clone({ setHeaders: { Authorization: 'Bearer ' + token } });
  return next(req);
}

bootstrapApplication(AppComponent, {
  providers: [provideHttpClient(withInterceptors([authInterceptor]))]
}).catch(err => console.error(err));
