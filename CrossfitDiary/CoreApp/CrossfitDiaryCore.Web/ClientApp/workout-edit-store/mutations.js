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
    }
};
//# sourceMappingURL=mutations.js.map