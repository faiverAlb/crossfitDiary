import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';

import 'bootstrap';
import './style/persons.scss';
import {ToLogWorkoutViewModel} from './models/viewModels/ToLogWorkoutViewModel';

import PersonsActivitiesComponent from "./components/person-activities-component.vue";
import CrossfitterService from "./CrossfitterService"
const apiService = new  CrossfitterService();

Vue.use(BootstrapVue);

//let workouts: ToLogWorkoutViewModel[] = [];

new Vue({
  el: '#home-page-container',
  template:"<PersonsActivitiesComponent :activities=\"activities\"/>",
  components: {
    PersonsActivitiesComponent
  },
  data(){
    return{
      activities:ToLogWorkoutViewModel[0],
    }
  },
  mounted() {
    apiService.getAllCrossfittersWorkouts()
      .then((data) => {
        this.activities = data;
      });
  },
});