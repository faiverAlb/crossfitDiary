module Models {
  export class ExerciseViewModelObservable {
    _exerciseMeasures: ExerciseMeasureViewModelObservable[];
    _canSeePersonalRecord: boolean;

    constructor(public model: ExerciseViewModel, public personMaximums: PersonExerciseRecord[]) {
      this._exerciseMeasures = model.exerciseMeasures.map(item => new ExerciseMeasureViewModelObservable(item));

      this._canSeePersonalRecord = true;
    }

    public toPlainObject = (): ExerciseViewModel => {
      let plainExercise = new ExerciseViewModel(
        {
          id: this.model.id,
          title: this.model.title,
          exerciseMeasures: this._exerciseMeasures.map(item => item.toPlainObject()),
          isAlternative: this.model.isAlternative
        });
      return plainExercise;
    };
  }
}