﻿module Pages {
  import CrossfitterService = General.CrossfitterService;
  import BasicParameters = General.BasicParameters;
  import BaseController = General.BaseController;
  import ErrorMessageViewModel = General.ErrorMessageViewModel;
  import ToLogWorkoutViewModel = Models.ToLogWorkoutViewModel;
  import WorkoutViewModel = Models.WorkoutViewModel;
  import ExerciseViewModel = Models.ExerciseViewModel;
  import WorkoutViewModelObservable = Models.WorkoutViewModelObservable;
  import BaseKeyValuePairModel = General.BaseKeyValuePairModel;
  import WorkoutType = Models.WorkoutType;
  import PersonExerciseRecord = Models.PersonExerciseRecord;

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
  export class ManageWorkoutController extends BaseController {

    /* Сivilians */
    private _service: CrossfitterService;
    private errorMessager: ErrorMessageViewModel;

    private _isEditMode: boolean;
    private _isRepeatMode: boolean;
    private _isDefaultMode: boolean;

    /* Observables */
    private _logWorkoutController: KnockoutObservable<LogWorkoutController>;
    public workoutToDisplay: KnockoutObservable<WorkoutViewModelObservable>;
    private selectedWorkoutType: KnockoutObservable<BaseKeyValuePairModel<number, string>>;
    private selectedWorkoutTypeId: KnockoutObservable<number>;

    private preselectedWorkout: WorkoutViewModel;
    private _exercises: ExerciseViewModel[];
    private _personMaximums: PersonExerciseRecord[];

    /* Computeds */
    private selectedForTimeText: KnockoutComputed<string>;
    private selectedEmomText: KnockoutComputed<string>;

    constructor(public parameters: BasicParameters, preselectedWorkoutObject: object, public preselectedCrossfitterWorkoutId: number|null = null) {
      super();

      /* Сivilians */
      this._service = new CrossfitterService(parameters.pathToApp, this.isDataLoading);
      this.errorMessager = new ErrorMessageViewModel();
      this.preselectedWorkout = new WorkoutViewModel().deserialize(preselectedWorkoutObject);
      this._isEditMode = this.preselectedWorkout != null && preselectedCrossfitterWorkoutId != null;
      this._isRepeatMode = this.preselectedWorkout != null && preselectedCrossfitterWorkoutId == null;
      this._isDefaultMode = this.preselectedWorkout == null && preselectedCrossfitterWorkoutId == null;


      /* Observables */
      this._logWorkoutController = ko.observable(null);
     
      this.selectedWorkoutType = ko.observable(null);
      this.workoutToDisplay = ko.observable(null);

      let workout = this.workoutToDisplay();
      this.selectedWorkoutTypeId = ko.observable(workout == null ? null : workout.model.workoutType);

      if (workout != null) {
        this.handleLogWorkoutController(false);
      }

      /* Computed */
      this.selectedForTimeText = ko.computed(() => {
        if (this.selectedWorkoutTypeId() == null || this.selectedWorkoutTypeId() !== WorkoutType.ForTimeManyInners) {
          return "FT";
        }
        return "FT*n";
      });


      this.selectedEmomText = ko.computed(() => {
        if (this.selectedWorkoutTypeId() == null || this.selectedWorkoutTypeId() !== WorkoutType.E2MOM) {
          return "EMOM";
        }
        return "E2MOM";
      });
      
      this.selectedWorkoutType.subscribe((selectedWorkoutType: BaseKeyValuePairModel<number, string>) => {
        if ((selectedWorkoutType == undefined || selectedWorkoutType == null) ) {
          this.handleLogWorkoutController(true);
          if (this._isEditMode === false) {
            this.workoutToDisplay(null);
          }
          return;
        }
        this.selectedWorkoutTypeId(this.selectedWorkoutType().id);
        let model = new WorkoutViewModel({
          workoutType: this.selectedWorkoutType().id,
          exercisesToDoList: [],
          children: [],
          isInnerWorkout:false
        });

        this.workoutToDisplay(new WorkoutViewModelObservable(model, this._exercises));
        this.handleLogWorkoutController(false);
      });

      this.loadExercises()
        .then(() => {
          return this._service
            .getPersonMaximums()
            .then((personMaximums: PersonExerciseRecord[]) => {
              this._personMaximums = personMaximums;
            });
        })
        .then(() => {
          if (this._isEditMode === false && this._isRepeatMode === false) {
            this.selectedWorkoutType(new BaseKeyValuePairModel(WorkoutType.ForTime, WorkoutType[WorkoutType.ForTime]));
          }
        })
        .then(() => {
          if (this.preselectedWorkout != null) {
            this.workoutToDisplay(new WorkoutViewModelObservable(this.preselectedWorkout, this._exercises));
            this.selectedWorkoutTypeId(this.preselectedWorkout.workoutType);
          }
        })
        .then(() => {
          if (this._isRepeatMode) {
            this.handleLogWorkoutController(false);
          }
          return this._isEditMode ? this.loadPersonLogging() : null;
        });
    }

    private selectWorkoutType(workoutType: number) {
      this.selectedWorkoutType(new BaseKeyValuePairModel(workoutType, WorkoutType[workoutType]));
    }

    private handleLogWorkoutController = (needToCleanLog: boolean, logModel?: ToLogWorkoutViewModel) => {
      if (needToCleanLog) {
        this._logWorkoutController(null);
        return;
      }
      this._logWorkoutController(new LogWorkoutController(this.workoutToDisplay(), true, this._service, this.errorMessager, this._isEditMode, logModel));
    }


    private loadExercises = () => {
      return this._service
        .getExercises()
        .then((exercises: ExerciseViewModel[]) => {
          this._exercises = exercises;
        });
    };

    private loadPersonLogging = () => {
      return this._service.getPersonLoggingInfo(this.preselectedCrossfitterWorkoutId)
        .then((logModel: ToLogWorkoutViewModel) => {
          this.handleLogWorkoutController(false, logModel);
        })
        .fail((response) => {
          this.errorMessager.addMessage(response.responseText, false);
        });

    };
  }
}