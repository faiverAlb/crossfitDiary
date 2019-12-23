<template>
    <div
            :class="{'person-workout': model.canBeRemovedByCurrentUser}"
            class="done-item offset-lg-3 col col-lg-5 my-2 px-3 py-2 rounded"
            v-if="model"
    >

        <div class="item-header d-flex flex-row justify-content-between  ">
            <div class="username">
        <span class="text-info">
          {{model.workouterName}}
        </span>
            </div>
            <div class="">
                {{model.displayDate}}
                <a
                        @click="deleteWorkout"
                        class="remove-workout pl-1 text-secondary pointer"
                        title="Remove your workout"
                        v-if="model.canBeRemovedByCurrentUser"
                >
                    <i
                            aria-hidden="true"
                            class="fa fa-trash-alt"
                    />
                </a>
            </div>
        </div>
        <div class="item-body pt-1">
            <WorkoutDisplayComponent :workoutViewModel="model.workoutViewModel"/>
            <div class="text-right">
        <span class="workout-result">
          <span v-if="model.workoutViewModel.IsHaveCapTime()">
            <span v-if="model.repsToFinishOnCapTime">
              <font-awesome-icon :icon="['far','grin-beam']"/> Cap + {{model.repsToFinishOnCapTime}}
            </span>
            <span v-else>
              <font-awesome-icon :icon="['far','clock']"/> Total time: {{model.timePassed}}
            </span>
          </span>
          <span v-if="model.workoutViewModel.IsAMRAP() && model.roundsFinished">
            Rounds: {{model.roundsFinished}}
            <span v-if="model.partialRepsFinished"> + {{model.partialRepsFinished}} partials</span>
          </span>
          <span v-if="model.workoutViewModel.IsEmoms()">
            <font-awesome-icon :icon="['far','clock']"/>: {{model.workoutViewModel.timeToWork}}
          </span>
          <div
                  class="result-comment"
                  v-if="model.comment"
          >{{model.comment}}</div>
        </span>

            </div>

        </div>
        <div class="item-footer text-right pt-1">
            <div class="action-buttons">
                <a
                        @click="showLeaderBoardModal(model.crossfitterWorkoutId)"
                        class="edit-workout-action pointer text-secondary float-left"
                        v-if="model.workoutViewModel.canSeeLeaderboard"
                >
                    <font-awesome-icon
                            :icon="['fas','medal']"
                            size="lg"
                    />
                    Leaderboard
                </a>
                <a
                        class="edit-workout-action pointer text-info"
                        v-bind:href="'Workout?crossfitterWorkoutId='+this.model.crossfitterWorkoutId"
                        v-if="model.canBeRemovedByCurrentUser"
                >
                    Edit
                    <font-awesome-icon :icon="['fas','edit']"/>
                </a>
                <a
                        class="repeat-workout-action pointer text-success pl-1"
                        v-bind:href="'Workout?workoutId='+this.model.workoutViewModel.id"
                >
                    <font-awesome-icon :icon="['fas','plus']"/>
                    <span class="do-it-text">Do it</span>
                </a>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    /* Font awesome icons */
    import {faPlus} from "@fortawesome/free-solid-svg-icons/faPlus";
    import {faEdit} from "@fortawesome/free-solid-svg-icons/faEdit";
    import {faGrinBeam} from "@fortawesome/free-regular-svg-icons/faGrinBeam";
    import {faClock} from "@fortawesome/free-regular-svg-icons/faClock";
    import {faTrashAlt} from "@fortawesome/free-solid-svg-icons/faTrashAlt";
    import {faMedal} from "@fortawesome/free-solid-svg-icons/faMedal";
    import {library} from "@fortawesome/fontawesome-svg-core";

    /* public components */
    import {Component, Prop, Vue} from "vue-property-decorator";
    import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";

    /* app components */
    import {ToLogWorkoutViewModel} from "../models/viewModels/ToLogWorkoutViewModel";
    import WorkoutDisplayComponent from "./workout-display-component.vue";
    library.add(faGrinBeam, faClock, faPlus, faTrashAlt, faEdit, faMedal);

    /* models and styles */
    import "./../style/workout-done-item.scss";

    @Component({
        components: {
            FontAwesomeIcon,
            WorkoutDisplayComponent
        }
    })
    export default class PersonsActivitesItemComponent extends Vue {
        @Prop() model: ToLogWorkoutViewModel;

        deleteWorkout(): void {
            this.$emit("deleteWorkout", this.model.crossfitterWorkoutId);
        }

        showLeaderBoardModal(crossfitterWorkoutId: number) {
            this.$emit("showLeaderboard", crossfitterWorkoutId);
        }
    }
</script>

<style>
</style>