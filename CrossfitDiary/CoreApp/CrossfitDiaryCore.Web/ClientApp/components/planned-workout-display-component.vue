<template>
  <div class="planned-workouts container">
    <!-- <transition-group
      tag="div"
      name="fade"
      mode="out-in"
    > -->
    <div
      class="row"
      v-if="isScaledSelected"
    >
      <div class="done-item offset-lg-3 col col-lg-5 px-3 py-2 rounded">
        <div class="item-header d-flex flex-row justify-content-between  ">
          <div class="username">
            <span class="text-info">
              Scaled
            </span>
          </div>
          <div class="">
            Today
          </div>
        </div>
        <div class="item-body pt-1">
          <WorkoutDisplayComponent :workoutViewModel="plannedScaled"></WorkoutDisplayComponent>
        </div>
        <div class="item-footer text-right pt-1">
          <div class="action-buttons">
          </div>
        </div>
      </div>
    </div>
    <div
      class="row"
      v-if="isRxSelected"
    >
      <div class="done-item offset-lg-3 col col-lg-5 px-3 py-2 rounded">
        <div class="item-header d-flex flex-row justify-content-between  ">
          <div class="username">
            <span class="text-info">
              Rx
            </span>
          </div>
          <div class="">
            Today
          </div>
        </div>
        <div class="item-body pt-1">
          <WorkoutDisplayComponent :workoutViewModel="plannedRx"></WorkoutDisplayComponent>
        </div>
        <div class="item-footer text-right pt-1">
          <div class="action-buttons">
          </div>
        </div>
      </div>
    </div>
    <div
      class="row"
      v-if="isRxPlusSelected"
    >
      <div class="done-item offset-lg-3 col col-lg-5 px-3 py-2 rounded">
        <div class="item-header d-flex flex-row justify-content-between  ">
          <div class="username">
            <span class="text-info">
              Rx Plus
            </span>
          </div>
          <div class="">
            Today
          </div>
        </div>
        <div class="item-body pt-1">
          <WorkoutDisplayComponent :workoutViewModel="plannedRxPlus"></WorkoutDisplayComponent>
        </div>
        <div class="item-footer text-right pt-1">
          <div class="action-buttons">
          </div>
        </div>
      </div>
    </div>
    <!-- </transition-group> -->
    <div class="row mt-1">
      <div class="col-sm mb-1 offset-lg-3 col col-lg-5 px-3 py-2">
        <b-button-group class="btn-group d-flex">
          <b-button
            v-if="plannedScaled"
            v-on:click="setSelectedPlanned(0)"
            v-bind:class="{focus:isScaledSelected}"
            class="w-100 "
            variant="success"
          >Scaled</b-button>
          <b-button
            v-if="plannedRx"
            v-on:click="setSelectedPlanned(1)"
            v-bind:class="{focus:isRxSelected}"
            class="w-100"
            variant="warning"
          >Rx</b-button>
          <b-button
            v-if="plannedRxPlus"
            v-on:click="setSelectedPlanned(2)"
            v-bind:class="{focus:isRxPlusSelected}"
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
/* models and styles */
import {
  WorkoutViewModel,
  PlanningWorkoutLevel
} from "../models/viewModels/WorkoutViewModel";

@Component({
  components: {
    FontAwesomeIcon,
    WorkoutDisplayComponent,
    bButtonGroup,
    bButton
  }
})
export default class PlannedWorkoutDisplayComponent extends Vue {
  @Prop() plannedWorkouts: WorkoutViewModel[];

  show: boolean = true;
  isScaledSelected: boolean = false;
  isRxSelected: boolean = false;
  isRxPlusSelected: boolean = false;

  mounted() {}
  get plannedScaled() {
    if (this.plannedWorkouts[0]) {
      //isScaledSelected
      let foundScaled = this.plannedWorkouts.find(
        x => x.planningWorkoutLevel == PlanningWorkoutLevel.Scaled
      );
      this.isScaledSelected = foundScaled != null;
      return foundScaled;
    }
    return null;
  }

  get plannedRx() {
    if (this.plannedWorkouts[0]) {
      return this.plannedWorkouts.find(
        x => x.planningWorkoutLevel == PlanningWorkoutLevel.Rx
      );
    }
    return null;
  }

  get plannedRxPlus() {
    if (this.plannedWorkouts[0]) {
      return this.plannedWorkouts.find(
        x => x.planningWorkoutLevel == PlanningWorkoutLevel.RxPlus
      );
    }
    return null;
  }

  setSelectedPlanned(planningWorkoutLevel: PlanningWorkoutLevel) {
    this.isScaledSelected = false;
    this.isRxSelected = false;
    this.isRxPlusSelected = false;
    switch (planningWorkoutLevel) {
      case PlanningWorkoutLevel.Scaled:
        this.isScaledSelected = true;
        break;
      case PlanningWorkoutLevel.Rx:
        this.isRxSelected = true;
        break;
      case PlanningWorkoutLevel.RxPlus:
        this.isRxPlusSelected = true;
        break;
    }
  }
}
</script>

<style>
</style>