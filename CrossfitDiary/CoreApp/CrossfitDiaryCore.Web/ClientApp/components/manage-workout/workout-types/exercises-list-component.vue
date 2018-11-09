<template>
  <div class="py-2">
    <select class="form-control exercise-selector" data-live-search="true" data-bind="options: _exercises, optionsText:'title',value: _selectedExercise, optionsCaption: 'Add an exercise...',selectPicker: {}"></select>
    <b-form-select class="exercise-selector" />
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
  // private loadExercises = () => {
  //   return this._service
  //     .getExercises()
  //     .then((exercises: ExerciseViewModel[]) => {
  //       this._exercises = exercises;
  //     });
  // };

  // mounted() {

  // }
}
</script>

<style>
</style>