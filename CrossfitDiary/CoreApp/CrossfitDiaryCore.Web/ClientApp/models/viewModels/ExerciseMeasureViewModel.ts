import Serializable = General.Serializable;
import { ExerciseMeasureTypeViewModel } from "./ExerciseMeasureTypeViewModel";
import { ExerciseMeasureType } from "./ExerciseMeasureType";

export class ExerciseMeasureViewModel
  implements Serializable<ExerciseMeasureViewModel> {
  exerciseMeasureType: ExerciseMeasureTypeViewModel | null = null;
  displayMeasure: string;

  constructor(params?: { exerciseMeasureType: ExerciseMeasureTypeViewModel }) {
    if (params == null) {
      return;
    }
    this.exerciseMeasureType = params.exerciseMeasureType;
    this.displayMeasure = this.setDisplayByMeasureType(
      this.exerciseMeasureType.measureType
    );
  }

  setDisplayByMeasureType = (measure: ExerciseMeasureType): string => {
    switch (measure) {
      case ExerciseMeasureType.Distance:
        return "m";
      case ExerciseMeasureType.Count:
        return "";
      case ExerciseMeasureType.Weight:
        return "kgs";
      case ExerciseMeasureType.Calories:
        return "calories";
      case ExerciseMeasureType.Height:
        return "cm";
      default:
        return "";
    }
  };

  deserialize(input: any): ExerciseMeasureViewModel {
    if (input == null) {
      return null as any;
    }

    return new ExerciseMeasureViewModel({
      exerciseMeasureType: new ExerciseMeasureTypeViewModel().deserialize(
        input.exerciseMeasureType
      )
    });
  }
}
