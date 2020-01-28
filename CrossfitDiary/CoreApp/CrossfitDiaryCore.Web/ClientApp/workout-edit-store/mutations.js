export var mutations = {
    exercisesLoaded: function (state, payload) {
        state.error = false;
        state.exercises = payload;
    },
    userMaximumsLoaded: function (state, payload) {
        state.error = false;
        state.userMaximums = payload;
    },
    canUserSeePlanWorkoutsConfigured: function (state, payload) {
        state.canUserSeePlanWorkouts = payload;
    },
    isFindMaxWeightConfigured: function (state, payload) {
        state.isFindMaxWeight = payload;
    }
};
//# sourceMappingURL=mutations.js.map