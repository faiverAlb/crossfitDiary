// profile/mutations.ts
import { MutationTree } from "vuex";
import { IWorkoutEditState } from "./types";
import { ExerciseViewModel } from "../models/viewModels/ExerciseViewModel";
import { PersonMaximumViewModel } from "../models/viewModels/PersonMaximumViewModel";

export const mutations: MutationTree<IWorkoutEditState> = {
  exercisesLoaded(state: IWorkoutEditState, payload: ExerciseViewModel[]) {
    state.error = false;
    state.exercises = payload;
  },
  userMaximumsLoaded(state: IWorkoutEditState, payload: PersonMaximumViewModel[]) {
    state.error = false;
    state.userMaximums = payload;
  },
  canUserSeePlanWorkoutsConfigured(state: IWorkoutEditState, payload: boolean) {
    state.canUserSeePlanWorkouts = payload;
  }
};
