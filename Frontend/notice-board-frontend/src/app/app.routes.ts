import { Routes } from '@angular/router';
import { Login } from './pages/login/login'
import { Board } from './pages/board/board';
import { authGuard } from './core/auth-guard';
import { Admin } from './pages/admin/admin';
export const routes: Routes = [
   { path: '', component: Login },
  { path: 'board', component: Board, canActivate: [authGuard] },
  { path: 'admin', component: Admin, canActivate: [authGuard] },
  { path: '**', redirectTo: '' }
];
