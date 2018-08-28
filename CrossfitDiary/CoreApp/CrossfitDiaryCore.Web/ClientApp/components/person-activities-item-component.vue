<template>
    <div class="done-item offset-lg-3 col col-lg-5 my-2 px-3 py-2 rounded">
        <div class="item-header d-flex flex-row justify-content-between  ">
            <div class="username">
                <span class="text-info">
                    {{model.workouterName}}
                </span>
            </div>
            <div class="">
                <span class="">{{model.displayDate}}</span>
            </div>
        </div>
        <div class="item-body pt-1">
            <div>workout-display-template</div>
            <div class="text-right">
                <span class="workout-result">
                    <span><i class="far fa-grin-beam"></i> Cap + </span>
                </span>
            </div>
        </div>
        <div class="item-footer text-right pt-1">
            <div class="action-buttons">
                <a class="repeat-workout-action pointer text-success pl-1">
                    <i class="fas fa-plus "></i><span class="do-it-text">Do it</span>
                </a>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { Vue, Component, Prop } from 'vue-property-decorator'
    import { ToLogWorkoutViewModel } from '../models/viewModels/ToLogWorkoutViewModel';

    @Component({})
    export default class PersonsActivitesItemComponent extends Vue {
        
        @Prop()
        model:ToLogWorkoutViewModel;


    }
</script>

<style>
</style>