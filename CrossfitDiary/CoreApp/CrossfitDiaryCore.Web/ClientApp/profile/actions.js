// profile/actions.ts
import axios from "axios";
export var actions = {
    fetchData: function (_a) {
        var commit = _a.commit;
        axios({
            url: "https://...."
        }).then(function (response) {
            var payload = response && response.data;
            commit("profileLoaded", payload);
        }, function (error) {
            console.log(error);
            commit("profileError");
        });
    }
};
//# sourceMappingURL=actions.js.map