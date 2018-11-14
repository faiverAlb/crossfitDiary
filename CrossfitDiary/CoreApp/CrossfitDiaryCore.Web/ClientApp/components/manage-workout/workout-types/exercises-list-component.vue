<template>
  <div class="py-2">
    <div class="form-group">
      <b-form-select class="form-control" :options="exercisesOptions" v-on:change="exerciseChange" text-field="title" value-field="id" :plain="true" v-model="selectedExercise" />
    </div>
    <div class="routines-container pt-2">
      <div class="simple-routine-item text-center no-selected-container" v-if="exercisesToDo.length == 0">
        No exercises selected
      </div>
      <div v-for="(exercise,index) in exercisesToDo" :key="`${exercise.id}-${index}`">
        <div class="simple-routine-item">
          <div class="form-row">
            <div class="form-group my-1 col-lg-auto">
              <b-input-group>
                <b-input-group-prepend class="exercise-title" is-text>
                  <span>{{exercise.title}}</span>
                  <span v-if="exercise._isDoUnbroken" class="do-unbroken-info badge badge-warning">do unbroken</span>
                </b-input-group-prepend>
                <b-dropdown slot="append">
                  <b-dropdown-item href="#"><i class="fas fa-long-arrow-alt-up text-info"></i> Move up </b-dropdown-item>
                  <b-dropdown-item href="#"><i class="fas fa-long-arrow-alt-down text-info"></i> Move down </b-dropdown-item>
                  <b-dropdown-item href="#"><i class="fas fa-plus fa-sm text-success" aria-hidden="true"></i> Repeat </b-dropdown-item>
                  <b-dropdown-divider></b-dropdown-divider>
                  <b-dropdown-item href="#"><i class="fas fa-trash fa-sm text-danger" aria-hidden="true"></i> Remove </b-dropdown-item>
                </b-dropdown>
              </b-input-group>
            </div>
            <!-- ko foreach:_exerciseMeasures-->
            <!-- ko if: _exerciseMeasureType.measureType() == @((int)MeasureType.Weight)-->
            <div class="form-group my-1 col-lg-3" data-bind="validationElement: _exerciseMeasureType.measureValue">
              <label class="sr-only" data-bind="attr:{for:'measure_input_id_' + $index()}"></label>
              <div class="input-group mx-1 pr-1">
                <input type="number" min="0" class="form-control measure-value-input" data-bind="value: _exerciseMeasureType.measureValue, valueUpdate: ['input', 'afterkeydown'], attr: { placeholder: _exerciseMeasureType.measureDesciption, id:'measure_input_id_' + $index()}" aria-describedby="prPercentHelpBlock">
                <span class="input-group-append">
                  <span class="input-group-text" data-bind="text:_exerciseMeasureType.shortMeasureDescription()"></span>
                </span>
                <!-- ko if: _exerciseMeasureType._canSeePersonalRecord -->
                <span class="w-100"></span>
                <small id="prPercentHelpBlock" class="form-text text-muted" data-bind="text: _exerciseMeasureType.personalRecordPercent">

                </small>
                <!-- /ko -->
              </div>
            </div>
            <!-- /ko -->
            <!-- ko ifnot: _exerciseMeasureType.measureType() == @((int)MeasureType.Weight)-->
            <div class="form-group my-1 col-lg-3" data-bind="validationElement: _exerciseMeasureType.measureValue">
              <label class="sr-only" data-bind="attr:{for:'measure_input_id_' + $index()}"></label>
              <div class="input-group mx-1 pr-1">
                <input type="number" min="0" class="form-control measure-value-input" data-bind="value: _exerciseMeasureType.measureValue, valueUpdate: 'afterkeydown', attr: { placeholder: _exerciseMeasureType.measureDesciption, id:'measure_input_id_' + $index()}">
                <span class="input-group-append">
                  <span class="input-group-text" data-bind="text:_exerciseMeasureType.shortMeasureDescription()"></span>
                </span>
              </div>
            </div>
            <!-- /ko -->
            <!-- /ko -->
          </div>
        </div>

      </div>
    </div>

  </div>
</template>

<script lang="ts">
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
import bInputGroup from "bootstrap-vue/es/components/input-group/input-group";
import bInputGroupAddon from "bootstrap-vue/es/components/input-group/input-group-addon";
import bInputGroupPrepend from "bootstrap-vue/es/components/input-group/input-group-prepend";

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
    bInputGroup,
    bInputGroupAddon,
    bInputGroupPrepend
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
      this.exercisesToDo.push(workoutToAdd);
    }
  }

  selectedExercise: number = -1;
  get exercisesOptions() {
    var exercisesOptions = this.workoutEdit.exercises;
    exercisesOptions.push(new DefaultExerciseViewModel());
    return exercisesOptions;
  }
}
</script>

<style>
</style>