<template>
    <h1>I'm persons template, bitttchesss!{{msg}}!</h1>
</template>

<script lang="ts">

    import Vue from 'vue'
    import Component from 'vue-class-component'

    // The @Component decorator indicates the class is a Vue component
    @Component({
    })
    export default class PersonsComponent extends Vue {
        msg = 123;
    }

</script>

<style></style>