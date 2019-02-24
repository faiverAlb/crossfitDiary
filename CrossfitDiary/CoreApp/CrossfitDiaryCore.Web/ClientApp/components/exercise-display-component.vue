<template>
  <div>
    <div class="exercise">
      <span>{{model.title}}</span>
      <span class="do-unbroken-info" v-if="model.isDoUnbroken">(unbroken)</span>
      <span v-for="measure in model.exerciseMeasures" :key="measure.measureType">
        <span v-if="measure.measureValue">
           : {{measure.measureValue}} {{measure.shortMeasureDescription}}
        </span>
      </span>
      <span class="new-record" title="Person new weight maximum!" v-if="model.isNewWeightMaximum">
        <span class="badge badge-success">new!</span>
        <span class="badge badge-warning" >{{model.addedToMaxWeightString}}</span>
      </span>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { ExerciseViewModel } from "../models/viewModels/ExerciseViewModel";
@Component({
  components: {
    
  }
})
export default class ExerciseDisplayComponent extends Vue {

    @Prop() 
    model: ExerciseViewModel;
}
</script>

<style>
</style>