import { GetterTree } from "vuex";
import { IWorkoutEditState } from "./types";
import { RootState } from "../types";
import { ExerciseViewModel } from "../models/viewModels/ExerciseViewModel";

export const getters: GetterTree<IWorkoutEditState, RootState> = {
  exercisesFromState(state: IWorkoutEditState): ExerciseViewModel[] {
    return state.exercises;
    // const { user } = state;
    // const firstName = (user && user.firstName) || "";
    // const lastName = (user && user.lastName) || "";
    // return `${firstName} ${lastName}`;
  }
};
