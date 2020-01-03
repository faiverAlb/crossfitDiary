<template>
    <div class="py-2">
        <div class="form-group">
            <v-select :options="exercisesOptions" :value="selectedExercise" @input="exerciseChange"
                      label="title" placeholder="Select exercise"/>
        </div>
        <div
                class="text-right actions-on-exercises"
                v-if="exercisesToDo.length > 0"
        >
            <b-dropdown
                    class="m-0"
                    dropleft
                    id="dropdown-form"
                    ref="dropdown"
                    size="sm"
                    text="Actions"
                    variant="link"
            >
                <b-dropdown-form class="p-3" size="sm">
                    <b-form-group
                            label="Schema (ex. 21 15 9)"
                            label-for="dropdown-form-email"
                            size="sm"
                    >
                        <b-form-input
                                id="dropdown-form-schema"
                                placeholder="21 15 9 3"
                                size="sm"
                                type="text"
                                v-model="schemaToGenerate"
                        />
                    </b-form-group>
                    <b-button
                            class="col"
                            size="sm"
                            v-on:click="generateSchema"
                            variant="warning"
                    >Generate
                    </b-button>
                </b-dropdown-form>
                <b-dropdown-divider/>
                <b-dropdown-item-button v-on:click="clearAllExercises">
                    <font-awesome-icon
                            :icon="['fas','trash']"
                            class="text-danger"
                            size="sm"
                    />
                    Clear all exercises
                </b-dropdown-item-button>
            </b-dropdown>
        </div>
        <div class="routines-container pt-0">
            <div
                    class="simple-routine-item text-center no-selected-container"
                    v-if="exercisesToDo.length == 0"
            >
                No exercises selected
            </div>
            <div
                    :key="`${exercise.id}-${index}`"
                    v-for="(exercise,index) in exercisesToDo"
            >
                <div class="simple-routine-item">
                    <div class="form-row">
                        <div class="form-group my-1 col-lg-auto">
                            <b-input-group size="sm">
                                <b-input-group-prepend
                                        class="form-control exercise-title"
                                        is-text
                                        size="sm"
                                >
                                    <span>{{exercise.title}}</span>
                                    <span
                                            class="do-unbroken-info badge badge-warning"
                                            v-if="exercise._isDoUnbroken"
                                    >do unbroken</span>
                                </b-input-group-prepend>
                                <b-dropdown
                                        class="actions-dropdown"
                                        dropleft
                                        size="sm"
                                        slot="append"
                                >
                                    <b-dropdown-item
                                            :disabled="canMoveExerciseUp(index)"
                                            v-on:click="moveExerciseUp(index)"
                                    >
                                        <font-awesome-icon :icon="['fas','long-arrow-alt-up']"/>
                                        Move up
                                    </b-dropdown-item>
                                    <b-dropdown-item
                                            :disabled="canMoveExerciseDown(index)"
                                            v-on:click="moveExerciseDown(index)"
                                    >
                                        <font-awesome-icon :icon="['fas','long-arrow-alt-down']"/>
                                        Move down
                                    </b-dropdown-item>
                                    <b-dropdown-item v-on:click="exerciseChange(exercise.id)">
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
                                    <b-input-group-text tag="span">
                                        {{measure.shortMeasureDescription}}
                                    </b-input-group-text>
                                </b-input-group-append>
                                <!-- TODO: Problem with border radious because of it -->

                            </b-input-group>
                        </div>
                    </div>
                </div>
            </div>

        </div>

    </div>
</template>

