import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';

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

export type CreateNoticeRequest = {
  title: string;
  content: string;
  location: {
    latitude: number;
    longitude: number;
    address?: string | null;
  };
};

export type UpdateNoticeRequest = {
  title: string;
  content: string;
  location: {
    latitude: number;
    longitude: number;
    address?: string | null;
  };
};

@Injectable({ providedIn: 'root' })
export class NoticeApi {
  constructor(private http: HttpClient) {}

  getAll(): Observable<NoticeDto[]> {
    return this.http.get<any>('/api/notices').pipe(
      map(res => Array.isArray(res) ? res : (res?.notices ?? []))
    );
  }


  create(req: CreateNoticeRequest): Observable<NoticeDto> {
    return this.http.post<NoticeDto>('/api/notices', req);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`/api/notices/${id}`);
   }
  
   update(id: string, req: UpdateNoticeRequest): Observable<void> {
  return this.http.put<void>(`/api/notices/${id}`, req);
}
   
}
