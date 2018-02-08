module Models {

  export interface IExerciseViewModel {
    id: number;
    title?: string;
    exerciseMeasures: ExerciseMeasureViewModel[];
    isAlternative:boolean;
  }
  export class ExerciseViewModel {
    constructor(model:IWorkoutViewModel) {

    }
  }
}