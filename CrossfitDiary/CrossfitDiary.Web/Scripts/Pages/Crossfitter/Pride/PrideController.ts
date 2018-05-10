module Pages {
  import CrossfitterService = General.CrossfitterService;
  import PersonExerciseRecord = Models.PersonExerciseRecord;
  import ObservablePersonExerciseRecord = Models.ObservablePersonExerciseRecord;
  import ExerciseViewModel = Models.ExerciseViewModel;
  import ErrorMessageViewModel = General.ErrorMessageViewModel;

  export class PrideController extends General.FilterableViewModel {
    /* Сivilians */
    private _service: CrossfitterService;
    private errorMessager: ErrorMessageViewModel;

    /* Observables */
    private _exercises: KnockoutObservableArray<ExerciseViewModel>;
    private _selectedExercise: KnockoutObservable<ExerciseViewModel>;
    private _personMaximums: KnockoutObservableArray<PersonExerciseRecord>;
    private _allPersonsMaximums: KnockoutObservableArray<ObservablePersonExerciseRecord>;
    isDataLoading: KnockoutObservable<boolean>;

    /* Computeds */

    constructor(basicParameters: General.BasicParameters) {
      super();

      this.errorMessager = new ErrorMessageViewModel();



      this.isDataLoading = ko.observable(false);
      this._service = new CrossfitterService(basicParameters.pathToApp, this.isDataLoading);
      this._exercises = ko.observableArray([]);
      this._personMaximums = ko.observableArray([]);
      this._allPersonsMaximums = ko.observableArray([]);

      this._selectedExercise = ko.observable();

      this.initiateFiltering(this._allPersonsMaximums,
        [{ value: "personName" }, { value: "date" }, { value: "workoutTitle" }]);


      this.loadExercises();

      ko.computed(() => {
        const exercise = this._selectedExercise();
        if (!exercise) {
          return;
        }
        this._service.getPersonExerciseMaximumWeight(exercise.id)
          .then((personMaximums: PersonExerciseRecord[]) => {
            this._personMaximums(personMaximums);
          })
          .then(() => {
            return this._service.getAllPersonsExerciseMaximumWeights(exercise.id);
          })
          .then((allPersonsRecords: PersonExerciseRecord[]) => {
            this._allPersonsMaximums($.map(allPersonsRecords,
              item => new ObservablePersonExerciseRecord(item.personName,
                item.maximumWeight,
                item.date,
                item.workoutTitle,
                item.positionBetweenOthers,
                item.isItMe)));
          })
          .fail((response) => {
            this.errorMessager.addMessage(response.responseText, false);
          });
      });

    }

    private loadExercises = () => {
      this._service.getStatisticalExercises()
        .then((exercises) => {
          this._exercises(exercises);
        })
        .fail((response) => {
          this.errorMessager.addMessage(response.responseText, false);
        });
    };
  }



}