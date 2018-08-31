<template>
    <div>
        <span class="workout-header d-flex justify-content-between">
            <span class="workout-title">
                <span>{{workoutViewModel.workoutTypeDisplay}}</span>:
                <span v-if="workoutViewModel.IsForTime()">
                    <span>{{workoutViewModel.roundsCount}} rounds</span>
                </span>
                <!-- ko if:workoutType == @((int)RoutineComplexType.AMRAP)-->
                <span data-bind="text:timeToWork"></span>
                <!-- /ko -->
            </span>
            <span class="cap-time-info" data-bind="if:timeCap">
                <span>Cap: </span>
                <span data-bind="text:timeCap"></span>
            </span>
        </span>
        <div class="workout-exercises pl-3 pt-1">
            <!-- ko foreach:exercisesToDoList-->
            <div class="exercise">
                <span data-bind="text:title"></span>
                <!-- ko if: isDoUnbroken-->
                <span class="do-unbroken-info">(unbroken)</span>
                <!-- /ko -->
                :
                <!-- ko foreach:exerciseMeasures-->
                <!-- ko if:exerciseMeasureType.measureValue != null && exerciseMeasureType.measureValue != "" -->
                <!-- ko if:exerciseMeasureType.measureType == @((int)MeasureTypeViewModel.Count)-->
                <span data-bind="text:exerciseMeasureType.measureValue"></span>
                <!-- /ko -->
                <!-- ko if:exerciseMeasureType.measureType == @((int)MeasureTypeViewModel.Distance)-->
                <span data-bind="text:exerciseMeasureType.measureValue"></span>
                <span> m</span>
                <!-- /ko -->
                <!-- ko if:exerciseMeasureType.measureType == @((int)MeasureTypeViewModel.Weight)-->
                (
                <span data-bind="text:exerciseMeasureType.measureValue"></span>
                <span> kgs</span>)
                <!-- /ko -->
                <!-- ko if:exerciseMeasureType.measureType == @((int)MeasureTypeViewModel.Calories)-->
                <span data-bind="text:exerciseMeasureType.measureValue"></span>
                <span> calories </span>
                <!-- /ko -->
                <!-- ko if:exerciseMeasureType.measureType == @((int)MeasureTypeViewModel.Height)-->
                (
                <span data-bind="text:exerciseMeasureType.measureValue"></span>
                <span> cm</span>)
                <!-- /ko -->
                <!-- /ko -->
                <!-- /ko -->
                <!-- ko if: isNewWeightMaximum -->
                <span class="new-record" title="Person new weight maximum!">
                    <span class="badge badge-success">new!</span>
                    <span class="badge badge-warning" data-bind="text:addedToMaxWeightString"></span>
                </span>
                <!-- /ko -->
            </div>
            <!-- /ko -->

            <!-- ko if: restBetweenRounds-->
            <div class="rest-between-rounds">
                <span>Rest: </span>
                <span data-bind="text: restBetweenRounds"></span>
            </div>
            <!-- /ko -->
        </div>
        <div class="inner-workouts pl-3 pt-1">
            <!-- ko foreach:children-->
            <div class="inner-workout">
                <div data-bind="template: { name: 'workout-display-template', workoutViewModel: $data }"></div>
            </div>
            <!-- /ko -->
        </div>
    </div>

</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { WorkoutViewModel } from "../models/viewModels/WorkoutViewModel";
@Component({
  components: {}
})
export default class WorkoutDisplayComponent extends Vue {
  @Prop() workoutViewModel: WorkoutViewModel;
}
</script>

<style>
</style>