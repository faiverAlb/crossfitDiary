import { getters } from "./getters";
import { actions } from "./actions";
import { mutations } from "./mutations";
export var state = {
    exercises: [],
    user: undefined,
    error: false
};
var namespaced = true;
export var workoutEdit = {
    namespaced: namespaced,
    state: state,
    getters: getters,
    actions: actions,
    mutations: mutations
};
//# sourceMappingURL=index.js.map