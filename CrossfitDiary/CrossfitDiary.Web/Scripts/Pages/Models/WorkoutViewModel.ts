module Models {

  export interface IWorkoutViewModel {
    id: number;
    title?: string;
    roundsCount?: number;
    timeToWork?: any;
    restBetweenExercises?: any;
    restBetweenRounds?:any;
    workoutType: WorkoutType;
    exercisesToDoList: ExerciseViewModel[];
  }
  export class WorkoutViewModel {
    constructor(model:IWorkoutViewModel) {

    }
  }
}