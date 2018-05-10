module Models {
  export class ExerciseMeasureViewModelObservable {
    _exerciseMeasureType: ExerciseMeasureTypeViewModelObservable;

    constructor(public model: ExerciseMeasureViewModel) {
      this._exerciseMeasureType = new ExerciseMeasureTypeViewModelObservable(model.exerciseMeasureType);
    }

    public toPlainObject = (): ExerciseMeasureViewModel => {
      let plainObject = new ExerciseMeasureViewModel({
        exerciseMeasureType: this._exerciseMeasureType.toPlainObject()
      });
      return plainObject;
    }
  }
}