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
      });
    }

  }
}