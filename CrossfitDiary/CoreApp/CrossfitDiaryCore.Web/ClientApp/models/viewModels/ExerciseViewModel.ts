import Deserializable = General.Deserializable;
import { ExerciseMeasureViewModel } from "./ExerciseMeasureViewModel";

export class ExerciseViewModel implements Deserializable {
  id: number = 0;
  title?: string;
  exerciseMeasures: ExerciseMeasureViewModel[] = [];
  isAlternative: boolean = false;
  isNewWeightMaximum?: boolean = false;
  isDoUnbroken: boolean = false;
  addedToMaxWeightString?: string;

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
