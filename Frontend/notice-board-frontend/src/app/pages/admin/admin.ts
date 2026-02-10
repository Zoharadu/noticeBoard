import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { NoticeApi, NoticeDto, CreateNoticeRequest, UpdateNoticeRequest } from '../../core/notice-api';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './admin.html',
  styleUrl: './admin.scss',
})
export class Admin implements OnInit {
  notices: NoticeDto[] = [];
  loading = true;
  error = '';

  q = '';

  form: CreateNoticeRequest = {
    title: '',
    content: '',
    location: { latitude: 0, longitude: 0, address: '' }
  };

  saving = false;
  saveMsg = '';

  editingId: string | null = null;
  editForm: UpdateNoticeRequest = {
    title: '',
    content: '',
    location: { latitude: 0, longitude: 0, address: '' }
  };

  busyId: string | null = null;

  constructor(private api: NoticeApi) {}

  ngOnInit(): void {
    this.load();
  }

  get filtered(): NoticeDto[] {
    const t = this.q.trim().toLowerCase();
    if (!t) return this.notices;

    return this.notices.filter(n =>
      (n.title ?? '').toLowerCase().includes(t) ||
      (n.content ?? '').toLowerCase().includes(t) ||
      (n.location?.address ?? '').toLowerCase().includes(t)
    );
  }

  load(): void {
    this.loading = true;
    this.error = '';
    this.api.getAll().subscribe({
      next: (data) => {
        this.notices = data ?? [];
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.error = 'שגיאה בטעינת מודעות';
        this.loading = false;
      }
    });
  }

  create(): void {
    this.saveMsg = '';

    if (!this.form.title.trim() || !this.form.content.trim()) {
      this.saveMsg = 'חובה למלא כותרת ותוכן';
      return;
    }

    this.saving = true;
    this.api.create(this.form).subscribe({
      next: () => {
        this.saving = false;
        this.saveMsg = 'נוצר בהצלחה';
        this.form.title = '';
        this.form.content = '';
        this.load();
      },
      error: (err) => {
        console.error(err);
        this.saving = false;
        this.saveMsg = 'שגיאה ביצירה';
      }
    });
  }

  startEdit(n: NoticeDto): void {
    this.editingId = n.id;
    this.editForm = {
      title: n.title,
      content: n.content,
      location: {
        latitude: n.location?.latitude ?? 0,
        longitude: n.location?.longitude ?? 0,
        address: n.location?.address ?? ''
      }
    };
  }

  cancelEdit(): void {
    this.editingId = null;
  }

  saveEdit(): void {
    if (!this.editingId) return;

    const id = this.editingId;
    this.busyId = id;

    this.api.update(id, this.editForm).subscribe({
      next: () => {
        this.busyId = null;
        this.editingId = null;
        this.load();
      },
      error: (err) => {
        console.error(err);
        this.busyId = null;
        alert('שגיאה בעדכון');
      }
    });
  }

  remove(id: string): void {
    if (!confirm('למחוק את המודעה?')) return;

    this.busyId = id;
    this.api.delete(id).subscribe({
      next: () => {
        this.busyId = null;
        if (this.editingId === id) this.editingId = null;
        this.load();
      },
      error: (err) => {
        console.error(err);
        this.busyId = null;
        alert('שגיאה במחיקה');
      }
    });
  }
}