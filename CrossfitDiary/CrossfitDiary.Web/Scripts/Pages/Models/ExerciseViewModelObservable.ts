module Models {
  export class ExerciseViewModelObservable {
    _exerciseMeasures: ExerciseMeasureViewModelObservable[];

    constructor(public model: ExerciseViewModel, personMaximums: PersonExerciseRecord[]) {
      let personMaximumWeight: number = this.getPersonMaximum(model.id, personMaximums);
      this._exerciseMeasures = model.exerciseMeasures.map(item => new ExerciseMeasureViewModelObservable(item, personMaximumWeight));

    }


    private getPersonMaximum = (exerciseId: number, personMaximums: PersonExerciseRecord[]): number => {
      for (let i = 0; i < personMaximums.length; i++) {
        if (personMaximums[i].exerciseId === exerciseId) {
          return personMaximums[i].maximumWeightValue;
        }
      }
      return null;
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