module Pages {
  import BaseController = General.BaseController;
  import CrossfitterService = General.CrossfitterService;

  export class HomePageController extends  BaseController {
    private allWorkouts: any;
    private _service: CrossfitterService;

    constructor(public parameters: { viewModel: { allWorkouts }, pathToApp: string }) {
      super();
      this.allWorkouts = this.parameters.viewModel.allWorkouts;
      this._service = new CrossfitterService(parameters.pathToApp);
    }

    private removeWorkout = (crossfitterWorkoutId: number) => {
      this._service.removeWorkout(crossfitterWorkoutId)
        .finally(() => {
          window.location.href = "/Home";
        });
    }
  };
}