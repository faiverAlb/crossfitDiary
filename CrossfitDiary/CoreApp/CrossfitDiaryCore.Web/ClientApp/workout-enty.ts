 import Vue from "vue";
import "./style/persons.scss";
import { ToLogWorkoutViewModel } from "./models/viewModels/ToLogWorkoutViewModel";

import PersonsActivitiesComponent from "./components/person-activities-component.vue";
import CrossfitterService from "./CrossfitterService";
const apiService = new CrossfitterService();

import { dom } from "@fortawesome/fontawesome-svg-core";

import Spinner from "vue-spinner-component/src/Spinner.vue";
import { SpinnerModel } from "./models/viewModels/SpinnerModel";
import { ErrorAlertModel } from "./models/viewModels/ErrorAlertModel";
import ErrorAlertComponent from "./components/error-alert-component.vue";

dom.watch(); // This will kick of the initial replacement of i to svg tags and configure a MutationObserver

new Vue({
  el: "#ManageWorkoutPageContainer",
  template: `
    <div class="manage-workout-page">
     
    </div>
    `,
  components: {
    Spinner,
    ErrorAlertComponent
  },
  data() {
    return {
      spinner: new SpinnerModel(true),
      errorAlertModel: new ErrorAlertModel()
    };
  },
  mounted() {
  }
});
