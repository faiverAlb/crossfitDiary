<template>
    <div>
        <div class="routine-complex-info">
            <div class="row">
                <div class="col">
                    <ErrorAlertComponent
                            :errorAlertModel="errorAlertModel"
                            class="my-1 mx-0"
                    />
                </div>
            </div>
            <div
                    class="pt-2 general-info-container"
                    v-bind:class="{saving:spinner.status}"
            >
                <div class="dashed-container-description border-info text-center">
                    <b-badge variant="info">1</b-badge>
                    Create workout:
                </div>
                <div
                        :key="`${childWorkout.id}-${index}`"
                        class="inner-workouts"
                        v-for="(childWorkout,index) in model.children"
                >
                    <div class="workout">
                        <div class="inner-workout-header">
                            <h5>
                <span
                        data-bind="if: $index() != 0"
                        v-if="index != 0"
                > then </span>
                                <span>{{index + 1}} workout</span>
                                <a
                                        data-bind="click:function(){ $parent.removeInnerWorkout($index())}"
                                        href="#"
                                        v-if="model.children.length > 1"
                                        v-on:click="removeInnerWorkout(index)"
                                >
                                    <font-awesome-icon
                                            :icon="['fas','times']"
                                            class="fa-sm text-secondary"
                                    />
                                </a>
                                <!-- /ko -->
                            </h5>
                        </div>
                        <div class="routine-complex-info">

                            <div class="pt-2 general-info-container">
                                <div class="row">
                                    <div class="col-lg-3 pb-2">
                                        <label
                                                class="sr-only"
                                                v-bind:for="'timeInput'+index"
                                        >Time to work:</label>
                                        <b-input-group size="sm">
                                            <b-input-group-prepend>
                                                <b-input-group-text tag="span">
                                                    <font-awesome-icon :icon="['far','clock']"/>
                                                </b-input-group-text>
                                            </b-input-group-prepend>
                                            <b-form-input
                                                    :state="fields['timeToWork'+index] && fields['timeToWork'+index].valid"
                                                    placeholder="Time to work"
                                                    type="tel"
                                                    v-bind:id="'timeToWork'+ index"
                                                    v-bind:name="'timeToWork'+ index"
                                                    v-mask="'##:##'"
                                                    v-model="childWorkout.timeToWork"
                                                    v-validate.initial="'required|length:5'"
                                            />
                                        </b-input-group>
                                    </div>

                                </div>
                                <ExercisesListComponent :exercisesToDo="childWorkout.exercisesToDoList"/>
                                <div
                                        class="row py-1"
                                        v-if="childWorkout.exercisesToDoList.length > 0"
                                >
                                    <div class="col-lg-4">
                                        <label
                                                class="sr-only"
                                                for="restInput"
                                        >Rest after</label>
                                        <b-input-group
                                                class="py-2"
                                                prepend="Rest after round:"
                                        >
                                            <b-form-input
                                                    aria-describedby="prPercentHelpBlock"
                                                    placeholder="Time"
                                                    type="tel"
                                                    id="restInput"
                                                    v-mask="'##:##'"
                                                    v-model="childWorkout.restBetweenRounds"
                                            />
                                        </b-input-group>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="mt-3">
                    <b-button
                            variant="warning"
                            size="sm"
                            v-on:click="addInnerWorkout"
                    >Add workout after
                    </b-button>
                </div>
            </div>
            <div class="comments-section">
                <b-form-textarea
                        size="sm"
                        :maxlength="150"
                        class="mt-2"
                        id="commentSection"
                        max-rows="3"
                        name="commentSection"
                        no-resize
                        placeholder="Note: ex. girls do max 30kg"
                        rows="2"
                        type="text"
                        v-model="model.comment"
                />
                <small
                        class="form-text text-muted text-right"
                        id="noteHelpBlock"
                >
                    Workout note
                </small>
            </div>
        </div>
        <EditPlannedWorkoutComponent
                :planningWorkout="model"
                @planWorkoutAction="planWorkoutAction"
                v-if="workoutEdit.canUserSeePlanWorkouts && spinner.status == false"
        />
        <div
                class="want-to-log-container my-3"
                v-if="!workoutEdit.canUserSeePlanWorkouts"
        >
            <div class="log-workout-container row">
                <div class="col-md-12 text-right">
                    <div class="dashed-container-description border-success text-center">
                        <b-badge variant="success">2</b-badge>
                        Write results:
                    </div>
                    <div
                            class="row justify-content-end"
                            v-bind:class="{saving:spinner.status}"
                    >
                        <div class="col-lg-3 col-sm data-selector-container pr-lg-3">
                            <div class="form-group">
                                <b-input-group size="sm">
                                    <date-picker
                                            :config="{ format: 'DD.MM.YYYY'}"
                                            :state="fields.toLogModelDate && fields.toLogModelDate.valid"
                                            :wrap="true"
                                            name="toLogModelDate"
                                            placeholder="Select date"
                                            v-model="toLogModel.displayDate"
                                            v-validate.initial="'required'"
                                    />
                                    <b-input-group-append>
                                        <button
                                                class="btn btn-secondary datepickerbutton"
                                                title="Toggle"
                                                type="button"
                                        >
                                            <font-awesome-icon :icon="['fas','calendar']"/>
                                        </button>
                                    </b-input-group-append>
                                </b-input-group>
                            </div>
                        </div>
                        <div class="col-lg-3 col-sm pr-lg-1">
                            <b-input-group class="mb-2" size="sm">
                                <b-input-group-prepend>
                                    <b-input-group-text tag="span">
                                        <font-awesome-icon :icon="['fas','hashtag']"/>
                                    </b-input-group-text>
                                </b-input-group-prepend>
                              <b-form-input
                                      aria-describedby="prPercentHelpBlock"
                                      inputmode="numeric"
                                      pattern="[0-9]*"
                                      placeholder="Rounds finished"
                                      type="tel"
                                      v-mask="'####'"
                                      v-model="toLogModel.roundsFinished"
                              />
                            </b-input-group>
                        </div>
                        <div class="col-lg-3 col-sm ">
                            <b-input-group class="mb-2" size="sm">
                                <b-input-group-prepend>
                                    <b-input-group-text tag="span">
                                        <font-awesome-icon :icon="['fas','hashtag']"/>
                                    </b-input-group-text>
                                </b-input-group-prepend>
                              <b-form-input
                                      aria-describedby="prPercentHelpBlock"
                                      inputmode="numeric"
                                      pattern="[0-9]*"
                                      placeholder="Partial repetitions"
                                      type="tel"
                                      v-mask="'####'"
                                      v-model="toLogModel.partialRepsFinished"
                              />
                            </b-input-group>
                        </div>
                    </div>
                    <div class="row">
                        <div class="offset-5">
                            <spinner
                                    :color="spinner.color"
                                    :depth="spinner.depth"
                                    :rotation="spinner.rotation"
                                    :size="spinner.size"
                                    :speed="spinner.speed"
                                    :status="spinner.status"
                            >
                            </spinner>
                        </div>
                    </div>
                    <div
                            class="comments-section"
                            v-bind:class="{saving:spinner.status}"
                    >
                        <b-form-textarea
                                size="sm"
                                :maxlength="200"
                                class="mt-2"
                                id="logWorkoutCommentSection"
                                max-rows="2"
                                name="commentSection"
                                no-resize
                                placeholder="Note: ex. Holy sh*t! Will do it again!"
                                rows="2"
                                type="text"
                                v-model="toLogModel.comment"
                        />
                        <small
                                class="form-text text-muted"
                                id="passwordHelpBlock"
                        >
                            Your thoughts on wod. Max length = 200
                        </small>
                    </div>
                    <div
                            class="row justify-content-end mt-3"
                            v-if="spinner.status == false"
                    >
                        <span class="col-md-2 col-sm px-md-1">
                          <b-button
                                  class=" btn-block "
                                  size="sm"
                                  v-on:click="showLogWorkoutModal"
                                  variant="success"
                          >Log workout</b-button>
                        </span>
                    </div>
                </div>
            </div>
        </div>
        <b-modal @ok="logWorkout" id="test123" okTitle="Log workout" okVariant="success" ref="logWorkoutModal"
                 title="Write your results">
            Are you sure you want to log this workout?
        </b-modal>
    </div>
