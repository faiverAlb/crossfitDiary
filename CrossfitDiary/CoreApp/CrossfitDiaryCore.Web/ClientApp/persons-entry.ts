//import fontawesome from '@fortawesome/fontawesome-free/js/all';
import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';

import 'bootstrap';
//import 'bootstrap-vue/dist/bootstrap-vue.css'
import './style/persons.scss';

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
    debugger;
    apiService.getAllCrossfittersWorkouts('test').then(data =>{
      debugger;
    });
  },
});