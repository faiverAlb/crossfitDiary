<template>
  <div class="wods-library-container">
    <div class="row">
      <div class="offset-5">
        <spinner
          :status="spinner.status"
          :size="spinner.size"
          :color="spinner.color"
          :depth="spinner.depth"
          :rotation="spinner.rotation"
          :speed="spinner.speed"
        ></spinner>
      </div>
    </div>
    <div class="container workouts-list">
      <div class="py-2" v-for="item in workouts" :key="item.id">
        <div class="wod-item offset-lg-3 col col-lg-5 px-3 py-2 rounded">
          <div class="item-body pt-1">
            <WorkoutDisplayComponent :workoutViewModel="item"></WorkoutDisplayComponent>
          </div>
          <div class="item-footer text-right pt-2">
            <div class="action-buttons">
              <b-button-group class="btn-group d-flex" size="sm">
                <b-button
                  class
                  variant="success"
                  v-on:click="planWodToday(item.id, 0)"
                >Plan as Sc today</b-button>
                <b-button
                  class
                  variant="warning"
                  v-on:click="planWodToday(item.id, 1)"
                >Plan as Rx today</b-button>
                <b-button
                  class
                  variant="danger"
                  v-on:click="planWodToday(item.id, 2)"
                >Plan as Rx+ today</b-button>
              </b-button-group>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
/* public components */
import { Vue, Component, Prop } from "vue-property-decorator";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { BButtonGroup } from "bootstrap-vue";
import { BButton } from "bootstrap-vue";

/* Font awesome icons */
import { faCalendar } from "@fortawesome/free-solid-svg-icons/faCalendar";
import { library } from "@fortawesome/fontawesome-svg-core";
library.add(faCalendar);

import { dom } from "@fortawesome/fontawesome-svg-core";
dom.watch(); // This will kick of the initial replacement of i to svg tags and configure a MutationObserver

/* public components */
import Spinner from "vue-spinner-component/src/Spinner.vue";

/* app components */
import CrossfitterService from "./../CrossfitterService";
import ErrorAlertComponent from "./error-alert-component.vue";
import WorkoutDisplayComponent from "./workout-display-component.vue";

/* models and styles */
import "./../style/wods-list.scss";
import { SpinnerModel } from "./../models/viewModels/SpinnerModel";
import { ErrorAlertModel } from "./../models/viewModels/ErrorAlertModel";
import {
  WorkoutViewModel,
  PlanningWorkoutLevel
} from "./../models/viewModels/WorkoutViewModel";
const apiService: CrossfitterService = new CrossfitterService();

@Component({
  name: "WodsComponent",
  components: {
    ErrorAlertComponent,
    WorkoutDisplayComponent,
    Spinner,
    FontAwesomeIcon,
    BButtonGroup,
    BButton
  }
})
export default class WodsComponent extends Vue {
  workouts: WorkoutViewModel[] = null;
  spinner: SpinnerModel = new SpinnerModel(true);
  errorAlertModel: ErrorAlertModel = new ErrorAlertModel();

  mounted() {
    this.spinner.activate();
    apiService
      .getWorkoutsList()
      .then(data => {
        this.workouts = data;
        this.spinner.disable();
      })
      .catch(data => {
        this.errorAlertModel.setError(data.response.statusText);
        this.spinner.disable();
      });
  }

  planWodToday(wodId: number, type: PlanningWorkoutLevel) {
    debugger;
  }
}
</script>

<style></style>
