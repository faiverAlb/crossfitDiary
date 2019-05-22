<template>
  <div>
    <a
      class="nav-link btn btn-info text-light d-inline-block d-md-none p-0"
      data-toggle="collapse"
      href="#collapseExample"
      role="button"
      aria-expanded="false"
      aria-controls="collapseExample"
    >
      <div class="small-action-link-button">
        <div class="icon-container">
          <svg class="svg-inline w-18">
            <use xlink:href="#trophy-icon"></use>
          </svg>
        </div>
        <div class="text-container">MAX</div>
      </div>
      <span class="d-none d-md-inline">Vue Weight maximums</span>
    </a>
    <a
      class="nav-link btn btn-info text-light d-none d-md-inline-block"
      data-toggle="collapse"
      href="#collapseExample"
      role="button"
      aria-expanded="false"
      aria-controls="collapseExample"
    >
      <svg class="svg-inline w-18">
        <use xlink:href="#trophy-icon"></use>
      </svg>
      <span class="d-none d-md-inline">Vue Weight maximums</span>
    </a>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
@Component({
  components: {}
})
export default class WeightMaximumNavComponent extends Vue {}
</script>

<style>
</style>