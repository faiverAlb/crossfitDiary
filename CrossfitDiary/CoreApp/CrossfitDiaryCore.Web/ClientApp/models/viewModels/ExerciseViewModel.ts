  import Serializable = General.Serializable;
  import {ExerciseMeasureViewModel} from './ExerciseMeasureViewModel';

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

    id: number = 0;
    title?: string;
    exerciseMeasures: ExerciseMeasureViewModel[]  = [];
    isAlternative: boolean = false;
    isNewWeightMaximum?: boolean = false;
    isDoUnbroken: boolean = false;
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

    deserialize(input: any): ExerciseViewModel {
      if (input == null) {
        return (null) as any;
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
