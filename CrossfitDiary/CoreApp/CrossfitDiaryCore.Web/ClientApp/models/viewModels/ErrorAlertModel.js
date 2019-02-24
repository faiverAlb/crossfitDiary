var ErrorAlertModel = /** @class */ (function () {
    function ErrorAlertModel() {
        this.errorMessage = "";
        this.isAlertVisible = false;
        this.errorMessage = "";
        this.isAlertVisible = false;
    }
    ErrorAlertModel.prototype.setError = function (errorMessage) {
        this.errorMessage = errorMessage;
        this.isAlertVisible = errorMessage.length > 0;
    };
    return ErrorAlertModel;
}());
export { ErrorAlertModel };
//# sourceMappingURL=ErrorAlertModel.js.map