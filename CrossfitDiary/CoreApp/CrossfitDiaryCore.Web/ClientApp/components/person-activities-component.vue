<template>
    <div>
        <div
                :key="item.id"
                class="activities-list container"
                v-for="item in activities"
        >
            <PersonsActivitesItemComponent
                    :model="item"
                    @deleteWorkout="deleteWorkoutClick"
                    @showLeaderboard="showLeaderBoardModal"
            />
        </div>
        <b-modal ok-only
                 okTitle="Close"
                 okVariant="info"
                 ref="leaderboardModal"
                 title="Leaderboard">
            <div class="workouts-results text-center">
                <b-table :items="leaderboardItems"/>
            </div>
        </b-modal>
        <b-modal @ok="deleteWorkout" okTitle="Delete" okVariant="danger" ref="removeWodModalRef"
                 title="Sure to remove workout?">
            Are you sure you want to remove it?
        </b-modal>
    </div>

</template>

<script lang="ts">
    /* Font awesome icons */
    /* public components */
    import {Component, Prop, Vue} from "vue-property-decorator";
    import {BButton, BFormCheckbox, BModal, TablePlugin} from "bootstrap-vue";
    import Spinner from "vue-spinner-component/src/Spinner.vue";
    /* app components */
    import PersonsActivitesItemComponent from "./person-activities-item-component.vue";
    /* models and styles */
    import {ToLogWorkoutViewModel} from "../models/viewModels/ToLogWorkoutViewModel";

    import CrossfitterService from "../CrossfitterService";
    import {LeaderboardItemViewModel} from "../models/viewModels/LeaderboardItemViewModel";

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
            removeWodModalRef: HTMLFormElement;
            leaderboardModal: HTMLFormElement;
        };

        leaderboardItems: LeaderboardItemViewModel[] = [];

        @Prop() activities: ToLogWorkoutViewModel[];

        _toDeleteCrossfitWorkoutId: number = -1;

        deleteWorkoutClick(crossfitterWorkoutId: number) {
            this.$refs.removeWodModalRef.show(crossfitterWorkoutId);
            this._toDeleteCrossfitWorkoutId = crossfitterWorkoutId;
        }

        mounted() {
            this._apiService = new CrossfitterService();
        }

        deleteWorkout(): void {
            this.$refs.removeWodModalRef.hide();

            this._apiService
                .removeWorkout(this._toDeleteCrossfitWorkoutId)
                .then(data => {
                    let indexOfWorkout = this.activities
                        .map(item => item.id)
                        .indexOf(this._toDeleteCrossfitWorkoutId);
                    this.activities.splice(indexOfWorkout, 1);
                });
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