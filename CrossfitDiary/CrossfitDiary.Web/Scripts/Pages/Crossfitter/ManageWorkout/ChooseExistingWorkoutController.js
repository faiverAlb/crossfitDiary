//module Pages {
//  import CrossfitterService = General.CrossfitterService;
//
//  export class ChooseExistingWorkoutController {
//    totalRoundsFinished;
//    _service: General.CrossfitterService;
//    availableWorkouts:KnockoutObservableArray<any>;
//    selectedWorkout;
//    isReadOnlyMode: true;
//    workoutToDisplay;
//    partialRepsFinished;
//    totalTime;
//    distance;
//    wasFinished;
//    isRx:any;
//
//
//    constructor(public parameters) {
//      this._service = new CrossfitterService(parameters.pathToApp);
//      this.availableWorkouts = ko.observableArray();
//
//      this.selectedWorkout = ko.observable();
//      this.isReadOnlyMode = true;
//
//
//      this.workoutToDisplay = ko.observable();
//
//      ko.computed(() => {
//        var workout = this.selectedWorkout();
//        if (!workout) {
//          return;
//        }
//        workout.isReadOnlyMode = this.isReadOnlyMode;
//        this.workoutToDisplay(new CrossfitterController(workout));
//
//      });
//      this.loadAvailableWorkouts();
//    }
//
//    clearState =  () => {
//      this.selectedWorkout(null);
//    };
//
//    toJSON =  () => {
//      var model = {
//        selectedWorkoutId: this.selectedWorkout().id,
//        roundsFinished: this.totalRoundsFinished,
//        partialRepsFinished: this.partialRepsFinished,
//        timePassed: this.totalTime,
//        distance: this.distance,
//        wasFinished: this.wasFinished,
//        isRx: this.isRx()
//      };
//      return model;
//    };
//
//    loadAvailableWorkouts = () => {
//      this._service.getAvailableWorkouts()
//        .then((availableWorkouts: any) => {
//          this.availableWorkouts(availableWorkouts);
//        });
//    };
//
// 
//    
//  }
//}
//
//
//# sourceMappingURL=ChooseExistingWorkoutController.js.map