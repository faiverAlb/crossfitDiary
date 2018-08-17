//import fontawesome from '@fortawesome/fontawesome-free/js/all';
import 'bootstrap';
import './style/app.scss';
import Vue from 'vue';
import PersonsActivitiesComponent from "./components/person-activities-component.vue";

var iAmVue = new Vue({
  el: '#home-page-container',
  components: {
    PersonsActivitiesComponent
  },
  render: function (createElement) {
    return createElement(PersonsActivitiesComponent);
  }
});