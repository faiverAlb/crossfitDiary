import Vue from "vue";
import "./style/manage-workout.scss";

import CrossfitterService from "./CrossfitterService";
const apiService = new CrossfitterService();

import { dom } from "@fortawesome/fontawesome-svg-core";

import Spinner from "vue-spinner-component/src/Spinner.vue";
import { SpinnerModel } from "./models/viewModels/SpinnerModel";
import { ErrorAlertModel } from "./models/viewModels/ErrorAlertModel";
import ErrorAlertComponent from "./components/error-alert-component.vue";

new Vue({
  el: "#ManageWorkoutPageContainer",
  template: `
   <div class="manage-workout-container my-2">
      <div>navigation header here</div>
      <div>navigation body here</div>
  </div>
    `,
  components: {
    Spinner,
    ErrorAlertComponent
  },
  data() {
    return {
      errorAlertModel: new ErrorAlertModel()
    };
  },
  mounted() {}
});
