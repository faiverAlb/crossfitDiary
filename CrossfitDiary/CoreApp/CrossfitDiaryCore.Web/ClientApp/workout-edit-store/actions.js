// profile/actions.ts
import CrossfitterService from "../CrossfitterService";
export var actions = {
    // tslint:disable-next-line:typedef
    fetchData: function (_a) {
        var commit = _a.commit;
        var crossfitterService = new CrossfitterService();
        crossfitterService.getExercises().then(function (data) {
            var exercises = data;
            var payload = {
                email: "email@email.com",
                firstName: "Andrea",
                lastName: "Micher",
                phone: "111-22-33"
            };
            commit("profileLoaded", payload);
            commit("exercisesLoaded", exercises);
        });
    }
    // }
};
//# sourceMappingURL=actions.js.map