</template>

<script lang="ts">
    /* Font awesome icons */
    import {faClock} from "@fortawesome/free-regular-svg-icons/faClock";
    import {faCalendar} from "@fortawesome/free-solid-svg-icons/faCalendar";
    import {faHashtag} from "@fortawesome/free-solid-svg-icons/faHashtag";
    import {library} from "@fortawesome/fontawesome-svg-core";
    /* public components */
    import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";
    import {Component, Vue} from "vue-property-decorator";
    import {BAlert, BFormInput, BFormTextarea, BModal,BButton, BBadge, InputGroupPlugin} from "bootstrap-vue";
    import datePicker from "vue-bootstrap-datetimepicker";
    import {mask} from "vue-the-mask";
    import VeeValidate from "vee-validate";
    import Spinner from "vue-spinner-component/src/Spinner.vue";
    import "pc-bootstrap4-datetimepicker/build/css/bootstrap-datetimepicker.css";
    import {IWorkoutEditState} from "../../../workout-edit-store/types";
    import {State} from "vuex-class";
    
    /* app components */
    import CrossfitterService from "../../../CrossfitterService";
    import ExercisesListComponent from "./exercises-list-component.vue";
    import ErrorAlertComponent from "../../error-alert-component.vue";
    import EditPlannedWorkoutComponent from "../edit-planned-workout-component.vue";
    
    /* models and styles */
    import {WorkoutViewModel} from "../../../models/viewModels/WorkoutViewModel";
    import {ToLogWorkoutViewModel} from "../../../models/viewModels/ToLogWorkoutViewModel";
    import {WorkoutType} from "../../../models/viewModels/WorkoutType";
    import {ErrorAlertModel} from "../../../models/viewModels/ErrorAlertModel";
    import {SpinnerModel} from "../../../models/viewModels/SpinnerModel";
    import {WindowHelper} from "../../../helpers/WindowHelper";

    library.add(faClock, faHashtag, faCalendar);

    Vue.use(InputGroupPlugin);
    Vue.use(VeeValidate);
    const namespace: string = "workoutEdit";

    declare var workouter: {
        toLogWorkoutRawModel: ToLogWorkoutViewModel;
        workoutViewModel: WorkoutViewModel;
    };

    @Component({
        components: {
            FontAwesomeIcon,
            ExercisesListComponent,
            BFormInput,
            datePicker,
            BFormTextarea,
            BModal,
            BAlert,
            BButton,
            BBadge,
            Spinner,
            ErrorAlertComponent,
            EditPlannedWorkoutComponent
        },
        directives: {mask}
    })
    export default class AmrapNEditComponentComponent extends Vue {
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
                this.model.workoutType = WorkoutType.AMRAPN;
                this.addInnerWorkout();
            }
        }

        mutateData(): void {
        }

        planWorkoutAction(): void {
            this.$validator.validate();

            let scrollToErrors = this.$el.querySelector("[aria-invalid=true]");
            if (scrollToErrors) {
                WindowHelper.scrollToTargetAdjusted(scrollToErrors);
            }
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

        showLogWorkoutModal(): void {
            this.$validator.validate();

            let scrollToErrors = this.$el.querySelector("[aria-invalid=true]");
            if (scrollToErrors) {
                WindowHelper.scrollToTargetAdjusted(scrollToErrors);
            }

            this.$validator.validate().then(isValid => {
                if (isValid) {
                    this.$refs.logWorkoutModal.show();
                }
            });
        }

        hideLogModal(): void {
            this.$refs.logWorkoutModal.hide();
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

        addInnerWorkout() {
            this.model.children.push(
                new WorkoutViewModel({
                    id: 0,
                    title: null,
                    workoutType: WorkoutType.AMRAP,
                    exercisesToDoList: [],
                    children: [],
                    isInnerWorkout: true
                })
            );
        }

        removeInnerWorkout(index) {
            this.model.children.splice(index, 1);
        }
    }
</script>

<style>
</style>