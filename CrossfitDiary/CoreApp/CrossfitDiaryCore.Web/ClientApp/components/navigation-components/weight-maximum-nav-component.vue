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
            :fields="['exerciseTitle','maximumWeight','maximumAlternativeWeight']"
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
import bModal from "bootstrap-vue/es/components/modal/modal";
import Spinner from "vue-spinner-component/src/Spinner.vue";
import bButton from "bootstrap-vue/es/components/button/button";
import bTable from "bootstrap-vue/es/components/table/table";
import CrossfitterService from "../../CrossfitterService";
import { PersonMaximumViewModel } from "../../models/viewModels/PersonMaximumViewModel";
@Component({
  components: {
    bModal,
    bButton,
    bTable
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