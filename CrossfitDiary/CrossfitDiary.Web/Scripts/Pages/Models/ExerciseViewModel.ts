module Models {
  export class ExerciseMeasureViewModel {
    exerciseMeasureType: ExerciseMeasureTypeViewModel;

    constructor(exerciseMeasureType: ExerciseMeasureTypeViewModel) {
      this.exerciseMeasureType = exerciseMeasureType;
    }
  }

  class ExerciseMeasureViewModelObservable {
    _exerciseMeasureType: ExerciseMeasureTypeViewModelObservable;

    constructor(public model: ExerciseMeasureViewModel) {
      this._exerciseMeasureType = new ExerciseMeasureTypeViewModelObservable(model.exerciseMeasureType);
    }

    public toPlainObject = (): ExerciseMeasureViewModel => {
      let plainObject = new ExerciseMeasureViewModel(this._exerciseMeasureType.toPlainObject());
      return plainObject;
    }
  }

  export class ExerciseViewModel {
    id: number;
    title?: string;
    exerciseMeasures: ExerciseMeasureViewModel[];
    isAlternative: boolean;

    constructor(id: number, title: string, exerciseMeasures: ExerciseMeasureViewModel[], isAlternative: boolean) {
      this.id = id;
      this.title = title;
      this.exerciseMeasures = exerciseMeasures;
      this.isAlternative = isAlternative;
    }
  }

  export class ExerciseViewModelObservable {
    _exerciseMeasures: ExerciseMeasureViewModelObservable[];
    constructor(public model: ExerciseViewModel) {
      this._exerciseMeasures = model.exerciseMeasures.map(item => new ExerciseMeasureViewModelObservable(item));
    }

    public toPlainObject = (): ExerciseViewModel => {
      let planExercise = new ExerciseViewModel(
        this.model.id,
        this.model.title,
        this._exerciseMeasures.map(item => item.toPlainObject()),
        this.model.isAlternative);
      return planExercise;
    };
  }
}