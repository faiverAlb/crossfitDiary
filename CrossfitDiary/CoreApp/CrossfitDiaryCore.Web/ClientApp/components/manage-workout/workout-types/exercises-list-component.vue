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
        {{exercise.title}}
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
    debugger;
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