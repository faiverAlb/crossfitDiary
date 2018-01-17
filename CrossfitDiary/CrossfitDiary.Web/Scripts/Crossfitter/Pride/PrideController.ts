class PrideController {

    _service: CrossfitterService;
    _exercises: KnockoutObservableArray<any>;
    _selectedExercise: KnockoutObservable<any>;

    constructor(basicParameters: General.IBasicParameters) {
        this._service = new CrossfitterService(basicParameters.pathToApp);
        this._exercises = ko.observableArray([]);
        this._selectedExercise = ko.observable();

        this.loadExercises();

        ko.computed(() => {
            let exercise = this._selectedExercise();
            if (!exercise) {
                return;
            }
            debugger;
        });

    }

    private loadExercises = () => {
        this._service.getStatisticalExercises()
            .then((exercises:any) => {
                this._exercises(exercises);
            });
    };


}