//import fontawesome from '@fortawesome/fontawesome-free/js/all';
import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import 'bootstrap';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import './style/persons.scss';
import PersonsActivitiesComponent from "./components/person-activities-component.vue";
Vue.use(BootstrapVue);
var iAmVue = new Vue({
    el: '#home-page-container',
    components: {
        PersonsActivitiesComponent: PersonsActivitiesComponent
    },
    render: function (createElement) {
        return createElement(PersonsActivitiesComponent);
    }
});
//# sourceMappingURL=persons-entry.js.map