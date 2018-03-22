﻿module Pages {
  import CrossfitterService = General.CrossfitterService;
  import PersonExerciseRecord = Models.PersonExerciseRecord;
  import ObservablePersonExerciseRecord = Models.ObservablePersonExerciseRecord;

  export class PrideController extends General.FilterableViewModel {
    /* Сivilians */
    private _service: CrossfitterService;

    /* Observables */
    private _exercises: KnockoutObservableArray<any>;
    private _selectedExercise: KnockoutObservable<any>;
    private _personMaximums: KnockoutObservableArray<PersonExerciseRecord>;
    private _allPersonsMaximums: KnockoutObservableArray<ObservablePersonExerciseRecord>;
    isDataLoading: KnockoutObservable<boolean>;

    /* Computeds */

    constructor(basicParameters: General.BasicParameters) {
      super();
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
          });
      });

    }

    private loadExercises = () => {
      this._service.getStatisticalExercises()
        .then((exercises: any) => {
          this._exercises(exercises);
        });
    };
  }



}