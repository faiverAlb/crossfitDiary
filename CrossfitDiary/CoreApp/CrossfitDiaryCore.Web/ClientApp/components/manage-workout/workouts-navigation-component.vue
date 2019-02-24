<template>
  <div class="d-flex justify-content-between">
    <div v-if="isEditMode">{{workoutTypeDisplay}}</div>
    <div
      class="d-inline-block"
      v-else
    >
      <b-dropdown
        variant="link"
        size="lg"
        no-caret
        class="workouts-dropdown"
      >
        <template slot="button-content">
          <a
            class="btn btn-secondary text-light  d-inline-block p-0 "
            href="#"
            v-bind:class="{'btn-info':($route.path == `/fortime` ||  $route.path == `/fortimen`)}"
          >
            <div class="small-action-link-button">
              <div class="icon-container">
                <font-awesome-icon :icon="['fas','clock']"></font-awesome-icon>
              </div>
              <div class="text-container">
                <span v-if="$route.path == `/fortime` || $route.path != `/fortimen`">FT</span>
                <span v-if="$route.path == `/fortimen`">FT*n</span>
                <font-awesome-icon :icon="['fas','caret-down']"></font-awesome-icon>
              </div>
            </div>
          </a>
        </template>
        <b-dropdown-item>
          <template>
            <router-link
              to="/fortime"
              active-class="btn-info"
              class="btn btn-secondary text-light  d-inline-block p-0"
            >
              <div class="small-action-link-button">
                <div class="icon-container">
                  <font-awesome-icon :icon="['fas','clock']"></font-awesome-icon>
                </div>
                <div class="text-container">
                  <span>FT</span>
                </div>
              </div>
            </router-link>
          </template>
        </b-dropdown-item>
        <b-dropdown-item>
          <template>
            <router-link
              to="/fortimen"
              active-class="btn-info"
              class="btn btn-secondary text-light  d-inline-block p-0"
              href="#"
            >
              <div class="small-action-link-button">
                <div class="icon-container">
                  <font-awesome-icon :icon="['fas','clock']"></font-awesome-icon>
                </div>
                <div class="text-container">FT*n</div>
              </div>
            </router-link>
          </template>
        </b-dropdown-item>
      </b-dropdown>

      <router-link
        active-class="btn-info"
        to="/amrap"
        class="btn btn-secondary text-light d-inline-block p-0"
      >
        <div class="small-action-link-button">
          <div class="icon-container">
            <font-awesome-icon :icon="['fas','list-ol']"></font-awesome-icon>
          </div>
          <div class="text-container">AMRAP</div>
        </div>
      </router-link>

      <b-dropdown
        variant="link"
        size="lg"
        no-caret
        class="workouts-dropdown"
      >
        <template slot="button-content">
          <a
            class="btn btn-secondary text-light  d-inline-block p-0 "
            href="#"
            v-bind:class="{'btn-info':($route.path == `/emom` ||  $route.path == `/e2mom`)}"
          >
            <div class="small-action-link-button">
              <div class="icon-container">
                <font-awesome-icon :icon="['fas','retweet']"></font-awesome-icon>
              </div>
              <div class="text-container">
                <span v-if="$route.path == `/emom` || $route.path != `/e2mom`">EMOM</span>
                <span v-if="$route.path == `/e2mom`">E2MOM</span>
                <font-awesome-icon :icon="['fas','caret-down']"></font-awesome-icon>
              </div>
            </div>
          </a>
        </template>
        <b-dropdown-item>
          <template>
            <router-link
              to="/emom"
              active-class="btn-info"
              class="btn btn-secondary text-light  d-inline-block p-0"
            >
              <div class="small-action-link-button">
                <div class="icon-container">
                  <font-awesome-icon :icon="['fas','retweet']"></font-awesome-icon>
                </div>
                <div class="text-container">
                  <span>EMOM</span>
                </div>
              </div>
            </router-link>
          </template>
        </b-dropdown-item>
        <b-dropdown-item>
          <template>
            <router-link
              to="/e2mom"
              active-class="btn-info"
              class="btn btn-secondary text-light  d-inline-block p-0"
            >
              <div class="small-action-link-button">
                <div class="icon-container">
                  <font-awesome-icon :icon="['fas','retweet']"></font-awesome-icon>
                </div>
                <div class="text-container">E2MOM</div>
              </div>
            </router-link>
          </template>
        </b-dropdown-item>
      </b-dropdown>
      <router-link
        to="/nft"
        active-class="btn-info"
        class="btn btn-secondary text-light  d-inline-block p-0"
      >
        <div class="small-action-link-button">
          <div class="icon-container">
            <font-awesome-icon :icon="['far','pause-circle']"></font-awesome-icon>
          </div>
          <div class="text-container">NFT</div>
        </div>
      </router-link>
    </div>
    <div v-if="canUserPlanWorkouts">
      <a
        class="btn btn-warning text-light  d-inline-block p-0 "
        href="#"
        v-on:click="planWorkoutClick"
      >
        <div class="small-action-link-button">
          <div class="icon-container">
            <font-awesome-icon :icon="['fas','calendar']"></font-awesome-icon>
          </div>
          <div class="text-container">
            <span v-if="!canUserSeePlanWorkouts">Plan</span>
            <span v-else>Log</span>
          </div>
        </div>
      </a>
    </div>
  </div>
