import { ExerciseViewModel } from "../models/viewModels/ExerciseViewModel";

export interface IWorkoutEditState {
  exercises: ExerciseViewModel[];
  error: boolean;
  canUserSeePlanWorkouts: boolean;
}
