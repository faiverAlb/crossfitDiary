import Serializable = General.Serializable;
import { WorkoutType } from "./WorkoutType";
import { ExerciseViewModel } from "./ExerciseViewModel";

interface IWorkoutViewModel {
  id?: number;
  title?: string;
  roundsCount?: number;
  timeToWork?: any;
  timeCap?: any;
  restBetweenExercises?: any;
  restBetweenRounds?: string;
  workoutType: WorkoutType;
  exercisesToDoList: ExerciseViewModel[];
  workoutTypeDisplay?: string;
  children: WorkoutViewModel[];
  displayPlanDate?: string;
  planningWorkoutLevel: PlanningWorkoutLevel;
  isInnerWorkout: boolean;
}
export enum PlanningWorkoutLevel {
  Scaled = 0,
  Rx = 1,
  RxPlus = 2
}
export class WorkoutViewModel implements Serializable<WorkoutViewModel> {
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
  displayPlanDate?: string = new Date().toLocaleDateString(); // default value for new model;

  public IsForTime = () => {
    return this.workoutType == WorkoutType.ForTime;
  };

  public IsAMRAP = () => {
    return this.workoutType == WorkoutType.AMRAP;
  };
  public IsEmoms = () => {
    return this.workoutType == WorkoutType.E2MOM || this.workoutType == WorkoutType.EMOM;
  };

  public IsHaveCapTime = () => {
    return this.workoutType == WorkoutType.ForTime || this.workoutType == WorkoutType.ForTimeManyInners;
  };

  constructor(params?: IWorkoutViewModel | any) {
    if (params == null) {
      return;
    }
    this.id = params.id;
    this.title = params.title;
    this.roundsCount = params.roundsCount;
    this.timeToWork = params.timeToWork;
    this.timeCap = params.timeCap;
    this.restBetweenExercises = params.restBetweenExercises;
    this.restBetweenRounds = params.restBetweenRounds;
    this.workoutType = params.workoutType;
    this.exercisesToDoList = params.exercisesToDoList;
    this.workoutTypeDisplay = params.workoutTypeDisplay;
    this.children = params.children;
    this.isInnerWorkout = params.isInnerWorkout;
    this.planningWorkoutLevel = params.planningWorkoutLevel;
    this.displayPlanDate = params.displayPlanDate;
  }

  public deserialize(jsonInput: any): WorkoutViewModel {
    if (jsonInput == null) {
      return null as any;
    }
    return new WorkoutViewModel({
      id: jsonInput.id,
      title: jsonInput.title,
      roundsCount: jsonInput.roundsCount,
      timeToWork: jsonInput.timeToWork,
      timeCap: jsonInput.timeCap,
      restBetweenExercises: jsonInput.restBetweenExercises,
      restBetweenRounds: jsonInput.restBetweenRounds,
      workoutType: jsonInput.workoutType,
      workoutTypeDisplay: jsonInput.workoutTypeDisplay,
      children: jsonInput.children.map((x): WorkoutViewModel => new WorkoutViewModel().deserialize(x)),
      exercisesToDoList: jsonInput.exercisesToDoList.map((x): ExerciseViewModel => new ExerciseViewModel().deserialize(x)),
      isInnerWorkout: false,
      planningWorkoutLevel: jsonInput.planningWorkoutLevel,
      displayPlanDate: jsonInput.displayPlanDate
    });
  }
}
