module Pages {
  import BaseController = General.BaseController;
  import CrossfitterService = General.CrossfitterService;
  import ErrorMessageViewModel = General.ErrorMessageViewModel;
  import ToLogWorkoutViewModel = Models.ToLogWorkoutViewModel;

  export class HomePageController extends  BaseController {
    /* Сivilians */
    private allWorkouts: KnockoutObservable<ToLogWorkoutViewModel[]>;
    private _service: CrossfitterService;
    private errorMessager: ErrorMessageViewModel;

    /* Observables */
    /* Computeds */

    constructor(public parameters: { pathToApp: string, userId:string, exerciseId?:number }) {
      super();
      this.errorMessager = new ErrorMessageViewModel();
      this._service = new CrossfitterService(parameters.pathToApp, this.isDataLoading);
      this._service.getAvailableWorkouts();
      this.allWorkouts = ko.observable([]);

      this.initialLoading();
    }


    private initialLoading = () => {
      debugger;
      this._service.getAllCrossfittersWorkouts(this.parameters.userId, this.parameters.exerciseId)
        .then((data) => {
          this.allWorkouts(data);
        })
        .fail((response) => {
          this.isDataLoading(false);
          this.errorMessager.addMessage(response.responseText, false);
        });

    };

    private removeWorkoutConfirmation = (crossfitterWorkoutId: number) => {

      ko.utils.showModalFromTemplate({
        templateName: TemplatesNames.ConfirmToRemoveWorkout,
        model: {crossfitterWorkoutId: crossfitterWorkoutId},
        title: "Sure to remove workout?",
        onOkModel: {
          okFunction: this.removeWorkout,
          okText: "Delete",
          cssClass: "btn-danger"
        }
      });
    };

    private removeWorkout = (model: { crossfitterWorkoutId: number}) => {
      this.isDataLoading(true);
      this._service.removeWorkout(model.crossfitterWorkoutId)
        .then(() => {
          this.isDataLoading(false);
          window.location.href = "/";
        })
        .fail((response) => {
          this.isDataLoading(false);
          this.errorMessager.addMessage(response.responseText, false);
        });
    }
  };
}