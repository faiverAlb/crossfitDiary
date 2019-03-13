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
        this.getAllCrossfittersWorkouts = function (page, pageSize) {
            return axios.get("api/getAllCrossfittersWorkouts?page=" + page + "&pageSize=" + pageSize).then(function (jsonData) {
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
        this.quickLogWorkout = function (logModel) {
            return axios.post("api/quickLogWorkout", logModel);
        };
        this.setShowOnlyUserWods = function (showOnlyUserWods) {
            return axios.post("api/setShowOnlyUserWods?showOnlyUserWods=" + showOnlyUserWods);
        };
    }
    return CrossfitterService;
}());
export default CrossfitterService;
//# sourceMappingURL=CrossfitterService.js.map