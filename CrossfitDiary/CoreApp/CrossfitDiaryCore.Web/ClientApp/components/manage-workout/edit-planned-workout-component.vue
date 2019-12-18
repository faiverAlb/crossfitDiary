<template>
  <div class="row justify-content-end mt-3">
    <b-modal
      ref="planWorkoutModal"
      title="Sure to plan this workout?"
    >
      Are you sure you want to plan this workout as {{selectedWorkoutDispayLevel}}?
      <div slot="modal-footer">
        <button
          type="button"
          data-dismiss="modal"
          class="btn btn-default"
          @click="hidePlanModal"
        >Close</button>
        <button
          type="button"
          data-dismiss="modal"
          class="btn btn-primary btn-info"
          @click="planWorkout"
        >Confirm</button>
      </div>
    </b-modal>
    <div class="col-lg-4 col-sm data-selector-container">
      <div class="form-group">
        <b-input-group>
          <date-picker
                  v-model="planningWorkout.displayPlanDate"
                  :config="{ format: 'DD.MM.YYYY'}"
                  placeholder="Select date"
                  name="toPlanModelDate"
                  :wrap="true"
          />
          <b-input-group-append>
            <button
              class="btn btn-secondary datepickerbutton"
              type="button"
              title="Toggle"
            >
              <font-awesome-icon :icon="['fas','calendar']"></font-awesome-icon>
            </button>
          </b-input-group-append>
        </b-input-group>
      </div>
    </div>
    <div class="col-lg-4 col-sm mb-1">
      <b-button-group class="btn-group d-flex">
        <b-button
          class="w-100"
          variant="success"
          v-bind:class="{focus:isActive(0)}"
          v-on:click="showPlanWorkoutModal(0)"
        >Plan as Sc</b-button>
        <b-button
          class="w-100"
          variant="warning"
          v-bind:class="{focus:isActive(1)}"
          v-on:click="showPlanWorkoutModal(1)"
        >Plan as Rx</b-button>
        <b-button
          class="w-100"
          variant="danger"
          v-bind:class="{focus:isActive(2)}"
          v-on:click="showPlanWorkoutModal(2)"
        >Plan as Rx+</b-button>
      </b-button-group>
    </div>

  </div>
</template>

<script lang="ts">
/* Font awesome icons */
import { faCalendar } from "@fortawesome/free-solid-svg-icons/faCalendar";
import { library } from "@fortawesome/fontawesome-svg-core";
library.add(faCalendar);

/* public components */
import { Vue, Component, Prop } from "vue-property-decorator";
import {BModal} from "bootstrap-vue";
import datePicker from "vue-bootstrap-datetimepicker";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import {BButtonGroup} from "bootstrap-vue";
import {BButton} from "bootstrap-vue";

/* app components */
/* models and styles */
import {
  WorkoutViewModel,
  PlanningWorkoutLevel
} from "../../models/viewModels/WorkoutViewModel";

@Component({
  components: { BModal, datePicker, FontAwesomeIcon, BButtonGroup, BButton }
})
export default class EditPlannedWorkoutComponent extends Vue {
  @Prop() planningWorkout: WorkoutViewModel;
  selectedWorkoutDispayLevel: string = "";
  selectedPlanningLevel?: PlanningWorkoutLevel = null;
  $refs: {
    planWorkoutModal: HTMLFormElement;
  };
  showPlanWorkoutModal(type: PlanningWorkoutLevel): void {
    this.selectedPlanningLevel = type;
    this.selectedWorkoutDispayLevel = PlanningWorkoutLevel[type];
    this.$refs.planWorkoutModal.show();
  }

  hidePlanModal(): void {
    this.$refs.planWorkoutModal.hide();
  }

  planWorkout() {
    this.planningWorkout.planningWorkoutLevel = this.selectedPlanningLevel;
    this.hidePlanModal();
    this.$emit("planWorkoutAction", {});
  }

  isActive(type: PlanningWorkoutLevel) {
    return type == this.planningWorkout.planningWorkoutLevel;
  }
}
</script>

<style>
</style>