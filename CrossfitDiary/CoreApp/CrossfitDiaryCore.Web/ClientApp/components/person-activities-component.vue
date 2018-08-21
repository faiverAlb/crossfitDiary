<template>
    <div class="home-container">
        <div class="activities-list container">
            <div class="row">
                <PersonsActivitesItemComponent></PersonsActivitesItemComponent>
            </div>
        </div>
    </div>
</template>

<script lang="ts">

    import Vue from 'vue'
    import Component from 'vue-class-component'
    import PersonsActivitesItemComponent from "./person-activities-item-component.vue"
    @Component({
        components: {
            PersonsActivitesItemComponent
        }
    })
    export default class PersonsActivitesComponent extends Vue {
    }

</script>

<style>
</style>