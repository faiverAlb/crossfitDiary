<template>
  <div>
    <div>
      <b-modal
        ref="leaderboardModal"
        title="Leaderboard"
      >
        <div class="workouts-results text-center">
          <b-table :items="leaderboardItems"/>
        </div>
        <div slot="modal-footer">
          <b-button
            data-dismiss="modal"
            @click="()=>{this.$refs.leaderboardModal.hide();}"
          >Close</b-button> 
        </div>
      </b-modal>
      <b-modal
        ref="myModalRef"
        title="Sure to remove workout?"
      >
        Are you sure you want to remove it?
        <div slot="modal-footer">
          <button
            type="button"
            data-dismiss="modal"
            class="btn btn-default"
            @click="hideModal"
          >Close</button>
          <button
            type="button"
            data-dismiss="modal"
            class="btn btn-primary btn-danger"
            @click="deleteWorkout"
          >Delete</button>
        </div>
      </b-modal>
    </div>
    <div
      class="activities-list container"
      v-for="item in activities"
      :key="item.id"
    >
        <PersonsActivitesItemComponent
                :model="item"
                @deleteWorkout="deleteWorkoutClick"
                @showLeaderboard="showLeaderBoardModal"
        />
    </div>
  </div>
</template>

<script lang="ts">
/* Font awesome icons */
/* public components */
import { Vue, Component, Prop } from "vue-property-decorator";
import {BModal} from "bootstrap-vue";
import Spinner from "vue-spinner-component/src/Spinner.vue";
import {BFormCheckbox} from "bootstrap-vue";
import {BButton} from "bootstrap-vue";
import {TablePlugin} from "bootstrap-vue";

/* app components */
import PersonsActivitesItemComponent from "./person-activities-item-component.vue";
/* models and styles */
import { ToLogWorkoutViewModel } from "../models/viewModels/ToLogWorkoutViewModel";

import CrossfitterService from "../CrossfitterService";
import { LeaderboardItemViewModel } from "../models/viewModels/LeaderboardItemViewModel";
Vue.use(TablePlugin);

@Component({
  components: {
    PersonsActivitesItemComponent,
    BModal,
    BFormCheckbox,
    BButton,
    Spinner
  }
})
export default class PersonsActivitesComponent extends Vue {
  _apiService: CrossfitterService = new CrossfitterService();
  $refs: {
    myModalRef: HTMLFormElement;
    leaderboardModal: HTMLFormElement;
  };

  leaderboardItems: LeaderboardItemViewModel[] = [];

  @Prop() activities: ToLogWorkoutViewModel[];

  _toDeleteCrossfitWorkoutId: number = -1;
  deleteWorkoutClick(crossfitterWorkoutId: number) {
    this.$refs.myModalRef.show(crossfitterWorkoutId);
    this._toDeleteCrossfitWorkoutId = crossfitterWorkoutId;
  }

  mounted() {
    this._apiService = new CrossfitterService();
  }

  deleteWorkout(): void {
    this.$refs.myModalRef.hide();

    this._apiService
      .removeWorkout(this._toDeleteCrossfitWorkoutId)
      .then(data => {
        let indexOfWorkout = this.activities
          .map(item => item.id)
          .indexOf(this._toDeleteCrossfitWorkoutId);
        this.activities.splice(indexOfWorkout, 1);
      });
  }

  hideModal(): void {
    this.$refs.myModalRef.hide();
  }

  showLeaderBoardModal(crossfitterWorkoutId: number) {
    this._apiService
      .getLeaderboardByWorkout(crossfitterWorkoutId)
      .then(data => {
        this.leaderboardItems = data;
        this.$refs.leaderboardModal.show();
      });
  }
}
</script>

<style>
</style>