import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ApplicationResponse} from '../models/ApplicationResponse';
import {CreateNewRegisterApplicationRequest} from '../models/CreateNewRegisterApplicationRequest';

@Injectable({
  providedIn: 'root'
})
export class ApplicationsService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7296/RegisterApplications';

  getAllApplications() {
    return this.http.get<ApplicationResponse[]>(`${this.apiUrl}/getAll`);
  }

  approveApplication(id: number, roles: number) {
    return this.http.put(`${this.apiUrl}/${id}/approve`, {roles});
  }

  rejectApplication(id: number, rejectionReason: string | null) {
    return this.http.put(`${this.apiUrl}/${id}/reject`, {rejectionReason});
  }

  createNewRegisterApplication(request: CreateNewRegisterApplicationRequest) {
    return this.http.post(`${this.apiUrl}/createNew`, request);
  }
}
