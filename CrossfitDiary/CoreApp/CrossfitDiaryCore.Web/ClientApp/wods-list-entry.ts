/* Font awesome icons */
import { dom } from "@fortawesome/fontawesome-svg-core";
dom.watch(); // This will kick of the initial replacement of i to svg tags and configure a MutationObserver

/* public components */
import Vue from "vue";
import Spinner from "vue-spinner-component/src/Spinner.vue";

/* app components */
import CrossfitterService from "./CrossfitterService";
import ErrorAlertComponent from "./components/error-alert-component.vue";

/* models and styles */
import "./style/wods-list.scss";

import { ToLogWorkoutViewModel } from "./models/viewModels/ToLogWorkoutViewModel";
import { SpinnerModel } from "./models/viewModels/SpinnerModel";
import { ErrorAlertModel } from "./models/viewModels/ErrorAlertModel";
import { WorkoutViewModel } from "./models/viewModels/WorkoutViewModel";
const apiService: CrossfitterService = new CrossfitterService();


let vue = new Vue({
  el: "#wods-library-container",
  template: `
    <div class="wods-library-container">
      <h3>WODS library from entry</h3>
    </div>
    `,
  components: {
    Spinner,
    ErrorAlertComponent
  },
  data() {
    return {
      plannedWorkouts: WorkoutViewModel,
      spinner: new SpinnerModel(true),
      errorAlertModel: new ErrorAlertModel()
    };
  },
  mounted() {
    this.spinner.activate();
    apiService
      .getPlannedWorkoutsForToday()
      .then(data => {
        this.plannedWorkouts = data;
      })
      .catch(data => {
        this.errorAlertModel.setError(data.response.statusText);
      });
    },
  methods: {}
});
