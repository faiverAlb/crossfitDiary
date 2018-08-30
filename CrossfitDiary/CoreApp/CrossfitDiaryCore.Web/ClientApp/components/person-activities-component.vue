<template>
    <div class="home-container">
        <div class="activities-list container" v-for="item in activities" :key="item.id">
            <div class="row">
                <PersonsActivitesItemComponent :model="item"></PersonsActivitesItemComponent>
            </div>

        </div>
    </div>
</template>

<script lang="ts">

    import { Vue, Component, Prop } from 'vue-property-decorator';
    import PersonsActivitesItemComponent from "./person-activities-item-component.vue";
    // import bAlert from 'bootstrap-vue/es/components/alert/alert';
    import { ToLogWorkoutViewModel } from '../models/viewModels/ToLogWorkoutViewModel';
    
    @Component({
        components: {
            PersonsActivitesItemComponent
        },
    })
    export default class PersonsActivitesComponent extends Vue {
        
        @Prop()
        activities:ToLogWorkoutViewModel[];
    }

</script>

<style>
</style>