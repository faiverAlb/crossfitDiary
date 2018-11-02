// profile/mutations.ts
import { MutationTree } from "vuex";
import { ProfileState, User } from "./types";

export const mutations: MutationTree<ProfileState> = {
  profileLoaded(state: ProfileState, payload: User) {
    state.error = false;
    state.user = payload;
  },
  profileError(state) {
    state.error = true;
    state.user = undefined;
  },
  changeUserName(state: ProfileState, newName: string) {
    debugger;
    state.user.firstName = newName;
  }
};
