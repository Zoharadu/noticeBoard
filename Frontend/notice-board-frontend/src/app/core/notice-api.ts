import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export type LocationDto = {
  latitude: number;
  longitude: number;
  address?: string | null;
};

export type NoticeDto = {
  id: string;
  title: string;
  content: string;
  createdAt: string;
  location: LocationDto;
};

@Injectable({ providedIn: 'root' })
export class NoticeApi {
  constructor(private http: HttpClient) {}

  getAll(): Observable<NoticeDto[]> {
    return this.http.get<NoticeDto[]>('/api/notices');
  }
}
