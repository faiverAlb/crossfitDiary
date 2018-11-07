// types.ts
export interface IUser {
  firstName: string;
  lastName: string;
  email: string;
  phone?: string;
}

export interface IWorkoutEditState {
  user?: IUser;
  error: boolean;
}
