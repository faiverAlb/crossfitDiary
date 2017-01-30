var modalLoading: any;
class CrossfitterService {
    
    constructor(public pathToApp: string) {
        
    }

    createWorkout = function (model) {
        modalLoading.init(true);
        return $.ajax({
            type: 'POST',
            url: this.pathToApp + "api/createWorkout",
            data: model,
            async: false
        }).done(function () {
            modalLoading.init(false);
            window.location.href = "/Home";
        });
    };

    logWorkout = function (model) {
        modalLoading.init(true);
        return $.post({
            url: this.pathToApp + "api/logWorkout",
            data: model
        }).done(function () {
            modalLoading.init(false);
            window.location.href = "/Home";
        });
    };

    createAndLogWorkout = function (model) {
        modalLoading.init(true);

        return $.post({
            url: this.pathToApp + "api/createAndLogNewWorkout",
            data: model
        }).done(function () {
            modalLoading.init(false);
            window.location.href = "/Home";
        });
    };
}
