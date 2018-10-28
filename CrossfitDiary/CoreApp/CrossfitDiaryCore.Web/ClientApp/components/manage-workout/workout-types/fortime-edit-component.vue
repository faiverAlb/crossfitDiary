<template>
  <div class="routine-complex-info">
    <div class="pt-2 general-info-container">
      <div class="row">
        <div class="col-lg-3 time-cap-container pb-2">
          <label for="timeCapInput" class="sr-only">Time cap:</label>
          <div class="input-group">
            <div class="input-group-prepend">
              <div class="input-group-text">
                <font-awesome-icon :icon="['fas','clock']" class="fa-lg time-cap-icon"></font-awesome-icon>
              </div>
            </div>
            <input v-model="model.timeCap" type="text" class="form-control " id="timeCapInput" placeholder="Time Cap" />
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-lg-3 pb-2">
          <label for="roundsInput" class="sr-only">Rounds count:</label>
          <div class="input-group ">
            <div class="input-group-prepend">
              <div class="input-group-text">
                <font-awesome-icon :icon="['fas','hashtag']"></font-awesome-icon>
              </div>
            </div>
            <input v-model="model.roundsCount" type="number" pattern="[0-9]*" inputmode="numeric" min="1" class="form-control " id="roundsInput" placeholder="Rounds count" />
          </div>
        </div>
      </div>
      <ExercisesListComponent :exercisesToDo="model.exercisesToDoList"></ExercisesListComponent>
    </div>
  </div>
</template>

<script lang="ts">
/* Font awesome icons */
import { faClock } from "@fortawesome/free-solid-svg-icons/faClock";
import { faHashtag } from "@fortawesome/free-solid-svg-icons/faHashtag";
import { library } from "@fortawesome/fontawesome-svg-core";

library.add(faClock, faHashtag);
/**/
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { Vue, Component, Prop } from "vue-property-decorator";
import { WorkoutViewModel } from "../../../models/viewModels/WorkoutViewModel";
import ExercisesListComponent from "./exercises-list-component.vue";

@Component({ components: { FontAwesomeIcon, ExercisesListComponent } })
export default class ForTimeEditComponent extends Vue {
  model: WorkoutViewModel = new WorkoutViewModel();
}
</script>

<style>
</style>