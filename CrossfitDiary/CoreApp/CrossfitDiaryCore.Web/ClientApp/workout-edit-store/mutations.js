export var mutations = {
    profileLoaded: function (state, payload) {
        state.error = false;
        state.user = payload;
    },
    profileError: function (state) {
        state.error = true;
        state.user = undefined;
    },
    changeUserName: function (state, newName) {
        debugger;
        state.user.firstName = newName;
    }
};
//# sourceMappingURL=mutations.js.map