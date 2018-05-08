module Models {
  import Serializable = General.Serializable;


  interface IExerciseViewModel {
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
}