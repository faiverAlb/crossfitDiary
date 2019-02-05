<template>
  <div class="planned-workouts container">
    <b-modal
      ref="logWorkoutModal"
      title="Log workout"
    >
      <div
        class="log-workout"
        v-if="selectedWorkout"
      >
        <div class="log-workout-container">
          <div
            class="row"
            v-if="selectedWorkout.IsHaveCapTime"
          >
            <div class="col-sm-12 total-time-log-container">
              <b-input-group class="mb-2">
                <b-input-group-prepend>
                  <b-input-group-text tag="span">
                    <font-awesome-icon :icon="['far','clock']"></font-awesome-icon>
                  </b-input-group-text>
                </b-input-group-prepend>
                <b-form-input
                  type="tel"
                  v-model="toLogModel.timePassed"
                  v-mask="'##:##'"
                  placeholder="Time"
                  aria-describedby="prPercentHelpBlock"
                ></b-form-input>
              </b-input-group>
            </div>
          </div>
          <div
            class="horizontal-divider d-block "
            v-if="selectedWorkout.IsHaveCapTime"
          >
            <hr class="mt-2" />
          </div>
          <div
            class="row"
            v-if="selectedWorkout.IsHaveCapTime"
          >
            <div class="col-sm  pl-lg-2 cap-reps-log-container">
              <b-input-group class="mb-2">
                <b-input-group-prepend>
                  <b-input-group-text tag="span">
                    Cap +
                  </b-input-group-text>
                </b-input-group-prepend>
                <b-form-input
                  type="number"
                  v-model="toLogModel.repsToFinishOnCapTime"
                  placeholder="Count"
                >
                </b-form-input>
              </b-input-group>
            </div>
          </div>
        </div>
      </div>

      <div slot="modal-footer">
        <b-button
          variant="warning"
          data-dismiss="modal"
          @click="logWorkout"
        >Log workout</b-button>
        <b-button
          data-dismiss="modal"
          @click="()=>{this.$refs.logWorkoutModal.hide();}"
        >Close</b-button>
      </div>
    </b-modal>
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
        <div class="item-footer text-right pt-2">
          <div class="action-buttons">
            <b-button
              variant="warning"
              @click="showLogWorkout(plannedScaled)"
            >Log workout</b-button>
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
            <a
              class="repeat-workout-action pointer text-success pl-1"
              v-bind:href="'Workout?workoutId='+this.plannedRx.id"
            >
              <font-awesome-icon :icon="['fas','plus']"></font-awesome-icon>
              <span class="do-it-text">Do it</span>
            </a>
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
            <a
              class="repeat-workout-action pointer text-success pl-1"
              v-bind:href="'Workout?workoutId='+this.plannedRxPlus.id"
            >
              <font-awesome-icon :icon="['fas','plus']"></font-awesome-icon>
              <span class="do-it-text">Do it</span>
            </a>
          </div>
        </div>
      </div>
    </div>
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
import { faCalendar } from "@fortawesome/free-solid-svg-icons/faCalendar";

import { library } from "@fortawesome/fontawesome-svg-core";
library.add(faGrinBeam, faClock, faPlus, faTrashAlt, faEdit, faCalendar);

/* public components */
import { Vue, Component, Prop } from "vue-property-decorator";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import bButtonGroup from "bootstrap-vue/es/components/button-group/button-group";
import bButton from "bootstrap-vue/es/components/button/button";
import bModal from "bootstrap-vue/es/components/modal/modal";
import bFormInput from "bootstrap-vue/es/components/form-input/form-input";
import datePicker from "vue-bootstrap-datetimepicker";
import "pc-bootstrap4-datetimepicker/build/css/bootstrap-datetimepicker.css";
import { InputGroup } from "bootstrap-vue/es/components";
Vue.use(InputGroup);
import { mask } from "vue-the-mask";

/* app components */
import WorkoutDisplayComponent from "./workout-display-component.vue";
/* models and styles */
import { ToLogWorkoutViewModel } from "../models/viewModels/ToLogWorkoutViewModel";

import {
  WorkoutViewModel,
  PlanningWorkoutLevel
} from "../models/viewModels/WorkoutViewModel";

@Component({
  components: {
    FontAwesomeIcon,
    WorkoutDisplayComponent,
    bButtonGroup,
    bButton,
    bModal,
    bFormInput,
    datePicker
  },
  directives: { mask }
})
export default class PlannedWorkoutDisplayComponent extends Vue {
  @Prop() plannedWorkouts: WorkoutViewModel[];

  show: boolean = true;
  isScaledSelected: boolean = false;
  isRxSelected: boolean = false;
  isRxPlusSelected: boolean = false;
  selectedWorkout: WorkoutViewModel = null;
  toLogModel: ToLogWorkoutViewModel = new ToLogWorkoutViewModel();
  isForTimesWorkouts: boolean = false;

  $refs: {
    logWorkoutModal: HTMLFormElement;
  };

  showLogWorkout(workoutViewModel: WorkoutViewModel) {
    this.selectedWorkout = workoutViewModel;
    this.$refs.logWorkoutModal.show();
  }

  logWorkout() {
    debugger;
  }
  get plannedScaled() {
    if (this.plannedWorkouts[0]) {
      //isScaledSelected
      let foundScaled = this.plannedWorkouts.find(
        x => x.planningWorkoutLevel == PlanningWorkoutLevel.Scaled
      );
      this.setSelectedPlanned(this.plannedWorkouts[0].planningWorkoutLevel);
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