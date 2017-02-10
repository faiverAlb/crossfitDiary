class CrossfitterService {
    
    constructor(public pathToApp: string) {
        
    }

    createWorkout =  model => {

        return Q.Promise((resolve, reject) => {
            return $.ajax({
                url: this.pathToApp + "api/createWorkout",
                method: 'POST',
                success: (data) => resolve(data),
                error: (error) => reject(error),
                data: model
            });
        }).finally(() => {
            window.location.href = "/Home";
        });
    };

    logWorkout = (model) => {

        return Q.Promise((resolve, reject) => {
            return $.ajax({
                url: this.pathToApp + "api/logWorkout",
                method: "POST",
                success: (data) => resolve(data),
                error: (error) => reject(error),
                data: model
            });
        }).finally(() => {
            window.location.href = "/Home";
        });
    };

    createAndLogWorkout = (model) => {

        return Q.Promise((resolve, reject) => {
            return $.ajax({
                url: this.pathToApp + "api/createAndLogNewWorkout",
                method: "POST",
                success: (data) => resolve(data),
                error: (error) => reject(error),
                data: model
            });
        }).finally(() => {
            window.location.href = "/Home";
        });
    };

    getAvailableWorkouts = () => {
        return Q.Promise((resolve, reject) => {
            return $.ajax({
                url: this.pathToApp + "api/getAvailableWorkouts",
                method: "GET",
                success: (data) => resolve(data),
                error: (error) => reject(error)
            });
        }).finally(() => {

        });
    };

    getExercises = () => {
        return Q.Promise((resolve, reject) => {
            return $.ajax({
                url: this.pathToApp + "api/getExercises",
                method: "GET",
                success: (data) => resolve(data),
                error: (error) => reject(error)
            });
        }).finally(() => {
        });
    };

}