<script lang="ts">
    /* Font awesome icons */
    import {faLongArrowAltUp} from "@fortawesome/free-solid-svg-icons/faLongArrowAltUp";
    import {faLongArrowAltDown} from "@fortawesome/free-solid-svg-icons/faLongArrowAltDown";
    import {faPlus} from "@fortawesome/free-solid-svg-icons/faPlus";
    import {faTrash} from "@fortawesome/free-solid-svg-icons/faTrash";
    import {library} from "@fortawesome/fontawesome-svg-core";
    /* public components */
    import {Component, Prop, Vue} from "vue-property-decorator";
    import vSelect from 'vue-select';
    import {
        BButton,
        BDropdown,
        BDropdownDivider,
        BDropdownForm,
        BDropdownItem,
        BDropdownItemButton,
        BFormGroup,
        BFormInput,
        InputGroupPlugin
    } from "bootstrap-vue";
    import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";
    import {State} from "vuex-class";
    /* app components */
    /* models and styles */
    import {ExerciseViewModel} from "../../../models/viewModels/ExerciseViewModel";
    import {IWorkoutEditState} from "../../../workout-edit-store/types";
    import "vue-select/src/scss/vue-select.scss";

    library.add(faLongArrowAltUp, faLongArrowAltDown, faPlus, faTrash);
    /**/

    Vue.use(InputGroupPlugin);
    Vue.component('v-select', vSelect);

    const namespace: string = "workoutEdit";

    @Component({
        components: {
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
    export default class ExercisesListComponent extends Vue {
        $refs: {
            dropdown: HTMLFormElement;
        };
        @Prop()
        exercisesToDo: ExerciseViewModel[];
        @State("workoutEdit")
        workoutEdit: IWorkoutEditState;
        selectedExercise: ExerciseViewModel = null;
        private schemaToGenerate: string = "";

        get exercisesOptions() {
            // exercisesOptions.push(new DefaultExerciseViewModel());
            return this.workoutEdit.exercises.slice();
        }

        exerciseChange(exerciseModel: ExerciseViewModel) {
            let selectedExerciseId = exerciseModel.id;
            let workoutToAdd = this.workoutEdit.exercises.find(
                x => x.id == selectedExerciseId
            );
            if (workoutToAdd.id != -1) {
                let copy = JSON.parse(JSON.stringify(workoutToAdd));
                this.exercisesToDo.push(copy);
            }
        }

        canMoveExerciseDown(index: number) {
            return this.exercisesToDo.length - 1 == index;
        }

        canMoveExerciseUp(index: number) {
            return index == 0;
        }

        isFloat(val) {
            var floatRegex = /^-?\d+(?:[.,]\d*?)?$/;
            if (!floatRegex.test(val)) return false;

            val = parseFloat(val);
            if (isNaN(val)) return false;
            return true;
        }

        private moveExerciseUp(index: number) {
            if (index == 0) {
                return;
            }
            this.exercisesToDo.splice(
                index - 1,
                2,
                this.exercisesToDo[index],
                this.exercisesToDo[index - 1]
            );
        }

        private moveExerciseDown(index: number) {
            if (index < this.exercisesToDo.length - 1) {
                this.exercisesToDo.splice(
                    index,
                    2,
                    this.exercisesToDo[index + 1],
                    this.exercisesToDo[index]
                );
            }
        }

        private deleteFromList(index: number) {
            this.exercisesToDo.splice(index, 1);
        }

        private generateSchema() {
            let toGenerateSchema = this.schemaToGenerate;
            let splittedItems: string[] = toGenerateSchema.split(" ");
            this.tryGenerateSchema(splittedItems);

            this.schemaToGenerate = "";
            this.$refs.dropdown.hide(true);
        }

        private tryGenerateSchema(splittedSchemaItems: string[]) {
            let initialExercisesListLength = this.exercisesToDo.length;
            for (let index = 0; index < splittedSchemaItems.length; index++) {
                const schemaItem = splittedSchemaItems[index];
                for (let j = 0; j < initialExercisesListLength; j++) {
                    const exerciseToUse = this.exercisesToDo[j];

                    let exerciseTemp = new ExerciseViewModel().deserialize(exerciseToUse);
                    if (index === 0) {
                        exerciseTemp = this.exercisesToDo[j];
                    }
                    if (exerciseToUse.exerciseMeasures.length > 0) {
                        exerciseTemp.exerciseMeasures[0].measureValue = schemaItem;
                    }
                    if (index === 0) {
                        continue;
                    }
                    this.exercisesToDo.push(exerciseTemp);
                }
            }
        }

        private clearAllExercises() {
            this.exercisesToDo.splice(0, this.exercisesToDo.length);
        }
    }
</script>

<style/>