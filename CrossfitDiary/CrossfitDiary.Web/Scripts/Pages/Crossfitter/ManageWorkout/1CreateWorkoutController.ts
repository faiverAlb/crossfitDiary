//module Pages {
//  declare var ko;
//  import WorkoutTypes = Models.WorkoutType;
//
//  export class CreateWorkoutController1 {
//
//    exercises: KnockoutObservableArray<any>;
//    alternativeExercises: KnockoutObservableArray<any>;
//    hasAnyRoutines: KnockoutComputed<boolean>;
//    selectedExercise: KnockoutObservable<any>;
//    selectedAlternativeExercise: KnockoutObservable<any>;
//    errors;
//
//
//    constructor(parameters: { pathToApp: string }) {
//
////      super(new CrossfitterParameters(parameters.pathToApp,false,[],null,null,null,null,null,null));
//
//
//      this.exercises = ko.observableArray([]);
//      this.alternativeExercises = ko.observableArray();
//      this.hasAnyRoutines = ko.computed(() => {
//        return this.simpleRoutines().length > 0;
//      });
//
//      this.selectedExercise = ko.observable();
//      this.selectedAlternativeExercise = ko.observable();
//
//      ko.computed(() => {
//        if (this.selectedWorkoutType() || !this.selectedWorkoutType()) {
//          this.simpleRoutines([]);
//        }
//      });
//      this.errors = ko.validation.group(this);
//
//      ko.computed( () => {
//        var exercise = this.selectedExercise();
//        if (!exercise) {
//          return;
//        }
//
//        this.simpleRoutines.push(new SimpleRoutine(exercise, this.selectedWorkoutType().id !== WorkoutTypes.Tabata));
//        this.selectedExercise('');
//      });
//
//      ko.computed(() =>{
//        var exercise = this.selectedAlternativeExercise();
//        if (!exercise) {
//          return;
//        }
//
//        this.simpleRoutines.push(new SimpleRoutine(exercise));
//        this.selectedAlternativeExercise('');
//      });
//       this.loadExercises();
//    }
//
//    loadExercises = () => {
//      this._service.getExercises().then((exercises:any) => {
//        ko.utils.arrayPushAll(this.exercises, exercises);
//        //  HACK for copy
//        var alternativeExercises = JSON.parse(JSON.stringify(exercises));
//        for (var i = 0; i < alternativeExercises.length; i++) {
//          alternativeExercises[i].isAlternative = true;
//        }
//        this.alternativeExercises(alternativeExercises);
//      });
//    };
//
//    canCreateCreateWorkout = () => {
//      if (this.errors().length > 0) {
//        this.errors.showAllMessages();
//        return false;
//      }
//      return true;
//    };
//
//    createWorkout = () => {
//      if (this.errors().length > 0) {
//        this.errors.showAllMessages();
//        return;
//      }
//      this._service.createWorkout(this.toJSON());
//    };
//
//
//    clearState = () => {
//      this.selectedWorkoutType(null);
//    };
//
//    getCreateWorkoutModel = () => {
//      return this.toJSON();
//    }
//
//
//  }
//
//  
//
//
//
//}
