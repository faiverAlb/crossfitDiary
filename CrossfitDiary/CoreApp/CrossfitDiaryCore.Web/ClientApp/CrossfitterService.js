import axios from "axios";
import { ToLogWorkoutViewModel } from "./models/viewModels/ToLogWorkoutViewModel";
import { ExerciseViewModel } from "./models/viewModels/ExerciseViewModel";
import { WorkoutViewModel } from "./models/viewModels/WorkoutViewModel";
var CrossfitterService = /** @class */ (function () {
    function CrossfitterService() {
        // tslint:disable-next-line:max-line-length
        this.createAndLogWorkout = function (model) {
            return axios.post("api/createAndLogNewWorkout", model);
        };
        // tslint:disable-next-line:max-line-length
        this.createAndPlanWorkout = function (workoutViewModel) {
            return axios.post("api/createAndPlanWorkout", workoutViewModel);
        };
        // tslint:disable-next-line:max-line-length
        this.getAllCrossfittersWorkouts = function (userId, exerciseId, page, pageSize) {
            return axios.get("api/getAllCrossfittersWorkouts?exerciseId=" + exerciseId + "&page=" + page + "&pageSize=" + pageSize).then(function (jsonData) {
                return jsonData.data.map(function (x) { return new ToLogWorkoutViewModel().deserialize(x); });
            });
        };
        this.getPlannedWorkoutsForToday = function () {
            return axios.get("api/getPlannedWorkoutsForToday").then(function (jsonData) {
                return jsonData.data.map(function (x) { return new WorkoutViewModel().deserialize(x); });
            });
        };
        this.getExercises = function () {
            return axios.get("api/getExercises").then(function (jsonData) {
                return jsonData.data.map(function (x) { return new ExerciseViewModel().deserialize(x); });
            });
        };
        this.removeWorkout = function (crossfitterWorkoutId) {
            return axios.delete("api/removeWorkout/" + crossfitterWorkoutId);
        };
        //
        //    public getPersonMaximums = (): Q.Promise<PersonExerciseRecord[]> => {
        ////      this.isDataLoading(true);
        //      return this.get<PersonExerciseRecord[]>(this.pathToApp + "api/getPersonMaximums")
        //        .then((jsonData) => {
        //          return jsonData.map(x => new PersonExerciseRecord().deserialize(x));
        //        });
        ////        .finally(() => { this.isDataLoading(false); });
        //    };
        //
        //    public getStatisticalExercises = (): Q.Promise<ExerciseViewModel[]> => {
        ////      this.isDataLoading(true);
        //      return this.get<ExerciseViewModel[]>(this.pathToApp + "api/getStatisticalExercises")
        //        .then((jsonData) => {
        //          return jsonData.map(x => new ExerciseViewModel().deserialize(x));
        //        });
        ////        .finally(() => { this.isDataLoading(false); });
        //    };
        //
        //    public getPersonExerciseMaximumWeight = (exerciseId: number) => {
        ////      this.isDataLoading(true);
        //      return this.get<PersonExerciseRecord[]>(this.pathToApp + `api/exercises/${exerciseId}/personMaximum`)
        //        .then((jsonData) => {
        //          return jsonData.map(x => new PersonExerciseRecord().deserialize(x));
        //        });
        ////        .finally(() => { this.isDataLoading(false); });
        //    };
        //
        //    public getAllPersonsExerciseMaximumWeights = (exerciseId: number) => {
        ////      this.isDataLoading(true);
        //      return this.get<PersonExerciseRecord[]>(this.pathToApp + `api/exercises/${exerciseId}/allPersonsMaximums`)
        //        .then((jsonData) => {
        //          return jsonData.map(x => new PersonExerciseRecord().deserialize(x));
        //        });
        ////        .finally(() => { this.isDataLoading(false); });
        //    };
        //
        //
        //    public getPersonLoggingInfo = (preselectedCrossfitterWorkoutId: number): Q.Promise<ToLogWorkoutViewModel> => {
        ////      this.isDataLoading(true);
        //      return this.get<ToLogWorkoutViewModel>(this.pathToApp +
        //          `api/getPersonLoggingInfo/${preselectedCrossfitterWorkoutId}`)
        //        .then((jsonData) => {
        //          return new ToLogWorkoutViewModel().deserialize(jsonData);
        //        });
        ////        .finally(() => {
        ////          this.isDataLoading(false);
        ////        });
        //    };
    }
    return CrossfitterService;
}());
export default CrossfitterService;
//# sourceMappingURL=CrossfitterService.js.map