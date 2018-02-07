
module Pages {
  declare var ko;
  export class LogWorkoutController {


    canSeeTotalRounds;
    canSeePassedDistance;
    canSeeTotalTime;
    date;
    logWorkoutText;
    totalRoundsFinished;
    partialRepsFinished;
    errors;
    plannedDate;
    distance;
    wasFinished;
    isRx;
    IsModified;
    totalTime;


    constructor(public lightModel, public logFunction) {
      this.canSeeTotalRounds = ko.observable(false);
      this.canSeePassedDistance = ko.observable(false);
      this.canSeeTotalTime = ko.observable(false);
      this.date = lightModel.date;

      this.logWorkoutText = ko.observable(lightModel.logWorkoutText);

      this.totalRoundsFinished = ko.observable()
        .extend({
          required: {
            onlyIf:  () => {
              return this.canSeeTotalRounds();
            }
          }
        });

      this.partialRepsFinished = ko.observable();

      this.plannedDate = ko.observable(new Date());



      this.distance = ko.observable()
        .extend({
          required: {
            onlyIf: () => {
              return this.canSeePassedDistance();
            }
          }
        });


      this.wasFinished = ko.observable();
      this.isRx = ko.observable(true);
      this.IsModified = ko.observable();
      this.totalTime = ko.observable()
        .extend({
          required: {
            onlyIf: ()  => {
              return this.canSeeTotalTime();
            }
          }
        });
      ko.computed(() => {
        this.updateInputsVisibility();
      });
      this.errors = ko.validation.group(this);
    }

    updateInputsVisibility = () => {
      if (!this.lightModel.selectedWorkoutType || !this.lightModel.simpleRoutines) {
        return;
      }
      var selectedTypeValue = this.lightModel.selectedWorkoutType.Value;

      /* Rounds */
      this.canSeeTotalRounds(selectedTypeValue == WorkoutTypes.AMRAP);

      //        /* Distance input */
      this.canSeePassedDistance(this.checkWorkoutContainsDistanceExercise() && selectedTypeValue == WorkoutTypes.EMOM);

      /* General time */
      this.canSeeTotalTime(selectedTypeValue == WorkoutTypes.ForTime);
    }



    checkWorkoutContainsDistanceExercise = () => {
      return ko.utils.arrayFirst(this.lightModel.simpleRoutines, routine => {
        var foundDistanceMeasure = ko.utils.arrayFirst(routine.exerciseMeasures(), measure => measure.measureType() == Crossfitter.ExerciseMeasureTypes.Distance);
        return foundDistanceMeasure;
      }) != null;
    }

    logWorkout = () => {
      this.logFunction();
    };

    toJSON =  () => {
      var date = this.plannedDate().toDate().toUTCString();
      var model = {
        selectedWorkoutId: this.lightModel.selectedWorkout ? this.lightModel.selectedWorkout().id : null,
        crossfitterWorkoutId: this.lightModel.crossfitterWorkoutId,
        roundsFinished: this.totalRoundsFinished(),
        partialRepsFinished: this.partialRepsFinished(),
        timePassed: this.totalTime(),
        distance: this.distance(),
        wasFinished: this.wasFinished(),
        isRx: this.isRx(),
        date: date
      };
      return model;
    };

    canLogWorkout = () => {
      if (this.errors().length > 0) {
        this.errors.showAllMessages();
        return false;
      }
      return true;
    };


  }
}
    

