<template>
  <div>
    <div class="routine-complex-info">
      <div class="pt-2 general-info-container">
        <div class="row">
          <div class="col-lg-3 time-cap-container pb-2">
            <label for="timeCapInput" class="sr-only">Time cap:</label>
            <div class="input-group">
              <div class="input-group-prepend">
                <div class="input-group-text">
                  <font-awesome-icon :icon="['fas','clock']" class="fa-lg time-cap-icon"></font-awesome-icon>
                </div>
              </div>
              <input v-model="model.timeCap" type="text" class="form-control " id="timeCapInput" placeholder="Time Cap" />
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-lg-3 pb-2">
            <label for="roundsInput" class="sr-only">Rounds count:</label>
            <div class="input-group ">
              <div class="input-group-prepend">
                <div class="input-group-text">
                  <font-awesome-icon :icon="['fas','hashtag']"></font-awesome-icon>
                </div>
              </div>
              <input v-model="model.roundsCount" type="number" pattern="[0-9]*" inputmode="numeric" min="1" class="form-control " id="roundsInput" placeholder="Rounds count" />
            </div>
          </div>
        </div>
        <ExercisesListComponent :exercisesToDo="model.exercisesToDoList"></ExercisesListComponent>
        <div class="row py-1" v-if="model.exercisesToDoList.length > 0">
          <div class="col-lg-4">
            <label for="timeInput" class="sr-only">Rest after</label>
            <b-input-group class="py-2" prepend="Rest after round:">
              <b-form-input type="text" v-model="restBetweenRounds" placeholder="Time" aria-describedby="prPercentHelpBlock"></b-form-input>
            </b-input-group>
          </div>
        </div>
      </div>
    </div>
    <div class="want-to-log-container my-3">
      <div class="log-workout-container">
        <div class="col-md-12 text-right">
          <div class="row justify-content-end">
            <div class="col-lg-3 col-sm data-selector-container pr-lg-3">
              <div class="form-group">
                <b-input-group>
                  <date-picker v-model="toLogModel.date" :config="options" :wrap="true"></date-picker>
                  <b-input-group-append>
                    <button class="btn btn-secondary datepickerbutton" type="button" title="Toggle">
                      <font-awesome-icon :icon="['fas','calendar']"></font-awesome-icon>
                    </button>
                  </b-input-group-append>
                </b-input-group>
              </div>
            </div>
            <div class="col-lg-3 col-sm pr-lg-2 total-time-log-container">
              <b-input-group class="mb-2">
                <b-input-group-prepend>
                  <b-input-group-text tag="span">
                    <font-awesome-icon :icon="['fas','clock']"></font-awesome-icon>
                  </b-input-group-text>
                </b-input-group-prepend>
                <b-form-input type="text" v-model="toLogModel.timePassed" v-mask="'##:##'" placeholder="Time" aria-describedby="prPercentHelpBlock"></b-form-input>
              </b-input-group>
            </div>
            <div class="vertical-divider d-none d-sm-block"></div>
            <div class="horizontal-divider d-block d-sm-none ">
              <hr />
            </div>
            <div class="col-lg-3 col-sm  pl-lg-2 cap-reps-log-container">
              <b-input-group class="mb-2">
                <b-input-group-prepend>
                  <b-input-group-text tag="span">
                    Cap +
                  </b-input-group-text>
                </b-input-group-prepend>
                <b-form-input type="number" v-model="toLogModel.repsToFinishOnCapTime" placeholder="Count"></b-form-input>
              </b-input-group>
            </div>
          </div>
          <button class="btn btn-success" v-on:click="logWorkout">Log workout</button>
        </div>
      </div>
    </div>

  </div>
</template>

<script lang="ts">
/* Font awesome icons */
import { faClock } from "@fortawesome/free-solid-svg-icons/faClock";
// import { faCalendar } from "@fortawesome/free-regular-svg-icons/faCalendar";
import { faCalendar } from "@fortawesome/free-solid-svg-icons/faCalendar";
import { faHashtag } from "@fortawesome/free-solid-svg-icons/faHashtag";
import { library } from "@fortawesome/fontawesome-svg-core";

library.add(faClock, faHashtag, faCalendar);
/**/
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { Vue, Component, Prop } from "vue-property-decorator";
import { WorkoutViewModel } from "../../../models/viewModels/WorkoutViewModel";
import ExercisesListComponent from "./exercises-list-component.vue";
import { ExerciseViewModel } from "../../../models/viewModels/ExerciseViewModel";
import bFormInput from "bootstrap-vue/es/components/form-input/form-input";
import { ToLogWorkoutViewModel } from "../../../models/viewModels/ToLogWorkoutViewModel";

import datePicker from "vue-bootstrap-datetimepicker";
import "pc-bootstrap4-datetimepicker/build/css/bootstrap-datetimepicker.css";
import { InputGroup } from "bootstrap-vue/es/components";
Vue.use(InputGroup);
import { mask } from "vue-the-mask";

@Component({
  components: {
    FontAwesomeIcon,
    ExercisesListComponent,
    bFormInput,
    datePicker
  },
  directives: { mask }
})
export default class ForTimeEditComponent extends Vue {
  model: WorkoutViewModel = new WorkoutViewModel();
  toLogModel: ToLogWorkoutViewModel = new ToLogWorkoutViewModel();
  date: Date = new Date();
  options: any = {
    format: "DD-MM-YYYY"
  };
  mounted() {}
  // computed variable based on user's email
  mutateData(): void {}

  logWorkout() {
    debugger;
  }
}
</script>

<style>
</style>