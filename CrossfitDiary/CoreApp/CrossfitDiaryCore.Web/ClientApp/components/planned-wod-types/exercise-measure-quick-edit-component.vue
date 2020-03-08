<template>
    <div class="form-group my-1 col">
        <b-input-group class="mx-1 pr-1" size="sm">
            <b-form-input
                    aria-describedby="prPercentHelpBlock"
                    class="measure-value-input"
                    inputmode="numeric"
                    pattern="[0-9]*"
                    placeholder="Count"
                    type="tel"
                    v-if="measure.measureType == 2"
                    v-model="measure.measureValue"
            />
            <b-form-input
                    :placeholder="measure.description"
                    aria-describedby="prPercentHelpBlock"
                    class="measure-value-input"
                    type="number"
                    v-else
                    v-model="measure.measureValue"
            />
            <b-input-group-append>
                <b-input-group-text class="bg-secondary p-0" tag="span"
                                    v-if="measure.measureType == 1">
                                        <span class="badge badge-secondary">
                                                    {{measure.shortMeasureDescription}}
                                        <font-awesome-icon :icon="['fas','walking']"
                                                           size="lg"/>
                                        </span>
                </b-input-group-text>
                <b-input-group-text class="bg-info p-0" tag="span"
                                    v-else-if="measure.measureType == 2">
                                        <span class="badge badge-info">
                                                    {{measure.shortMeasureDescription}}
                                        <span>#</span>
                                        </span>
                </b-input-group-text>
                <b-input-group-text class="bg-primary p-0" tag="span"
                                    v-else-if="measure.measureType == 3">
                                        <span class="badge badge-primary">
                                                    {{measure.shortMeasureDescription}}
                                            <font-awesome-icon :icon="['fas','male']" size="lg"/>
                                        </span>
                </b-input-group-text>
                <b-input-group-text class="bg-warning p-0" tag="span"
                                    v-else-if="measure.measureType == 4">
                                        <span class="badge badge-warning">
                                        
                                            cal's
                                        <font-awesome-icon :icon="['fas','burn']"/>
                                        </span>
                </b-input-group-text>
                <b-input-group-text class="bg-pink p-0" tag="span"
                                    v-else-if="measure.measureType == 8">
                                        <span class="badge badge-pink">
                                                    {{measure.shortMeasureDescription}}
                                        <font-awesome-icon :icon="['fas','female']" size="lg"
                                        />
                                        </span>
                </b-input-group-text>
                <b-input-group-text class="bg-green p-0" tag="span"
                                    v-else-if="measure.measureType == 11">
                                        <span class="badge badge-green">
                                                    {{measure.shortMeasureDescription}}
                                        <font-awesome-icon :icon="['fas','clock']" size="lg"
                                        />
                                        </span>
                </b-input-group-text>
                <b-input-group-text tag="span" v-else>
                    {{measure.shortMeasureDescription}}
                </b-input-group-text>
            </b-input-group-append>
        </b-input-group>
    </div>


</template>

<script lang="ts">
    /* Font awesome icons */

    /* public components */
    import {Component, Prop, Vue} from "vue-property-decorator";
    import {
        BButton,
        BDropdown,
        BDropdownDivider,
        BDropdownForm,
        BDropdownItem,
        BDropdownItemButton,
        BFormGroup,
        BFormInput,
        InputGroupPlugin,
    } from "bootstrap-vue";
    /* models and styles */
    import {Getter} from "vuex-class";
    import {FontAwesomeIcon} from "@fortawesome/vue-fontawesome";

    import {faMale} from "@fortawesome/free-solid-svg-icons/faMale";
    import {faFemale} from "@fortawesome/free-solid-svg-icons/faFemale";
    
    import {ExerciseMeasureViewModel} from "../../models/viewModels/ExerciseMeasureViewModel";
    import {library} from "@fortawesome/fontawesome-svg-core";
    /* app components */
    const namespace: string = "workoutEdit";
    Vue.use(InputGroupPlugin);

    library.add( faMale, faFemale);
    
    @Component({
        components: {
            BDropdown,
            BDropdownItem,
            BDropdownItemButton,
            BDropdownDivider,
            BDropdownForm,
            BFormInput,
            BButton,
            BFormGroup,
            FontAwesomeIcon
        }

    })
    export default class ExerciseMeasureQuickEditComponent extends Vue {
        @Prop()
        measure: ExerciseMeasureViewModel;

        // @Getter('isFindMaxWeightGetter', {namespace})
        isFindMaxWeight: boolean = false;

        // updateMeasure() {
        //     debugger;
        //     this.$emit('onMeasureChange', this.measure);
        // }

    }
</script>

<style/>