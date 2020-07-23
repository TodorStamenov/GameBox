export interface IUser {
  id: string;
  username: string;
  token: string;
  expirationDate: Date;
  isAdmin: boolean;
}
