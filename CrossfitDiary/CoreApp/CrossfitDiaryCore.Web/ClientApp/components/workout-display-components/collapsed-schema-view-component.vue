<template>
    <div>

        <div v-if="workoutViewModel.canShowCountOnce == false">
            <div
                    :key="`${group[0].id}`"
                    v-for="group in workoutViewModel.groupedDictionary"
            >
          <span
                  :key="`grp_c_${group[0].id}_${exercise.id}_${index}`"
                  v-for="(exercise,index) in group"
          >{{exercise.count}}<span v-if="exercise.weight"> x {{exercise.weight}}kg</span><span v-if="exercise.calories">{{exercise.calories}}cal</span><span
                  v-if="index + 1 < group.length">-</span></span>
            </div>
            <div
                    :key="`grp_${group[0].id}`"
                    v-for="group in workoutViewModel.groupedDictionary"
            >
                {{group[0].title}}
            </div>

        </div>
        <div v-else>
            <div>
                {{workoutViewModel.oneTimeSchema.schemaString}}
            </div>
            <div
                    :key="`grp_${group[0].id}`"
                    v-for="group in workoutViewModel.groupedDictionary"
            >
                {{group[0].title}}
                <span v-if="group[0].weight">
            (<span
                        :key="`grp_c_${group[0].id}_${exercise.id}_${index}`"
                        v-for="(exercise,index) in group"
                >

              <span v-if="exercise.weight">{{exercise.weight}}kg<span v-if="index + 1 < group.length"> - </span></span>
            </span>)
          </span>
            </div>
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
    export default class CollapsedSchemaViewComponent extends Vue {
        @Prop()
        workoutViewModel: WorkoutViewModel;
    }
</script>

<style>
</style>