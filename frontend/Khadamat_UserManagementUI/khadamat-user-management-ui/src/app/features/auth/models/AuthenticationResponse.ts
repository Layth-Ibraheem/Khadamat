export interface AuthenticationResponse {
  id: number;
  userName: string;  // Note: matches API response exactly
  email: string;
  roles: number;
  isActive: boolean;
  token: string;
}
