// index.ts
import Vue from "vue";
import Vuex from "vuex";
import { workoutEdit } from "./index";
Vue.use(Vuex);
var store = {
    state: {
        version: "1.0.0" // a simple property
    },
    modules: {
        workoutEdit: workoutEdit
    }
};
export default new Vuex.Store(store);
//# sourceMappingURL=workout-store.js.map