</template>

<script lang="ts">
/* Font awesome icons */
import { faClock } from "@fortawesome/free-solid-svg-icons/faClock";
import { faCalendar } from "@fortawesome/free-solid-svg-icons/faCalendar";
import { faCaretDown } from "@fortawesome/free-solid-svg-icons/faCaretDown";
import { faListOl } from "@fortawesome/free-solid-svg-icons/faListOl";
import { faRetweet } from "@fortawesome/free-solid-svg-icons/faRetweet";
import { library } from "@fortawesome/fontawesome-svg-core";
import { faPauseCircle } from "@fortawesome/free-regular-svg-icons/faPauseCircle";

library.add(
  faClock,
  faCaretDown,
  faListOl,
  faRetweet,
  faPauseCircle,
  faCalendar
);
/**/

import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { Vue, Component, Prop } from "vue-property-decorator";
import { State, Action, Getter } from "vuex-class";
import bDropdown from "bootstrap-vue/es/components/dropdown/dropdown";
import bDropdownItem from "bootstrap-vue/es/components/dropdown/dropdown-item";
import bDropdownItemButton from "bootstrap-vue/es/components/dropdown/dropdown-item-button";

const namespace: string = "workoutEdit";
import { WorkoutType } from "../../models/viewModels/WorkoutType";
declare var workouter: {
  toLogWorkoutRawModel: any;
  workoutViewModel: any;
  canUserPlanWorkouts: boolean;
};

@Component({
  components: { FontAwesomeIcon, bDropdown, bDropdownItem }
})
export default class WorkoutsNavigationComponent extends Vue {
  private routes = [
    { workoutType: WorkoutType.ForTime, path: "/fortime" },
    { workoutType: WorkoutType.ForTimeManyInners, path: "/fortimen" },
    { workoutType: WorkoutType.AMRAP, path: "/amrap" },
    { workoutType: WorkoutType.EMOM, path: "/emom" },
    { workoutType: WorkoutType.E2MOM, path: "/e2mom" },
    { workoutType: WorkoutType.NotForTime, path: "/nft" },
    { workoutType: WorkoutType.ForTime, path: "/" }
  ];

  @Action("fetchExercises", { namespace })
  fetchExercises: any;

  @Action("setCanUserSeePlanWorkouts", { namespace })
  setCanUserSeePlanWorkouts: any;

  isEditMode: boolean = false;
  canUserPlanWorkouts: boolean = false;
  canUserSeePlanWorkouts: boolean = false;
  workoutTypeDisplay: string = "";

  mounted() {
    // fetching data as soon as the component's been mounted
    this.canUserPlanWorkouts = workouter.canUserPlanWorkouts;
    this.setCanUserSeePlanWorkouts(this.canUserSeePlanWorkouts);
    this.fetchExercises();
    this.isEditMode =
      workouter != null &&
      (workouter.toLogWorkoutRawModel != null ||
        workouter.workoutViewModel != null);
    this.redirectIfNeeded();
  }

  redirectIfNeeded() {
    if (
      workouter == null ||
      (workouter.toLogWorkoutRawModel == null &&
        workouter.workoutViewModel == null)
    ) {
      return;
    }
    let workoutViewModel =
      workouter.toLogWorkoutRawModel != null
        ? workouter.toLogWorkoutRawModel.workoutViewModel
        : workouter.workoutViewModel;
    let currentWorkoutType = workoutViewModel.workoutType;
    for (let i = 0; i < this.routes.length; i++) {
      if (this.routes[i].workoutType == currentWorkoutType) {
        let newPath = this.routes[i].path;
        this.$router.replace({ path: newPath });
        this.workoutTypeDisplay = workoutViewModel.workoutTypeDisplay;
        break;
      }
    }
  }

  planWorkoutClick() {
    this.canUserSeePlanWorkouts = !this.canUserSeePlanWorkouts;
    this.setCanUserSeePlanWorkouts(this.canUserSeePlanWorkouts);
  }
}
</script>

<style>
</style>