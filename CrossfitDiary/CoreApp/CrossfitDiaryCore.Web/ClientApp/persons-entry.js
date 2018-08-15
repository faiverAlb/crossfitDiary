//import fontawesome from '@fortawesome/fontawesome-free/js/all';
import 'bootstrap';
import './style/app.scss';
import Vue from 'vue';
import Persons from "./components/persons/persons-component.vue";
var vueObj = new Vue({
    el: '#home-page-container',
    components: {
        Persons: Persons
    },
    render: function (createElement) {
        return createElement(Persons);
    }
});
//# sourceMappingURL=persons-entry.js.map