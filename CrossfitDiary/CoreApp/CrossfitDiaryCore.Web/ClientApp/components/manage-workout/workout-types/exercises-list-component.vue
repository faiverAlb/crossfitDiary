<template>
  <div class="py-2">
    <div class="form-group">
      <b-form-select
        class="form-control"
        :options="exercisesOptions"
        v-on:change="exerciseChange"
        text-field="title"
        value-field="id"
        :plain="true"
        v-model="selectedExercise"
      />
    </div>
    <div
      v-if="exercisesToDo.length > 0"
      class="text-right actions-on-exercises"
    >
      <b-dropdown
        id="dropdown-form"
        text="Actions"
        ref="dropdown"
        class="m-0"
        variant="link"
        dropleft
      >
        <b-dropdown-form class="p-3">
          <b-form-group
            label="Schema (ex. 21 15 9)"
            label-for="dropdown-form-email"
          >
            <b-form-input
              id="dropdown-form-schema"
              size="sm"
              v-model="schemaToGenerate"
              type="tel"
              placeholder="21 15 9 3"
            ></b-form-input>
          </b-form-group>
          <b-button
            variant="warning"
            size="sm"
            class="col"
            v-on:click="generateSchema"
          >Generate</b-button>
        </b-dropdown-form>
        <b-dropdown-divider></b-dropdown-divider>
        <b-dropdown-item-button v-on:click="clearAllExercises">
          <font-awesome-icon
            :icon="['fas','trash']"
            class="text-danger"
            size="sm"
          ></font-awesome-icon> Clear all exercises
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
        v-for="(exercise,index) in exercisesToDo"
        :key="`${exercise.id}-${index}`"
      >
        <div class="simple-routine-item">
          <div class="form-row">
            <div class="form-group my-1 col-lg-auto">
              <b-input-group>
                <b-input-group-prepend
                  class="form-control exercise-title"
                  is-text
                >
                  <span>{{exercise.title}}</span>
                  <span
                    v-if="exercise._isDoUnbroken"
                    class="do-unbroken-info badge badge-warning"
                  >do unbroken</span>
                </b-input-group-prepend>
                <b-dropdown
                  slot="append"
                  class="actions-dropdown"
                  dropleft
                >
                  <b-dropdown-item
                    v-on:click="moveExerciseUp(index)"
                    :disabled="canMoveExerciseUp(index)"
                  >
                    <font-awesome-icon :icon="['fas','long-arrow-alt-up']"></font-awesome-icon> Move up
                  </b-dropdown-item>
                  <b-dropdown-item
                    v-on:click="moveExerciseDown(index)"
                    :disabled="canMoveExerciseDown(index)"
                  >
                    <font-awesome-icon :icon="['fas','long-arrow-alt-down']"></font-awesome-icon> Move down
                  </b-dropdown-item>
                  <b-dropdown-item v-on:click="exerciseChange(exercise.id)">
                    <font-awesome-icon
                      :icon="['fas','plus']"
                      class="text-success"
                      size="sm"
                    ></font-awesome-icon>
                    Repeat
                  </b-dropdown-item>
                  <b-dropdown-divider></b-dropdown-divider>
                  <b-dropdown-item v-on:click="deleteFromList(index)">
                    <font-awesome-icon
                      :icon="['fas','trash']"
                      class="text-danger"
                      size="sm"
                    ></font-awesome-icon>
                    Remove
                  </b-dropdown-item>
                </b-dropdown>
              </b-input-group>
            </div>
            <div
              class="form-group my-1 col-lg-2"
              v-for="(measure,index) in exercise.exerciseMeasures"
              :key="measure.measureType + index"
            >
              <label
                class="sr-only"
                v-bind:for="`measure_input_id_` + index"
              ></label>
              <b-input-group class="mx-1 pr-1">
                <b-form-input
                  v-if="measure.measureType == 2"
                  v-model="measure.measureValue"
                  pattern="[0-9]*"
                  type="tel"
                  inputmode="numeric"
                  class="measure-value-input"
                  placeholder="Count"
                  aria-describedby="prPercentHelpBlock"
                ></b-form-input>
                <b-form-input
                  v-else
                  v-model="measure.measureValue"
                  type="number"
                  class="measure-value-input"
                  :placeholder="measure.description"
                  aria-describedby="prPercentHelpBlock"
                ></b-form-input>
                <b-input-group-append>
                  <b-input-group-text tag="span">
                    {{measure.shortMeasureDescription}}
                  </b-input-group-text>
                </b-input-group-append>
                <!-- TODO: Problem with border radious because of it -->
                <span
                  class="w-100"
                  v-if="canSeeMaximumHelper(exercise,measure)"
                ></span>
                <small
                  id="prPercentHelpBlock"
                  class="form-text text-muted"
                  v-if="canSeeMaximumHelper(exercise,measure)"
                >
                  {{calculatePersentForMainWeight(exercise,measure.measureValue)}}% of PM
                </small>

                <span
                  class="w-100"
                  v-if="canSeeAltMaximumHelper(exercise,measure)"
                ></span>
                <small
                  id="prPercentHelpBlock"
                  class="form-text text-muted"
                  v-if="canSeeAltMaximumHelper(exercise,measure)"
                >
                  {{calculatePersentForMainWeight(exercise,measure.measureValue)}}% of PM
                </small>
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
import { faLongArrowAltUp } from "@fortawesome/free-solid-svg-icons/faLongArrowAltUp";
import { faLongArrowAltDown } from "@fortawesome/free-solid-svg-icons/faLongArrowAltDown";
import { faPlus } from "@fortawesome/free-solid-svg-icons/faPlus";
import { faTrash } from "@fortawesome/free-solid-svg-icons/faTrash";
import { library } from "@fortawesome/fontawesome-svg-core";

