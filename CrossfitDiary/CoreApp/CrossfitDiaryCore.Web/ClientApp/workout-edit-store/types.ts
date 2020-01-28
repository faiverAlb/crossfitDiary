import {ExerciseViewModel} from "../models/viewModels/ExerciseViewModel";
import {PersonMaximumViewModel} from "../models/viewModels/PersonMaximumViewModel";

export interface IWorkoutEditState {
    exercises: ExerciseViewModel[];
    userMaximums: PersonMaximumViewModel[];
    error: boolean;
    canUserSeePlanWorkouts: boolean;
    isFindMaxWeight: boolean;
}
