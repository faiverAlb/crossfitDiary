import {WeightDisplayType} from "../../../models/viewModels/WeightDisplayType";
<template>
    <div class="simple-routine-item">
        <div class="form-row">
            <div class="form-group my-1 col-lg-auto">
                <b-input-group size="sm">
                    <b-input-group-prepend class="form-control exercise-title" is-text size="sm">
                        <span>{{exercise.title}}</span>
                    </b-input-group-prepend>
                    <b-dropdown
                            class="actions-dropdown"
                            dropleft
                            size="sm"
                            slot="append"
                    >
                        <b-dropdown-form class="p-3" size="sm" v-if="isFindMaxWeight === false">

                            <p-radio
                                    :value="getWeightDisplayType(0)"
                                    class="p-default  p-round"
                                    color="primary-o" name="color"
                                    v-model="exercise.weightDisplayType">Weight in Kgs
                            </p-radio>
                            <p-radio :value="getWeightDisplayType(1)" class="p-default  p-round" color="info-o"
                                     name="color"
                                     v-model="exercise.weightDisplayType">Weight % of previous PM
                            </p-radio>
                            <p-radio :value="getWeightDisplayType(2)" class="p-default  p-round" color="success-o"
                                     name="color"
                                     v-model="exercise.weightDisplayType">Weight % of
                                max PM
                            </p-radio>
                        </b-dropdown-form>
                        <b-dropdown-divider v-if="isFindMaxWeight === false"/>
                        <b-dropdown-item
                                :disabled="canMoveExerciseUp"
                                v-on:click="$emit('moveExerciseUp',index)"
                        >
                            <font-awesome-icon :icon="['fas','long-arrow-alt-up']"/>
                            Move up
                        </b-dropdown-item>
                        <b-dropdown-item
                                :disabled="canMoveExerciseDown"
                                v-on:click="$emit('moveExerciseDown',index)"
                        >
                            <font-awesome-icon :icon="['fas','long-arrow-alt-down']"/>
                            Move down
                        </b-dropdown-item>
                        <b-dropdown-item v-on:click="$emit('exerciseChange',exercise)">
                            <font-awesome-icon
                                    :icon="['fas','plus']"
                                    class="text-success"
                                    size="sm"
                            />
                            Repeat
                        </b-dropdown-item>
                        <b-dropdown-divider/>
                        <b-dropdown-item v-on:click="$emit('deleteFromList',index)">
                            <font-awesome-icon
                                    :icon="['fas','trash']"
                                    class="text-danger"
                                    size="sm"
                            />
                            Remove
                        </b-dropdown-item>
                    </b-dropdown>
                </b-input-group>
            </div>
            <ExerciseMeasureEditComponent :measure="exercise.distanceMeasure" v-if="exercise.distanceMeasure "/>
            <ExerciseMeasureEditComponent :measure="exercise.countMeasure" v-if="exercise.countMeasure "/>
            <ExerciseMeasureEditComponent :measure="exercise.weightMeasure" v-if="canSeeWeightsMeasures"/>
            <ExerciseMeasureEditComponent :measure="exercise.altWeightMeasure" v-if="canSeeWeightsMeasures"/>
            <template v-if="shouldShowPercentWeightsMeasures()">
                <div class="form-group my-1 col-lg-3">
                    <b-input-group class="mx-1 pr-1" size="sm">
                        <b-form-input
                                class="measure-value-input"
                                inputmode="numeric"
                                pattern="[0-9]*"
                                placeholder="% value"
                                type="tel"
                                v-model="exercise.weightPercentValue"/>
                        <b-input-group-append>
                            <b-input-group-text :class="weightTypeBadgeClass" class=" p-0" tag="span">
                                <span :class="weightTypeBadgeClass" class="badge ">{{weightTypeDescription}}</span>
                            </b-input-group-text>
                        </b-input-group-append>
                    </b-input-group>
                </div>
            </template>
            <ExerciseMeasureEditComponent :measure="exercise.heightMeasure" v-if="exercise.heightMeasure"/>
            <ExerciseMeasureEditComponent :measure="exercise.caloriesMeasure" v-if="exercise.caloriesMeasure"/>


        </div>
    </div>
</template>

<script lang="ts">
    /* Font awesome icons */

    /* public components */
    import {Component, Prop, Vue} from "vue-property-decorator";
    import {
        BButton,
        BDropdown,
        BDropdownDivider,
        BDropdownForm,
        BDropdownItem,
        BDropdownItemButton,
        BFormGroup,
        BFormInput,
        InputGroupPlugin,
    } from "bootstrap-vue";
    /* models and styles */
    import {ExerciseViewModel} from "../../../models/viewModels/ExerciseViewModel";
    import {Getter} from "vuex-class";
    import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";
    import {WeightDisplayType} from "../../../models/viewModels/WeightDisplayType";
    import ExerciseMeasureEditComponent from "./exercise-measure-edit-component.vue";
    /* app components */
    const namespace: string = "workoutEdit";
    Vue.use(InputGroupPlugin);
    @Component({
        components: {
            ExerciseMeasureEditComponent,
            ExercisesListItemComponent,
            BDropdown,
            BDropdownItem,
            BDropdownItemButton,
            BDropdownDivider,
            BDropdownForm,
            BFormInput,
            BButton,
            BFormGroup,
            FontAwesomeIcon
        }

    })
    export default class ExercisesListItemComponent extends Vue {
        @Prop()
        exercise: ExerciseViewModel;
        @Prop()
        index: number;
        @Prop()
        canMoveExerciseDown: boolean;
        @Prop()
        canMoveExerciseUp: boolean;
        @Getter('isFindMaxWeightGetter', {namespace})
        isFindMaxWeight: boolean;

        get weightTypeDescription() {
            if (this.exercise.weightDisplayType == WeightDisplayType.PercentMaxPM) {
                return "% of max weight PM";
            }
            if (this.exercise.weightDisplayType == WeightDisplayType.PercentPreviousPM) {
                return "% of previous weight PM";
            }
            return "";
        }

        get weightTypeBadgeClass() {
            if (this.exercise.weightDisplayType == WeightDisplayType.PercentMaxPM) {
                return "badge-success";
            }
            if (this.exercise.weightDisplayType == WeightDisplayType.PercentPreviousPM) {
                return "badge-info";
            }
            return "";
        }

        get canSeeWeightsMeasures() {
            return this.exercise.weightMeasure && this.isFindMaxWeight === false && this.exercise.weightDisplayType == WeightDisplayType.Default;
        }

        getWeightDisplayType(weightDisplayType: WeightDisplayType) {
            return weightDisplayType;
        }

        private shouldShowPercentWeightsMeasures(): boolean {
            return this.exercise.weightDisplayType != WeightDisplayType.Default && this.isFindMaxWeight === false;
        }

    }
</script>

<style/>