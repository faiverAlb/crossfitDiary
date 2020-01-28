export var getters = {
    exercisesFromState: function (state) {
        return state.exercises;
        // const { user } = state;
        // const firstName = (user && user.firstName) || "";
        // const lastName = (user && user.lastName) || "";
        // return `${firstName} ${lastName}`;
    },
    isFindMaxWeightGetter: function (state) {
        return state.isFindMaxWeight;
    },
};
//# sourceMappingURL=getters.js.map