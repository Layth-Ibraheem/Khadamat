export class LoginRequest {
  private username: string;
  private password: string;

  constructor(userName: string, password: string) {
    this.username = userName;
    this.password = password;
  }

  get UserName(): string {
    return this.username;
  }

  set UserName(username: string) {
    this.username = username;
  }

  get Password(): string {
    return this.password;
  }

  set Password(password: string) {
    this.password = password;
  }
}
