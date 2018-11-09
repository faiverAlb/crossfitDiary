<template>
  <div class="py-2">
    <div class="form-group">
      <label for="exampleFormControlSelect1">Example select</label>
      <select class="form-control" id="exampleFormControlSelect1">
        <option>1</option>
        <option>2</option>
        <option>3</option>
        <option>4</option>
        <option>5</option>
      </select>
      <b-form-select class="form-control" :options="exercisesOptions" :plain="true" />
    </div>
    <!-- <select class="form-control exercise-selector" data-live-search="true" data-bind="options: _exercises, optionsText:'title',value: _selectedExercise, optionsCaption: 'Add an exercise...',selectPicker: {}"></select> -->
    <!-- <b-form-select v-model="selected" :options="options" class="exercise-selector" /> -->
    <div v-for="item in workoutEdit.exercises" :key="item.id">
      <div>{{item.title}}</div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { ExerciseViewModel } from "../../../models/viewModels/ExerciseViewModel";
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

  get exercisesOptions() {
    return [
      { text: "Item 1", value: "first" },
      { text: "Item 2", value: "second" },
      { text: "Item 3", value: "third", disabled: true },
      { text: "Item 3", value: { foo: "bar", baz: true } }
    ];
  }
}
</script>

<style>
</style>