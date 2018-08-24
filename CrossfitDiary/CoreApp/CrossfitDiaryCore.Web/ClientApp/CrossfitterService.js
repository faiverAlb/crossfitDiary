import axios from "axios";
import { ToLogWorkoutViewModel } from './models/viewModels/ToLogWorkoutViewModel';
var CrossfitterService = /** @class */ (function () {
    function CrossfitterService() {
        //    public createAndLogWorkout = (model: { newWorkoutViewModel: WorkoutViewModel, logWorkoutViewModel: ToLogWorkoutViewModel }) => {
        ////      this.isDataLoading(true);
        //      return this.post(this.pathToApp + "api/createAndLogNewWorkout", model);
        //    };
        //
        this.getAllCrossfittersWorkouts = function (userId, exerciseId, page, pageSize) {
            //      this.isDataLoading(true);
            return axios.get("api/getAllCrossfittersWorkouts?exerciseId=" + exerciseId + "&page=" + page + "&pageSize=" + pageSize)
                .then(function (jsonData) {
                debugger;
                return jsonData.map(function (x) { return new ToLogWorkoutViewModel().deserialize(x); });
            });
        };
    }
    return CrossfitterService;
}());
export default CrossfitterService;
//# sourceMappingURL=CrossfitterService.js.map