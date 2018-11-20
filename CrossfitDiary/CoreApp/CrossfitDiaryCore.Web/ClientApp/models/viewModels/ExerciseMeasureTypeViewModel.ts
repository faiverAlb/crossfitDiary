import Serializable = General.Serializable;
import { ExerciseMeasureType } from "./ExerciseMeasureType";
interface IExerciseMeasureTypeViewModel {
  measureType: ExerciseMeasureType;
  measureValue: string;
  description: string;
  shortMeasureDescription: string;
  isRequired: boolean;
}

export class ExerciseMeasureTypeViewModel
  implements Serializable<ExerciseMeasureTypeViewModel> {
  measureType: ExerciseMeasureType = ExerciseMeasureType.Weight;
  measureValue: string = "";
  description: string = "";
  shortMeasureDescription: string = "";
  isRequired: boolean = false;

  constructor(params?: IExerciseMeasureTypeViewModel) {
    if (params == null) {
      return;
    }

    this.measureType = params.measureType;
    this.measureValue = params.measureValue;
    this.description = params.description;
    this.shortMeasureDescription = params.shortMeasureDescription;
    this.isRequired = params.isRequired;
  }

  deserialize(jsonInput: any): ExerciseMeasureTypeViewModel {
    if (jsonInput == null) {
      return null as any;
    }

    return new ExerciseMeasureTypeViewModel({
      measureType: <ExerciseMeasureType>jsonInput.measureType,
      measureValue: jsonInput.measureValue,
      description: jsonInput.description,
      isRequired: jsonInput.isRequired,
      shortMeasureDescription: jsonInput.shortMeasureDescription
    });
  }
}
