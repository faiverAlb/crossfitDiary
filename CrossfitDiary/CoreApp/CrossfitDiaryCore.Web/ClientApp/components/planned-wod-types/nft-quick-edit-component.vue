<template>
    <div>
        <div class="routine-complex-info">
            <div class="pt-2 general-info-container">
                
                <div class="planned-exercises">
                    <div class="py-2">
                        <div class="routines-container pt-0">
                            <div
                                    :key="`${exercise.id}-${index}`"
                                    v-for="(exercise,index) in model.exercisesToDoList"
                            >
                                <div class="simple-routine-item">
                                    <div class="form-row">
                                        <div class="form-group my-1 col-2">
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
    export default class NFTQuickEditComponent extends Vue {
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