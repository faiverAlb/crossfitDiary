import Deserializable = General.Deserializable;
import { ExerciseMeasureViewModel } from "./ExerciseMeasureViewModel";
import { ExerciseMeasureType } from "./ExerciseMeasureType";

export class ExerciseViewModel implements Deserializable {
  id: number = 0;
  title?: string;
  exerciseMeasures: ExerciseMeasureViewModel[] = [];
  isAlternative: boolean = false;
  isNewWeightMaximum?: boolean = false;
  isDoUnbroken: boolean = false;
  addedToMaxWeightString?: string;

  count?: string = null;
  weight?: string = null;
  calories?: string = null;

  constructor(input?: any) {
    if (input == null) {
      return;
    }
    Object.assign(this, input);
  }

  public deserialize(input: any): this {
    if (input == null) {
      return;
    }
    Object.assign(this, input);
    this.exerciseMeasures = input.exerciseMeasures.map(x => new ExerciseMeasureViewModel().deserialize(x));

    this.count = this.getMeasureValue(ExerciseMeasureType.Count);
    this.weight = this.getMeasureValue(ExerciseMeasureType.Weight);
    this.calories = this.getMeasureValue(ExerciseMeasureType.Calories);
    return this;
  }

  public haveSameCountAndCalories = (toCompareExercise: ExerciseViewModel): boolean => {
    if (this.count == null && toCompareExercise.count == null) {
      if (this.calories == null && toCompareExercise.calories == null) {
        return false;
      } else {
        return this.calories === toCompareExercise.calories;
      }
    } else {
      if (this.calories == null && toCompareExercise.calories == null) {
        return this.count === toCompareExercise.count;
      } else {
        let result: boolean =
          this.count === toCompareExercise.count ||
          this.count === toCompareExercise.calories ||
          this.calories === toCompareExercise.count ||
          this.calories === toCompareExercise.calories;
        return result;
      }
    }
  };
  getMeasureValue(measureType: ExerciseMeasureType): string {
    let foundCountMeasure: ExerciseMeasureViewModel = this.exerciseMeasures.find(x => x.measureType === measureType);
    let result: string = foundCountMeasure ? foundCountMeasure.measureValue : null;
    return result;
  }
}

