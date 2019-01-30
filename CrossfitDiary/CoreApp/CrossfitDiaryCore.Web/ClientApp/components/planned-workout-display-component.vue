<template>
  <div class="planned-workouts container">
    <div class="row">
      <div class="done-item offset-lg-3 col col-lg-5 px-3 py-2 rounded">
        <div class="item-header d-flex flex-row justify-content-between  ">
          <div class="username">
            <span class="text-info">
              Planned workout
            </span>
          </div>
          <div class="">
            Today

          </div>
        </div>
        <div class="item-body pt-1">
          <!-- <WorkoutDisplayComponent :workoutViewModel="model.workoutViewModel"></WorkoutDisplayComponent> -->
          test test
          <hr />
          test test
        </div>
        <div class="item-footer text-right pt-1">
          <div class="action-buttons">
          </div>
        </div>
      </div>
    </div>
    <div class="row mt-1">
      <div class="col-sm mb-1 offset-lg-3 col col-lg-5 px-3 py-2">
        <b-button-group class="btn-group d-flex">
          <b-button
            class="w-100 "
            variant="success"
          >Scaled</b-button>
          <b-button
            class="w-100 active focus"
            variant="warning"
          >Rx</b-button>
          <b-button
            class="w-100"
            variant="danger"
          >Rx+</b-button>
        </b-button-group>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
/* Font awesome icons */
import { faPlus } from "@fortawesome/free-solid-svg-icons/faPlus";
import { faEdit } from "@fortawesome/free-solid-svg-icons/faEdit";
import { faGrinBeam } from "@fortawesome/free-regular-svg-icons/faGrinBeam";
import { faClock } from "@fortawesome/free-regular-svg-icons/faClock";
import { faTrashAlt } from "@fortawesome/free-solid-svg-icons/faTrashAlt";

import { library } from "@fortawesome/fontawesome-svg-core";
library.add(faGrinBeam, faClock, faPlus, faTrashAlt, faEdit);

/* public components */
import { Vue, Component, Prop } from "vue-property-decorator";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import bButtonGroup from "bootstrap-vue/es/components/button-group/button-group";
import bButton from "bootstrap-vue/es/components/button/button";

/* app components */
import WorkoutDisplayComponent from "./workout-display-component.vue";
// import CrossfitterService from "./../CrossfitterService";
// const _apiService: CrossfitterService = new CrossfitterService();

/* models and styles */
import { ToLogWorkoutViewModel } from "../models/viewModels/ToLogWorkoutViewModel";

@Component({
  components: {
    FontAwesomeIcon,
    WorkoutDisplayComponent,
    bButtonGroup,
    bButton
  }
})
export default class PlannedWorkoutDisplayComponent extends Vue {}
</script>

<style>
</style>