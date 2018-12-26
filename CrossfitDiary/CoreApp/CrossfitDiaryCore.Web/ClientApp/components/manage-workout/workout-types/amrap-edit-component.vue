<template>
  <div>
    <div class="routine-complex-info">
      <div class="row">
        <div class="col">
          <ErrorAlertComponent
            class="my-1 mx-0"
            :errorAlertModel="errorAlertModel"
          ></ErrorAlertComponent>
        </div>
      </div>
      <div class="pt-2 general-info-container">
        <div class="row">
          <div class="col-lg-3 pb-2">
            <label
              for="timeInput"
              class="sr-only"
            >Time to work:</label>
            <b-input-group>
              <b-input-group-prepend>
                <b-input-group-text tag="span">
                  <font-awesome-icon :icon="['far','clock']"></font-awesome-icon>
                </b-input-group-text>
              </b-input-group-prepend>
              <b-form-input
                v-model="model.timeToWork"
                v-mask="'##:##'"
                type="tel"
                name="timeToWork"
                v-validate="'required|length:5'"
                :state="fields.timeToWork && fields.timeToWork.valid"
                id="timeToWork"
                placeholder="Time to work"
              />
            </b-input-group>
          </div>

        </div>

        <ExercisesListComponent :exercisesToDo="model.exercisesToDoList"></ExercisesListComponent>
        <div
          class="row py-1"
          v-if="model.exercisesToDoList.length > 0"
        >
          <div class="col-lg-4">
            <label
              for="timeInput"
              class="sr-only"
            >Rest after</label>
            <b-input-group
              class="py-2"
              prepend="Rest after round:"
            >
              <b-form-input
                type="tel"
                v-mask="'##:##'"
                v-model="model.restBetweenRounds"
                placeholder="Time"
                aria-describedby="prPercentHelpBlock"
              ></b-form-input>
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
                  <date-picker
                    v-model="toLogModel.displayDate"
                    :config="{ format: 'DD.MM.YYYY'}"
                    placeholder="Select date"
                    name="toLogModelDate"
                    :state="fields.toLogModelDate && fields.toLogModelDate.valid"
                    v-validate="'required'"
                    :wrap="true"
                  ></date-picker>
                  <b-input-group-append>
                    <button
                      class="btn btn-secondary datepickerbutton"
                      type="button"
                      title="Toggle"
                    >
                      <font-awesome-icon :icon="['fas','calendar']"></font-awesome-icon>
                    </button>
                  </b-input-group-append>
                </b-input-group>
              </div>
            </div>
            <div class="col-lg-3 col-sm pr-lg-1">
              <b-input-group class="mb-2">
                <b-input-group-prepend>
                  <b-input-group-text tag="span">
                    <font-awesome-icon :icon="['fas','hashtag']"></font-awesome-icon>
                  </b-input-group-text>
                </b-input-group-prepend>
                <b-form-input
                  type="tel"
                  v-model="toLogModel.roundsFinished"
                  v-mask="'####'"
                  placeholder="Rounds finished"
                  aria-describedby="prPercentHelpBlock"
                ></b-form-input>
              </b-input-group>
            </div>
            <div class="col-lg-3 col-sm ">
              <b-input-group class="mb-2">
                <b-input-group-prepend>
                  <b-input-group-text tag="span">
                    <font-awesome-icon :icon="['fas','hashtag']"></font-awesome-icon>
                  </b-input-group-text>
                </b-input-group-prepend>
                <b-form-input
                  type="tel"
                  v-model="toLogModel.partialRepsFinished"
                  v-mask="'####'"
                  placeholder="Partial repetitions"
                  aria-describedby="prPercentHelpBlock"
                ></b-form-input>
              </b-input-group>
            </div>
          </div>
          <button
            class="btn btn-success"
            v-on:click="logWorkout"
          >Log workout</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
/* Font awesome icons */
import { faClock } from "@fortawesome/free-regular-svg-icons/faClock";
import { faCalendar } from "@fortawesome/free-solid-svg-icons/faCalendar";
import { faHashtag } from "@fortawesome/free-solid-svg-icons/faHashtag";
import { library } from "@fortawesome/fontawesome-svg-core";
import CrossfitterService from "../../../CrossfitterService";

library.add(faClock, faHashtag, faCalendar);
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
/**/

import { Vue, Component, Prop } from "vue-property-decorator";
import { WorkoutViewModel } from "../../../models/viewModels/WorkoutViewModel";
import ExercisesListComponent from "./exercises-list-component.vue";
import { ExerciseViewModel } from "../../../models/viewModels/ExerciseViewModel";
import bFormInput from "bootstrap-vue/es/components/form-input/form-input";
import bAlert from "bootstrap-vue/es/components/alert/alert";
import { ToLogWorkoutViewModel } from "../../../models/viewModels/ToLogWorkoutViewModel";

import datePicker from "vue-bootstrap-datetimepicker";
import "pc-bootstrap4-datetimepicker/build/css/bootstrap-datetimepicker.css";
import { InputGroup } from "bootstrap-vue/es/components";
Vue.use(InputGroup);
import { mask } from "vue-the-mask";
import VeeValidate from "vee-validate";
import { WorkoutType } from "../../../models/viewModels/WorkoutType";

import ErrorAlertComponent from "../../error-alert-component.vue";
import { ErrorAlertModel } from "../../../models/viewModels/ErrorAlertModel";

Vue.use(VeeValidate);
declare var workouter: {
  toLogWorkoutRawModel: ToLogWorkoutViewModel;
  workoutViewModel: WorkoutViewModel;
};

@Component({
  components: {
    FontAwesomeIcon,
    ExercisesListComponent,
    bFormInput,
    datePicker,
    bAlert,
    ErrorAlertComponent
  },
  directives: { mask }
})
export default class AmrapEditComponent extends Vue {
  model: WorkoutViewModel = new WorkoutViewModel();
  toLogModel: ToLogWorkoutViewModel = new ToLogWorkoutViewModel();
  errorAlertModel: ErrorAlertModel = new ErrorAlertModel();

  mounted() {
    if (workouter != null && workouter.toLogWorkoutRawModel != null) {
      this.model = workouter.toLogWorkoutRawModel.workoutViewModel;
      this.toLogModel = workouter.toLogWorkoutRawModel;
    } else if (workouter != null && workouter.workoutViewModel != null) {
      this.model = workouter.workoutViewModel;
    } else {
      this.model.workoutType = WorkoutType.AMRAP;
    }
  }
  mutateData(): void {}

  logWorkout() {
    this.$validator.validate();

    this.$validator.validate().then(isValid => {
      if (isValid) {
        let workoutModel = this.model;
        const model = {
          newWorkoutViewModel: workoutModel,
          logWorkoutViewModel: this.toLogModel
        };
        let crossfitterService: CrossfitterService = new CrossfitterService();

        crossfitterService
          .createAndLogWorkout(model)
          .then(data => {
            window.location.href = "\\";
          })
          .catch(data => {
            this.errorAlertModel.setError(data.response.statusText);
          });
      }
    });
  }
}
</script>

<style>
</style>