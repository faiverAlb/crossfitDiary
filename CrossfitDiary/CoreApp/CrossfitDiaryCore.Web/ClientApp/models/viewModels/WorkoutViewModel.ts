import { WorkoutType } from "./WorkoutType";
import { ExerciseViewModel } from "./ExerciseViewModel";
import Deserializable = General.Deserializable;

export enum PlanningWorkoutLevel {
  Scaled = 0,
  Rx = 1,
  RxPlus = 2
}
export class WorkoutViewModel implements Deserializable {
  id: number = 0;
  title: string = "";
  roundsCount?: number;
  timeToWork?: any;
  timeCap?: any;
  restBetweenExercises?: any;
  restBetweenRounds?: string;
  workoutType: WorkoutType = WorkoutType.ForTime;
  workoutTypeDisplay?: string;
  exercisesToDoList: ExerciseViewModel[] = [];
  children: WorkoutViewModel[] = [];
  isInnerWorkout: boolean = false;
  planningWorkoutLevel: PlanningWorkoutLevel;
  displayPlanDate?: string; // default value for new model;
  comment?: string;
  resultsCount: number;
  canSeeLeaderboard: boolean;
  haveCollapsedVersion: boolean = false;
  groupedDictionary: {} = {};

  getDefaultDate = (): string => {
    let date: Date = new Date();
    let result: string = ("0" + date.getDate()).slice(-2) + "." + ("0" + (date.getMonth() + 1)).slice(-2) + "." + date.getFullYear();

    return result;
  };
  public IsForTime = () => {
    return this.workoutType === WorkoutType.ForTime;
  };

  public IsAMRAP = () => {
    return this.workoutType === WorkoutType.AMRAP || this.workoutType === WorkoutType.AMRAPN;
  };
  public IsEmoms = () => {
    return this.workoutType === WorkoutType.E2MOM || this.workoutType === WorkoutType.EMOM;
  };

  public IsHaveCapTime = () => {
    return this.workoutType === WorkoutType.ForTime || this.workoutType === WorkoutType.ForTimeManyInners;
  };

  private tryCollapseWorkout(): void {
    let exercisedToUse: ExerciseViewModel[] = this.exercisesToDoList;
    let distinctFirst: ExerciseViewModel[] = [];
    for (let index = 0; index < exercisedToUse.length; index++) {
      const element: ExerciseViewModel = exercisedToUse[index];
      let foundInDistinct = distinctFirst.find(x => {
        return x.id === element.id;
      });
      if (!foundInDistinct) {
        distinctFirst.push(element);
      } else {
        break;
      }
    }

    let canBeCollapsed: boolean = true;
    if (distinctFirst.length === 0) {
      return;
    }
    if (exercisedToUse.length === distinctFirst.length) {
      // trivial case
      return;
    }
    if (exercisedToUse.length % distinctFirst.length !== 0) {
      return;
    }

    for (let i = 0; i < exercisedToUse.length; i++) {
      if (canBeCollapsed === false) {
        break;
      }

      for (let j = 0; j < distinctFirst.length; j++) {
        const distinctItem: ExerciseViewModel = distinctFirst[j];
        if (i + j < exercisedToUse.length && exercisedToUse[i + j].id !== distinctItem.id) {
          canBeCollapsed = false;
          break;
        }
      }
      i = i + distinctFirst.length - 1;
    }
    this.haveCollapsedVersion = canBeCollapsed;
    if (this.haveCollapsedVersion === false) {
      return;
    }
    for (let i = 0; i < exercisedToUse.length; i++) {
      const exercise: ExerciseViewModel = exercisedToUse[i];
      if (this.groupedDictionary[exercise.id]) {
        this.groupedDictionary[exercise.id].push(exercise);
      } else {
        this.groupedDictionary[exercise.id] = [];
        this.groupedDictionary[exercise.id].push(exercise);
      }
    }
  }

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
    this.children = input.children.map((x): WorkoutViewModel => new WorkoutViewModel().deserialize(x));
    this.exercisesToDoList = input.exercisesToDoList.map((x): ExerciseViewModel => new ExerciseViewModel().deserialize(x));
    this.tryCollapseWorkout();
    return this;
  }
}
