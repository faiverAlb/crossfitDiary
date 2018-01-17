class PrideController extends General.FilterableViewModel {

    _service: CrossfitterService;
    _exercises: KnockoutObservableArray<any>;
    _selectedExercise: KnockoutObservable<any>;

    _personMaximums: KnockoutObservableArray<PersonExerciseRecord>;

    constructor(basicParameters: General.IBasicParameters) {
        super();
        this._service = new CrossfitterService(basicParameters.pathToApp);
        this._exercises = ko.observableArray([]);
        this._personMaximums = ko.observableArray([]);
        this._selectedExercise = ko.observable();

        this.initiateFiltering(this._exercises, [{ value: "personName" }, { value: "date" }]);



        this.loadExercises();

        ko.computed(() => {
            const exercise = this._selectedExercise();
            if (!exercise) {
                return;
            }
            this._service.getPersonExerciseMaximumWeight(exercise.id)
                .then((personMaximums: PersonExerciseRecord[]) => {
                    this._personMaximums(personMaximums);
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

