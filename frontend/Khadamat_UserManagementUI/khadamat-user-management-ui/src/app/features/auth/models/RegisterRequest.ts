export class RegisterRequest {
  private username: string;
  private password: string;
  private email: string;
  private roles: number;
  private isActive: boolean;

  constructor(username: string, password: string, email: string, roles: number, isActive: boolean) {
    this.username = username;
    this.password = password;
    this.email = email;
    this.roles = roles;
    this.isActive = isActive;
  }
  get Username() {
    return this.username;
  }
  set Username(username: string) {
    this.username = username;
  }
  get Password() {
    return this.password;
  }
  set Password(password: string) {
    this.password = password;
  }
  get Email() {
    return this.email;
  }
  set Email(email: string) {
    this.email = email;
  }
  get Roles() {
    return this.roles;
  }
  set Roles(roles: number) {
    this.roles = roles;
  }
  get IsActive() {
    return this.isActive;
  }
  set IsActive(isActive: boolean) {
    this.isActive = isActive;
  }
}
