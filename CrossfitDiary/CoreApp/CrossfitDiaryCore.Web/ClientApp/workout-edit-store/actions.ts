// profile/actions.ts

import {ActionTree} from "vuex";
import {IWorkoutEditState} from "./types";
import {RootState} from "../types";
import CrossfitterService from "../CrossfitterService";
import {ExerciseViewModel} from "../models/viewModels/ExerciseViewModel";
import {PersonMaximumViewModel} from "../models/viewModels/PersonMaximumViewModel";

export const actions: ActionTree<IWorkoutEditState, RootState> = {
    fetchExercises({commit}): any {
        let crossfitterService: CrossfitterService = new CrossfitterService();
        crossfitterService.getExercises().then(data => {
            const exercises: ExerciseViewModel[] = data;
            commit("exercisesLoaded", exercises);
        });
    },
    // fetchUserMaximums({commit}): any {
    //     let crossfitterService: CrossfitterService = new CrossfitterService();
    //     crossfitterService.getExerciseMaximums().then(data => {
    //         const userMaximums: PersonMaximumViewModel[] = data;
    //         commit("userMaximumsLoaded", userMaximums);
    //     });
    // },
    setCanUserSeePlanWorkouts({commit}, newValue): any {
        commit("canUserSeePlanWorkoutsConfigured", newValue);
    },
    setIsFindMaxWeight({commit}, newValue): any {
        commit("isFindMaxWeightConfigured", newValue);
    }
};
// }
