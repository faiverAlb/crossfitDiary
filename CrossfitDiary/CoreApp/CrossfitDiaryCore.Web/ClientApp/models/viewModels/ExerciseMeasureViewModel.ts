  import Serializable = General.Serializable;
  import {ExerciseMeasureTypeViewModel} from "./ExerciseMeasureTypeViewModel";
  import {ExerciseMeasureType} from "./ExerciseMeasureType";

  export  class ExerciseMeasureViewModel implements Serializable<ExerciseMeasureViewModel>{
    

    exerciseMeasureType: ExerciseMeasureTypeViewModel | null = null;
    displayMeasure: string;

    constructor(params?: { exerciseMeasureType: ExerciseMeasureTypeViewModel }) {
      if (params == null) {
        return;
      }
      this.exerciseMeasureType = params.exerciseMeasureType;
      this.displayMeasure = this.setDisplayByMeasureType(this.exerciseMeasureType.measureType);
    }

    setDisplayByMeasureType = (measure: ExerciseMeasureType): string  => {
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

      /*<!-- ko if:exerciseMeasureType.measureValue != null && exerciseMeasureType.measureValue != "" -->
      <!-- ko if:exerciseMeasureType.measureType == @((int)MeasureTypeViewModel.Count)-->
      <span data-bind="text:exerciseMeasureType.measureValue"></span>
      <!-- /ko -->
      <!-- ko if:exerciseMeasureType.measureType == @((int)MeasureTypeViewModel.Distance)-->
      <span data-bind="text:exerciseMeasureType.measureValue"></span>
      <span> m</span>
      <!-- /ko -->
      <!-- ko if:exerciseMeasureType.measureType == @((int)MeasureTypeViewModel.Weight)-->
      (
      <span data-bind="text:exerciseMeasureType.measureValue"></span>
      <span> kgs</span>)
      <!-- /ko -->
      <!-- ko if:exerciseMeasureType.measureType == @((int)MeasureTypeViewModel.Calories)-->
      <span data-bind="text:exerciseMeasureType.measureValue"></span>
      <span> calories </span>
      <!-- /ko -->
      <!-- ko if:exerciseMeasureType.measureType == @((int)MeasureTypeViewModel.Height)-->
      (
      <span data-bind="text:exerciseMeasureType.measureValue"></span>
      <span> cm</span>)
      <!-- /ko -->
      <!-- /ko --> */
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
