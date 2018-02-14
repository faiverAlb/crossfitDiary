module Models {
  declare var ko;

  export class ToLogWorkoutViewModel {
    canBeRemovedByCurrentUser: boolean;
    crossfitterWorkoutId: number;
    date: Date;
    displayDate: string;
    distance?: number;
    isRx: boolean;
    measureDisplayName: string;
    partialRepsFinished?: number;
    roundsFinished?: number;
    selectedWorkoutId: string;
    selectedWorkoutName: string;
    timePassed: string;
    wasFinished: boolean;
    workouterName: string;
  }

  export class ToLogWorkoutViewModelObservable {

    /* Сivilians */

    /* Observables */

    /* Computeds */

    constructor(public model: ToLogWorkoutViewModel) {

    }
  }
}