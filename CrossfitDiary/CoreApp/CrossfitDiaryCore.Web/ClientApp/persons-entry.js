import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import 'bootstrap';
import './style/persons.scss';
import { ToLogWorkoutViewModel } from './models/viewModels/ToLogWorkoutViewModel';
import PersonsActivitiesComponent from "./components/person-activities-component.vue";
import CrossfitterService from "./CrossfitterService";
var apiService = new CrossfitterService();
Vue.use(BootstrapVue);
//let workouts: ToLogWorkoutViewModel[] = [];
new Vue({
    el: '#home-page-container',
    template: "<PersonsActivitiesComponent :activities=\"activities\"/>",
    components: {
        PersonsActivitiesComponent: PersonsActivitiesComponent
    },
    data: function () {
        return {
            activities: ToLogWorkoutViewModel[0],
        };
    },
    mounted: function () {
        var _this = this;
        apiService.getAllCrossfittersWorkouts()
            .then(function (data) {
            _this.activities = data;
        });
    },
});
//# sourceMappingURL=persons-entry.js.map