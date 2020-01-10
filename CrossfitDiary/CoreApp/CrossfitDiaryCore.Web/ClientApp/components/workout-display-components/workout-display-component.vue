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
        <div class="workout-exercises pl-3 pt-1"
             v-if="workoutViewModel.asNonBreakingSet">
            <NonBreakingSetViewComponent :workoutViewModel="workoutViewModel"/>
        </div>
        <div class="workout-exercises pl-3 pt-1"
             v-else-if="workoutViewModel.haveCollapsedVersion">
            <CollapsedSchemaViewComponent :workoutViewModel="workoutViewModel"/>
        </div>
        <template v-else>
            <div class="mt-1" v-if="workoutViewModel.roundsCount && workoutViewModel.workoutType == 4">{{workoutViewModel.roundsCount}} round(s) of:</div>
            <div
                    :key="`${workoutViewModel.id}-${exercise.id}-${index}`"
                    class="workout-exercises pl-3 pt-1"
                    
                    v-for="(exercise,index) in workoutViewModel.exercisesToDoList"
            >
                <ExerciseDisplayComponent :model="exercise"/>

            </div>
        </template>
        <div
                class="rest-between-rounds pl-3 pt-1"
                v-if="workoutViewModel.restBetweenRounds"
        >
            <span>Rest: {{workoutViewModel.restBetweenRounds}}</span>
        </div>
        <div class="inner-workouts pl-3 pt-1">
            <div
                    :key="childWorkout.id"
                    class="inner-workout"
                    v-for="childWorkout in workoutViewModel.children"
            >
                <WorkoutDisplayComponent :workoutViewModel="childWorkout"/>
            </div>
        </div>
        <div
                class="workout-comment-section"
                v-if="workoutViewModel.comment"
        >Note: <span class="workout-comment">{{workoutViewModel.comment}}</span></div>
    </div>

</template>

<script lang="ts">
    import {Component, Prop, Vue} from "vue-property-decorator";
    import {WorkoutViewModel} from "../../models/viewModels/WorkoutViewModel";
    import ExerciseDisplayComponent from "./../exercise-display-component.vue";
    import CollapsedSchemaViewComponent from "./collapsed-schema-view-component.vue";
    import NonBreakingSetViewComponent from "./non-breaking-set-view-component.vue";

    @Component({
        components: {ExerciseDisplayComponent, CollapsedSchemaViewComponent, NonBreakingSetViewComponent}
    })
    export default class WorkoutDisplayComponent extends Vue {
        @Prop()
        workoutViewModel: WorkoutViewModel;
    }
</script>

<style>
</style>