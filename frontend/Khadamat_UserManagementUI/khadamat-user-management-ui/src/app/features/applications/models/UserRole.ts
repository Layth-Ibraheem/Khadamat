export enum UserRole {
  Admin = -1,
  ListUsers = 1 << 0,
  AddUser = 1 << 1,
  UpdateUser = 1 << 2,
  DeleteUser = 1 << 3,
  ManageUsers = ListUsers | AddUser | UpdateUser | DeleteUser
}
