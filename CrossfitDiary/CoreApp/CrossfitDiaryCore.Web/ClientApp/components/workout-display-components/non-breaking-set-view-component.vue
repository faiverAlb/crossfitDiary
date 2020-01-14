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
                     {{exercise.getMeasureValue(2)}} <span :title="exercise.title">{{exercise.abbreviation}}</span>  <span v-if="workoutViewModel.findMaxWeight == false">({{exercise.getMeasureValue(3)}}kg</span>
                    <span
                            class="new-record"
                            title="Person new weight maximum!"
                            v-if="exercise.isNewWeightMaximum"
                    >
                        
                        (<b-badge variant="warning" class="px-0">{{exercise.addedToMaxWeightString}}</b-badge>
                        <b-badge variant="success" >new!</b-badge>)

                  </span>
                    <template v-if="exercise.getMeasureValue(8)">/{{exercise.getMeasureValue(8)}}kg</template>
                    <span v-if="workoutViewModel.findMaxWeight == false">)</span>
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
    import {BBadge} from 'bootstrap-vue';
    

    @Component({
        components: {ExerciseDisplayComponent, BBadge}
    })
    export default class NonBreakingSetViewComponent extends Vue {
        @Prop()
        workoutViewModel: WorkoutViewModel;
        
    }
</script>

<style>
</style>