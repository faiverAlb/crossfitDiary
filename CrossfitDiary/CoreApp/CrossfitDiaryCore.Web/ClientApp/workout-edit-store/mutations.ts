// profile/mutations.ts
import { MutationTree } from "vuex";
import { IWorkoutEditState, IUser } from "./types";

export const mutations: MutationTree<IWorkoutEditState> = {
  profileLoaded(state: IWorkoutEditState, payload: IUser) {
    state.error = false;
    state.user = payload;
  },
  profileError(state) {
    state.error = true;
    state.user = undefined;
  },
  changeUserName(state: IWorkoutEditState, newName: string) {
    debugger;
    state.user.firstName = newName;
  }
};
