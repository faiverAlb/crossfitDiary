<template>
 <span v-if="model.exerciseMeasureType.measureValue">
   {{model.exerciseMeasureType.measureValue}} {{model.displayMeasure}}
 </span>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import {ExerciseMeasureViewModel} from "../models/viewModels/ExerciseMeasureViewModel";
@Component({
  components: {}
})
export default class ExerciseMeasureComponent extends Vue {

  @Prop() 
  model: ExerciseMeasureViewModel;
   
}
</script>

<style>
</style>