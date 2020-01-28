// profile/actions.ts
import CrossfitterService from "../CrossfitterService";
export var actions = {
    fetchExercises: function (_a) {
        var commit = _a.commit;
        var crossfitterService = new CrossfitterService();
        crossfitterService.getExercises().then(function (data) {
            var exercises = data;
            commit("exercisesLoaded", exercises);
        });
    },
    // fetchUserMaximums({commit}): any {
    //     let crossfitterService: CrossfitterService = new CrossfitterService();
    //     crossfitterService.getExerciseMaximums().then(data => {
    //         const userMaximums: PersonMaximumViewModel[] = data;
    //         commit("userMaximumsLoaded", userMaximums);
    //     });
    // },
    setCanUserSeePlanWorkouts: function (_a, newValue) {
        var commit = _a.commit;
        commit("canUserSeePlanWorkoutsConfigured", newValue);
    },
    setIsFindMaxWeight: function (_a, newValue) {
        var commit = _a.commit;
        commit("isFindMaxWeightConfigured", newValue);
    }
};
// }
//# sourceMappingURL=actions.js.map