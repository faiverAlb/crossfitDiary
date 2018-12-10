// profile/actions.ts

import { ActionTree } from "vuex";
import axios from "axios";
import { IWorkoutEditState } from "./types";
import { RootState } from "../types";
import CrossfitterService from "../CrossfitterService";
import { ExerciseViewModel } from "../models/viewModels/ExerciseViewModel";

export const actions: ActionTree<IWorkoutEditState, RootState> = {
  // tslint:disable-next-line:typedef
  fetchExercises({ commit }): any {
    let crossfitterService: CrossfitterService = new CrossfitterService();
    crossfitterService.getExercises().then(data => {
      const exercises: ExerciseViewModel[] = data;
      commit("exercisesLoaded", exercises);
    });
  }
};
// }
