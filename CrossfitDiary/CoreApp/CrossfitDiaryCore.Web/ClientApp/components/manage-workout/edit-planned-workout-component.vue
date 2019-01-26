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
          ></date-picker>
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
          v-on:click="showPlanWorkoutModal(0)"
        >Plan as Sc</b-button>
        <b-button
          class="w-100"
          variant="warning"
          v-on:click="showPlanWorkoutModal(1)"
        >Plan as Rx</b-button>
        <b-button
          class="w-100"
          variant="danger"
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
import bModal from "bootstrap-vue/es/components/modal/modal";
import datePicker from "vue-bootstrap-datetimepicker";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import bButtonGroup from "bootstrap-vue/es/components/button-group/button-group";
import bButton from "bootstrap-vue/es/components/button/button";

/* app components */
/* models and styles */
import {
  WorkoutViewModel,
  PlanningWorkoutLevel
} from "../../models/viewModels/WorkoutViewModel";

@Component({
  components: { bModal, datePicker, FontAwesomeIcon, bButtonGroup, bButton }
})
export default class EditPlannedWorkoutComponent extends Vue {
  @Prop() planningWorkout: WorkoutViewModel;
  selectedWorkoutDispayLevel: string = "";
  $refs: {
    planWorkoutModal: HTMLFormElement;
  };
  showPlanWorkoutModal(type: PlanningWorkoutLevel): void {
    this.$validator.validate();
    this.$validator.validate().then(isValid => {
      if (isValid) {
        this.planningWorkout.planningWorkoutLevel = type;
        this.selectedWorkoutDispayLevel = PlanningWorkoutLevel[type];
        this.$refs.planWorkoutModal.show();
      }
    });
  }

  hidePlanModal(): void {
    this.$refs.planWorkoutModal.hide();
  }

  planWorkout() {
    this.hidePlanModal();
    // this.$validator.validate();

    // this.$validator.validate().then(isValid => {
    //   if (isValid) {
    //     let crossfitterService: CrossfitterService = new CrossfitterService();
    //     this.hidePlanModal();
    //     this.spinner.activate();
    //     crossfitterService
    //       .createAndPlanWorkout(this.model)
    //       .then(data => {
    //         window.location.href = "\\";
    //       })
    //       .catch(data => {
    //         this.spinner.disable();
    //         this.errorAlertModel.setError(data.response.statusText);
    //       });
    //   }
    // });
  }
}
</script>

<style>
</style>