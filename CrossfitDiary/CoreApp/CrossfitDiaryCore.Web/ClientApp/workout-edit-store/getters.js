export var getters = {
    exercisesFromState: function (state) {
        return state.exercises;
        // const { user } = state;
        // const firstName = (user && user.firstName) || "";
        // const lastName = (user && user.lastName) || "";
        // return `${firstName} ${lastName}`;
    },
    fullName: function (state) {
        var user = state.user;
        var firstName = (user && user.firstName) || "";
        var lastName = (user && user.lastName) || "";
        return firstName + " " + lastName;
    }
};
//# sourceMappingURL=getters.js.map