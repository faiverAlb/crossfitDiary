// profile/mutations.ts
import { MutationTree } from "vuex";
import { IWorkoutEditState } from "./types";
import { ExerciseViewModel } from "../models/viewModels/ExerciseViewModel";

export const mutations: MutationTree<IWorkoutEditState> = {
  exercisesLoaded(state: IWorkoutEditState, payload: ExerciseViewModel[]) {
    state.error = false;
    state.exercises = payload;
  },
  canUserSeePlanWorkoutsConfigured(state: IWorkoutEditState, payload: boolean) {
    state.canUserSeePlanWorkouts = payload;
  }
};
