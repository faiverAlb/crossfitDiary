// profile/actions.ts

import { ActionTree } from "vuex";
import axios from "axios";
import { IWorkoutEditState } from "./types";
import { RootState } from "../types";
import CrossfitterService from "../CrossfitterService";
import { ExerciseViewModel } from "../models/viewModels/ExerciseViewModel";
import { PersonMaximumViewModel } from "../models/viewModels/PersonMaximumViewModel";

export const actions: ActionTree<IWorkoutEditState, RootState> = {
  // tslint:disable-next-line:typedef
  fetchExercises({ commit }): any {
    let crossfitterService: CrossfitterService = new CrossfitterService();
    crossfitterService.getExercises().then(data => {
      const exercises: ExerciseViewModel[] = data;
      commit("exercisesLoaded", exercises);
    });
  },
  // tslint:disable-next-line:typedef
  fetchUserMaximums({ commit }): any {
    let crossfitterService: CrossfitterService = new CrossfitterService();
    crossfitterService.getExerciseMaximums().then(data => {
      const userMaximums: PersonMaximumViewModel[] = data;
      commit("userMaximumsLoaded", userMaximums);
    });
  },
  // tslint:disable-next-line:typedef
  setCanUserSeePlanWorkouts({ commit }, newValue): any {
    commit("canUserSeePlanWorkoutsConfigured", newValue);
  }
};
// }
