module Models {
  import Serializable = General.Serializable;


  interface IExerciseViewModel {
    id: number;
    title: string;
    exerciseMeasures: ExerciseMeasureViewModel[];
    isAlternative: boolean;
    isNewWeightMaximum?: boolean;
    isDoUnbroken: boolean;
    addedToMaxWeightString?: string;
  };

  export class ExerciseViewModel implements Serializable<ExerciseViewModel> {

    id: number;
    title?: string;
    exerciseMeasures: ExerciseMeasureViewModel[];
    isAlternative: boolean;
    isNewWeightMaximum?: boolean;
    isDoUnbroken: boolean;
    addedToMaxWeightString?: string;

    constructor(params?: IExerciseViewModel) {
      if (params == null) {
        return;
      }
      this.id = params.id;
      this.title = params.title;
      this.exerciseMeasures = params.exerciseMeasures;
      this.isAlternative = params.isAlternative;
      this.isNewWeightMaximum = params.isNewWeightMaximum;
      this.isDoUnbroken = params.isDoUnbroken;
      this.addedToMaxWeightString = params.addedToMaxWeightString;
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
        isNewWeightMaximum: input.isNewWeightMaximum,
        isDoUnbroken: input.isDoUnbroken,
        addedToMaxWeightString: input.addedToMaxWeightString,
      });
    };

  }
}