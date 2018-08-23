  import Serializable = General.Serializable;
  import {ExerciseMeasureTypeViewModel} from "./ExerciseMeasureTypeViewModel";
  export  class ExerciseMeasureViewModel implements Serializable<ExerciseMeasureViewModel>{

    exerciseMeasureType: ExerciseMeasureTypeViewModel | null = null;

    constructor(params?: { exerciseMeasureType: ExerciseMeasureTypeViewModel }) {
      if (params == null) {
        return;
      }
      this.exerciseMeasureType = params.exerciseMeasureType;
    }

    deserialize(input: any): ExerciseMeasureViewModel {
      if (input == null) {
        return (null) as any;
      }

      return new ExerciseMeasureViewModel({
        exerciseMeasureType: new ExerciseMeasureTypeViewModel().deserialize(input.exerciseMeasureType)
      });
    }

  }
