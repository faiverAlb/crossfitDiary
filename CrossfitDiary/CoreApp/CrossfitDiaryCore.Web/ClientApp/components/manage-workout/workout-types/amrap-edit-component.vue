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
                <div class="row">
                    <div class="col-lg-3 pb-2">
                        <label
                                class="sr-only"
                                for="timeToWork"
                        >Time to work:</label>
                        <b-input-group size="sm">
                            <b-input-group-prepend>
                                <b-input-group-text tag="span">
                                    <font-awesome-icon :icon="['far','clock']"/>
                                </b-input-group-text>
                            </b-input-group-prepend>
                            <b-form-input
                                    :state="fields.timeToWork && fields.timeToWork.valid"
                                    id="timeToWork"
                                    name="timeToWork"
                                    placeholder="Time to work"
                                    type="tel"
                                    v-mask="'##:##'"
                                    v-model="model.timeToWork"
                                    v-validate.initial="'required|length:5'"
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
                                for="restTime"
                        >Rest after</label>
                        <b-input-group
                                class="py-2"
                                prepend="Rest after round:"
                                size="sm"
                        >
                            <b-form-input
                                    aria-describedby="prPercentHelpBlock"
                                    id="restTime"
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
                            id="commentSection"
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
                            id="workoutHelpBlock"
                    >
                        Workout note
                    </small>
                </div>
            </div>
        </div>
        <EditPlannedWorkoutComponent
                :planningWorkout="planWorkoutModel"
                @planWorkoutAction="planWorkoutAction"
                v-if="workoutEdit.canUserSeePlanWorkouts && spinner.status == false"
        />

        <div class="want-to-log-container my-3"
             v-if="!workoutEdit.canUserSeePlanWorkouts">
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
                                id="wodThoughtsHelpBlock"
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
                                    v-model="toLogModel.wodSubType"
                            />
                        </b-form-group>

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
        <b-modal @ok="logWorkout"  okTitle="Log workout" okVariant="success" ref="logWorkoutModal"
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
    import {Component, Mixins, Vue} from "vue-property-decorator";
    import {
        BAlert,
        BBadge,
        BButton,
        BFormGroup,
        BFormInput,
        BFormRadioGroup,
        BFormTextarea,
        BModal,
        InputGroupPlugin,
    } from "bootstrap-vue";
    import datePicker from "vue-bootstrap-datetimepicker";
    import {mask} from "vue-the-mask";
    import VeeValidate from "vee-validate";
    import Spinner from "vue-spinner-component/src/Spinner.vue";
    import "pc-bootstrap4-datetimepicker/build/css/bootstrap-datetimepicker.css";
    /* app components */
    import ExercisesListComponent from "./exercises-list-component.vue";
    import ErrorAlertComponent from "../../error-alert-component.vue";
    import EditPlannedWorkoutComponent from "../edit-planned-workout-component.vue";
    /* models and styles */
    import {WorkoutType} from "../../../models/viewModels/WorkoutType";
    import {WorkoutTypeComponent} from "./workoutTypeMixin";

    library.add(faClock, faHashtag, faCalendar);

    Vue.use(InputGroupPlugin);
    Vue.use(VeeValidate);


    @Component({
        components: {
            FontAwesomeIcon,
            ExercisesListComponent,
            BFormInput,
            datePicker,
            BAlert,
            BButton,
            BModal,
            BFormTextarea,
            BBadge,
            BFormGroup,
            BFormRadioGroup,
            Spinner,
            ErrorAlertComponent,
            EditPlannedWorkoutComponent
        },
        directives: {mask}
    })
    export default class AmrapEditComponent extends Mixins(WorkoutTypeComponent) {
        mounted() {
            this.model.workoutType = WorkoutType.AMRAP;
        }
    }
</script>

<style>
</style>