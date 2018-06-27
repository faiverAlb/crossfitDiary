module Models {
  export class ExerciseMeasureViewModelObservable {
    _exerciseMeasureType: ExerciseMeasureTypeViewModelObservable;

    constructor(public model: ExerciseMeasureViewModel, personMaximumWeight?: number) {
      this._exerciseMeasureType = new ExerciseMeasureTypeViewModelObservable(model.exerciseMeasureType, personMaximumWeight);
    }

    public toPlainObject = (): ExerciseMeasureViewModel => {
      let plainObject = new ExerciseMeasureViewModel({
        exerciseMeasureType: this._exerciseMeasureType.toPlainObject()
      });
      return plainObject;
    }
  }
}