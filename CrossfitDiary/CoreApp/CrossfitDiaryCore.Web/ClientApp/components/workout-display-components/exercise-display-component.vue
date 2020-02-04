<template>
    <div>
        <div class="exercise">
            <span>{{model.title}}:</span>
            <span class="do-unbroken-info" v-if="model.isDoUnbroken">(unbroken)</span>
            <span v-if="model.countMeasure">{{model.countMeasure.measureValue}}</span>
            <span v-if="canSeeWeightsMeasures"> x {{model.weightMeasure.measureValue}} kgs</span>
            <span v-if="canSeeWeightsMeasures && model.altWeightMeasure && model.altWeightMeasure.measureValue">/{{model.altWeightMeasure.measureValue}} kgs</span>
            <span v-if="model.weightPercentValue "> 
                <span class="badge badge-info">{{model.weightPercentValue}}{{weightTypeDescription}}</span>
            </span>
            <span v-if="model.caloriesMeasure && model.caloriesMeasure.measureValue"> {{model.caloriesMeasure.measureValue}} cals</span>
            <span v-if="model.distanceMeasure && model.distanceMeasure.measureValue"> {{model.distanceMeasure.measureValue}} m</span>
            <span v-if="model.timeMeasure && model.timeMeasure.measureValue">{{model.timeMeasure.measureValue}} sec</span>
            <span v-if="model.heightMeasure && model.heightMeasure.measureValue">({{model.heightMeasure.measureValue}} cm)</span>
            <span   class="new-record"
                    title="Person new weight maximum!"
                    v-if="model.isNewWeightMaximum">
                <span class="badge badge-success">new!</span>
                <span class="badge badge-info ml-1">{{model.addedToMaxWeightString}}</span>
              </span>
        </div>
    </div>
</template>

<script lang="ts">
    import {Component, Prop, Vue} from "vue-property-decorator";
    import {ExerciseViewModel} from "../../models/viewModels/ExerciseViewModel";
    import {WeightDisplayType} from "../../models/viewModels/WeightDisplayType";

    @Component({
        components: {}
    })
    export default class ExerciseDisplayComponent extends Vue {
        @Prop()
        model: ExerciseViewModel;

        get canSeeWeightsMeasures() {
            return this.model.weightMeasure && this.model.weightMeasure.measureValue;
        }

        get weightTypeDescription() {
            if (this.model.weightDisplayType == WeightDisplayType.PercentMaxPM) {
                return "% max PM";
            }
            if (this.model.weightDisplayType == WeightDisplayType.PercentPreviousPM) {
                return "% prev PM";
            }
            return "";
        }


    }
</script>

<style>
</style>