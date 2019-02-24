<template>
  <div>
    <div>
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
      <div class="row">
        <PersonsActivitesItemComponent
          :model="item"
          @deleteWorkout="deleteWorkoutClick"
        ></PersonsActivitesItemComponent>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import PersonsActivitesItemComponent from "./person-activities-item-component.vue";
import { ToLogWorkoutViewModel } from "../models/viewModels/ToLogWorkoutViewModel";
import bModal from "bootstrap-vue/es/components/modal/modal";
import Spinner from "vue-spinner-component/src/Spinner.vue";
import CrossfitterService from "../CrossfitterService";

@Component({
  components: {
    PersonsActivitesItemComponent,
    bModal,
    Spinner
  }
})
export default class PersonsActivitesComponent extends Vue {
  _apiService: CrossfitterService = new CrossfitterService();
  $refs: {
    myModalRef: HTMLFormElement;
  };
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
}
</script>

<style>
</style>