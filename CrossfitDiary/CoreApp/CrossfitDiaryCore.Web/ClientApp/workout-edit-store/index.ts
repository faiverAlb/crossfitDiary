import { Module } from "vuex";
import { getters } from "./getters";
import { actions } from "./actions";
import { mutations } from "./mutations";
import { IWorkoutEditState } from "./types";
import { RootState } from "../types";

export const state: IWorkoutEditState = {
  exercises: [],
  userMaximums:[],
  error: false,
  canUserSeePlanWorkouts: false
};

const namespaced: boolean = true;

export const workoutEdit: Module<IWorkoutEditState, RootState> = {
  namespaced,
  state,
  getters,
  actions,
  mutations
};
