import axios from "axios";
import { ToLogWorkoutViewModel } from "./models/viewModels/ToLogWorkoutViewModel";
import { ExerciseViewModel } from "./models/viewModels/ExerciseViewModel";
var CrossfitterService = /** @class */ (function () {
    function CrossfitterService() {
        //    public createAndLogWorkout = (model: { newWorkoutViewModel: WorkoutViewModel, logWorkoutViewModel: ToLogWorkoutViewModel }) => {
        ////      this.isDataLoading(true);
        //      return this.post(this.pathToApp + "api/createAndLogNewWorkout", model);
        //    };
        //
        this.getAllCrossfittersWorkouts = function (userId, exerciseId, page, pageSize) {
            //      this.isDataLoading(true);
            return axios
                .get("api/getAllCrossfittersWorkouts?exerciseId=" + exerciseId + "&page=" + page + "&pageSize=" + pageSize)
                .then(function (jsonData) {
                return jsonData.data.map(function (x) {
                    return new ToLogWorkoutViewModel().deserialize(x);
                });
            });
        };
        //
        this.getExercises = function () {
            //      this.isDataLoading(true);
            return axios.get("api/getExercises").then(function (jsonData) {
                return jsonData.data.map(function (x) { return new ExerciseViewModel().deserialize(x); });
            });
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
        this.removeWorkout = function (crossfitterWorkoutId) {
            return axios.delete("api/removeWorkout/" + crossfitterWorkoutId);
        };
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