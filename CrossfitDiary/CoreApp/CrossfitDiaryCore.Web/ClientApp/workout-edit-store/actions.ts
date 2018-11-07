// profile/actions.ts

import { ActionTree } from "vuex";
import axios from "axios";
import { IWorkoutEditState, IUser } from "./types";
import { RootState } from "../types";
import CrossfitterService from "../CrossfitterService";

export const actions: ActionTree<IWorkoutEditState, RootState> = {
  // tslint:disable-next-line:typedef
  fetchData({ commit }): any {
    let crossfitterService: CrossfitterService = new CrossfitterService();
    crossfitterService.getExercises().then(data => {
      debugger;
    });
    axios({ url: "https://...." }).then(
      response => {
        const payload: IUser = response && response.data;
        commit("profileLoaded", payload);
      },
      error => {
        const payload: IUser = {
          email: "email@email.com",
          firstName: "Andrea",
          lastName: "Micher",
          phone: "111-22-33"
        };
        commit("profileLoaded", payload);

        // console.log(error);
        // commit("profileError");
      }
    );
  },
  // tslint:disable-next-line:typedef
  doAction({ commit }, payload): any {
    commit("changeUserName", payload.value);
  }
};
