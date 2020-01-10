<template>
    <div>
        <div class="mt-1" v-if="workoutViewModel.roundsCount && workoutViewModel.workoutType == 4">
            {{workoutViewModel.roundsCount}} round(s) of:
        </div>
        <div class="workout-exercises pl-3 pt-1">
            <span
                    :key="`${workoutViewModel.id}-${exercise.id}-${index}`"
                    v-for="(exercise,index) in workoutViewModel.exercisesToDoList">            
                
                <template>
                    {{exercise.getMeasureValue(2)}} {{exercise.abbreviation}} ({{exercise.getMeasureValue(3)}}kg/{{exercise.getMeasureValue(8)}}kg)
                </template>
                <template v-if="index != workoutViewModel.exercisesToDoList.length - 1">
                    +
                </template>
    
            </span>
        </div>
    </div>
</template>

<script lang="ts">
    import {Component, Prop, Vue} from "vue-property-decorator";
    import {WorkoutViewModel} from "../../models/viewModels/WorkoutViewModel";
    import ExerciseDisplayComponent from "./../exercise-display-component.vue";

    @Component({
        components: {ExerciseDisplayComponent}
    })
    export default class NonBreakingSetViewComponent extends Vue {
        @Prop()
        workoutViewModel: WorkoutViewModel;
    }
</script>

<style>
</style>