library.add(faLongArrowAltUp, faLongArrowAltDown, faPlus, faTrash);
/**/

/* public components */
import { Vue, Component, Prop } from "vue-property-decorator";
import {BFormSelect} from "bootstrap-vue";
import {BDropdown} from "bootstrap-vue";
import {BDropdownItem} from "bootstrap-vue";
import {BDropdownItemButton} from "bootstrap-vue";
import {BDropdownDivider} from "bootstrap-vue";
import {BDropdownForm} from "bootstrap-vue";
import {BFormInput} from "bootstrap-vue";
import { InputGroupPlugin } from "bootstrap-vue";
Vue.use(InputGroupPlugin);
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { State, Action, Getter } from "vuex-class";
import {BButton} from "bootstrap-vue";
import {BFormGroup} from "bootstrap-vue";

/* app components */
/* models and styles */
import {
  ExerciseViewModel,
  DefaultExerciseViewModel
} from "../../../models/viewModels/ExerciseViewModel";
import { ExerciseMeasureViewModel } from "../../../models/viewModels/ExerciseMeasureViewModel";

import { IWorkoutEditState } from "./../../../workout-edit-store/types";
import { ExerciseMeasureType } from "../../../models/viewModels/ExerciseMeasureType";
const namespace: string = "workoutEdit";

@Component({
  components: {
    BFormSelect,
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
  private schemaToGenerate: string = "";

  $refs: {
    dropdown: HTMLFormElement;
  };

  @Prop()
  exercisesToDo: ExerciseViewModel[];

  @State("workoutEdit")
  workoutEdit: IWorkoutEditState;

  exerciseChange(selectedExerciseId: number) {
    let workoutToAdd = this.workoutEdit.exercises.find(
      x => x.id == selectedExerciseId
    );
    if (workoutToAdd.id != -1) {
      let copy = JSON.parse(JSON.stringify(workoutToAdd));
      this.exercisesToDo.push(copy);
    }
  }

  selectedExercise: number = -1;
  get exercisesOptions() {
    var exercisesOptions = this.workoutEdit.exercises.slice();
    exercisesOptions.push(new DefaultExerciseViewModel());
    return exercisesOptions;
  }

  canMoveExerciseDown(index: number) {
    return this.exercisesToDo.length - 1 == index;
  }

  canMoveExerciseUp(index: number) {
    return index == 0;
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

  isFloat(val) {
    var floatRegex = /^-?\d+(?:[.,]\d*?)?$/;
    if (!floatRegex.test(val)) return false;

    val = parseFloat(val);
    if (isNaN(val)) return false;
    return true;
  }

  private getUserMaximumByExerciseId(exerciseId: number) {
    let maximum = this.workoutEdit.userMaximums.find(
      x => x.exerciseId == exerciseId
    );
    return maximum;
  }
  private canSeeMaximumHelper(
    exercise: ExerciseViewModel,
    measure: ExerciseMeasureViewModel
  ): boolean {
    if (
      measure.measureType != ExerciseMeasureType.Weight ||
      measure.measureValue == null
    )
      return;
    let foundMaximum = this.getUserMaximumByExerciseId(exercise.id);
    if (foundMaximum && foundMaximum.maximumWeight != null) {
      return true;
    }
    return false;
  }

  private canSeeAltMaximumHelper(
    exercise: ExerciseViewModel,
    measure: ExerciseMeasureViewModel
  ): boolean {
    if (
      measure.measureType != ExerciseMeasureType.AlternativeWeight ||
      measure.measureValue == null
    )
      return;
    let foundMaximum = this.getUserMaximumByExerciseId(exercise.id);
    if (foundMaximum && foundMaximum.maximumAlternativeWeight != null) {
      return true;
    }
    return false;
  }

  private calculatePersentForMainWeight(
    exercise: ExerciseViewModel,
    measureValue: string
  ) {
    if (this.isFloat(measureValue) == false) return "";
    let maxValue = this.getUserMaximumByExerciseId(exercise.id);
    if (
      (maxValue.maximumWeight == null || maxValue.maximumWeight == 0) &&
      (maxValue.maximumAlternativeWeight == null ||
        maxValue.maximumAlternativeWeight == 0)
    )
      return "";
    let max = Math.max(
      maxValue.maximumWeight,
      maxValue.maximumAlternativeWeight
    );
    let parsed = parseFloat(measureValue);
    let returnValue = (parsed / max) * 100;

    return Math.round((returnValue + 0.00001) * 100) / 100;
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

<style></style>