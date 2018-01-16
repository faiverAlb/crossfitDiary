class CrossfitterService extends BaseService {

    constructor(public pathToApp: string) {
        super();
    }

    createWorkout = model => {
        return this.post(this.pathToApp + "api/createWorkout", model)
            .finally(() => {
                window.location.href = "/Home";
            });
    };

    logWorkout = (model) => {
        return this.post(this.pathToApp + "api/logWorkout", model)
            .finally(() => {
                window.location.href = "/Home";
            });
    };

    createAndLogWorkout = (model) => {
        return this.post(this.pathToApp + "api/createAndLogNewWorkout", model)
            .finally(() => {
                window.location.href = "/Home";
            });
    };

    getAvailableWorkouts = () => {
        return this.get(this.pathToApp + "api/getAvailableWorkouts");
    };

    getExercises = () => {
        return this.get(this.pathToApp + "api/getExercises");
    };
}