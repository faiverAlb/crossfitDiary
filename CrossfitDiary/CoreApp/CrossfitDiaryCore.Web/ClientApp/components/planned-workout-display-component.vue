planned-workouts containerimport {PlanningWorkoutLevel} from "../models/viewModels/WorkoutViewModel";
import {PlanningWorkoutLevel} from "../models/viewModels/WorkoutViewModel";
<template>
    <div class="planned-workouts container">
        <div
                :key="levelPlanningWod.id"
                class="row"
                v-for="levelPlanningWod in selectedLevelPlanningWorkouts">
            <div class="done-item offset-lg-3 col col-lg-5 p-0 rounded row no-gutters">
                <div class="workout-sub-type-display mr-2 py-1 rounded-left"
                     v-bind:class="levelPlanningWod.subTypeClass">
                    {{levelPlanningWod.workoutSubTypeDisplayValue}}
                </div>
                <div class="col-11 p-1">
                    <div class="item-header d-flex flex-row justify-content-between">
                        <div class="username">
                            <span class="text-info">
                              {{levelPlanningWod.planningLevelDisplayValue}}
                            </span>
                        </div>
                        <div class="">
                            Today
                            <a
                                    @click="showDeleteWorkoutConfirmation(levelPlanningWod.id)"
                                    class="remove-workout pl-1 text-secondary pointer"
                                    title="Remove workout planned"
                            >
                                <i aria-hidden="true" class="fa fa-trash-alt"/>
                            </a>
                        </div>
                    </div>
                    <div class="item-body pt-1">
                        <WorkoutDisplayComponent :workoutViewModel="levelPlanningWod.workoutViewModel"/>
                    </div>
                    <div class="item-footer text-right pt-2" v-if="levelPlanningWod.workoutViewModel">
                        <div class="action-buttons">
                            <a
                                    class="btn btn-secondary float-left btn-sm"
                                    role="button"
                                    v-bind:href="'Workout?workoutId=' + levelPlanningWod.workoutViewModel.id"
                            >
                              <span class="do-it-text"
                              >Edit <font-awesome-icon :icon="['fas', 'edit']"/></span> </a>
                            <b-button @click="showLogWorkout(levelPlanningWod)" size="sm"
                                      variant="warning">
                                Log workout
                            </b-button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm mb-1 offset-lg-3 col col-lg-5 px-0 ">
                <b-button-group class="btn-group d-flex" size="sm">
                    <b-button
                            class=" "
                            v-bind:class="{active:isScaledSelected}"
                            v-if="hasPlannedForLevel(0)"
                            v-on:click="setSelectedPlanned(0)"
                            variant="outline-info"
                    >
                        <font-awesome-icon :icon="['fas', 'cat']" class="" size="lg"/>
                        Scaled
                    </b-button
                    >
                    <b-button
                            class=""
                            v-bind:class="{  active:isRxSelected  }"
                            v-if="hasPlannedForLevel(1)"
                            v-on:click="setSelectedPlanned(1)"
                            variant="outline-info"
                    >
                        <font-awesome-icon :icon="['fas', 'horse']" size="lg"/>
                        Rx
                    </b-button
                    >
                    <b-button
                            class=""
                            v-bind:class="{    active:isRxPlusSelected   }"
                            v-if="hasPlannedForLevel(2)"
                            v-on:click="setSelectedPlanned(2)"
                            variant="outline-info"
                    >
                        <span class="text-left">
                            <font-awesome-icon :icon="['fas', 'dog']" class="mr-1" size="lg"/>  
                        </span>
                        <span>
                            Rx+
                        </span>
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
                    <div class="row d-flex justify-content-end mt-3" v-if="selectedWorkout.IsFindMaxWeight()">
                        <div class="col-sm-12">
                            <label class="sr-only"
                                   for="maxWeightInput"
                            >Found max weight</label>
                            <b-input-group size="sm">
                                <b-input-group-prepend>
                                    <b-input-group-text tag="span">
                                        Max weight (kg):
                                    </b-input-group-text>
                                </b-input-group-prepend>
                                <b-form-input
                                        :state="fields.weight && fields.weight.valid"
                                        id="maxWeightInput"
                                        name="weight"
                                        placeholder="Weight"
                                        type="number"
                                        v-model="toLogModel.weight"
                                        v-validate.initial="'required'"
                                />
                            </b-input-group>
                        </div>
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
    import {faCat} from "@fortawesome/free-solid-svg-icons/faCat";
    import {faHorse} from "@fortawesome/free-solid-svg-icons/faHorse";
    import {faDog} from "@fortawesome/free-solid-svg-icons/faDog";

    import {library} from "@fortawesome/fontawesome-svg-core";
    /* public components */
    import {Component, Prop, Vue, Watch} from "vue-property-decorator";
    import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";
    import {BButton, BButtonGroup, BFormInput, BFormTextarea, BModal, InputGroupPlugin} from "bootstrap-vue";
    import datePicker from "vue-bootstrap-datetimepicker";
    import "pc-bootstrap4-datetimepicker/build/css/bootstrap-datetimepicker.css";
    import {mask} from "vue-the-mask";
    import VeeValidate from "vee-validate";
    /* app components */
    import WorkoutDisplayComponent from "./workout-display-components/workout-display-component.vue";
    /* models and styles */
    import {ToLogWorkoutViewModel} from "../models/viewModels/ToLogWorkoutViewModel";
    import "./../style/workout-done-item.scss";

    import {PlanningWorkoutLevel, WorkoutViewModel} from "../models/viewModels/WorkoutViewModel";
    import {PlanningWorkoutViewModel} from "../models/viewModels/PlanningWorkoutViewModel";
    import {WorkoutType} from "../models/viewModels/WorkoutType";
    import {WodSubType} from "../models/viewModels/WodSubType";
    import CrossfitterService from "../CrossfitterService";

    library.add(
        faGrinBeam,
        faClock,
        faPlus,
        faTrashAlt,
        faEdit,
        faCalendar,
        faHashtag,
        faCat,
        faHorse,
        faDog
    );

    Vue.use(InputGroupPlugin);
    Vue.use(VeeValidate);

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
        @Prop()
        plannedWorkouts: { planningWorkoutLevel: PlanningWorkoutLevel, planningWodItem: PlanningWorkoutViewModel[] };
        show: boolean = true;
        isScaledSelected: boolean = false;
        isRxSelected: boolean = false;
        isRxPlusSelected: boolean = false;
        selectedWorkout: WorkoutViewModel = null;
        selectedPlannedWorkoutLevel: WodSubType;
        selectedLevelPlanningWorkouts: PlanningWorkoutViewModel[] = [];
        toLogModel: ToLogWorkoutViewModel = new ToLogWorkoutViewModel();
        selectedPlanningLevel: PlanningWorkoutLevel = PlanningWorkoutLevel.Scaled;
        toRemovePlannedId: number = 0;
        $refs: {
            logWorkoutModal: HTMLFormElement;
            removeFromPlannedModal: HTMLFormElement;
        };

        @Watch('plannedWorkouts')
        onPlannedWorkoutsUpdate(oldVal, newVal) {
            this.selectFirstLevelWods();
        }

        selectFirstLevelWods() {
            if (this.plannedWorkouts[PlanningWorkoutLevel.Scaled] != null) {
                this.selectedLevelPlanningWorkouts = this.plannedWorkouts[PlanningWorkoutLevel.Scaled];
                this.setVisibilityByLevel(PlanningWorkoutLevel.Scaled);
                return;
            }
            if (this.plannedWorkouts[PlanningWorkoutLevel.Rx] != null) {
                this.selectedLevelPlanningWorkouts = this.plannedWorkouts[PlanningWorkoutLevel.Rx];
                this.setVisibilityByLevel(PlanningWorkoutLevel.Rx);
                return;
            }
            if (this.plannedWorkouts[PlanningWorkoutLevel.RxPlus] != null) {
                this.selectedLevelPlanningWorkouts = this.plannedWorkouts[PlanningWorkoutLevel.RxPlus];
                this.setVisibilityByLevel(PlanningWorkoutLevel.RxPlus);
                return;
            }
            this.selectedLevelPlanningWorkouts = [];
            this.setVisibilityByLevel(null);
        }

        mounted() {
            this.selectFirstLevelWods();
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

        showLogWorkout(planningWorkoutViewModel: PlanningWorkoutViewModel) {
            this.selectedWorkout = planningWorkoutViewModel.workoutViewModel;
            this.selectedPlannedWorkoutLevel = planningWorkoutViewModel.wodSubType;

            this.toLogModel = new ToLogWorkoutViewModel();
            this.toLogModel.selectedWorkoutId = this.selectedWorkout.id;
            // this.toLogModel.displayDate = workoutViewModel.displayPlanDate;

            this.$refs.logWorkoutModal.show();
        }

        logWorkout() {
            this.$validator.validate().then(isValid => {
                if (isValid) {
                    let toLogWorkoutModel = this.toLogModel;
                    toLogWorkoutModel.wodSubType = this.selectedPlannedWorkoutLevel;
                    
                    this.$emit("logWorkout", toLogWorkoutModel,this.selectedWorkout);
                    this.$refs.logWorkoutModal.hide();
                }
            });
           
        }

        setVisibilityByLevel(planningWorkoutLevel: PlanningWorkoutLevel) {
            this.isScaledSelected = this.isRxSelected = this.isRxPlusSelected = false;
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
            let found = this.plannedWorkouts[planningWorkoutLevel];
            return found != null;
        }

        setSelectedPlanned(planningWorkoutLevel: PlanningWorkoutLevel) {
            this.selectedLevelPlanningWorkouts = this.plannedWorkouts[planningWorkoutLevel];
            this.setVisibilityByLevel(planningWorkoutLevel);
        }
    }
</script>

<style/>
