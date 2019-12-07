/* app components */
import Vue from "vue";
import WodsComponent from "./components/wods-component.vue";
var vue = new Vue({
    el: "#wods-library-container",
    template: "\n    <WodsComponent />\n    ",
    components: {
        WodsComponent: WodsComponent
    },
    data: function () {
        return {};
    },
    methods: {}
});
//# sourceMappingURL=wods-list-entry.js.map