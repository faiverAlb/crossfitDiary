﻿<template>
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
                                    placeholder="General time Cap"
                                    type="text"
                                    v-mask="'##:##'"
                                    v-model="model.timeCap"
                                    v-validate.initial="'required|length:5'"
                            />
                        </b-input-group>
                    </div>
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
                                                v-bind:for="'roundsCount'+index"
                                        >Rounds count:</label>
                                        <b-input-group size="sm">
                                            <b-input-group-prepend>
                                                <b-input-group-text tag="span">
                                                    <font-awesome-icon :icon="['fas','hashtag']"/>
                                                </b-input-group-text>
                                            </b-input-group-prepend>
                                            <b-form-input
                                                    :state="fields['roundsCount'+index] && fields['roundsCount'+index].valid"
                                                    inputmode="numeric"
                                                    min="1"
                                                    placeholder="Rounds count"
                                                    type="text"
                                                    v-bind:id="'rounds'+ index"
                                                    v-bind:name="'roundsCount'+ index"
                                                    v-mask="'#####'"
                                                    v-model="childWorkout.roundsCount"
                                                    v-validate.initial="'required'"
                                            />
                                        </b-input-group>
                                    </div>
                                </div>
                                <ExercisesListComponent :exercisesToDo="childWorkout.exercisesToDoList">
                                    <template v-slot:additional-exercise-settings>
                                        <p-check class="p-icon p-smooth" color="info" name="check"
                                                 v-model="childWorkout.asNonBreakingSet">
                                            <font-awesome-icon :icon="['fas','check']" class="icon" slot="extra"/>
                                            As non-breaking set
                                        </p-check>
                                    </template>
                                </ExercisesListComponent>
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
                                                size="sm"
                                        >
                                            <b-form-input
                                                    aria-describedby="prPercentHelpBlock"
                                                    id="restInput"
                                                    placeholder="Time"
                                                    type="tel"
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
                    <button
                            class="btn btn-warning"
                            v-on:click="addInnerWorkout"
                    >Add workout after
                    </button>
                </div>
            </div>
            <div class="comments-section">
                <b-form-textarea
                        :maxlength="150"
                        class="mt-2" id="commentSection"
                        max-rows="2"
                        name="commentSection"
                        no-resize
                        placeholder="Note: ex. girls do max 30kg"
                        rows="2"
                        size="sm"
                        t
                        type="text"
                        v-model="model.comment"
                />
                <small
                        class="form-text text-muted text-right"
                        id="mutedWorkoutNote"
                >
                    Workout note
                </small>
            </div>
        </div>
        <EditPlannedWorkoutComponent
                :planningWorkout="planWorkoutModel"
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
                                        placeholder="Count"
                                        type="number"
                                        v-model="toLogModel.repsToFinishOnCapTime"
                                >
                                </b-form-input>
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
                                id="passwordHelpBlock"
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
    import {faTimes} from "@fortawesome/free-solid-svg-icons/faTimes";
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
        InputGroupPlugin
    } from "bootstrap-vue";
    import datePicker from "vue-bootstrap-datetimepicker";
    import {mask} from "vue-the-mask";
    import Spinner from "vue-spinner-component/src/Spinner.vue";
    import "pc-bootstrap4-datetimepicker/build/css/bootstrap-datetimepicker.css";
    /* app components */
    import ExercisesListComponent from "./exercises-list-component.vue";
    import ErrorAlertComponent from "../../error-alert-component.vue";
    import EditPlannedWorkoutComponent from "../edit-planned-workout-component.vue";
    /* models and styles */
    import {WorkoutViewModel} from "../../../models/viewModels/WorkoutViewModel";
    import {WorkoutType} from "../../../models/viewModels/WorkoutType";
    import {WorkoutTypeComponent} from "./workoutTypeMixin";

    library.add(faClock, faHashtag, faCalendar, faTimes);
    Vue.use(InputGroupPlugin);

    @Component({
        components: {
            FontAwesomeIcon,
            ExercisesListComponent,
            BButton,
            BFormInput,
            datePicker,
            BFormTextarea,
            BModal,
            BAlert,
            BBadge,
            Spinner,
            ErrorAlertComponent,
            EditPlannedWorkoutComponent,
            BFormRadioGroup,
            BFormGroup,

        },
        directives: {mask}
    })
    export default class ForTimeEdiForTimeNEditComponenttComponent extends Mixins(WorkoutTypeComponent) {
        mounted() {
            this.model.workoutType = WorkoutType.ForTimeManyInners;
            if (this.model.children.length != 0) {
                return;
            }
            this.addInnerWorkout();
        }

        addInnerWorkout() {
            this.model.children.push(
                new WorkoutViewModel({
                    id: 0,
                    title: null,
                    workoutType: WorkoutType.ForTime,
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