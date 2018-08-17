<template>
    <div class="home-container">
       <PersonsActivitesItemComponent></PersonsActivitesItemComponent>
    </div>
</template>

<script lang="ts">

    import Vue from 'vue'
    import Component from 'vue-class-component'
    import PersonsActivitesItemComponent from "./person-activities-item-component.vue"
    @Component({
        components:{
            PersonsActivitesItemComponent
        }
    })
    export default class PersonsActivitesComponent extends Vue {
        
    }

</script>

<style>

</style>