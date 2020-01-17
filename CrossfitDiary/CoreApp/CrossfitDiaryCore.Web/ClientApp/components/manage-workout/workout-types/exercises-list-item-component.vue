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
                            <p-check class="p-default " color="primary-o"
                                     name="percentOfPrevPM"
                                     v-model="exercise.isUseWeightPersentPreviousPM">Use
                                weight % of previous PM
                            </p-check>
                            <p-check class="p-default " color="primary-o"
                                     name="percentOfMaxPM"
                                     v-model="exercise.isUseWeightPersentMaxPM">Use weight % of
                                max PM
                            </p-check>
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
                        <!-- TODO: Problem with border radious because of it -->

                    </b-input-group>

                </template>
            </div>
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
    } from "bootstrap-vue";
    /* models and styles */
    import {ExerciseViewModel} from "../../../models/viewModels/ExerciseViewModel";
    import {Getter} from "vuex-class";
    import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";
    /* app components */
    const namespace: string = "workoutEdit";

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

        private moveExerciseUp(index: number) {
            this.$emit("moveExerciseUp", index);
        }

        private moveExerciseDown(index: number) {
            this.$emit("moveExerciseDown", index);
        }

        private deleteFromList(index: number) {
            this.$emit("deleteFromList", index);
        }
    }
</script>

<style/>