import {PlanningWorkoutLevel} from "../models/viewModels/WorkoutViewModel";
<template>
    <div class="planned-workouts container" v-if="selectedPlannedWorkout != null">
        <div class="row">
            <div class="done-item offset-lg-3 col col-lg-5 my-2 p-0 rounded row no-gutters">
                <div class="workout-sub-type-display mr-2 py-1 rounded-left" v-bind:class="subTypeClass">
                    {{workoutSubTypeDisplayValue}}
                </div>
                <div class="col-11 p-1">
                    <div class="item-header d-flex flex-row justify-content-between  ">
                        <div class="username">
                    <span class="text-info">
                      {{planningLevelDisplayValue}}
                    </span>
                        </div>
                        <div class="">
                            Today
                            <a
                                    @click="showDeleteWorkoutConfirmation(selectedPlannedWorkout.id)"
                                    class="remove-workout pl-1 text-secondary pointer"
                                    title="Remove workout planned"
                            >
                                <i aria-hidden="true" class="fa fa-trash-alt"/>
                            </a>
                        </div>
                    </div>
                    <div class="item-body pt-1">
                        <WorkoutDisplayComponent :workoutViewModel="selectedPlannedWorkout"/>
                    </div>
                    <div class="item-footer text-right pt-2" v-if="selectedPlannedWorkout">
                        <div class="action-buttons">
                            <a
                                    class="btn btn-secondary float-left btn-sm"
                                    role="button"
                                    v-bind:href="'Workout?workoutId=' + this.selectedPlannedWorkout.id"
                            >
              <span class="do-it-text"
              >Edit <font-awesome-icon :icon="['fas', 'edit']"/></span> </a>
                            <b-button @click="showLogWorkout(selectedPlannedWorkout)" size="sm" variant="warning"
                            >Log workout
                            </b-button
                            >
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row mt-1">
            <div class="col-sm mb-1 offset-lg-3 col col-lg-5 px-3 py-2">
                <b-button-group class="btn-group d-flex" size="sm">
                    <b-button
                            class="w-100 "
                            v-bind:class="{ focus: isScaledSelected }"
                            v-if="hasPlannedForLevel(0)"
                            v-on:click="setSelectedPlanned(0)"
                            variant="success"
                    >Scaled
                    </b-button
                    >
                    <b-button
                            class="w-100"
                            v-bind:class="{ focus: isRxSelected }"
                            v-if="hasPlannedForLevel(1)"
                            v-on:click="setSelectedPlanned(1)"
                            variant="warning"
                    >Rx
                    </b-button
                    >
                    <b-button
                            class="w-100"
                            v-bind:class="{ focus: isRxPlusSelected }"
                            v-if="hasPlannedForLevel(2)"
                            v-on:click="setSelectedPlanned(2)"
                            variant="danger"
                    >Rx+
                    </b-button
                    >
                </b-button-group>
            </div>
        </div>


        <b-modal ref="logWorkoutModal" title="Log workout">
            <div class="log-workout" v-if="selectedWorkout">
                <div class="log-workout-container">
                    <div v-if="selectedWorkout.IsHaveCapTime()">
                        <div class="row">
                            <div class="col-sm-12">
                                <b-input-group class="mb-2">
                                    <b-input-group-prepend>
                                        <b-input-group-text tag="span">
                                            <font-awesome-icon
                                                    :icon="['far', 'clock']"
                                            />
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
                        </div>
                        <div class="horizontal-divider d-block ">
                            <hr class="mt-2"/>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 cap-reps-log-container">
                                <b-input-group class="mb-2">
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
                    </div>
                    <div v-if="selectedWorkout.IsAMRAP()">
                        <div class="row">
                            <div class="col-sm-12">
                                <b-input-group class="mb-2">
                                    <b-input-group-prepend>
                                        <b-input-group-text tag="span">
                                            <font-awesome-icon
                                                    :icon="['fas', 'hashtag']"
                                            />
                                        </b-input-group-text>
                                    </b-input-group-prepend>
                                    <b-form-input
                                            aria-describedby="prPercentHelpBlock"
                                            inputmode="numeric"
                                            pattern="[0-9]*"
                                            placeholder="Rounds finished"
                                            type="text"
                                            v-mask="'####'"
                                            v-model="toLogModel.roundsFinished"
                                    />
                                </b-input-group>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <b-input-group class="mb-2">
                                    <b-input-group-prepend>
                                        <b-input-group-text tag="span">
                                            <font-awesome-icon
                                                    :icon="['fas', 'hashtag']"
                                            />
                                        </b-input-group-text>
                                    </b-input-group-prepend>
                                    <b-form-input
                                            aria-describedby="prPercentHelpBlock"
                                            inputmode="numeric"
                                            pattern="[0-9]*"
                                            placeholder="Partial repetitions"
                                            type="text"
                                            v-mask="'####'"
                                            v-model="toLogModel.partialRepsFinished"
                                    />
                                </b-input-group>
                            </div>
                        </div>
                    </div>
                    <div
                            v-if="
              !selectedWorkout.IsAMRAP() && !selectedWorkout.IsHaveCapTime()
            "
                    >
                        Just log workout
                    </div>
                </div>
            </div>

            <div class="col-md-12 text-right">
                <div class="row justify-content-end comments-section">
                    <b-form-textarea
                            :maxlength="200"
                            class="mt-2"
                            id="logWorkoutCommentSection"
                            max-rows="2"
                            name="commentSection"
                            no-resize
                            placeholder="Note: ex. Holy sh*t! Will do it again! Never!"
                            rows="2"
                            type="text"
                            v-model="toLogModel.comment"
                    />
                    <small class="form-text text-muted" id="passwordHelpBlock">
                        Your thoughts on workout. Max length = 200;
                    </small>
                </div>
            </div>
            <div slot="modal-footer">
                <b-button @click="logWorkout" data-dismiss="modal" variant="warning"
                >Log workout
                </b-button>
                <b-button @click="() => {this.$refs.logWorkoutModal.hide();}" data-dismiss="modal">
                    Close
                </b-button
                >
            </div>
        </b-modal>
        <b-modal @ok="deletePlannedWorkout" okTitle="Delete" okVariant="danger" ref="removeFromPlannedModal"
                 title="Sure to remove workout from planning?">
            Are you sure to remove workout from planning?
        </b-modal>
    </div>
