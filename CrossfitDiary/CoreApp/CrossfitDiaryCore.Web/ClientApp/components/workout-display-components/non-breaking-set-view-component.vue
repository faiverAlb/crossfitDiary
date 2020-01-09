<template>
    <div>
        <h3>test</h3>
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