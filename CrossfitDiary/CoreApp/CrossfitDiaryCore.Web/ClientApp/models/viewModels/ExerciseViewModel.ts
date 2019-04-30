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
    let foundCountMeasure: ExerciseMeasureViewModel = this.exerciseMeasures.find(x => x.measureType === ExerciseMeasureType.Count);
    this.count = foundCountMeasure ? foundCountMeasure.measureValue : null;
    return this;
  }
}

export class DefaultExerciseViewModel extends ExerciseViewModel {
  constructor() {
    super({
      id: -1,
      title: "Select exercise",
      exerciseMeasures: [],
      isAlternative: false,
      isDoUnbroken: false
    });
  }
}
