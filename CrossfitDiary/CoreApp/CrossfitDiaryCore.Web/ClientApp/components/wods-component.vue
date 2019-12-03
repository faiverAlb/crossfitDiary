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
        >
        </spinner>
      </div>
    </div>
    <div class="container workouts-list">
      <div class="py-2" v-for="item in workouts" :key="item.id">
        <div class="wod-item offset-lg-3 col col-lg-5 px-3 py-2 rounded">
          <div class="item-body pt-1">
            <WorkoutDisplayComponent
              :workoutViewModel="item"
            ></WorkoutDisplayComponent>
          </div>
          <div class="item-footer text-right pt-2">
            <div class="action-buttons"></div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
/* Font awesome icons */
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
import { WorkoutViewModel } from "./../models/viewModels/WorkoutViewModel";
const apiService: CrossfitterService = new CrossfitterService();

@Component({
  name: "WodsComponent",
  components: { ErrorAlertComponent, WorkoutDisplayComponent, Spinner }
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
}
</script>

<style></style>
