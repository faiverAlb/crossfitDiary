export class ErrorAlertModel {
  public errorMessage: string = "";
  public isAlertVisible: boolean = false;

  constructor() {
    this.errorMessage = "";
    this.isAlertVisible = false;
  }

  public setError(errorMessage: string) {
    this.errorMessage = errorMessage;
    this.isAlertVisible = errorMessage.length > 0;
  }
}
