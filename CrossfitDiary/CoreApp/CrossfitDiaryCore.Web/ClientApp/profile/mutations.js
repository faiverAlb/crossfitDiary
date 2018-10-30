export var mutations = {
    profileLoaded: function (state, payload) {
        state.error = false;
        state.user = payload;
    },
    profileError: function (state) {
        state.error = true;
        state.user = undefined;
    }
};
//# sourceMappingURL=mutations.js.map