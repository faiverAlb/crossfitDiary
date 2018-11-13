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
              <div class="input-group">
                <div class="form-control exercise-title">
                  <span>{{exercise.title}}</span>
                  <!-- ko if: _isDoUnbroken-->
                  <span class="do-unbroken-info badge badge-warning">do unbroken</span>
                  <!-- /ko -->

                </div>
                <div class="input-group-append">

                  <button type="button" class="btn btn-outline-secondary dropdown-toggle  dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    <span class="sr-only">Toggle Dropdown</span>
                  </button>
                  <div class="dropdown-menu">
                    <a class="dropdown-item" href="#" data-bind="click: changeUnbrokenState"> <input type="checkbox" data-bind="checked: _isDoUnbroken"> Unbroken</a>
                    <div role="separator" class="dropdown-divider"></div>
                    <a class="dropdown-item" href="#" data-bind="click:function(){$parent.moveSimpleRoutineUp($index());}, css:{disabled: $index() == 0}"> <i class="fas fa-long-arrow-alt-up text-info"></i> Move up </a>
                    <a class="dropdown-item" href="#" data-bind="click:function(){$parent.moveSimpleRoutineDown($index());}, css:{disabled: $parent._exercisesToBeDone().length -1  == $index()}"><i class="fas fa-long-arrow-alt-down text-info"></i> Move down </a>
                    <a class="dropdown-item" href="#" data-bind="click:$parent.addSimpleRoutineFromToDo"><i class="fas fa-plus fa-sm text-success" aria-hidden="true"></i> Repeat </a>
                    <div role="separator" class="dropdown-divider"></div>
                    <a class="dropdown-item " href="#" data-bind="click:function(){ $parent.removeSimpleRoutineFromToDo($index)}"><i class="fas fa-trash fa-sm text-danger" aria-hidden="true"></i> Remove </a>
                  </div>
                </div>
              </div>
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
import { IWorkoutEditState } from "./../../../workout-edit-store/types";
import { State, Action, Getter } from "vuex-class";
const namespace: string = "workoutEdit";

@Component({ components: { bFormSelect } })
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