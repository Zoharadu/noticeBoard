import { Component, OnInit } from '@angular/core';
import { NoticeApi, NoticeDto } from '../../core/notice-api';
import { CommonModule } from '@angular/common';
import { Auth } from '../../core/auth';
import { RouterLink } from '@angular/router';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-board',
  imports: [CommonModule, RouterLink],
  templateUrl: './board.html',
  styleUrl: './board.scss',
})
export class Board implements OnInit {
 notices: NoticeDto[] = [];
  loading = true;
  error = '';

 constructor(
  private api: NoticeApi,
  private auth: Auth,
  private cdr: ChangeDetectorRef
) {}

ngOnInit(): void {
  this.api.getAll().subscribe({
    next: (data) => {
      this.notices = data ?? [];
      this.loading = false;
      this.cdr.detectChanges();
    },
    error: (err) => {
      console.error(err);
      this.error = 'שגיאה בטעינת מודעות';
      this.loading = false;
      this.cdr.detectChanges();
    }
  });
}

   logout(): void {
    this.auth.logout();
    location.href = '/'; 
  }
}
