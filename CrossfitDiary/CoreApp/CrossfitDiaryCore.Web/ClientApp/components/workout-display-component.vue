<template>
  <div class="workout-display-container">
    <span class="workout-header d-flex justify-content-between">
      <span class="workout-title">
        <span>{{workoutViewModel.workoutTypeDisplay}}</span>:
        <span v-if="workoutViewModel.IsForTime()">
          {{workoutViewModel.roundsCount}} rounds
        </span>
        <span v-if="workoutViewModel.IsAMRAP()">
          {{workoutViewModel.timeToWork}}
        </span>
      </span>
      <span
        class="cap-time-info mr-1"
        v-if="workoutViewModel.timeCap"
      >
        Cap: {{workoutViewModel.timeCap}}
      </span>
    </span>

    <div
      class="workout-exercises pl-3 pt-1"
      v-if="workoutViewModel.haveCollapsedVersion"
    >
      <div
        v-for="group in workoutViewModel.groupedDictionary"
        :key="`grp_${group[0].id}`"
      >
        {{group[0].title}}
      </div>
      <div
        v-for="group in workoutViewModel.groupedDictionary"
        :key="`${group[0].id}`"
      >
        <span
          v-for="(exercise,index) in group"
          :key="`grp_${group[0].id}_${exercise.id}_${index}`"
        >{{exercise.count}}<span v-if="index + 1 < group.length">-</span></span>
      </div>

    </div>
    <div
      v-else
      class="workout-exercises pl-3 pt-1"
      v-for="(exercise,index) in workoutViewModel.exercisesToDoList"
      :key="`${workoutViewModel.id}-${exercise.id}-${index}`"
    >
      <ExerciseDisplayComponent :model="exercise"></ExerciseDisplayComponent>

    </div>
    <div
      class="rest-between-rounds pl-3 pt-1"
      v-if="workoutViewModel.restBetweenRounds"
    >
      <span>Rest: {{workoutViewModel.restBetweenRounds}}</span>
    </div>
    <div class="inner-workouts pl-3 pt-1">
      <div
        class="inner-workout"
        v-for="childWorkout in workoutViewModel.children"
        :key="childWorkout.id"
      >
        <WorkoutDisplayComponent :workoutViewModel="childWorkout"></WorkoutDisplayComponent>
      </div>
    </div>
    <div
      class="workout-comment-section"
      v-if="workoutViewModel.comment"
    >Note: <span class="workout-comment">{{workoutViewModel.comment}}</span></div>
  </div>

</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { WorkoutViewModel } from "../models/viewModels/WorkoutViewModel";
import ExerciseDisplayComponent from "./exercise-display-component.vue";

@Component({
  name: "WorkoutDisplayComponent",
  components: { ExerciseDisplayComponent }
})
export default class WorkoutDisplayComponent extends Vue {
  @Prop()
  workoutViewModel: WorkoutViewModel;
}
</script>

<style>
</style>