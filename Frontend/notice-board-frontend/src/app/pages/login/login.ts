import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Auth } from '../../core/auth';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
 user = '';
  pass = '';
  error = '';

  constructor(private router: Router, private auth: Auth) {}

  submit(): void {
    this.error = '';

  const u = this.user.trim();
  const p = this.pass;

  if (!u || !p) {
    this.error = 'נא למלא שם משתמש וסיסמה';
    return;
  }

  const ok = this.auth.login(u, p);
  if (!ok) {
    this.error = 'שם משתמש או סיסמה שגויים (admin/admin)';
    return;
  }

  this.router.navigateByUrl('/board');
  }
}
