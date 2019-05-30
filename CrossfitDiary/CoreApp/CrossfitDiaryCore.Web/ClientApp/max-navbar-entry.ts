/* Font awesome icons */
import { dom } from "@fortawesome/fontawesome-svg-core";
dom.watch(); // This will kick of the initial replacement of i to svg tags and configure a MutationObserver

/* public components */
import Vue from "vue";
/* app components */
import WeightMaximumNavComponent from "./components/navigation-components/weight-maximum-nav-component.vue";
import CrossfitterService from "./CrossfitterService";
import ErrorAlertComponent from "./components/error-alert-component.vue";
/* models and styles */
import "./style/persons.scss";
import { ToLogWorkoutViewModel } from "./models/viewModels/ToLogWorkoutViewModel";
import { SpinnerModel } from "./models/viewModels/SpinnerModel";
import { ErrorAlertModel } from "./models/viewModels/ErrorAlertModel";
import { WorkoutViewModel } from "./models/viewModels/WorkoutViewModel";
const apiService: CrossfitterService = new CrossfitterService();

let vue = new Vue({
  el: "#nav-bar-max-container",
  template: `
  <li class="nav-item mr-1">
  <WeightMaximumNavComponent />
  </li>
    `,
  components: { WeightMaximumNavComponent }
});
