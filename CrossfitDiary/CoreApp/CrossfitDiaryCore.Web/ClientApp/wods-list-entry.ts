/* app components */
import Vue from "vue";
import WodsComponent from "./components/wods-component.vue";
let vue = new Vue({
  el: "#wods-library-container",
  template: `
    <WodsComponent />
    `,
  components: {
    WodsComponent
  },
  data() {
    return {};
  },

  methods: {}
});
