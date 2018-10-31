<template>
  <div class="container">
    <div v-if="profile.user">
      <p>
        Full name: {{ fullName }}
      </p>
      <p>
        Email: {{ email }}
      </p>
    </div>
    <div v-if="profile.error">
      Oops an error occured
    </div>

    <button v-on:click="mutateData">Mutate data</button>
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
import { State, Action, Getter } from "vuex-class";
import { WorkoutViewModel } from "../../../models/viewModels/WorkoutViewModel";
import ExercisesListComponent from "./exercises-list-component.vue";
import { ProfileState, User } from "./../../../profile/types";
const namespace: string = "profile";

@Component({ components: { FontAwesomeIcon, ExercisesListComponent } })
export default class ForTimeEditComponent extends Vue {
  model: WorkoutViewModel = new WorkoutViewModel();

  @State("profile")
  profile: ProfileState;

  @Action("fetchData", { namespace })
  fetchData: any;

  @Getter("fullName", { namespace })
  fullName: string;

  mounted() {
    // fetching data as soon as the component's been mounted
    debugger;
    this.fetchData();
  }

  // computed variable based on user's email
  get email() {
    const user = this.profile && this.profile.user;
    return (user && user.email) || "";
  }

  mutateData(): void {
    debugger;
  }
}
</script>

<style>
</style>