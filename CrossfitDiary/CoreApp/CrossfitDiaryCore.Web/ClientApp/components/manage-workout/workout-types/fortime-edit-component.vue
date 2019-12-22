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
            <div class="pt-2 general-info-container" v-bind:class="{saving:spinner.status}">
                <div class="dashed-container-description border-info text-center">
                    <b-badge variant="info">1</b-badge>
                    Create workout:
                </div>
                <div class="row">
                    <div class="col-lg-3 time-cap-container pb-2">
                        <label
                                class="sr-only"
                                for="timeCapInput"
                        >Time cap:</label>
                        <b-input-group class="input-group" size="sm">
                            <b-input-group-prepend>
                                <b-input-group-text tag="span">
                                    <font-awesome-icon
                                            :icon="['fas','clock']"
                                            class="fa-lg time-cap-icon"
                                    />
                                </b-input-group-text>
                            </b-input-group-prepend>
                            <b-form-input
                                    :state="fields.capTime && fields.capTime.valid"
                                    id="timeCapInput"
                                    name="capTime"
                                    placeholder="Time Cap"
                                    type="text"
                                    v-mask="'##:##'"
                                    v-model="model.timeCap"
                                    v-validate.initial="'required|length:5'"
                            />
                        </b-input-group>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 pb-2">
                        <label
                                class="sr-only"
                                for="roundsInput"
                        >Rounds count:</label>
                        <b-input-group size="sm">
                            <b-input-group-prepend>
                                <b-input-group-text tag="span">
                                    <font-awesome-icon :icon="['fas','hashtag']"/>
                                </b-input-group-text>
                            </b-input-group-prepend>
                            <b-form-input
                                    :state="fields.roundsCount && fields.roundsCount.valid"
                                    id="roundsInput"
                                    inputmode="numeric"
                                    min="1"
                                    name="roundsCount"
                                    placeholder="Rounds count"
                                    type="text"
                                    v-mask="'#####'"
                                    v-model="model.roundsCount"
                                    v-validate.initial="'required'"
                            />
                        </b-input-group>
                    </div>
                </div>
                <ExercisesListComponent :exercisesToDo="model.exercisesToDoList"/>
                <div
                        class="row py-1"
                        v-if="model.exercisesToDoList.length > 0"
                >
                    <div class="col-lg-4">
                        <label
                                class="sr-only"
                                for="timeRestInput"
                        >Rest after</label>
                        <b-input-group
                                class="py-2"
                                prepend="Rest after round:"
                                size="sm"
                        >
                            <b-form-input
                                    aria-describedby="prPercentHelpBlock"
                                    id="timeRestInput"
                                    placeholder="Time"
                                    type="tel"
                                    v-mask="'##:##'"
                                    v-model="model.restBetweenRounds"
                            />
                        </b-input-group>
                    </div>
                </div>
                <div class="comments-section">
                    <b-form-textarea
                            :maxlength="150"
                            class="mt-2"
                            id="workoutCommentSection"
                            max-rows="3"
                            name="commentSection"
                            no-resize
                            placeholder="Note: ex. girls do max 30kg"
                            rows="2"
                            size="sm"
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
                        <div class="col-lg-4 col-sm data-selector-container">
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
                        <div class="col-lg-3 col-sm pr-lg-2 total-time-log-container">
                            <b-input-group class="mb-2" size="sm">
                                <b-input-group-prepend>
                                    <b-input-group-text tag="span">
                                        <font-awesome-icon :icon="['fas','clock']"/>
                                    </b-input-group-text>
                                </b-input-group-prepend>
                                <b-form-input
                                        aria-describedby="prPercentHelpBlock"
                                        placeholder="Time"
                                        type="tel"
                                        v-mask="'##:##'"
                                        v-model="toLogModel.timePassed"
                                />
                            </b-input-group>
                        </div>
                        <div class="vertical-divider d-none d-sm-block"></div>
                        <div class="horizontal-divider d-block d-sm-none ">
                            <hr/>
                        </div>
                        <div class="col-lg-3 col-sm  pl-lg-2 cap-reps-log-container">
                            <b-input-group class="mb-2" size="sm">
                                <b-input-group-prepend>
                                    <b-input-group-text tag="span">
                                        Cap +
                                    </b-input-group-text>
                                </b-input-group-prepend>
                                <b-form-input
                                        inputmode="numeric"
                                        pattern="[0-9]*"
                                        placeholder="Count"
                                        type="text"
                                        v-model="toLogModel.repsToFinishOnCapTime"
                                >
                                </b-form-input>
                            </b-input-group>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm offset-5">
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
                                :maxlength="200"
                                class="mt-2"
                                id="logWorkoutCommentSection"
                                max-rows="2"
                                name="commentSection"
                                no-resize
                                placeholder="Note: ex. Holy sh*t! Will do it again!"
                                rows="2"
                                size="sm"
                                type="text"
                                v-model="toLogModel.comment"
                        />
                        <small
                                class="form-text text-muted"
                                id="thoughtsHelpBlock"
                        >
                            Your thoughts on wod. Max length = 200
                        </small>
                    </div>
                    <div class="d-flex justify-content-end mt-3">
                        <b-form-group>
                            <b-form-radio-group
                                    :options="wodSubTypes"
                                    button-variant="outline-primary"
                                    buttons
                                    name="radio-btn-outline"
                                    size="sm"
                                    v-model="selectedSubType"
                            />
                        </b-form-group>

                    </div>
                    <div
                            class="row justify-content-end mt-3"
                            v-if="spinner.status == false"
                    >
                            <span class="col-md-2 col-sm px-md-1 mx-md-2">
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
    import {faClock} from "@fortawesome/free-solid-svg-icons/faClock";
    import {faCalendar} from "@fortawesome/free-solid-svg-icons/faCalendar";
    import {faHashtag} from "@fortawesome/free-solid-svg-icons/faHashtag";
    import {library} from "@fortawesome/fontawesome-svg-core";
    /* public components */
    import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";
    import {Component, Vue} from "vue-property-decorator";
    import {
        BAlert,
        BBadge,
        BButton,
        BButtonGroup,
        BFormGroup,
        BFormInput,
        BFormRadioGroup,
        BFormTextarea,
        BModal,
        InputGroupPlugin
    } from "bootstrap-vue";

    import datePicker from "vue-bootstrap-datetimepicker";
    import {mask} from "vue-the-mask";
    import VeeValidate from "vee-validate";
    import Spinner from "vue-spinner-component/src/Spinner.vue";
    import "pc-bootstrap4-datetimepicker/build/css/bootstrap-datetimepicker.css";
    import {State} from "vuex-class";
    import {IWorkoutEditState} from "../../../workout-edit-store/types";
    /* app components */
    import CrossfitterService from "../../../CrossfitterService";
    import ExercisesListComponent from "./exercises-list-component.vue";
    import ErrorAlertComponent from "../../error-alert-component.vue";
    import EditPlannedWorkoutComponent from "../edit-planned-workout-component.vue";
    /* models and styles */
    import {WorkoutViewModel} from "../../../models/viewModels/WorkoutViewModel";
    import {ToLogWorkoutViewModel} from "../../../models/viewModels/ToLogWorkoutViewModel";
    import {WorkoutType} from "../../../models/viewModels/WorkoutType";
    import {WodSubType} from "../../../models/viewModels/WodSubType";
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
            datePicker,
            BFormInput,
            BFormTextarea,
            BAlert,
            BButton,
            BButtonGroup,
            BModal,
            BBadge,
            BFormRadioGroup,
            BFormGroup,
            Spinner,
            ErrorAlertComponent,
            EditPlannedWorkoutComponent,
        },
        directives: {mask}
    })
    export default class ForTimeEditComponent extends Vue {
        model: WorkoutViewModel = new WorkoutViewModel();
        toLogModel: ToLogWorkoutViewModel = new ToLogWorkoutViewModel();
        errorAlertModel: ErrorAlertModel = new ErrorAlertModel();
        spinner: SpinnerModel = new SpinnerModel(false);
        $refs: {
            logWorkoutModal: HTMLFormElement;
        };
        wodSubTypes = [
            {text: 'Skill', value: WodSubType.Skill},
            {text: 'Workout', value: WodSubType.Wod},
            {text: 'Accessory', value: WodSubType.AccessoryWork}
        ];
        selectedSubType = WodSubType.Skill;

        @State("workoutEdit")
        workoutEdit: IWorkoutEditState;

        mounted() {
            if (workouter != null && workouter.toLogWorkoutRawModel != null) {
                this.model = workouter.toLogWorkoutRawModel.workoutViewModel;
                this.toLogModel = workouter.toLogWorkoutRawModel;
                this.selectedSubType = workouter.toLogWorkoutRawModel.wodSubType;
            } else if (workouter != null && workouter.workoutViewModel != null) {
                this.model = workouter.workoutViewModel;
            } else {
                this.model.workoutType = WorkoutType.ForTime;
            }
        }

        mutateData(): void {
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

    }
</script>

<style>
</style>