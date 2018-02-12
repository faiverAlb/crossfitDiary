module Models {
  export interface IExerciseMeasureViewModel {
    exerciseMeasureType: IExerciseMeasureTypeViewModel
  }

  class ExerciseMeasureViewModelObservable {
    _exerciseMeasureType: ExerciseMeasureTypeViewModelObservable;

    constructor(model: IExerciseMeasureViewModel) {
      this._exerciseMeasureType = new ExerciseMeasureTypeViewModelObservable(model.exerciseMeasureType);
    }
  }

  export interface IExerciseViewModel {
    id: number;
    title?: string;
    exerciseMeasures: IExerciseMeasureViewModel[];
    isAlternative: boolean;
  }
  export class ExerciseViewModelObservable {
    _exerciseMeasures: ExerciseMeasureViewModelObservable[];
    constructor(public model: IExerciseViewModel) {
      this._exerciseMeasures = model.exerciseMeasures.map(item => new ExerciseMeasureViewModelObservable(item));
    }
  }
}