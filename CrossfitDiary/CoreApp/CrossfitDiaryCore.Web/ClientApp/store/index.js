// index.ts
import Vue from "vue";
import Vuex from "vuex";
import { profile } from "./../profile/index";
Vue.use(Vuex);
var store = {
    state: {
        version: "1.0.0" // a simple property
    },
    modules: {
        profile: profile
    }
};
export default new Vuex.Store(store);
//# sourceMappingURL=index.js.map