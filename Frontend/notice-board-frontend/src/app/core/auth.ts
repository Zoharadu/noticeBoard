import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class Auth {
  private readonly key = 'nb_logged_in';

  login(user: string, pass: string): boolean {
    const ok = user === 'admin' && pass === 'admin';
    if (ok) localStorage.setItem(this.key, 'true');
    return ok;
  }

  isLoggedIn(): boolean {
    return localStorage.getItem(this.key) === 'true';
  }

  logout(): void {
    localStorage.removeItem(this.key);
  }
}
