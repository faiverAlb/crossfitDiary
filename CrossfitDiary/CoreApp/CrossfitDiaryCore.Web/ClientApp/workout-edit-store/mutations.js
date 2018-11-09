export var mutations = {
    exercisesLoaded: function (state, payload) {
        state.error = false;
        state.exercises = payload;
    },
    profileLoaded: function (state, payload) {
        state.error = false;
        state.user = payload;
    }
};
//# sourceMappingURL=mutations.js.map