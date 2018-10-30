import Vue from "vue";
import VueRouter from "vue-router";

import "./style/manage-workout.scss";

import { dom } from "@fortawesome/fontawesome-svg-core";

import Spinner from "vue-spinner-component/src/Spinner.vue";
import { SpinnerModel } from "./models/viewModels/SpinnerModel";
import { ErrorAlertModel } from "./models/viewModels/ErrorAlertModel";
import ErrorAlertComponent from "./components/error-alert-component.vue";
import WorkoutsNavigationComponent from "./components/manage-workout/workouts-navigation-component.vue";
import AmrapEditComponent from "./components/manage-workout/workout-types/amrap-edit-component.vue";
import ForTimeEditComponent from "./components/manage-workout/workout-types/fortime-edit-component.vue";
import ForTimeNEditComponent from "./components/manage-workout/workout-types/fortime-n-edit-component.vue";
import EmomEditComponent from "./components/manage-workout/workout-types/emom-edit-component.vue";
import E2momEditComponent from "./components/manage-workout/workout-types/e2mom-edit-component.vue";
import NFTEditComponent from "./components/manage-workout/workout-types/nft-edit-component.vue";
import store from "./store/index";

Vue.use(VueRouter);

const routes = [
  { path: "/fortime", component: ForTimeEditComponent /*, alias: "/"*/ },
  { path: "/fortimen", component: ForTimeNEditComponent /*, alias: "/"*/ },
  { path: "/amrap", component: AmrapEditComponent },
  { path: "/emom", component: EmomEditComponent },
  { path: "/e2mom", component: E2momEditComponent },
  { path: "/amrap", component: AmrapEditComponent },
  { path: "/nft", component: NFTEditComponent },
  { path: "/", component: ForTimeEditComponent, redirect: "fortime" }
];

new Vue({
  el: "#manage-workout-page-container",
  template: `
   <div class="manage-workout-container my-2">
      <div>
        <div class="crossfitter-edit-workout-container">
                <div class="col-md-12 add-new-workout-container workout-container">
                  <div class="card">
                    <div class="card-header ">
                        <workouts-navigation-component></workouts-navigation-component>
                    </div>
                    <div class="card-body pt-0">
                      <router-view></router-view>
                    </div>
                  </div>
                </div>
        </div>
      </div>
  </div>
    `,
  store,
  router: new VueRouter({ routes }),
  components: {
    Spinner,
    ErrorAlertComponent,
    WorkoutsNavigationComponent
  },
  data() {
    return {
      errorAlertModel: new ErrorAlertModel()
    };
  },
  mounted() {}
});
