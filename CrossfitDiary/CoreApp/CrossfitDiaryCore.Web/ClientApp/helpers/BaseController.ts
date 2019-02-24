module General {
  export class BaseController {
//    isDataLoading: KnockoutObservable<boolean>;

    constructor() {
//      this.isDataLoading = ko.observable<boolean>(false);
    }
  }

  export interface Serializable<T> {
    deserialize(input: any): T;
  }
}