// profile/actions.ts
import axios from "axios";
import CrossfitterService from "../CrossfitterService";
export var actions = {
    // tslint:disable-next-line:typedef
    fetchData: function (_a) {
        var commit = _a.commit;
        var crossfitterService = new CrossfitterService();
        crossfitterService.getExercises().then(function (data) {
            debugger;
        });
        axios({ url: "https://...." }).then(function (response) {
            var payload = response && response.data;
            commit("profileLoaded", payload);
        }, function (error) {
            var payload = {
                email: "email@email.com",
                firstName: "Andrea",
                lastName: "Micher",
                phone: "111-22-33"
            };
            commit("profileLoaded", payload);
            // console.log(error);
            // commit("profileError");
        });
    },
    // tslint:disable-next-line:typedef
    doAction: function (_a, payload) {
        var commit = _a.commit;
        commit("changeUserName", payload.value);
    }
};
//# sourceMappingURL=actions.js.map