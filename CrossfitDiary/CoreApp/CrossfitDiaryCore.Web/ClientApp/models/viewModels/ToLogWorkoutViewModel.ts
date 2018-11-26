import Serializable = General.Serializable;
import { WorkoutViewModel } from "./WorkoutViewModel";

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
  canBeRemovedByCurrentUser?: boolean;
  workouterName?: string;
  workouterId?: string;
  displayDate?: string;
  workoutViewModel?: WorkoutViewModel;
}
export class ToLogWorkoutViewModel
  implements Serializable<ToLogWorkoutViewModel> {
  id: number = 0;
  crossfitterWorkoutId: number = 0;
  date: string = null;
  partialRepsFinished?: number;
  roundsFinished?: number;
  repsToFinishOnCapTime?: number;
  selectedWorkoutId: number = 0;
  timePassed: string = null;
  isEditMode: boolean = false;
  canBeRemovedByCurrentUser?: boolean;
  workouterName?: string;
  workouterId?: string;
  displayDate?: string;
  workoutViewModel?: WorkoutViewModel;

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
    this.repsToFinishOnCapTime = params.repsToFinishOnCapTime;
    this.canBeRemovedByCurrentUser = params.canBeRemovedByCurrentUser;
    this.workouterName = params.workouterName;
    this.workouterId = params.workouterId;
    this.displayDate = params.displayDate;
    this.workoutViewModel = params.workoutViewModel;
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
      workoutViewModel: new WorkoutViewModel().deserialize(
        jsonInput.workoutViewModel
      )
    });
  }
}
