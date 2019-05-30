/* Font awesome icons */
import { dom } from "@fortawesome/fontawesome-svg-core";
dom.watch(); // This will kick of the initial replacement of i to svg tags and configure a MutationObserver
/* public components */
import Vue from "vue";
/* app components */
import WeightMaximumNavComponent from "./components/navigation-components/weight-maximum-nav-component.vue";
import CrossfitterService from "./CrossfitterService";
/* models and styles */
import "./style/persons.scss";
var apiService = new CrossfitterService();
var vue = new Vue({
    el: "#nav-bar-max-container",
    template: "\n  <li class=\"nav-item mr-1\">\n  <WeightMaximumNavComponent />\n  </li>\n    ",
    components: { WeightMaximumNavComponent: WeightMaximumNavComponent }
});
//# sourceMappingURL=max-navbar-entry.js.map