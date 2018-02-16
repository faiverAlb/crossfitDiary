module General {
  export class BaseController {
    _isDataLoading: KnockoutObservable<boolean>;

    constructor() {
      this._isDataLoading = ko.observable<boolean>(false);
    }
  }
}