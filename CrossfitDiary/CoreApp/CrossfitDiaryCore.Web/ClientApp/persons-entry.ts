/* Font awesome icons */
import { dom } from "@fortawesome/fontawesome-svg-core";
dom.watch(); // This will kick of the initial replacement of i to svg tags and configure a MutationObserver

/* public components */
import Vue from "vue";
import Spinner from "vue-spinner-component/src/Spinner.vue";
/* app components */
import PersonsActivitiesComponent from "./components/person-activities-component.vue";
import PlannedWorkoutDisplayComponent from "./components/planned-workout-display-component.vue";
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
  el: "#home-page-container",
  template: `
    <div class="home-container">
      <div class="row">
        <div class="col">
          <ErrorAlertComponent :errorAlertModel="errorAlertModel"></ErrorAlertComponent>
        </div>
      </div>
      <PlannedWorkoutDisplayComponent :plannedWorkouts="plannedWorkouts"></PlannedWorkoutDisplayComponent>
      <div class="row">
        <div class="offset-5">
          <spinner
            :status="spinner.status"
            :size="spinner.size"
            :color="spinner.color"
            :depth="spinner.depth"
            :rotation="spinner.rotation"
            :speed="spinner.speed">
          </spinner>
        </div>
      </div>
      <PersonsActivitiesComponent :activities="activities"/>
    </div>
    `,
  components: {
    PersonsActivitiesComponent,
    PlannedWorkoutDisplayComponent,
    Spinner,
    ErrorAlertComponent
  },
  data() {
    return {
      activities: ToLogWorkoutViewModel[0],
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
    apiService
      .getAllCrossfittersWorkouts()
      .then(data => {
        this.activities = data;
        this.spinner.disable();
      })
      .catch(data => {
        this.errorAlertModel.setError(data.response.statusText);
        this.spinner.disable();
      });
  }
});
