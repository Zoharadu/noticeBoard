import { Component, OnInit } from '@angular/core';
import { NoticeApi, NoticeDto } from '../../core/notice-api';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-board',
  imports: [CommonModule],
  templateUrl: './board.html',
  styleUrl: './board.scss',
})
export class Board implements OnInit {
 notices: NoticeDto[] = [];
  loading = true;
  error = '';

  constructor(private api: NoticeApi) {}

  ngOnInit(): void {
    this.api.getAll().subscribe({
      next: (data) => {
        this.notices = data;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.error = 'שגיאה בטעינת מודעות';
        this.loading = false;
      }
    });
  }
}
