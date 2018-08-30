import Vue from 'vue';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { faCoffee } from '@fortawesome/free-solid-svg-icons/faCoffee';

library.add(faCoffee);

Vue.component('font-awesome-icon', FontAwesomeIcon);

Vue.config.productionTip = false;


import BootstrapVue from 'bootstrap-vue';

import './style/persons.scss';
 import {ToLogWorkoutViewModel} from './models/viewModels/ToLogWorkoutViewModel';

import PersonsActivitiesComponent from "./components/person-activities-component.vue";
import CrossfitterService from "./CrossfitterService"
const apiService = new  CrossfitterService();

Vue.use(BootstrapVue);


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