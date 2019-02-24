// profile/actions.ts
import CrossfitterService from "../CrossfitterService";
export var actions = {
    // tslint:disable-next-line:typedef
    fetchExercises: function (_a) {
        var commit = _a.commit;
        var crossfitterService = new CrossfitterService();
        crossfitterService.getExercises().then(function (data) {
            var exercises = data;
            commit("exercisesLoaded", exercises);
        });
    },
    // tslint:disable-next-line:typedef
    setCanUserSeePlanWorkouts: function (_a, newValue) {
        var commit = _a.commit;
        commit("canUserSeePlanWorkoutsConfigured", newValue);
    }
};
// }
//# sourceMappingURL=actions.js.map