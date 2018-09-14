<template>
    <div v-if="errorAlertModel.isAlertVisible">
        <b-alert show variant="danger">{{errorAlertModel.errorMessage}}</b-alert>
        <img src="images/error-wow.jpg" class="img-fluid mx-auto d-block" alt="Error message here ;)" />
    </div>

</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import bAlert from "bootstrap-vue/es/components/alert/alert";
import { ErrorAlertModel } from "../models/viewModels/ErrorAlertModel";

@Component({
  components: { bAlert }
})
export default class ErrorAlertComponent extends Vue {
  @Prop() errorAlertModel: ErrorAlertModel;
}
</script>

<style>
</style>