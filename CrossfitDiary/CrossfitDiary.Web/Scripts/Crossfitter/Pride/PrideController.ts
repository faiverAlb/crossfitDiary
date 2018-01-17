class PrideController extends General.FilterableViewModel {

    _service: CrossfitterService;
    _exercises: KnockoutObservableArray<any>;
    _selectedExercise: KnockoutObservable<any>;

    _personMaximums: KnockoutObservableArray<PersonExerciseRecord>;
    _allPersonsMaximums: KnockoutObservableArray<ObservablePersonExerciseRecord>;

    constructor(basicParameters: General.IBasicParameters) {
        super();
        this._service = new CrossfitterService(basicParameters.pathToApp);
        this._exercises = ko.observableArray([]);
        this._personMaximums = ko.observableArray([]);
        this._allPersonsMaximums = ko.observableArray([]);

        this._selectedExercise = ko.observable();

        this.initiateFiltering(this._allPersonsMaximums, [{ value: "personName" }, { value: "date" }]);



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
                    this._allPersonsMaximums($.map(allPersonsRecords, item => new ObservablePersonExerciseRecord(item.personName,item.maximumWeight,item.date)));
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



class PersonExerciseRecord {
    personName: string;
    maximumWeight: string;
    date: string;
}

class ObservablePersonExerciseRecord {
    personName: KnockoutObservable<string>;
    maximumWeight: KnockoutObservable<string>;
    date: KnockoutObservable<string>;

    constructor(personName: string, maximumWeight: string, date: string) {
        this.personName = ko.observable(personName);
        this.maximumWeight = ko.observable(maximumWeight);
        this.date = ko.observable(date);
    }
}

