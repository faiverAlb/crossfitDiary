<template>
    <div class="d-flex justify-content-between">
        <div v-if="isEditMode">{{ workoutTypeDisplay }}</div>
        <div class="d-inline-block" v-else>
            <router-link
                    active-class="btn-info"
                    class="btn btn-secondary text-light  d-inline-block p-0"
                    to="/nft"
            >
                <div class="small-action-link-button">
                    <div class="icon-container">
                        <font-awesome-icon :icon="['far', 'pause-circle']"/>
                    </div>
                    <div class="text-container">NFT</div>
                </div>
            </router-link>
            <b-dropdown class="workouts-dropdown" no-caret size="lg" variant="link">
                <template slot="button-content">
                    <a
                            class="btn btn-secondary text-light  d-inline-block p-0 "
                            href="#"
                            v-bind:class="{
              'btn-info':
                $route.path == `/fortime` || $route.path == `/fortimen`
            }"
                    >
                        <div class="small-action-link-button">
                            <div class="icon-container">
                                <font-awesome-icon :icon="['fas', 'clock']"/>
                            </div>
                            <div class="text-container">
                <span
                        v-if="$route.path == `/fortime` || $route.path != `/fortimen`"
                >FT</span
                >
                                <span v-if="$route.path == `/fortimen`">FT*n</span>
                                <font-awesome-icon
                                        :icon="['fas', 'caret-down']"
                                />
                            </div>
                        </div>
                    </a>
                </template>
                <b-dropdown-item>
                    <template>
                        <router-link
                                active-class="btn-info"
                                class="btn btn-secondary text-light  d-inline-block p-0"
                                to="/fortime"
                        >
                            <div class="small-action-link-button">
                                <div class="icon-container">
                                    <font-awesome-icon
                                            :icon="['fas', 'clock']"
                                    />
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
                                active-class="btn-info"
                                class="btn btn-secondary text-light  d-inline-block p-0"
                                href="#"
                                to="/fortimen"
                        >
                            <div class="small-action-link-button">
                                <div class="icon-container">
                                    <font-awesome-icon
                                            :icon="['fas', 'clock']"
                                    />
                                </div>
                                <div class="text-container">FT*n</div>
                            </div>
                        </router-link>
                    </template>
                </b-dropdown-item>
            </b-dropdown>
            <b-dropdown class="workouts-dropdown" no-caret size="lg" variant="link">
                <template slot="button-content">
                    <a
                            class="btn btn-secondary text-light  d-inline-block p-0 "
                            href="#"
                            v-bind:class="{
              'btn-info': $route.path == `/amrap` || $route.path == `/amrapn`
            }"
                    >
                        <div class="small-action-link-button amrap-icon">
                            <div class="icon-container">
                                <font-awesome-icon :icon="['fas', 'clock']"/>
                            </div>
                            <div class="text-container">
                <span v-if="$route.path == `/amrap` || $route.path != `/amrapn`"
                >AMRAP</span
                >
                                <span v-if="$route.path == `/amrapn`">AMRAP*n</span>
                                <font-awesome-icon
                                        :icon="['fas', 'caret-down']"
                                />
                            </div>
                        </div>
                    </a>
                </template>
                <b-dropdown-item>
                    <template>
                        <router-link
                                active-class="btn-info"
                                class="btn btn-secondary text-light d-inline-block p-0"
                                to="/amrap"
                        >
                            <div class="small-action-link-button amrap-icon">
                                <div class="icon-container">
                                    <font-awesome-icon
                                            :icon="['fas', 'list-ol']"
                                    />
                                </div>
                                <div class="text-container">AMRAP</div>
                            </div>
                        </router-link>
                    </template>
                </b-dropdown-item>
                <b-dropdown-item>
                    <template>
                        <router-link
                                active-class="btn-info"
                                class="btn btn-secondary text-light  d-inline-block p-0"
                                href="#"
                                to="/amrapn"
                        >
                            <div class="small-action-link-button">
                                <div class="icon-container">
                                    <font-awesome-icon
                                            :icon="['fas', 'list-ol']"
                                    />
                                </div>
                                <div class="text-container">AMRAP*n</div>
                            </div>
                        </router-link>
                    </template>
                </b-dropdown-item>
            </b-dropdown>

            <b-dropdown class="workouts-dropdown" no-caret size="lg" variant="link">
                <template slot="button-content">
                    <a
                            class="btn btn-secondary text-light  d-inline-block p-0 "
                            href="#"
                            v-bind:class="{
              'btn-info': $route.path == `/emom` || $route.path == `/e2mom`
            }"
                    >
                        <div class="small-action-link-button">
                            <div class="icon-container">
                                <font-awesome-icon
                                        :icon="['fas', 'retweet']"
                                />
                            </div>
                            <div class="text-container">
                <span v-if="$route.path == `/emom` || $route.path != `/e2mom`"
                >EMOM</span
                >
                                <span v-if="$route.path == `/e2mom`">E2MOM</span>
                                <font-awesome-icon
                                        :icon="['fas', 'caret-down']"
                                />
                            </div>
                        </div>
                    </a>
                </template>
                <b-dropdown-item>
                    <template>
                        <router-link
                                active-class="btn-info"
                                class="btn btn-secondary text-light  d-inline-block p-0"
                                to="/emom"
                        >
                            <div class="small-action-link-button">
                                <div class="icon-container">
                                    <font-awesome-icon
                                            :icon="['fas', 'retweet']"
                                    />
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
                                active-class="btn-info"
                                class="btn btn-secondary text-light  d-inline-block p-0"
                                to="/e2mom"
                        >
                            <div class="small-action-link-button">
                                <div class="icon-container">
                                    <font-awesome-icon :icon="['fas', 'retweet']"/>
                                </div>
                                <div class="text-container">E2MOM</div>
                            </div>
                        </router-link>
                    </template>
                </b-dropdown-item>
            </b-dropdown>

        </div>
        <div>
            <a
                    class="btn btn-warning text-light  d-inline-block p-0 "
                    href="#"
                    v-on:click="planWorkoutClick"
            >
                <div class="small-action-link-button">
                    <div class="icon-container">
                        <font-awesome-icon :icon="['fas', 'calendar']"/>
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
    import {faClock} from "@fortawesome/free-solid-svg-icons/faClock";
    import {faCalendar} from "@fortawesome/free-solid-svg-icons/faCalendar";
    import {faCaretDown} from "@fortawesome/free-solid-svg-icons/faCaretDown";
    import {faListOl} from "@fortawesome/free-solid-svg-icons/faListOl";
    import {faRetweet} from "@fortawesome/free-solid-svg-icons/faRetweet";
    import {library} from "@fortawesome/fontawesome-svg-core";
    import {faPauseCircle} from "@fortawesome/free-regular-svg-icons/faPauseCircle";
    import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";
    import {Component, Vue} from "vue-property-decorator";
    import {Action} from "vuex-class";
    import {BDropdown, BDropdownItem} from "bootstrap-vue";
    import {WorkoutType} from "../../models/viewModels/WorkoutType";

    library.add(
        faClock,
        faCaretDown,
        faListOl,
        faRetweet,
        faPauseCircle,
        faCalendar
    );
    /**/

    const namespace: string = "workoutEdit";
    declare var workouter: {
        toLogWorkoutRawModel: any;
        workoutViewModel: any;
    };

    @Component({
        components: {FontAwesomeIcon, BDropdown, BDropdownItem}
    })
    export default class WorkoutsNavigationComponent extends Vue {
        @Action("fetchExercises", {namespace})
        fetchExercises: any;
        @Action("fetchUserMaximums", {namespace})
        fetchUserMaximums: any;
        @Action("setCanUserSeePlanWorkouts", {namespace})
        setCanUserSeePlanWorkouts: any;
        isEditMode: boolean = false;
        canUserSeePlanWorkouts: boolean = false;
        workoutTypeDisplay: string = "";
        private routes = [
            {workoutType: WorkoutType.ForTime, path: "/fortime"},
            {workoutType: WorkoutType.ForTimeManyInners, path: "/fortimen"},
            {workoutType: WorkoutType.AMRAP, path: "/amrap"},
            {workoutType: WorkoutType.AMRAPN, path: "/amrapn"},
            {workoutType: WorkoutType.EMOM, path: "/emom"},
            {workoutType: WorkoutType.E2MOM, path: "/e2mom"},
            {workoutType: WorkoutType.NotForTime, path: "/nft"},
            {workoutType: WorkoutType.ForTime, path: "/"}
        ];

        mounted() {
            // fetching data as soon as the component's been mounted
            this.setCanUserSeePlanWorkouts(this.canUserSeePlanWorkouts);
            this.fetchExercises();
            this.fetchUserMaximums();
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
                    this.$router.replace({path: newPath});
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
