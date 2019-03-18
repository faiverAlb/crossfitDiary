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
    <div class="routines-container pt-2">
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
                  v-model="measure.measureValue"
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
                <span class="w-100"></span>
                <small
                  id="prPercentHelpBlock"
                  class="form-text text-muted"
                >
                  personalRecordPercent
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

import { Vue, Component, Prop } from "vue-property-decorator";
import {
  ExerciseViewModel,
  DefaultExerciseViewModel
} from "../../../models/viewModels/ExerciseViewModel";
import bFormSelect from "bootstrap-vue/es/components/form-select/form-select";
import bDropdown from "bootstrap-vue/es/components/dropdown/dropdown";
import bDropdownItem from "bootstrap-vue/es/components/dropdown/dropdown-item";
import bDropdownItemButton from "bootstrap-vue/es/components/dropdown/dropdown-item-button";
import bDropdownDivider from "bootstrap-vue/es/components/dropdown/dropdown-divider";
import bFormInput from "bootstrap-vue/es/components/form-input/form-input";
import { InputGroup } from "bootstrap-vue/es/components";
Vue.use(InputGroup);

import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

import { IWorkoutEditState } from "./../../../workout-edit-store/types";
import { State, Action, Getter } from "vuex-class";
const namespace: string = "workoutEdit";

@Component({
  components: {
    bFormSelect,
    bDropdown,
    bDropdownItem,
    bDropdownItemButton,
    bDropdownDivider,
    bFormInput,
    FontAwesomeIcon
  }
})
export default class ExercisesListComponent extends Vue {
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
}
</script>

<style>
</style>