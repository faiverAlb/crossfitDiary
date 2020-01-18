import {WeightDisplayType} from "../../../models/viewModels/WeightDisplayType";
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
                        <b-dropdown-form class="p-3" size="sm">

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
                        <b-dropdown-divider/>
                        <b-dropdown-item
                                :disabled="canMoveExerciseUp"
                                v-on:click="moveExerciseUp(index)"
                        >
                            <font-awesome-icon :icon="['fas','long-arrow-alt-up']"/>
                            Move up
                        </b-dropdown-item>
                        <b-dropdown-item
                                :disabled="canMoveExerciseDown"
                                v-on:click="moveExerciseDown(index)"
                        >
                            <font-awesome-icon :icon="['fas','long-arrow-alt-down']"/>
                            Move down
                        </b-dropdown-item>
                        <b-dropdown-item v-on:click="exerciseChange(exercise)">
                            <font-awesome-icon
                                    :icon="['fas','plus']"
                                    class="text-success"
                                    size="sm"
                            />
                            Repeat
                        </b-dropdown-item>
                        <b-dropdown-divider/>
                        <b-dropdown-item v-on:click="deleteFromList(index)">
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
            <template v-if="shouldShowDefaultMeasures()">
                <div
                        :key="`${measure.measureType}-${index}`"
                        class="form-group my-1 col-lg-2"
                        v-for="(measure,index) in exercise.exerciseMeasures"

                >
                    <template
                            v-if="measure.measureType == 2 || (measure.measureType != 2 && isFindMaxWeight == false)">

                        <label
                                class="sr-only"
                                v-bind:for="`measure_input_id_` + index"
                        />
                        <b-input-group class="mx-1 pr-1" size="sm">
                            <b-form-input
                                    aria-describedby="prPercentHelpBlock"
                                    class="measure-value-input"
                                    inputmode="numeric"
                                    pattern="[0-9]*"
                                    placeholder="Count"
                                    type="tel"

                                    v-if="measure.measureType == 2"
                                    v-model="measure.measureValue"
                            />
                            <b-form-input
                                    :placeholder="measure.description"
                                    aria-describedby="prPercentHelpBlock"
                                    class="measure-value-input"
                                    type="number"
                                    v-else
                                    v-model="measure.measureValue"
                            />
                            <b-input-group-append>
                                <b-input-group-text class="bg-secondary p-0" tag="span"
                                                    v-if="measure.measureType == 1">
                                        <span class="badge badge-secondary">
                                                    {{measure.shortMeasureDescription}}
                                        <font-awesome-icon :icon="['fas','walking']"
                                                           size="lg"/>
                                        </span>
                                </b-input-group-text>
                                <b-input-group-text class="bg-info p-0" tag="span"
                                                    v-else-if="measure.measureType == 2">
                                        <span class="badge badge-info">
                                                    {{measure.shortMeasureDescription}}
                                        <span>#</span>
                                        </span>
                                </b-input-group-text>
                                <b-input-group-text class="bg-primary p-0" tag="span"
                                                    v-else-if="measure.measureType == 3">
                                        <span class="badge badge-primary">
                                                    {{measure.shortMeasureDescription}}
                                            <font-awesome-icon :icon="['fas','male']" size="lg"/>
                                        </span>
                                </b-input-group-text>
                                <b-input-group-text class="bg-warning p-0" tag="span"
                                                    v-else-if="measure.measureType == 4">
                                        <span class="badge badge-warning">
                                        
                                            cal's
                                        <font-awesome-icon :icon="['fas','burn']"/>
                                        </span>
                                </b-input-group-text>
                                <b-input-group-text class="bg-pink p-0" tag="span"
                                                    v-else-if="measure.measureType == 8">
                                        <span class="badge badge-pink">
                                                    {{measure.shortMeasureDescription}}
                                        <font-awesome-icon :icon="['fas','female']" size="lg"
                                        />
                                        </span>
                                </b-input-group-text>

                                <b-input-group-text tag="span" v-else>
                                    {{measure.shortMeasureDescription}}
                                </b-input-group-text>
                            </b-input-group-append>
                        </b-input-group>

                    </template>
                </div>
            </template>
            <template v-else>
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
                            <b-input-group-text class=" p-0" :class="weightTypeBadgeClass" tag="span">
                                <span class="badge " :class="weightTypeBadgeClass">{{weightTypeDescription}}</span>
                            </b-input-group-text>
                        </b-input-group-append>
                    </b-input-group>
                </div>
            </template>
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
    /* app components */
    const namespace: string = "workoutEdit";
    Vue.use(InputGroupPlugin);
    @Component({
        components: {
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

        getWeightDisplayType(weightDisplayType: WeightDisplayType) {
            return weightDisplayType;
        }

        exerciseChange(exerciseModel: ExerciseViewModel) {
            this.$emit("exerciseChange", exerciseModel);
        }

        private moveExerciseUp(index: number) {
            this.$emit("moveExerciseUp", index);
        }

        private moveExerciseDown(index: number) {
            this.$emit("moveExerciseDown", index);
        }

        private deleteFromList(index: number) {
            this.$emit("deleteFromList", index);
        }

        private shouldShowDefaultMeasures(): boolean {
            return this.exercise.weightDisplayType == WeightDisplayType.Default;
        }

    }
</script>

<style/>