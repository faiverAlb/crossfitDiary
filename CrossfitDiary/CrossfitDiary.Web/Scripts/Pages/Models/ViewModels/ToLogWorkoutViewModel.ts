module Models {
  import Serializable = General.Serializable;

  interface IToLogWorkoutViewModel {
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
  export class ToLogWorkoutViewModel implements Serializable<ToLogWorkoutViewModel>{

    crossfitterWorkoutId: number;
    date: string;
    partialRepsFinished?: number;
    roundsFinished?: number;
    repsToFinishOnCapTime?: number;
    selectedWorkoutId: number;
    timePassed: string;
    isEditMode: boolean;
    canBeRemovedByCurrentUser?: boolean;
    workouterName?: string;
    workouterId?: string;
    displayDate?: string;
    workoutViewModel?: WorkoutViewModel;

    constructor(
      params?: IToLogWorkoutViewModel) {
      if (params == null) {
        return;
      }
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

    deserialize(jsonInput): ToLogWorkoutViewModel {
      if (jsonInput == null) {
        return null;
      }

      return new ToLogWorkoutViewModel({
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
        workoutViewModel: new WorkoutViewModel().deserialize(jsonInput.workoutViewModel)
      });
    }

  }
}