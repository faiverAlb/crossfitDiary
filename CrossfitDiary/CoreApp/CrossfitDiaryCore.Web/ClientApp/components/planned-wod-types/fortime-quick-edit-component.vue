<template>
    <div>
        <div class="routine-complex-info">
            <div class="pt-2 general-info-container">
                <div class="row">
                    <div class="col time-cap-container pb-2">
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
                                    disabled="true"
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

                    <div class="col pb-2">
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
                                    disabled="true"
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
                <div class="planned-exercises">
                    <div class="py-2">
                        <div class="routines-container pt-0">
                            <div
                                    :key="`${exercise.id}-${index}`"
                                    v-for="(exercise,index) in model.exercisesToDoList"
                            >
                                <div class="simple-routine-item">
                                    <div class="form-row">
                                        <div class="form-group my-1 col-lg-auto">
                                            <b-input-group size="sm">
                                                <b-input-group-prepend class="form-control exercise-title" is-text
                                                                       size="sm">
                                                    <span>{{exercise.abbreviation}}</span>
                                                </b-input-group-prepend>
                                            </b-input-group>
                                        </div>
                                        <ExerciseMeasureQuickEditComponent :measure="exercise.distanceMeasure"
                                                                           v-if="exercise.distanceMeasure "/>
                                        <ExerciseMeasureQuickEditComponent :measure="exercise.countMeasure"
                                                                           v-if="exercise.countMeasure "/>
                                        <ExerciseMeasureQuickEditComponent :measure="exercise.weightMeasure"
                                                                           v-if="canSeeWeightsMeasures(exercise)"/>
                                        <ExerciseMeasureQuickEditComponent :measure="exercise.altWeightMeasure"
                                                                           v-if="canSeeWeightsMeasures(exercise)"/>
                                        <ExerciseMeasureQuickEditComponent :measure="exercise.heightMeasure"
                                                                           v-if="exercise.heightMeasure"/>
                                        <ExerciseMeasureQuickEditComponent :measure="exercise.caloriesMeasure"
                                                                           v-if="exercise.caloriesMeasure"/>
                                        <ExerciseMeasureQuickEditComponent :measure="exercise.timeMeasure"
                                                                           v-if="exercise.timeMeasure"/>


                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
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
    import {Component, Prop, Vue} from "vue-property-decorator";
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
    import "pc-bootstrap4-datetimepicker/build/css/bootstrap-datetimepicker.css";
    /* app components */
    // import ExercisesListComponent from "./exercises-list-component.vue";
    // import ErrorAlertComponent from "../../error-alert-component.vue";
    // import EditPlannedWorkoutComponent from "../edit-planned-workout-component.vue";
    import {WorkoutViewModel} from "../../models/viewModels/WorkoutViewModel";
    import ExerciseMeasureQuickEditComponent from "./exercise-measure-quick-edit-component.vue";
    import {WeightDisplayType} from "../../models/viewModels/WeightDisplayType";

    import "./../../style/quick-manage-workout.scss";

    library.add(faClock, faHashtag, faCalendar);

    Vue.use(InputGroupPlugin);

    @Component({
        components: {
            ExerciseMeasureQuickEditComponent,
            FontAwesomeIcon,
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
        },
        directives: {mask}
    })
    export default class FortimeQuickEditComponent extends Vue {
        @Prop()
        model: WorkoutViewModel;


        canSeeWeightsMeasures(exercise) {
            return exercise.weightMeasure;
        }

        weightTypeBadgeClass(exercise) {
            if (exercise.weightDisplayType == WeightDisplayType.PercentMaxPM) {
                return "badge-success";
            }
            if (exercise.weightDisplayType == WeightDisplayType.PercentPreviousPM) {
                return "badge-info";
            }
            return "";
        }

        weightTypeDescription(exercise) {
            if (exercise.weightDisplayType == WeightDisplayType.PercentMaxPM) {
                return "% of max weight PM";
            }
            if (exercise.weightDisplayType == WeightDisplayType.PercentPreviousPM) {
                return "% of previous weight PM";
            }
            return "";
        }

        shouldShowPercentWeightsMeasures(exercise): boolean {
            return exercise.weightDisplayType != WeightDisplayType.Default /* && this.isFindMaxWeight === false */;
        }


    }
</script>

<style>

</style>