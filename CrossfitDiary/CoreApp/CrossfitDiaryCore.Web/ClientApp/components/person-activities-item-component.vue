<template>
    <div class="done-item offset-lg-3 col col-lg-5 my-2 px-3 py-2 rounded" v-if="model">
        <div class="item-header d-flex flex-row justify-content-between  ">
            <div class="username">
                <span class="text-info">
                    {{model.workouterName}}
                </span>
            </div>
            <div class="">
                <span class="">{{model.displayDate}}</span>
            </div>
        </div>
        <div class="item-body pt-1">
            <WorkoutDisplayComponent :workoutViewModel="model.workoutViewModel"></WorkoutDisplayComponent>
            <div class="text-right">
                <span class="workout-result">
                    <span v-if="model.workoutViewModel.IsHaveCapTime()">
                        <span v-if="model.repsToFinishOnCapTime">
                            <i class="far fa-grin-beam"></i> Cap + {{model.repsToFinishOnCapTime}}
                        </span>
                        <span v-else>
                            <i class="far fa-clock"></i> Total time: {{model.timePassed}}
                        </span>
                    </span>
                    <span v-if="model.workoutViewModel.IsAMRAP()">
                        Rounds: {{model.roundsFinished}}
                        <span v-if="model.partialRepsFinished"> + {{model.partialRepsFinished}} partials</span> 
                    </span>
                    <span v-if="model.workoutViewModel.IsEmoms()">
                        <i class="far fa-clock"></i>: {{model.workoutViewModel.timeToWork}}
                    </span>
                </span>
            </div>

        </div>
        <div class="item-footer text-right pt-1">
            <div class="action-buttons">
                <a class="repeat-workout-action pointer text-success pl-1">
                    <i class="fas fa-plus "></i>
                    <span class="do-it-text">Do it</span>
                </a>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { ToLogWorkoutViewModel } from "../models/viewModels/ToLogWorkoutViewModel";
import { faCoffee } from "@fortawesome/free-solid-svg-icons/faCoffee";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import WorkoutDisplayComponent from "./workout-display-component.vue";

import { library } from "@fortawesome/fontawesome-svg-core";
library.add(faCoffee);

@Component({
  components: {
    FontAwesomeIcon,
    WorkoutDisplayComponent
  }
})
export default class PersonsActivitesItemComponent extends Vue {
  @Prop() model: ToLogWorkoutViewModel;
}
</script>

<style>
</style>