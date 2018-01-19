class HomePageController {
    private allWorkouts: any;
    private _service: CrossfitterService;

    constructor(public parameters: { viewModel: { allWorkouts }, pathToApp: string }) {
        this.allWorkouts = this.parameters.viewModel.allWorkouts;

        this._service = new CrossfitterService(parameters.pathToApp);

    }

    private removeWorkout = (crossfitterWorkoutId:number) => {
        this._service.removeWorkout(crossfitterWorkoutId)
            .finally(() => {
                window.location.href = "/Home";
            });
    }
};