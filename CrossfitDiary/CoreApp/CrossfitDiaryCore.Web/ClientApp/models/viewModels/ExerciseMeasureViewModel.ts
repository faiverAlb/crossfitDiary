import Deserializable = General.Deserializable;
import { ExerciseMeasureType } from "./ExerciseMeasureType";

export class ExerciseMeasureViewModel implements Deserializable {
  measureType: ExerciseMeasureType = ExerciseMeasureType.Weight;
  measureValue: string = "";
  description: string = "";
  shortMeasureDescription: string = "";
  isRequired: boolean = false;

  constructor(input?: any) {
    if (input == null) {
      return;
    }
    Object.assign(this, input);
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

  public deserialize(input: any): this {
    if (input == null) {
      return;
    }
    Object.assign(this, input);
    return this;
  }
}
