module General {
  export class ErrorMessageViewModel {
//    message: KnockoutObservable<string> = ko.observable<string>(null);
//    isDismissable: KnockoutObservable<boolean>;
//    hasMessage: KnockoutComputed<boolean>;

    constructor() {
//      this.message = ko.observable<string>(null);
//      this.isDismissable = ko.observable(true);
//      this.hasMessage = ko.pureComputed(() => this.message() !== null && this.message() !== undefined);
    }

    addMessage(message: string, isDismissable: boolean = true, scrollToError: boolean = true): void {
//      this.message(message);
//      this.isDismissable(isDismissable);
///
//      if (scrollToError) {
//        let elem = document.getElementsByClassName("errors-container")[0];
//        if (elem) {
//          elem.scrollIntoView({ behavior: "smooth" });
//        };
//      };
    };

    deleteMessage(): void {
//      this.message(null);
//      this.isDismissable(true);
    };

    ajaxFail(xmlRequest: any): void {
//      if (xmlRequest.responseText) {
//        this.message(`${xmlRequest.statusText} : "${xmlRequest.responseText}"`);
//        return;
//      }

//      if (xmlRequest.statusText) {
//        this.message(xmlRequest.statusText);
//        return;
//      }

//      this.message(xmlRequest.stack);
    }
  }
}