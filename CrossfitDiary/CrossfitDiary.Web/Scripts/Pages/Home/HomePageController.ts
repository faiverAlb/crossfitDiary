module Pages {
  import BaseController = General.BaseController;
  import CrossfitterService = General.CrossfitterService;
  import ErrorMessageViewModel = General.ErrorMessageViewModel;
  import ToLogWorkoutViewModel = Models.ToLogWorkoutViewModel;

  export class HomePageController extends  BaseController {
    /* Сivilians */
    private allWorkouts: KnockoutObservableArray<ToLogWorkoutViewModel>;
    private _service: CrossfitterService;
    private errorMessager: ErrorMessageViewModel;

    private page: number = 1;
    private pageSize: number = 10;

    /* Observables */
    private hasMoreElements : KnockoutObservable<boolean>;

    /* Computeds */

    constructor(public parameters: { pathToApp: string, userId:string, exerciseId?:number }) {
      super();
      this.errorMessager = new ErrorMessageViewModel();
      this._service = new CrossfitterService(parameters.pathToApp, this.isDataLoading);
      this.allWorkouts = ko.observableArray([]);
      this.hasMoreElements  = ko.observable(false);

      this.loadElements();
    }


    private loadElements = () => {
      
      this.isDataLoading(true);
      this._service.getAllCrossfittersWorkouts(this.parameters.userId, this.parameters.exerciseId, this.page, this.pageSize)
        .then((data) => {
          ko.utils.arrayPushAll(this.allWorkouts, data);
          this.hasMoreElements (data.length === this.pageSize);
        })
        .fail((response) => {
          this.errorMessager.addMessage(response.responseText, false);
        })
        .finally(() => {
          this.isDataLoading(false);
          this.page += 1;
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
          window.location.href = "/";
        })
        .fail((response) => {
          this.errorMessager.addMessage(response.responseText, false);
        })
        .finally(() => {
          this.isDataLoading(false);
        });
    }
  };
}