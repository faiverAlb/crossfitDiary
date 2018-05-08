module Models {
  declare var ko;

  export class ToLogWorkoutViewModel {
    canBeRemovedByCurrentUser: boolean;
    crossfitterWorkoutId: number;
    date: string;
    displayDate: string;
    distance?: number;
    isRx: boolean;
    measureDisplayName: string;
    partialRepsFinished?: number;
    roundsFinished?: number;
    selectedWorkoutId: number;
    selectedWorkoutName: string;
    timePassed: string;
    wasFinished: boolean;
    workouterName: string;
    isEditMode: boolean;

    constructor(
      crossfitterWorkoutId: number,
      date: string,
      partialRepsFinished: number,
      roundsFinished: number,
      selectedWorkoutId: number,
      timePassed: string,
      isEdtiMode: boolean
    ) {
      this.crossfitterWorkoutId = crossfitterWorkoutId;
      this.date = date;
      this.partialRepsFinished = partialRepsFinished;
      this.roundsFinished = roundsFinished;
      this.selectedWorkoutId = selectedWorkoutId;
      this.timePassed = timePassed;
      this.isEditMode = isEdtiMode;
    }
  }
}