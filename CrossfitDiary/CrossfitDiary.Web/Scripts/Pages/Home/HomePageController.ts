module Pages {
  import BaseController = General.BaseController;
  import CrossfitterService = General.CrossfitterService;
  import ErrorMessageViewModel = General.ErrorMessageViewModel;

  export class HomePageController extends  BaseController {
    /* Сivilians */
    private allWorkouts: any;
    private _service: CrossfitterService;
    private errorMessager: ErrorMessageViewModel;

    /* Observables */
    /* Computeds */

    constructor(public parameters: { viewModel: { allWorkouts }, pathToApp: string }) {
      super();
      this.errorMessager = new ErrorMessageViewModel();
      this._service = new CrossfitterService(parameters.pathToApp, this.isDataLoading);
    }

    private removeWorkout = (crossfitterWorkoutId: number) => {
      this.isDataLoading(true);
      this._service.removeWorkout(crossfitterWorkoutId)
        .then(() => {
          this.isDataLoading(false);
          window.location.href = "/Home";
        })
        .fail((response) => {
          this.isDataLoading(false);
          this.errorMessager.addMessage(response.responseText, false);
        });
    }
  };
}