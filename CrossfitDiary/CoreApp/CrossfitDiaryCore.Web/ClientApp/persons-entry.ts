import Vue from "vue";
import "./style/persons.scss";
import { ToLogWorkoutViewModel } from "./models/viewModels/ToLogWorkoutViewModel";

import PersonsActivitiesComponent from "./components/person-activities-component.vue";
import CrossfitterService from "./CrossfitterService";
const apiService = new CrossfitterService();

import { dom } from "@fortawesome/fontawesome-svg-core";

import Spinner from "vue-spinner-component/src/Spinner.vue";
import { SpinnerModel } from "./models/viewModels/SpinnerModel";

dom.watch(); // This will kick of the initial replacement of i to svg tags and configure a MutationObserver

new Vue({
  el: "#home-page-container",
  template: `
    <div class="home-container">
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
    Spinner
  },
  data() {
    return {
      activities: ToLogWorkoutViewModel[0],
      spinner: new SpinnerModel(true)
    };
  },
  mounted() {
    this.spinner.activate();
    apiService.getAllCrossfittersWorkouts().then(data => {
      this.activities = data;
      this.spinner.disable();
    });
  }
});
