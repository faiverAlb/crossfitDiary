module Models {
  import Serializable = General.Serializable;

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


  export class ExerciseMeasureViewModel implements Serializable<ExerciseMeasureViewModel>{

    exerciseMeasureType: ExerciseMeasureTypeViewModel;

    constructor(params?: { exerciseMeasureType: ExerciseMeasureTypeViewModel }) {
      if (params == null) {
        return;
      }
      this.exerciseMeasureType = params.exerciseMeasureType;
    }

    deserialize(input): ExerciseMeasureViewModel {
      if (input == null) {
        return null;
      }

      return new ExerciseMeasureViewModel({
        exerciseMeasureType: new ExerciseMeasureTypeViewModel().deserialize(input.exerciseMeasureType)
      });
    }

  }
}