import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';

import 'bootstrap';
import './style/persons.scss';
import {ToLogWorkoutViewModel} from './models/viewModels/ToLogWorkoutViewModel';

import PersonsActivitiesComponent from "./components/person-activities-component.vue";
import CrossfitterService from "./CrossfitterService"
const apiService = new  CrossfitterService();

Vue.use(BootstrapVue);

let iAmVue = new Vue({
  el: '#home-page-container',
  components: {
    PersonsActivitiesComponent
  },
  render: function (createElement) {
    return createElement(PersonsActivitiesComponent);
  },
  mounted() {
    apiService.getAllCrossfittersWorkouts()
      .then((data) => {
        debugger;
      });
  },
});