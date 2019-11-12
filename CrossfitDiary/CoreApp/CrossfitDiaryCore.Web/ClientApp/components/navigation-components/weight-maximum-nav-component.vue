<template>
  <div>
    <div>
      <b-modal
        ref="weightMaximumsModal"
        title="Weight maximums"
      >
        <div class="weight-maximums">
          <b-table
            :items="personMaximums"
            :fields="['exerciseTitle','maximumWeight']"
            :small="true"
          ></b-table>
        </div>
        <div slot="modal-footer">
          <b-button
            data-dismiss="modal"
            @click="()=>{this.$refs.weightMaximumsModal.hide();}"
          >Close</b-button>
        </div>
      </b-modal>
    </div>
    <a
      class="nav-link btn btn-info text-light d-inline-block d-md-none p-0"
      data-toggle="collapse"
      href="#collapseExample"
      role="button"
      aria-expanded="false"
      aria-controls="collapseExample"
      @click="showMaximums"
    >
      <div class="small-action-link-button">
        <div class="icon-container">
          <svg class="svg-inline w-18">
            <use xlink:href="#trophy-icon"></use>
          </svg>
        </div>
        <div class="text-container">MAX</div>
      </div>
      <span class="d-none d-md-inline">Weight maximums</span>
    </a>
    <a
      class="nav-link btn btn-info text-light d-none d-md-inline-block"
      data-toggle="collapse"
      href="#collapseExample"
      role="button"
      aria-expanded="false"
      aria-controls="collapseExample"
      @click="showMaximums"
    >
      <svg class="svg-inline w-18">
        <use xlink:href="#trophy-icon"></use>
      </svg>
      <span class="d-none d-md-inline">Weight maximums</span>
    </a>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import {BModal} from "bootstrap-vue";
import Spinner from "vue-spinner-component/src/Spinner.vue";
import {BButton} from "bootstrap-vue";
import {TablePlugin}  from "bootstrap-vue";
import CrossfitterService from "../../CrossfitterService";
import { PersonMaximumViewModel } from "../../models/viewModels/PersonMaximumViewModel";
Vue.use(TablePlugin);

@Component({
  components: {
    BModal,
    BButton,
    // TablePlugin
  }
})
export default class WeightMaximumNavComponent extends Vue {
  _apiService: CrossfitterService = new CrossfitterService();
  $refs: {
    weightMaximumsModal: HTMLFormElement;
  };

  personMaximums: PersonMaximumViewModel[] = [];

  mounted() {
    this._apiService = new CrossfitterService();
    this._apiService.getWeightsMaximums().then(data => {
      this.personMaximums = data;
    });
  }
  showMaximums(): void {
    this.$refs.weightMaximumsModal.show();
  }
}
</script>

<style>
</style>