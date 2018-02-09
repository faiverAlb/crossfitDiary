module Models {

  export interface IExerciseViewModel {
    id: number;
    title?: string;
    exerciseMeasures: IExerciseMeasureViewModel[];
    isAlternative: boolean;
  }

  export interface IExerciseMeasureViewModel {
    exerciseMeasureType: IExerciseMeasureTypeViewModel
  }

  class ExerciseMeasureViewModelObservable {
    constructor(model: IExerciseMeasureViewModel) {}
  }

  export class ExerciseViewModelObservable {
    constructor(public model: IExerciseViewModel) {

    }
  }
}