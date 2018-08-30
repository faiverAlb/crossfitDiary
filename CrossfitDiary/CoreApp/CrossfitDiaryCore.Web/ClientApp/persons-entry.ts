import Vue from 'vue';
import './style/persons.scss';
import {ToLogWorkoutViewModel} from './models/viewModels/ToLogWorkoutViewModel';

import PersonsActivitiesComponent from "./components/person-activities-component.vue";
import CrossfitterService from "./CrossfitterService"
const apiService = new  CrossfitterService();


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