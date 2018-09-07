<template>
    <div :class="{'person-workout': model.canBeRemovedByCurrentUser}" class="done-item offset-lg-3 col col-lg-5 my-2 px-3 py-2 rounded" v-if="model">
        <div>
            <b-modal ref="myModalRef" title="Sure to remove workout?">
                Are you sure you want to remove it?
                <div slot="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-default" @click="hideModal">Close</button>
                    <button type="button" data-dismiss="modal" class="btn btn-primary btn-danger" @click="deleteWorkout">Delete</button>
                </div>
            </b-modal>
        </div>
        <div class="item-header d-flex flex-row justify-content-between  ">
            <div class="username">
                <span class="text-info">
                    {{model.workouterName}}
                </span>
            </div>
            <div class="">
                {{model.displayDate}}
                <a v-if="model.canBeRemovedByCurrentUser" class="remove-workout pl-1 text-secondary pointer" title="Remove your workout" @click="showModal" data-bind="click: function(){$parent.removeWorkoutConfirmation(crossfitterWorkoutId);}">
                    <i class="fa fa-trash-alt" aria-hidden="true"></i>
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
/* Font awesome icons */
import { faPlus } from "@fortawesome/free-solid-svg-icons/faPlus";
import { faGrinBeam } from "@fortawesome/free-regular-svg-icons/faGrinBeam";
import { faClock } from "@fortawesome/free-regular-svg-icons/faClock";
import { faTrashAlt } from "@fortawesome/free-solid-svg-icons/faTrashAlt";

import { library } from "@fortawesome/fontawesome-svg-core";
library.add(faGrinBeam, faClock, faPlus, faTrashAlt);
/**/

import { Vue, Component, Prop } from "vue-property-decorator";
import { ToLogWorkoutViewModel } from "../models/viewModels/ToLogWorkoutViewModel";

import WorkoutDisplayComponent from "./workout-display-component.vue";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

import bModal from "bootstrap-vue/es/components/modal/modal";
import vBModal from "bootstrap-vue/es/directives/modal/modal";
import CrossfitterService from "../CrossfitterService";

@Component({
  components: {
    FontAwesomeIcon,
    WorkoutDisplayComponent,
    bModal
  }
})
export default class PersonsActivitesItemComponent extends Vue {
  $refs: {
    myModalRef: HTMLFormElement;
  };

  _apiService: CrossfitterService = new CrossfitterService();
  @Prop() model: ToLogWorkoutViewModel;

  showModal(): void {
    this.$refs.myModalRef.show();
  }

  deleteWorkout(): void {
    this.$refs.myModalRef.hide();
    debugger;

    this._apiService
      .removeWorkout(this.model.crossfitterWorkoutId)
      .then(data => {
        debugger;
      });
  }

  hideModal(): void {
    this.$refs.myModalRef.hide();
  }
}
</script>

<style>
</style>