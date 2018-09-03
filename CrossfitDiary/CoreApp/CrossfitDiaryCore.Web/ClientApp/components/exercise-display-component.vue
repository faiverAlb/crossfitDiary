<template>
  <div>
    <div class="exercise">
      <span>{{model.title}}</span>
      <span class="do-unbroken-info" v-if="model.isDoUnbroken">(unbroken)</span>
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
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { ExerciseViewModel } from "../models/viewModels/ExerciseViewModel";

@Component({
  components: {}
})
export default class ExerciseDisplayComponent extends Vue {

   @Prop() 
   model: ExerciseViewModel;
}
</script>

<style>
</style>