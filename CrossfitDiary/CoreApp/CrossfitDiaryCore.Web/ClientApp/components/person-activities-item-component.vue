<template>
    <div class="done-item offset-lg-3 col col-lg-5 my-2 px-3 py-2 rounded">
        <div class="item-header d-flex flex-row justify-content-between  ">
            <div class="username">
                <a class="text-info">
                    Super user name
                </a>
            </div>
            <div class="">
                <span class="">Today date</span>
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
    import Vue from "vue";
    import Component from "vue-class-component";

    @Component({})
    export default class PersonsActivitesItemComponent extends Vue {
        msg = 123;
        test = "best";
    }
</script>

<style>
</style>