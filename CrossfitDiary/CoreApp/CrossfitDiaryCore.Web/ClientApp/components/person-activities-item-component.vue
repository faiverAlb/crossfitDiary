<template>
  <div
    :class="{'person-workout': model.canBeRemovedByCurrentUser}"
    class="done-item offset-lg-3 col col-lg-5 my-2 px-3 py-2 rounded"
    v-if="model"
  >
    <b-modal
      ref="leaderboardModal"
      title="Leaderboard"
    >
      <h2>Leaders</h2>
      <div slot="modal-footer">
        <b-button
          data-dismiss="modal"
          @click="()=>{this.$refs.leaderboardModal.hide();}"
        >Close</b-button>
      </div>
    </b-modal>
    <div class="item-header d-flex flex-row justify-content-between  ">
      <div class="username">
        <span class="text-info">
          {{model.workouterName}}
        </span>
      </div>
      <div class="">
        {{model.displayDate}}
        <a
          v-if="model.canBeRemovedByCurrentUser"
          class="remove-workout pl-1 text-secondary pointer"
          title="Remove your workout"
          @click="deleteWorkout"
        >
          <i
            class="fa fa-trash-alt"
            aria-hidden="true"
          ></i>
        </a>
      </div>
    </div>
    <div class="item-body pt-1">
      <WorkoutDisplayComponent :workoutViewModel="model.workoutViewModel"></WorkoutDisplayComponent>
      <div class="text-right">
        <span class="workout-result">
          <span v-if="model.workoutViewModel.IsHaveCapTime()">
            <span v-if="model.repsToFinishOnCapTime">
              <font-awesome-icon :icon="['far','grin-beam']"></font-awesome-icon> Cap + {{model.repsToFinishOnCapTime}}
            </span>
            <span v-else>
              <font-awesome-icon :icon="['far','clock']"></font-awesome-icon> Total time: {{model.timePassed}}
            </span>
          </span>
          <span v-if="model.workoutViewModel.IsAMRAP()">
            Rounds: {{model.roundsFinished}}
            <span v-if="model.partialRepsFinished"> + {{model.partialRepsFinished}} partials</span>
          </span>
          <span v-if="model.workoutViewModel.IsEmoms()">
            <font-awesome-icon :icon="['far','clock']"></font-awesome-icon>: {{model.workoutViewModel.timeToWork}}
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
          class="edit-workout-action pointer text-secondary float-left"
          @click="showLeaderBoardModal(model.workoutViewModel.id)"
        >
          <font-awesome-icon
            size="lg"
            :icon="['fas','medal']"
          ></font-awesome-icon> Leaderboard (####)
        </a>
        <a
          v-if="model.canBeRemovedByCurrentUser"
          class="edit-workout-action pointer text-info"
          v-bind:href="'Workout?crossfitterWorkoutId='+this.model.crossfitterWorkoutId"
        >
          Edit <font-awesome-icon :icon="['fas','edit']"></font-awesome-icon>
        </a>
        <a
          class="repeat-workout-action pointer text-success pl-1"
          v-bind:href="'Workout?workoutId='+this.model.workoutViewModel.id"
        >
          <font-awesome-icon :icon="['fas','plus']"></font-awesome-icon>
          <span class="do-it-text">Do it</span>
        </a>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
/* Font awesome icons */
import { faPlus } from "@fortawesome/free-solid-svg-icons/faPlus";
import { faEdit } from "@fortawesome/free-solid-svg-icons/faEdit";
import { faGrinBeam } from "@fortawesome/free-regular-svg-icons/faGrinBeam";
import { faClock } from "@fortawesome/free-regular-svg-icons/faClock";
import { faTrashAlt } from "@fortawesome/free-solid-svg-icons/faTrashAlt";
import { faMedal } from "@fortawesome/free-solid-svg-icons/faMedal";
import { library } from "@fortawesome/fontawesome-svg-core";
library.add(faGrinBeam, faClock, faPlus, faTrashAlt, faEdit, faMedal);
/**/

/* public components */
import { Vue, Component, Prop } from "vue-property-decorator";
import bModal from "bootstrap-vue/es/components/modal/modal";
import bButton from "bootstrap-vue/es/components/button/button";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

/* app components */
import { ToLogWorkoutViewModel } from "../models/viewModels/ToLogWorkoutViewModel";
import WorkoutDisplayComponent from "./workout-display-component.vue";

@Component({
  components: {
    FontAwesomeIcon,
    WorkoutDisplayComponent,
    bModal,
    bButton
  }
})
export default class PersonsActivitesItemComponent extends Vue {
  @Prop() model: ToLogWorkoutViewModel;

  $refs: {
    leaderboardModal: HTMLFormElement;
  };

  deleteWorkout(): void {
    this.$emit("deleteWorkout", this.model.crossfitterWorkoutId);
  }
  showLeaderBoardModal(workoutViewModelId: number) {
    this.$refs.leaderboardModal.show();
  }
}
</script>

<style>
</style>