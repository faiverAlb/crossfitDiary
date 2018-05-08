module Models {
  export class ExerciseViewModelObservable {
    _exerciseMeasures: ExerciseMeasureViewModelObservable[];

    constructor(public model: ExerciseViewModel) {
      this._exerciseMeasures = model.exerciseMeasures.map(item => new ExerciseMeasureViewModelObservable(item));
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