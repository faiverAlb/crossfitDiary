<template>
  <div>
    <b-modal
      ref="logWorkoutModal"
      title="Sure to log this workout?"
    >
      Are you sure you want to log this workout?
      <div slot="modal-footer">
        <button
          type="button"
          data-dismiss="modal"
          class="btn btn-default"
          @click="hideLogModal"
        >Close</button>
        <button
          type="button"
          data-dismiss="modal"
          class="btn btn-primary btn-success"
          @click="logWorkout"
        >Confirm</button>
      </div>
    </b-modal>
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
        <ExercisesListComponent :exercisesToDo="model.exercisesToDoList"></ExercisesListComponent>

        <div class="comments-section">
          <b-form-textarea
            id="commentSection"
            class="mt-2"
            v-model="model.comment"
            name="commentSection"
            placeholder="Note: ex. girls do max 30kg"
            :maxlength="150"
            type="text"
            rows="3"
            max-rows="3"
            no-resize
          />
          <small
            id="passwordHelpBlock"
            class="form-text text-muted text-right"
          >
            Workout note
          </small>
        </div>
      </div>

    </div>
    <EditPlannedWorkoutComponent
      :planningWorkout="model"
      @planWorkoutAction="planWorkoutAction"
      v-if="workoutEdit.canUserSeePlanWorkouts && spinner.status == false"
    ></EditPlannedWorkoutComponent>

    <div
      class="want-to-log-container my-3"
      v-if="!workoutEdit.canUserSeePlanWorkouts"
    >
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
          <div
            class="comments-section"
            v-bind:class="{saving:spinner.status}"
          >
            <b-form-textarea
              id="logWorkoutCommentSection"
              class="mt-2"
              v-model="toLogModel.comment"
              name="commentSection"
              placeholder="Note: ex. Holy sh*t! Will do it again!"
              :maxlength="200"
              type="text"
              rows="2"
              max-rows="2"
              no-resize
            />
            <small
              id="passwordHelpBlock"
              class="form-text text-muted"
            >
              Your thoughts on wod. Max length = 200
            </small>
          </div>
          <div
            class="row justify-content-end mt-3"
            v-if="spinner.status == false"
          >
            <span class="col-md-2 col-sm px-md-1">
              <button
                class=" btn btn-success btn-block"
                v-on:click="showLogWorkoutModal"
              >Log workout</button>
            </span>
          </div>
        </div>
      </div>
    </div>

  </div>
</template>

<script lang="ts">
/* Font awesome icons */
import { faClock } from "@fortawesome/free-solid-svg-icons/faClock";
import { faCalendar } from "@fortawesome/free-solid-svg-icons/faCalendar";
import { faHashtag } from "@fortawesome/free-solid-svg-icons/faHashtag";
import { library } from "@fortawesome/fontawesome-svg-core";
library.add(faClock, faHashtag, faCalendar);

/* public components */
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { Vue, Component, Prop } from "vue-property-decorator";
import bFormInput from "bootstrap-vue/es/components/form-input/form-input";
import bAlert from "bootstrap-vue/es/components/alert/alert";
import datePicker from "vue-bootstrap-datetimepicker";
import "pc-bootstrap4-datetimepicker/build/css/bootstrap-datetimepicker.css";
import { InputGroup } from "bootstrap-vue/es/components";
import { mask } from "vue-the-mask";
import VeeValidate from "vee-validate";
import Spinner from "vue-spinner-component/src/Spinner.vue";
import BFormTextarea from "bootstrap-vue/es/components/form-textarea/form-textarea";
import bModal from "bootstrap-vue/es/components/modal/modal";

Vue.use(InputGroup);
Vue.use(VeeValidate);
import { IWorkoutEditState } from "./../../../workout-edit-store/types";
import { State, Action, Getter } from "vuex-class";
const namespace: string = "workoutEdit";

/* app components */
import CrossfitterService from "../../../CrossfitterService";
import ExercisesListComponent from "./exercises-list-component.vue";
import ErrorAlertComponent from "../../error-alert-component.vue";
import EditPlannedWorkoutComponent from "../edit-planned-workout-component.vue";

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
    BFormTextarea,
    bModal,

    bAlert,
    Spinner,
    ErrorAlertComponent,
    EditPlannedWorkoutComponent
  },
  directives: { mask }
})
export default class NFTEditComponent extends Vue {
  model: WorkoutViewModel = new WorkoutViewModel();
  toLogModel: ToLogWorkoutViewModel = new ToLogWorkoutViewModel();
  errorAlertModel: ErrorAlertModel = new ErrorAlertModel();
  spinner: SpinnerModel = new SpinnerModel(false);

  $refs: {
    logWorkoutModal: HTMLFormElement;
  };
  @State("workoutEdit")
  workoutEdit: IWorkoutEditState;

  mounted() {
    if (workouter != null && workouter.toLogWorkoutRawModel != null) {
      this.model = workouter.toLogWorkoutRawModel.workoutViewModel;
      this.toLogModel = workouter.toLogWorkoutRawModel;
    } else if (workouter != null && workouter.workoutViewModel != null) {
      this.model = workouter.workoutViewModel;
    } else {
      this.model.workoutType = WorkoutType.NotForTime;
    }
  }
  mutateData(): void {}
  planWorkoutAction(): void {
    this.$validator.validate();

    this.$validator.validate().then(isValid => {
      if (isValid) {
        let crossfitterService: CrossfitterService = new CrossfitterService();
        this.spinner.activate();
        crossfitterService
          .createAndPlanWorkout(this.model)
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

  showLogWorkoutModal(): void {
    this.$validator.validate();

    this.$validator.validate().then(isValid => {
      if (isValid) {
        this.$refs.logWorkoutModal.show();
      }
    });
  }

  hideLogModal(): void {
    this.$refs.logWorkoutModal.hide();
  }
}
</script>

<style>
</style>