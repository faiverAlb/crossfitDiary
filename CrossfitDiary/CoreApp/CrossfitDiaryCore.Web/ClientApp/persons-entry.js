import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import 'bootstrap';
import './style/persons.scss';
import { ToLogWorkoutViewModel } from './models/viewModels/ToLogWorkoutViewModel';
import PersonsActivitiesComponent from "./components/person-activities-component.vue";
import CrossfitterService from "./CrossfitterService";
var apiService = new CrossfitterService();
Vue.use(BootstrapVue);
var workouts = [];
var iAmVue = new Vue({
    el: '#home-page-container',
    template: "<PersonsActivitiesComponent activities=\"workouts\"></PersonsActivitiesComponent>",
    components: {
        PersonsActivitiesComponent: PersonsActivitiesComponent
    },
    data: function () {
        return {
            workouts: ToLogWorkoutViewModel[0]
        };
    },
    // render: function (createElement) {
    //   return createElement(PersonsActivitiesComponent);
    // },
    mounted: function () {
        apiService.getAllCrossfittersWorkouts()
            .then(function (data) {
            debugger;
            workouts = data;
        });
    },
});
//# sourceMappingURL=persons-entry.js.map