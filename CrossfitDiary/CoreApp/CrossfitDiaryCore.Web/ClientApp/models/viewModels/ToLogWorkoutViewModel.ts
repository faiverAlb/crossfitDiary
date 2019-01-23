import Serializable = General.Serializable;
import { WorkoutViewModel } from "./WorkoutViewModel";

export enum PlanningWorkoutLevel {
  Scaled = 0,
  Rx = 1,
  RxPlus = 2
}
interface IToLogWorkoutViewModel {
  id: number;
  crossfitterWorkoutId: number;
  date: string;
  partialRepsFinished: number;
  roundsFinished: number;
  repsToFinishOnCapTime: number;
  selectedWorkoutId: number;
  timePassed: string;
  isEditMode: boolean;
  isPlanned: boolean;
  canBeRemovedByCurrentUser?: boolean;
  workouterName?: string;
  workouterId?: string;
  displayDate?: string;
  workoutViewModel?: WorkoutViewModel;
  planningWorkoutLevel: PlanningWorkoutLevel;
}
export class ToLogWorkoutViewModel implements Serializable<ToLogWorkoutViewModel> {
  id: number = 0;
  crossfitterWorkoutId: number = 0;
  date: string = new Date().toLocaleDateString();
  partialRepsFinished?: number;
  roundsFinished?: number;
  repsToFinishOnCapTime?: number;
  selectedWorkoutId: number = 0;
  timePassed: string = null;
  isEditMode: boolean = false;
  isPlanned: boolean = false;
  canBeRemovedByCurrentUser?: boolean;
  workouterName?: string;
  workouterId?: string;
  displayDate?: string = new Date().toLocaleDateString(); // default value for new model
  workoutViewModel?: WorkoutViewModel;
  planningWorkoutLevel: PlanningWorkoutLevel;
  constructor(params?: IToLogWorkoutViewModel) {
    if (params == null) {
      return;
    }
    this.id = params.id;
    this.crossfitterWorkoutId = params.crossfitterWorkoutId;
    this.date = params.date;
    this.partialRepsFinished = params.partialRepsFinished;
    this.roundsFinished = params.roundsFinished;
    this.selectedWorkoutId = params.selectedWorkoutId;
    this.timePassed = params.timePassed;
    this.isEditMode = params.isEditMode;
    this.isPlanned = params.isPlanned;
    this.repsToFinishOnCapTime = params.repsToFinishOnCapTime;
    this.canBeRemovedByCurrentUser = params.canBeRemovedByCurrentUser;
    this.workouterName = params.workouterName;
    this.workouterId = params.workouterId;
    this.displayDate = params.displayDate;
    this.workoutViewModel = params.workoutViewModel;
    this.planningWorkoutLevel = params.planningWorkoutLevel;
  }

  deserialize(jsonInput: any): ToLogWorkoutViewModel {
    if (jsonInput == null) {
      return null as any;
    }

    return new ToLogWorkoutViewModel({
      id: jsonInput.id,
      crossfitterWorkoutId: jsonInput.crossfitterWorkoutId,
      date: jsonInput.date,
      isEditMode: jsonInput.isEditMode,
      partialRepsFinished: jsonInput.partialRepsFinished,
      roundsFinished: jsonInput.roundsFinished,
      selectedWorkoutId: jsonInput.selectedWorkoutId,
      timePassed: jsonInput.timePassed,
      repsToFinishOnCapTime: jsonInput.repsToFinishOnCapTime,
      canBeRemovedByCurrentUser: jsonInput.canBeRemovedByCurrentUser,
      workouterName: jsonInput.workouterName,
      workouterId: jsonInput.workouterId,
      displayDate: jsonInput.displayDate,
      workoutViewModel: new WorkoutViewModel().deserialize(jsonInput.workoutViewModel),
      isPlanned: jsonInput.isPlanned,
      planningWorkoutLevel: jsonInput.planningWorkoutLevel
    });
  }
}
