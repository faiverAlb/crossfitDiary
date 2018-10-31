// profile/actions.ts

import { ActionTree } from "vuex";
import axios from "axios";
import { ProfileState, User } from "./types";
import { RootState } from "../types";

export const actions: ActionTree<ProfileState, RootState> = {
  fetchData({ commit }): any {
    axios({
      url: "https://...."
    }).then(
      response => {
        const payload: User = response && response.data;
        commit("profileLoaded", payload);
      },
      error => {
        const payload: User = {
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
  }
};
