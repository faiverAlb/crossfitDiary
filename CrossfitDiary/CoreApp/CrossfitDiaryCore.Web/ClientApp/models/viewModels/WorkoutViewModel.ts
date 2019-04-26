import Serializable = General.Serializable;
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
    return this;
  }
}
