import Serializable = General.Serializable;
// import { ExerciseMeasureTypeViewModel } from "./ExerciseMeasureTypeViewModel";
import { ExerciseMeasureType } from "./ExerciseMeasureType";

export class ExerciseMeasureViewModel
  implements Serializable<ExerciseMeasureViewModel> {
  measureType: ExerciseMeasureType = ExerciseMeasureType.Weight;
  measureValue: string = "";
  description: string = "";
  shortMeasureDescription: string = "";
  isRequired: boolean = false;

  constructor(params?:any) {
    if (params == null) {
      return;
    }
    this.measureType = params.measureType;
    this.measureValue = params.measureValue;
    this.description = params.description;
    this.shortMeasureDescription = params.shortMeasureDescription;
    this.isRequired = params.isRequired;
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

  deserialize(jsonInput: any): ExerciseMeasureViewModel {
    if (jsonInput == null) {
      return null as any;
    }
    return new ExerciseMeasureViewModel({
      measureType: <ExerciseMeasureType>jsonInput.measureType,
      measureValue: jsonInput.measureValue,
      description: jsonInput.description,
      isRequired: jsonInput.isRequired,
      shortMeasureDescription: jsonInput.shortMeasureDescription
    });
  }
}
