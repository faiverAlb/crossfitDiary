//import fontawesome from '@fortawesome/fontawesome-free/js/all';
import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';

import 'bootstrap';
import './style/persons.scss';

import PersonsActivitiesComponent from "./components/person-activities-component.vue";

Vue.use(BootstrapVue);

var iAmVue = new Vue({
  el: '#home-page-container',
  components: {
    PersonsActivitiesComponent
  },
  render: function (createElement) {
    return createElement(PersonsActivitiesComponent);
  }
});