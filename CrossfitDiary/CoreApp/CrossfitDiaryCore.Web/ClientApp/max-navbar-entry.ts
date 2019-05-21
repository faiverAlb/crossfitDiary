/* Font awesome icons */
import { dom } from "@fortawesome/fontawesome-svg-core";
dom.watch(); // This will kick of the initial replacement of i to svg tags and configure a MutationObserver

/* public components */
import Vue from "vue";
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
  el: "#nav-bar-max-container",
  template: `
  <li class="nav-item mr-1">
  <a class="nav-link btn btn-info text-light d-inline-block d-md-none p-0" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">
      <div class="small-action-link-button">
          <div class="icon-container">
              <svg class="svg-inline w-18"><use xlink:href="#trophy-icon"></use></svg>
          </div>
          <div class="text-container">MAX</div>
      </div>
      <span class="d-none d-md-inline">Vue Weight maximums</span>
  </a>
  <a class="nav-link btn btn-info text-light d-none d-md-inline-block" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">
      <svg class="svg-inline w-18"><use xlink:href="#trophy-icon"></use></svg>
      <span class="d-none d-md-inline">Vue Weight maximums</span>
  </a>
</li>
    `,
  components: {
  }
});
