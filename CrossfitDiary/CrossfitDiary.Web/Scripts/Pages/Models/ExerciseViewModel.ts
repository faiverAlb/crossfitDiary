module Models {
  import Serializable = General.Serializable;
  export interface IExerciseViewModel {
    id: number;
    title: string;
    exerciseMeasures: ExerciseMeasureViewModel[];
    isAlternative: boolean;
  };

  export class ExerciseViewModel implements Serializable<ExerciseViewModel> {

    id: number;
    title?: string;
    exerciseMeasures: ExerciseMeasureViewModel[];
    isAlternative: boolean;

    constructor(params?: IExerciseViewModel) {
      if (params == null) {
        return;
      }
      this.id = params.id;
      this.title = params.title;
      this.exerciseMeasures = params.exerciseMeasures;
      this.isAlternative = params.isAlternative;
    }

    deserialize(input): ExerciseViewModel {
      if (input == null) {
        return null;
      }

      return new ExerciseViewModel({
        id: input.id,
        title: input.title,
        exerciseMeasures: input.exerciseMeasures.map(x => new ExerciseMeasureViewModel().deserialize(x)),
        isAlternative: input.isAlternative,
      });
    };

  }

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