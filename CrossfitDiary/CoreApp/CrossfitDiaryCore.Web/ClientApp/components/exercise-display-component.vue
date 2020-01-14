<template>
    <div>
        <div class="exercise">
            <span>{{model.title}}</span><span
                class="do-unbroken-info"
                v-if="model.isDoUnbroken"
        >(unbroken)</span><span
                :key="measure.measureType"
                v-for="(measure,index) in model.exerciseMeasures"
        ><span v-if="measure.measureValue">
          <span v-if="measure.measureType == 8">/{{measure.measureValue}} {{measure.shortMeasureDescription}}</span>
          <span v-else>
            <span v-if="index > 0 && index < model.exerciseMeasures.length"> x </span><span v-else>:</span> {{measure.measureValue}} <span
                  v-if="measure.measureType != 2">{{measure.shortMeasureDescription}}</span></span>
        </span>
      </span>
            <span
                    class="new-record"
                    title="Person new weight maximum!"
                    v-if="model.isNewWeightMaximum"
            >
                <span class="badge badge-success">new!</span>
                <span class="badge badge-warning">{{model.addedToMaxWeightString}}</span>
              </span>
        </div>
    </div>
</template>

<script lang="ts">
    import {Component, Prop, Vue} from "vue-property-decorator";
    import {ExerciseViewModel} from "../models/viewModels/ExerciseViewModel";

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