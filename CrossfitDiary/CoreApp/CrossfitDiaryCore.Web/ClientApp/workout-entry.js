import Vue from "vue";
import VueRouter from "vue-router";
import "./style/manage-workout.scss";
import Spinner from "vue-spinner-component/src/Spinner.vue";
import { ErrorAlertModel } from "./models/viewModels/ErrorAlertModel";
import ErrorAlertComponent from "./components/error-alert-component.vue";
import WorkoutsNavigationComponent from "./components/manage-workout/workouts-navigation-component.vue";
import AmrapEditComponent from "./components/manage-workout/workout-types/amrap-edit-component.vue";
import ForTimeEditComponent from "./components/manage-workout/workout-types/fortime-edit-component.vue";
import ForTimeNEditComponent from "./components/manage-workout/workout-types/fortime-n-edit-component.vue";
import EmomEditComponent from "./components/manage-workout/workout-types/emom-edit-component.vue";
import E2momEditComponent from "./components/manage-workout/workout-types/e2mom-edit-component.vue";
import NFTEditComponent from "./components/manage-workout/workout-types/nft-edit-component.vue";
import store from "./workout-edit-store/workout-store";
Vue.use(VueRouter);
var routes = [
    { path: "/fortime", component: ForTimeEditComponent /*, alias: "/"*/ },
    { path: "/fortimen", component: ForTimeNEditComponent /*, alias: "/"*/ },
    { path: "/amrap", component: AmrapEditComponent },
    { path: "/emom", component: EmomEditComponent },
    { path: "/e2mom", component: E2momEditComponent },
    { path: "/nft", component: NFTEditComponent },
    { path: "/", component: AmrapEditComponent, redirect: "amrap" }
];
var vue = new Vue({
    el: "#manage-workout-page-container",
    template: "\n   <div class=\"manage-workout-container my-2\">\n      <div>\n        <div class=\"crossfitter-edit-workout-container\">\n                <div class=\"col-md-12 add-new-workout-container workout-container\">\n                  <div class=\"card\">\n                    <div class=\"card-header\">\n                        <workouts-navigation-component></workouts-navigation-component>\n                    </div>\n                    <div class=\"card-body pt-0\">\n                      <router-view></router-view>\n                    </div>\n                  </div>\n                </div>\n        </div>\n      </div>\n  </div>\n    ",
    store: store,
    router: new VueRouter({ routes: routes }),
    components: {
        Spinner: Spinner,
        ErrorAlertComponent: ErrorAlertComponent,
        WorkoutsNavigationComponent: WorkoutsNavigationComponent
    },
    data: function () {
        // let test = new ToLogWorkoutViewModel().deserialize(workouter.toLogWorkoutRawModel);
        return {
            errorAlertModel: new ErrorAlertModel()
        };
    },
    mounted: function () { }
});
//# sourceMappingURL=workout-entry.js.map