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
      <div
        class="pt-2 general-info-container"
        v-bind:class="{saving:spinner.status}"
      >
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
          <div
            class="row justify-content-end"
            v-bind:class="{saving:spinner.status}"
          >
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
          </div>
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
          <button
            class="btn btn-success"
            v-on:click="logWorkout"
            v-if="spinner.status == false"
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
library.add(faClock, faHashtag, faCalendar);

/* public components */
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { Vue, Component, Prop } from "vue-property-decorator";
import datePicker from "vue-bootstrap-datetimepicker";
import bFormInput from "bootstrap-vue/es/components/form-input/form-input";
import bAlert from "bootstrap-vue/es/components/alert/alert";
import "pc-bootstrap4-datetimepicker/build/css/bootstrap-datetimepicker.css";
import { InputGroup } from "bootstrap-vue/es/components";
import { mask } from "vue-the-mask";
import VeeValidate from "vee-validate";
Vue.use(InputGroup);
Vue.use(VeeValidate);
import Spinner from "vue-spinner-component/src/Spinner.vue";

/* app components */
import CrossfitterService from "../../../CrossfitterService";
import ExercisesListComponent from "./exercises-list-component.vue";
import ErrorAlertComponent from "../../error-alert-component.vue";

/* models and styles */
import { WorkoutViewModel } from "../../../models/viewModels/WorkoutViewModel";
import { ExerciseViewModel } from "../../../models/viewModels/ExerciseViewModel";
import { ToLogWorkoutViewModel } from "../../../models/viewModels/ToLogWorkoutViewModel";
import { WorkoutType } from "../../../models/viewModels/WorkoutType";
import { ErrorAlertModel } from "../../../models/viewModels/ErrorAlertModel";
import { SpinnerModel } from "./../../../models/viewModels/SpinnerModel";

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
    Spinner,
    ErrorAlertComponent
  },
  directives: { mask }
})
export default class E2momEditComponent extends Vue {
  model: WorkoutViewModel = new WorkoutViewModel();
  toLogModel: ToLogWorkoutViewModel = new ToLogWorkoutViewModel();
  errorAlertModel: ErrorAlertModel = new ErrorAlertModel();
  spinner: SpinnerModel = new SpinnerModel(false);

  mounted() {
    if (workouter != null && workouter.toLogWorkoutRawModel != null) {
      this.model = workouter.toLogWorkoutRawModel.workoutViewModel;
      this.toLogModel = workouter.toLogWorkoutRawModel;
    } else if (workouter != null && workouter.workoutViewModel != null) {
      this.model = workouter.workoutViewModel;
    } else {
      this.model.workoutType = WorkoutType.E2MOM;
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
        this.spinner.activate();

        crossfitterService
          .createAndLogWorkout(model)
          .then(data => {
            window.location.href = "\\";
          })
          .catch(data => {
            this.spinner.disable();
            this.errorAlertModel.setError(data.response.statusText);
          });
      }
    });
  }
}
</script>

<style>
</style>