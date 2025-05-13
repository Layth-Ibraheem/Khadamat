export interface ApplicationResponse {
  id: number;
  username: string;
  email: string;
  status: 'Draft' | 'Approved' | 'Rejected';
}
