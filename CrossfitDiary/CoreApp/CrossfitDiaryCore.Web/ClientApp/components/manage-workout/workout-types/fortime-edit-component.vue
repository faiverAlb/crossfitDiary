<template>
  <div class="routine-complex-info">
    <div class="pt-2 general-info-container">
      <div class="row" data-bind="if: _canSeeTimeCap">
        <div class="col-lg-3 time-cap-container pb-2">
          <label for="timeCapInput" class="sr-only">Time cap:</label>
          <div class="input-group">
            <div class="input-group-prepend">
              <div class="input-group-text">
                <font-awesome-icon :icon="['fas','clock']" class="fa-lg time-cap-icon"></font-awesome-icon>
              </div>
            </div>
            <input type="text" pattern="[0-9]*" inputmode="numeric" class="form-control " id="timeCapInput" placeholder="Time Cap" data-bind="validationElement: _timeCap, inputmask:{value: _timeCap, mask:'99:59',definitions: { '5': { validator: '[0-5]' } }}">
          </div>
        </div>
      </div>
      <div class="row" data-bind="if: _canSeeTimeToWork">
        <div class="col-lg-3 pb-2">
          <label for="timeInput" class="sr-only">Time to work:</label>
          <div class="input-group">
            <div class="input-group-prepend">
              <div class="input-group-text"><i class="far fa-clock"></i></div>
            </div>
            <input type="text" pattern="[0-9]*" inputmode="numeric" class="form-control" id="timeInput" placeholder="Time to work" data-bind="validationElement: _timeToWork, inputmask:{value: _timeToWork, mask:'99:59',definitions: { '5': { validator: '[0-5]' } }}">
          </div>
        </div>

      </div>
    </div>
  </div>
</template>

<script lang="ts">
/* Font awesome icons */
import { faClock } from "@fortawesome/free-solid-svg-icons/faClock";
import { library } from "@fortawesome/fontawesome-svg-core";

library.add(faClock);
/**/
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { Vue, Component, Prop } from "vue-property-decorator";

@Component({ components: { FontAwesomeIcon } })
export default class ForTimeEditComponent extends Vue {}
</script>

<style>
</style>