</template>

<script lang="ts">
    /* Font awesome icons */
    import {faPlus} from "@fortawesome/free-solid-svg-icons/faPlus";
    import {faEdit} from "@fortawesome/free-solid-svg-icons/faEdit";
    import {faGrinBeam} from "@fortawesome/free-regular-svg-icons/faGrinBeam";
    import {faClock} from "@fortawesome/free-regular-svg-icons/faClock";
    import {faTrashAlt} from "@fortawesome/free-solid-svg-icons/faTrashAlt";
    import {faCalendar} from "@fortawesome/free-solid-svg-icons/faCalendar";
    import {faHashtag} from "@fortawesome/free-solid-svg-icons/faHashtag";

    import {library} from "@fortawesome/fontawesome-svg-core";
    /* public components */
    import {Component, Prop, Vue} from "vue-property-decorator";
    import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";
    import {BButton, BButtonGroup, BFormInput, BFormTextarea, BModal, InputGroupPlugin} from "bootstrap-vue";
    import datePicker from "vue-bootstrap-datetimepicker";
    import "pc-bootstrap4-datetimepicker/build/css/bootstrap-datetimepicker.css";
    import {mask} from "vue-the-mask";
    /* app components */
    import WorkoutDisplayComponent from "./workout-display-component.vue";
    /* models and styles */
    import {ToLogWorkoutViewModel} from "../models/viewModels/ToLogWorkoutViewModel";
    import "./../style/workout-done-item.scss";

    import {PlanningWorkoutLevel, WorkoutViewModel} from "../models/viewModels/WorkoutViewModel";
    import {WodSubType} from "../models/viewModels/WodSubType";

    library.add(
        faGrinBeam,
        faClock,
        faPlus,
        faTrashAlt,
        faEdit,
        faCalendar,
        faHashtag
    );

    Vue.use(InputGroupPlugin);

    @Component({
        components: {
            FontAwesomeIcon,
            WorkoutDisplayComponent,
            BButtonGroup,
            BButton,
            BModal,
            BFormInput,
            datePicker,
            BFormTextarea
        },
        directives: {mask}
    })
    export default class PlannedWorkoutDisplayComponent extends Vue {
        @Prop() plannedWorkouts: WorkoutViewModel[];
        show: boolean = true;
        isScaledSelected: boolean = false;
        isRxSelected: boolean = false;
        isRxPlusSelected: boolean = false;
        selectedWorkout: WorkoutViewModel = null;
        selectedPlannedWorkout: WorkoutViewModel = null;
        toLogModel: ToLogWorkoutViewModel = new ToLogWorkoutViewModel();
        // isForTimesWorkouts: boolean = false;
        selectedPlanningLevel: PlanningWorkoutLevel = PlanningWorkoutLevel.Scaled;

        toRemovePlannedId: number = 0;
        subTypeClass: string = "";

        $refs: {
            logWorkoutModal: HTMLFormElement;
            removeFromPlannedModal: HTMLFormElement;
        };

        get workoutSubTypeDisplayValue() {
            if (this.selectedPlannedWorkout == null) {
                return;
            }
            this.subTypeClass = this.getSubTypeClass();
            switch (this.selectedPlannedWorkout.wodSubType) {
                case WodSubType.Skill:
                    return "Skill";
                case WodSubType.Wod:
                    return "WOD";
                case WodSubType.AccessoryWork:
                    return "Accessory";
            }
            
        }

        get planningLevelDisplayValue() {
            if (this.selectedPlannedWorkout == null) {
                return;
            }
            switch (this.selectedPlannedWorkout.planningWorkoutLevel) {
                case PlanningWorkoutLevel.Scaled:
                    return "Scaled";
                case PlanningWorkoutLevel.Rx:
                    return "Rx";
                case PlanningWorkoutLevel.RxPlus:
                    return "Rx+";
            }
        }

        mounted() {
            this.selectedPlannedWorkout = this.plannedWorkouts[0];
            if (this.selectedPlannedWorkout == null) {
                return;
            }
            this.setVisibilityByLevel(this.selectedPlannedWorkout.planningWorkoutLevel);
        }

        getSubTypeClass() {
            switch (this.selectedPlannedWorkout.wodSubType) {
                case WodSubType.Skill:
                    return 'bg-info text-white';
                case WodSubType.Wod:
                    return 'bg-danger text-white';
                case WodSubType.AccessoryWork:
                    return 'bg-warning text-white';
            }

        }

        deletePlannedWorkout() {
            this.$refs.removeFromPlannedModal.hide();
            this.isScaledSelected = false;
            this.isRxSelected = false;
            this.isRxPlusSelected = false;
            this.$emit("deletePlannedWorkout", this.toRemovePlannedId);
        }

        showDeleteWorkoutConfirmation(wodId) {
            this.toRemovePlannedId = wodId;
            this.$refs.removeFromPlannedModal.show();
        }

        showLogWorkout(workoutViewModel: WorkoutViewModel) {
            this.selectedWorkout = workoutViewModel;

            this.toLogModel = new ToLogWorkoutViewModel();
            this.toLogModel.selectedWorkoutId = this.selectedWorkout.id;
            this.toLogModel.displayDate = workoutViewModel.displayPlanDate;

            this.$refs.logWorkoutModal.show();
        }

        logWorkout() {
            this.$refs.logWorkoutModal.hide();
            let toLogWorkoutModel = this.toLogModel;
            this.$emit("logWorkout", toLogWorkoutModel);
        }

        setVisibilityByLevel(planningWorkoutLevel: PlanningWorkoutLevel) {
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

        hasPlannedForLevel(planningWorkoutLevel: PlanningWorkoutLevel) {
            let found = this.plannedWorkouts.find(x => x.planningWorkoutLevel == planningWorkoutLevel);
            return found != null;
        }

        setSelectedPlanned(planningWorkoutLevel: PlanningWorkoutLevel) {
            debugger;
            this.selectedPlanningLevel = planningWorkoutLevel;
            let found = this.plannedWorkouts.find(x => x.planningWorkoutLevel == planningWorkoutLevel);
            this.selectedPlannedWorkout = found;
            this.setVisibilityByLevel(planningWorkoutLevel);
            this.setVisibilityByLevel(planningWorkoutLevel);
        }
    }
</script>

<style/>
