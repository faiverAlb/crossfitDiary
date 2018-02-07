module Pages {
  declare var ko;

  ko.validation.init({
    errorElementClass: 'has-error',
    errorMessageClass: 'help-block',
    decorateInputElement: true,
    insertMessages: false,
    grouping: {
      deep: true,
      live: true,
      observable: true
    }
  });


  import BaseController = General.BaseController;
  import CrossfitterService = General.CrossfitterService;
  import SimpleRoutine = Crossfitter.SimpleRoutine;

  interface ICrossfitterParameters {
    viewModel: any;
    pathToApp: string;
    isReadOnlyMode: boolean;
    exercisesToDoList: any;
    selectedWorkoutType: any;
    title: string;
    restBetweenExercises: any;
    restBetweenRounds: any;
    timeToWork: any;
    roundsCount: any;
//  { viewModel: { allWorkouts }, pathToApp: string,  }

  }

  export class CrossfitterController extends BaseController {
    protected  _service: CrossfitterService;
    private isReadOnlyMode: boolean;
    simpleRoutines: KnockoutObservableArray<any>;
    selectedWorkoutType: KnockoutObservable<any>;
    private title: KnockoutObservable<string>;
    private restBetweenExercises: KnockoutObservable<any>;
    private restBetweenRounds: KnockoutObservable<any>;
    private canSeeRoundsCount: KnockoutComputed<false | boolean>;
    private anyUsualExercises: KnockoutComputed<boolean>;
    private canSeeAlternativeExercises: KnockoutComputed<any>;
    private canCreateWorkout: KnockoutComputed<boolean>;
    private timeToWork: KnockoutObservable<any>;
    private canSeeTimeToWork: KnockoutComputed<false | boolean>;
    private anyAlternative: KnockoutComputed<boolean>;
    private roundsCount: KnockoutObservable<any>;

    constructor(public parameters: ICrossfitterParameters) {
      super();
      this._service = new CrossfitterService(parameters.pathToApp);
      this.isReadOnlyMode = parameters.isReadOnlyMode != undefined ? parameters.isReadOnlyMode : false;
      this.simpleRoutines = ko.observableArray([]);

      if (parameters.exercisesToDoList) {
        for (var i = 0; i < parameters.exercisesToDoList.length; i++) {
          var exerciseToDo = parameters.exercisesToDoList[i];
          this.simpleRoutines.push(new SimpleRoutine(exerciseToDo, false));
        }
      }


      this.selectedWorkoutType = ko.observable(parameters.selectedWorkoutType);

      this.title = ko.observable(parameters.title);
      this.restBetweenExercises = ko.observable(parameters.restBetweenExercises);
      this.restBetweenRounds = ko.observable(parameters.restBetweenRounds);


      this.canSeeRoundsCount = ko.computed(() => {
        if (!this.selectedWorkoutType()) {
          return false;
        }
        var selectedType = this.selectedWorkoutType().Value;
        return selectedType === WorkoutTypes.ForTime || selectedType === WorkoutTypes.Tabata;
      });

      this.anyUsualExercises = ko.computed(() => {
        return ko.utils.arrayFirst(this.simpleRoutines(), routine => routine.exercise.isAlternative === false) != null;
      });


      this.canSeeAlternativeExercises = ko.computed(() => {
        if (!this.selectedWorkoutType()) {
          return false;
        }
        var selectedType = this.selectedWorkoutType().Value;
        var typeIsNeeded =
          selectedType == WorkoutTypes.EMOM || selectedType == WorkoutTypes.E2MOM;
        return typeIsNeeded && this.anyUsualExercises();
      });


      this.anyAlternative = ko.computed(() => {
        return ko.utils.arrayFirst(this.simpleRoutines(), routine => routine.exercise.isAlternative) != null;
      });




      this.roundsCount = ko.observable(parameters.roundsCount).extend({
        required: {
          onlyIf: () => {
            return this.canSeeRoundsCount();
          }
        }
      });


      this.canSeeTimeToWork = ko.computed(() => {
        if (!this.selectedWorkoutType()) {
          return false;
        }
        var selectedType = this.selectedWorkoutType().Value;
        return selectedType == WorkoutTypes.AMRAP
          || selectedType == WorkoutTypes.EMOM
          || selectedType == WorkoutTypes.E2MOM;
      });



      this.timeToWork = ko.observable(parameters.timeToWork)
        .extend({
          required: {
            onlyIf: () => {
              return this.canSeeTimeToWork();
            }
          }
        });

      this.canCreateWorkout = ko.computed(() => {
        return this.simpleRoutines().length > 0;
      });
    }

    removeSimpleRoutineFromToDo = (index) =>  {
        this.simpleRoutines.splice(index(), 1);
    };

    

    toJSON = () => {
        let model = {
          title: this.title(),
          roundsCount: this.roundsCount(),
          timeToWork: this.timeToWork(),
          restBetweenExercises: this.restBetweenExercises(),
          restBetweenRounds: this.restBetweenRounds(),
          workoutTypeViewModel: this.selectedWorkoutType().Value,
            exercisesToDoList: []
        };
        $.each(this.simpleRoutines(), (index, routine) => {
          let innerRoutineJson = routine.toJSON();
          model.exercisesToDoList.push(innerRoutineJson);
        });

        return model;
    };


   
  };
}